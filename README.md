# Azure AI Document Intelligence Web Application

This **Document Processing Web Application** uses **Azure AI Document Intelligence** (formerly known as Azure Form Recognizer) to analyze uploaded documents, such as receipts, invoices, or other structured forms. The application extracts key information like date, total amount, vendor details, and itemized entries, providing a powerful solution for automating document processing workflows.

## Features

- **Document Upload**: Allows users to upload various types of documents (PDF, JPEG, PNG).
- **Automated Data Extraction with Azure AI Document Intelligence**: Utilizes Azure’s document analysis capabilities to extract key information from uploaded documents.
- **Real-Time Results Display**: Displays the extracted data in a structured format on the webpage, providing instant feedback and insights.

## Getting Started

### Prerequisites

- **Azure Subscription** with Azure AI Document Intelligence enabled - For setup instructions, refer to the [Azure Document Intelligence Documentation](https://learn.microsoft.com/en-us/azure/ai-services/document-intelligence/).
- **Azure Document Intelligence API Key and Endpoint** - Retrieve these from your Azure portal after setting up the service.
- **.NET SDK** (6.0 or higher) - if the backend is implemented in .NET.

### Setup

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-username/AzureAIDocumentIntelligenceApp.git
   cd AzureAIDocumentIntelligenceApp
   ```

2. **Install Dependencies**
   - Install the necessary packages for the backend and frontend by navigating to each directory and running:
     ```bash
     dotnet restore   # For .NET backend
     ```

3. **Access the Web Application**
   - `dotnet run` 

## Using the Application

1. Go to the application’s main page.
2. Upload a document (e.g., a receipt or invoice) by clicking the “Upload” button and selecting a file.
3. The application sends the document to Azure Document Intelligence for analysis.
4. Once processed, the extracted data (such as vendor name, transaction date, and total amount) is displayed on the page in an organized format.

## Example Workflow

1. **User Uploads Document**: The user uploads a receipt for analysis.
2. **Azure AI Document Intelligence Processes Document**: The backend sends the document to Azure, which analyzes it and extracts details like vendor, date, total, and line items.
3. **Data Displayed**: The extracted information is shown on the webpage, providing quick access to key details.

## Customization

- **Extend Document Types**: Modify the application to support other document types, such as invoices, purchase orders, or contracts, by configuring custom document models in Azure Document Intelligence.
- **Enhance Styling**: Customize the web interface’s CSS for a more personalized and branded experience.

## Contributing

Contributions are welcome! Please submit issues or pull requests to improve this document processing application.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Resources

- [Azure Document Intelligence Documentation](https://learn.microsoft.com/en-us/azure/ai-services/document-intelligence/)
- [Azure SDK for .NET](https://learn.microsoft.com/en-us/azure/sdk-for-net/)

---

This **Document Processing Web Application** is a robust solution for automating document data extraction, streamlining tasks like expense reporting, record keeping, and data entry, and demonstrating the power of Azure AI Document Intelligence in transforming document workflows.
