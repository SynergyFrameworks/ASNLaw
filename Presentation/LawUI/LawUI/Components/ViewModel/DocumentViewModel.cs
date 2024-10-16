using FluentValidation;
using LawUI.Abstracts;
using LawUI.Components.Pages;
using LawUI.Domain;
using LawUI.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MongoDB.Bson;
using MudBlazor;
using System.Net.Http.Headers;

namespace LawUI.ViewModels
{
    public class DocumentViewModel
    {
        private readonly ILogger<Documents> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISnackbar _snackbar;
        private readonly IJSRuntime _js;
        private readonly MongoDbService _mongoDbService;

        public Model Model { get; private set; } = new();
        public ModelFluentValidator ValidationRules { get; } = new();
        public MudFileUpload<IReadOnlyList<IBrowserFile>>? FileUpload { get; set; }
        public bool IsValid { get; set; }
        public bool IsTouched { get; set; }
        public string DragClass { get; private set; }

        private const string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private const long MaxFileSize = 1024 * 1024 * 100; // 100 MB
        private const int MaxAllowedFiles = 3;

        public DocumentViewModel(ILogger<Documents> logger, IHttpClientFactory clientFactory, ISnackbar snackbar, IJSRuntime js, MongoDbService mongoDbService)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _snackbar = snackbar;
            _js = js;
            _mongoDbService = mongoDbService;
            DragClass = DefaultDragClass;
        }

        public void SetDragClass() => DragClass = $"{DefaultDragClass} mud-border-primary";
        public void ClearDragClass() => DragClass = DefaultDragClass;

        public Task OpenFilePickerAsync() => FileUpload?.OpenFilePickerAsync() ?? Task.CompletedTask;
        public Task ClearAsync() => FileUpload?.ClearAsync() ?? Task.CompletedTask;

        public async Task UploadToApi()
        {
            if (Model.Files == null || !Model.Files.Any())
            {
                _snackbar.Add("No files selected for upload.", MudBlazor.Severity.Warning);
                return;
            }

            var client = _clientFactory.CreateClient();
            var content = new MultipartFormDataContent();

            foreach (var file in Model.Files)
            {
                if (file.Size > MaxFileSize)
                {
                    _snackbar.Add($"File {file.Name} is too large. Maximum size is 100 MB.", MudBlazor.Severity.Warning);
                    continue;
                }

                var fileContent = new StreamContent(file.OpenReadStream(MaxFileSize));
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.Name);
            }

            content.Add(new StringContent(Model.Description), "Description");

            try
            {
                var response = await client.PostAsync("api/documentSource", content);

                if (response.IsSuccessStatusCode)
                {
                    _snackbar.Add("Document(s) uploaded successfully!", MudBlazor.Severity.Success);
                    await ClearAsync();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _snackbar.Add($"Failed to upload document(s). Error: {errorContent}", MudBlazor.Severity.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading document(s)");
                _snackbar.Add("An error occurred while uploading the document(s).", MudBlazor.Severity.Error);
            }
        }

        public async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var filePath = Path.Combine("wwwroot/uploads", file.Name);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.OpenReadStream().CopyToAsync(stream);
            }

            ITextExtractor textExtractor = TextExtractorFactory.GetTextExtractor(filePath);
            string extractedText = textExtractor.ExtractText(filePath);

            var document = new BsonDocument
            {
                { "ExtractedText", extractedText }
            };

            await _mongoDbService.InsertDocumentAsync("YourCollectionName", document);
        }

        private Stream GetFileStream()
        {
            var randomBinaryData = new byte[50 * 1024];
            return new MemoryStream(randomBinaryData);
        }

        public async Task DownloadFileFromStream()
        {
            var fileStream = GetFileStream();
            var fileName = "log.bin";

            using var streamRef = new DotNetStreamReference(stream: fileStream);
            await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }
    }

    public class Model
    {
        public IReadOnlyList<IBrowserFile>? Files { get; set; } = new List<IBrowserFile>();
        public string Description { get; set; } = string.Empty;
    }

    public class ModelFluentValidator : AbstractValidator<Model>
    {
        public ModelFluentValidator()
        {
            RuleFor(x => x.Files)
                .NotEmpty()
                .WithMessage("There must be at least 1 file.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters.");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<Model>.CreateWithOptions((Model)model, x => x.IncludeProperties(propertyName)));
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}