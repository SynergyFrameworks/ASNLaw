//using Microsoft.SemanticKernel;


//using TelegramChatGptApi.Application.Interfaces;

//namespace TelegramChatGptApi.Application.Services
//{
//    public class OpenAIService : IOpenAIService
//    {
//        private readonly ISemanticKernel _semanticKernel;
//        private readonly List<string> _conversationHistory;

//        public OpenAIService(ISemanticKernel semanticKernel)
//        {
//            _semanticKernel = semanticKernel;
//            _conversationHistory = new List<string>();
//        }

//        public async Task<string> GenerateCompletionAsync(string prompt)
//        {
//            var result = await _semanticKernel.GenerateTextAsync(prompt);

//            // Add both prompt and response to history
//            _conversationHistory.Add($"User: {prompt}");
//            _conversationHistory.Add($"AI: {result}");

//            return result;
//        }

//        public Task<IEnumerable<string>> GetHistoryAsync()
//        {
//            return Task.FromResult(_conversationHistory.AsEnumerable());
//        }

//        // Process uploaded file and treat its content as a prompt
//        public async Task<string> ProcessFileAsync(IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//                return "File is empty.";

//            string prompt;
//            using (var reader = new StreamReader(file.OpenReadStream()))
//            {
//                prompt = await reader.ReadToEndAsync();
//            }

//            // Use the file content as the prompt for OpenAI
//            return await GenerateCompletionAsync(prompt);
//        }

//        // Streaming response from OpenAI
//        public async IAsyncEnumerable<string> StreamResponseAsync(string prompt)
//        {
//            await foreach (var token in _semanticKernel.GenerateTextStreamAsync(prompt))
//            {
//                yield return token;
//            }
//        }
//    }

//}
