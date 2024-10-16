
    //using Microsoft.SemanticKernel;

    ////using Microsoft.SemanticKernel.SkillDefinition;
    //using Polly;
    //using Polly.Retry;
    //using Microsoft.Extensions.Logging;
    //using System;
    //using System.Linq;
    //using System.Threading;
    //using System.Threading.Tasks;

    //using TelegramChatGptApi.Application.Interfaces;
    //using Microsoft.EntityFrameworkCore.Metadata;
    //using Microsoft.EntityFrameworkCore;

    //public class ChatGptService : IChatGptService
    //{
    //    private readonly IKernel _kernel;
    //    private readonly ILogger<ChatGptService> _logger;
    //    private readonly AsyncRetryPolicy _retryPolicy;

    //    public ChatGptService(string apiKey, ILogger<ChatGptService> logger)
    //    {
    //        _logger = logger;

    //        // Initialize the Semantic Kernel
    //        _kernel = Kernel.Builder
    //            .Configure(config => config.AddOpenAIChatCompletionService("gpt-4", apiKey))
    //            .Build();

    //        _retryPolicy = Policy
    //          .Handle<Exception>()
    //          .WaitAndRetryAsync(
    //              retryCount: 3,
    //              sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
    //              onRetry: (exception, timeSpan, retryCount, context) =>
    //              {
    //                  _logger.LogWarning($"Retry {retryCount} due to {exception.Message}. Waiting {timeSpan} before next retry.");
    //              });
    //    }

    //    #region Chat Completion (Streaming & Non-Streaming)
    //    public async Task GetChatGptStreamResponseAsync(string userMessage, Action<string> onStreamData, CancellationToken cancellationToken = default)
    //    {
    //        await _retryPolicy.ExecuteAsync(async () =>
    //        {
    //            // SK Function: Streaming Response
    //            var contextVariables = new ContextVariables { ["input"] = userMessage };
    //            var resultStream = _kernel.RunAsync("gpt-4", contextVariables);

    //            await foreach (var result in resultStream)
    //            {
    //                onStreamData(result.Result); // Stream the response back to the caller
    //            }
    //        });
    //    }
    //}
    //    public async Task<string> GetChatGptResponseAsync(string userMessage)
    //    {
    //        return await _retryPolicy.ExecuteAsync(async () =>
    //        {
    //            // SK Function: Regular Chat Completion
    //            var contextVariables = new ContextVariables { ["input"] = userMessage };
    //            var result = await _kernel.RunAsync("gpt-4", contextVariables);
    //            return result.Result;
    //        });
    //    }
    //    #endregion

    //    #region Image Generation
    //    public async Task<string> GenerateImageAsync(string prompt)
    //    {
    //        return await _retryPolicy.ExecuteAsync(async () =>
    //        {
    //            // Semantic Kernel currently doesn't support direct image generation, so use OpenAI SDK here if needed
    //            var skill = _kernel.ImportSkill(new CustomImageGenerationSkill());

    //            var result = await skill["GenerateImage"].InvokeAsync(prompt);
    //            return result.Result;
    //        });
    //    }
    //    #endregion

    //    #region Embeddings
    //    public async Task<float[]> GetEmbeddingsAsync(string input)
    //    {
    //        return await _retryPolicy.ExecuteAsync(async () =>
    //        {
    //            var skill = _kernel.ImportSkill(new CustomEmbeddingSkill());

    //            var result = await skill["GetEmbedding"].InvokeAsync(input);
    //            return result.Variables.Get<float[]>("embedding");
    //        });
    //    }
    //    #endregion

    //    #region Audio Transcription
    //    public async Task<string> TranscribeAudioAsync(string filePath)
    //    {
    //        return await _retryPolicy.ExecuteAsync(async () =>
    //        {
    //            var skill = _kernel.ImportSkill(new CustomAudioTranscriptionSkill());

    //            var result = await skill["TranscribeAudio"].InvokeAsync(filePath);
    //            return result.Result;
    //        });
    //    }
    //    #endregion

    //    #region File Upload
    //    public async Task<string> UploadFileAsync(string filePath)
    //    {
    //        return await _retryPolicy.ExecuteAsync(async () =>
    //        {
    //            var skill = _kernel.ImportSkill(new CustomFileUploadSkill());

    //            var result = await skill["UploadFile"].InvokeAsync(filePath);
    //            return result.Result;
    //        });
    //    }
    //    #endregion
    //}

    //// Custom Skills for specific actions not directly handled by the Kernel
    //public class CustomImageGenerationSkill : ISKFunction
    //{
    //    [SKFunction("GenerateImage")]
    //    public async Task<string> GenerateImageAsync(string prompt)
    //    {
    //        // OpenAI Image Generation SDK call can go here
    //        return "https://example.com/image.png"; // Placeholder URL
    //    }
    //}

    //public class CustomEmbeddingSkill : ISKFunction
    //{
    //    [SKFunction("GetEmbedding")]
    //    public async Task<SKContext> GetEmbeddingAsync(string input)
    //    {
    //        // OpenAI Embedding SDK call can go here
    //        var embedding = new float[1024]; // Example embedding array
    //        return new SKContext().WithVariable("embedding", embedding);
    //    }
    //}

    //public class CustomAudioTranscriptionSkill : ISKFunction
    //{
    //    [SKFunction("TranscribeAudio")]
    //    public async Task<string> TranscribeAudioAsync(string filePath)
    //    {
    //        // OpenAI Audio Transcription SDK call can go here
    //        return "Transcribed text"; // Placeholder transcription result
    //    }
    //}

    //public class CustomFileUploadSkill : ISKFunction
    //{
    //    [SKFunction("UploadFile")]
    //    public async Task<string> UploadFileAsync(string filePath)
    //    {
    //        // OpenAI File Upload SDK call can go here
    //        return "file-id"; // Placeholder file ID
    //    }
    //}
