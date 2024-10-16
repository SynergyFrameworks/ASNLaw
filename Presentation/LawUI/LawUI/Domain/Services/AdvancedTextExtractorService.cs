using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Text;


namespace LawUI.Domain.Services;

public class AdvancedTextExtractorService : IAdvancedTextExtractorService
{
    private readonly ComputerVisionClient _computerVisionClient;
    private readonly DocumentAnalysisClient _documentAnalysisClient;

    public AdvancedTextExtractorService(ComputerVisionClient computerVisionClient, DocumentAnalysisClient documentAnalysisClient)
    {
        _computerVisionClient = computerVisionClient;
        _documentAnalysisClient = documentAnalysisClient;
    }

    public async Task<string> ExtractTextAsync(string filePath)
    {
        string fileExtension = Path.GetExtension(filePath).ToLower();

        switch (fileExtension)
        {
            case ".pdf":
                return await ExtractTextFromPdfAsync(filePath);
            case ".docx":
                return await ExtractTextFromDocxAsync(filePath);
            case ".jpg":
            case ".jpeg":
            case ".png":
                return await ExtractTextFromImageAsync(filePath);
            default:
                throw new NotSupportedException($"File type {fileExtension} is not supported.");
        }
    }

    private async Task<string> ExtractTextFromPdfAsync(string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Open);
        var operation = await _documentAnalysisClient.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-document", stream);
        var result = operation.Value;

        StringBuilder textBuilder = new StringBuilder();
        foreach (var page in result.Pages)
        {
            foreach (var line in page.Lines)
            {
                textBuilder.AppendLine(line.Content);
            }
        }

        return textBuilder.ToString();
    }

    private async Task<string> ExtractTextFromDocxAsync(string filePath)
    {
        // For DOCX, we'll use the existing WordTextExtractor
        var wordExtractor = new WordTextExtractor();
        return wordExtractor.ExtractText(filePath);
    }

    private async Task<string> ExtractTextFromImageAsync(string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Open);
        var textOperationResult = await _computerVisionClient.ReadInStreamAsync(stream);
        var operationLocation = textOperationResult.OperationLocation;

        string operationId = operationLocation.Substring(operationLocation.Length - 36);

        ReadOperationResult results;
        do
        {
            results = await _computerVisionClient.GetReadResultAsync(Guid.Parse(operationId));
        }
        while ((results.Status == OperationStatusCodes.Running ||
                results.Status == OperationStatusCodes.NotStarted));

        StringBuilder textBuilder = new StringBuilder();
        var textUrlFileResults = results.AnalyzeResult.ReadResults;
        foreach (Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models.ReadResult page in textUrlFileResults)
        {
            foreach (Line line in page.Lines)
            {
                textBuilder.AppendLine(line.Text);
            }
        }

        return textBuilder.ToString();
    }
}
