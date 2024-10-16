using Newtonsoft.Json.Linq;
using OpenAI_API; // For OpenAI 1.11.0
using OpenAI_API.Chat; // Chat-related namespace
using OpenAI_API.Embedding;
using OpenAI_API.Images; // Image-related namespace
using Polly;
using Polly.Retry;
using RestSharp;
using TelegramChatGptApi.Application.Interfaces;

public class ChatGptService : IChatGptService
{
    private readonly OpenAIAPI _openAiClient;
    private readonly ILogger<ChatGptService> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly string _apiUrl;

    // Updated constructor to accept apiUrl as per the injector signature
    public ChatGptService(string apiKey, string apiUrl, ILogger<ChatGptService> logger)
    {
        _logger = logger;
        _apiUrl = apiUrl;
        _openAiClient = new OpenAIAPI(apiKey); // Constructor for OpenAI 1.11.0

        _retryPolicy = Policy
          .Handle<HttpRequestException>()
          .WaitAndRetryAsync(
              retryCount: 3,
              sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
              onRetry: (exception, timeSpan, retryCount, context) =>
              {
                  _logger.LogWarning($"Retry {retryCount} due to {exception.Message}. Waiting {timeSpan} before next retry.");
              });
    }

    #region Chat Completion (Non-Streaming)
    public async Task<string> GetChatGptResponseAsync(string userMessage)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var chatRequest = new ChatRequest()
            {
                Model = OpenAI_API.Models.Model.DefaultChatModel, // Or specify "gpt-3.5-turbo" or "gpt-4"
                Messages = new[]
                {
                    new OpenAI_API.Chat.ChatMessage(ChatMessageRole.User, userMessage)
                }
            };

            var result = await _openAiClient.Chat.CreateChatCompletionAsync(chatRequest);
            return result.Choices.FirstOrDefault()?.Message?.Content ?? "No response available";
        });
    }
    #endregion

    #region Chat Completion (Streaming)
    public async Task GetChatGptStreamResponseAsync(string userMessage, Action<string> onStreamData, CancellationToken cancellationToken = default)
    {
        await _retryPolicy.ExecuteAsync(async () =>
        {
            var client = new RestClient($"{_apiUrl}/v1/chat/completions");

            var request = new RestRequest("", Method.Post);
            request.AddHeader("Authorization", $"Bearer {_openAiClient.Auth.ApiKey}");
            request.AddHeader("Content-Type", "application/json");

            var body = new
            {
                model = "gpt-4", // or "gpt-3.5-turbo"
                messages = new[]
                {
                new
                {
                    role = "user",
                    content = userMessage
                }
                },
                stream = true // Enable streaming
            };

            request.AddJsonBody(body);

            var response = await client.ExecuteAsync(request, cancellationToken);
            using var responseStream = response.RawBytes != null
                ? new MemoryStream(response.RawBytes)
                : throw new Exception("Empty response stream");

            using var streamReader = new StreamReader(responseStream);
            string line;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                if (line.StartsWith("data: "))
                {
                    string json = line.Substring("data: ".Length).Trim();
                    if (json == "[DONE]") break;

                    var parsed = JObject.Parse(json);
                    var content = parsed["choices"]?[0]?["delta"]?["content"]?.ToString();
                    if (!string.IsNullOrEmpty(content))
                    {
                        onStreamData(content);
                    }
                }
            }
        });
    }
    #endregion

    #region Image Generation
    public async Task<string> GenerateImageAsync(string prompt)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var imageRequest = new ImageGenerationRequest(prompt);
            var imageResult = await _openAiClient.ImageGenerations.CreateImageAsync(imageRequest);
            return imageResult.Data.FirstOrDefault()?.Url ?? "No image available";
        });
    }
    #endregion

    #region Embeddings
    public async Task<float[]> GetEmbeddingsAsync(string input)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var embeddingRequest = new EmbeddingRequest(input);
            var embeddingResult = await _openAiClient.Embeddings.CreateEmbeddingAsync(embeddingRequest);
            return embeddingResult.Data.FirstOrDefault()?.Embedding ?? new float[0];
        });
    }
    #endregion

    #region Audio Processing
    public async Task<string> TranscribeAudioAsync(string filePath)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var client = new RestClient($"{_apiUrl}/v1/audio/transcriptions");
            var request = new RestRequest();

            request.AddHeader("Authorization", $"Bearer {_openAiClient.Auth.ApiKey}");
            request.AddFile("file", filePath);
            request.AddParameter("model", "whisper-1");

            var response = await client.PostAsync(request);
            var jsonResponse = JObject.Parse(response.Content);
            return jsonResponse["text"]?.ToString() ?? "Transcription failed.";
        });
    }
    #endregion

    #region File Upload
    public async Task<string> UploadFileAsync(string filePath)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var client = new RestClient($"{_apiUrl}/v1/files");
            var request = new RestRequest();

            request.AddHeader("Authorization", $"Bearer {_openAiClient.Auth.ApiKey}");
            request.AddFile("file", filePath);
            request.AddParameter("purpose", "fine-tune");

            var response = await client.PostAsync(request);
            var jsonResponse = JObject.Parse(response.Content);
            return jsonResponse["id"]?.ToString() ?? "File upload failed.";
        });
    }
    #endregion
}



//// Using the Assistant
//var assistantResponse = await chatGptService.GetAssistantResponseAsync("What's the weather today?");

//// Chat Streaming
//await chatGptService.GetChatGptStreamResponseAsync("Tell me a joke", Console.WriteLine);

//// Chat Non-Streaming
//var chatResponse = await chatGptService.GetChatGptResponseAsync("What is AI?");

//// Image Generation
//var imageUrl = await chatGptService.GenerateImageAsync("A futuristic city skyline");

//// Embeddings
//var embeddings = await chatGptService.GetEmbeddingsAsync("AI is transforming the world");

//// Audio Transcription
//var transcription = await chatGptService.TranscribeAudioAsync("/path/to/audio.mp3");

//// File Upload
//var fileId = await chatGptService.UploadFileAsync("/path/to/file.csv");
