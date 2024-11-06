using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureDocumentIntelligenceDemo.Pages;

public class IndexModel : PageModel
{

    public class AnalysisResult
    {
        public string MerchantName { get; set; } = string.Empty;
        public string TransactionDate { get; set; } = string.Empty;
        public List<(string desc, string total)> Items { get; set; } = [];
        public string Total { get; set; } = string.Empty;
    }
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public IFormFile Document { get; set; }
    public AnalysisResult Result { get; set; } = new AnalysisResult();



    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        string endpoint = "https://document-intel-demo-trev.cognitiveservices.azure.com/";
        string apiKey = "1QbWjZJb65syF8ZJGmiApD8Z0ShmlRfkMpQSiiA7apEZOxElGYZjJQQJ99AKACHYHv6XJ3w3AAALACOGnIOb";

        var credential = new AzureKeyCredential(apiKey);
        var client = new DocumentAnalysisClient(new Uri(endpoint), credential);

        using MemoryStream ms = new();
        await Document.CopyToAsync(ms);
        ms.Position = 0;

        AnalyzeDocumentOperation operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-receipt", ms);

        AnalyzeResult receipts = operation.Value;

        foreach(AnalyzedDocument receipt in receipts.Documents)
        {
            if(receipt.Fields.TryGetValue("MerchantName", out DocumentField merchantName))
            {
                if(merchantName.FieldType == DocumentFieldType.String)
                {
                    string merchant = merchantName.Value.AsString();
                    Result.MerchantName = $"Merchant Name: '{merchant}', with confidence {merchantName.Confidence}";
                }
            }

            if(receipt.Fields.TryGetValue("TransactionDate", out DocumentField transactionDate))
            {
                if(transactionDate.FieldType == DocumentFieldType.Date)
                {
                    DateTimeOffset date = transactionDate.Value.AsDate();
                    Result.TransactionDate = $"Transaction Date: '{transactionDate}', with confidence {transactionDate.Confidence}";
                }
            }

            if(receipt.Fields.TryGetValue("Total", out DocumentField total))
            {
                if(total.FieldType == DocumentFieldType.Double)
                {
                    double amount = total.Value.AsDouble();
                    Result.Total = $"Total: '{amount}', with confidence '{total.Confidence}'";
                }
            }

            if (receipt.Fields.TryGetValue("Items", out DocumentField itemsField))
            {
                if (itemsField.FieldType == DocumentFieldType.List)
                {
                    foreach (DocumentField itemField in itemsField.Value.AsList())
                    {
                        string Description = string.Empty;
                        string TotalPrice = string.Empty;
                        
                        if (itemField.FieldType == DocumentFieldType.Dictionary)
                        {
                            IReadOnlyDictionary<string, DocumentField> itemFields = itemField.Value.AsDictionary();

                            if (itemFields.TryGetValue("Description", out DocumentField itemDescriptionField))
                            {
                                if (itemDescriptionField.FieldType == DocumentFieldType.String)
                                {
                                    string itemDescription = itemDescriptionField.Value.AsString();

                                    Description = $"  Description: '{itemDescription}', with confidence {itemDescriptionField.Confidence}";
                                }
                            }

                            if (itemFields.TryGetValue("TotalPrice", out DocumentField itemTotalPriceField))
                            {
                                if (itemTotalPriceField.FieldType == DocumentFieldType.Double)
                                {
                                    double itemTotalPrice = itemTotalPriceField.Value.AsDouble();

                                    TotalPrice = $"  Total Price: '{itemTotalPrice}', with confidence {itemTotalPriceField.Confidence}";
                                }
                            }
                        }
                        Result.Items.Add((Description, TotalPrice));
                    }
                }
            }
        }

        return Page();
    }
}

