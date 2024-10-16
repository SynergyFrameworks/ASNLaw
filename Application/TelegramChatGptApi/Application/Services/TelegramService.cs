using Flurl;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using RestSharp;
using TelegramChatGptApi.Application.Interfaces;

namespace TelegramChatGptApi.Application.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly string _telegramToken;
        private readonly string _telegramBaseUrl;
        private readonly string _telegramFileBaseUrl;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly ILogger<TelegramService> _logger;

        public TelegramService(string telegramBotToken, string telegramBaseUrl, ILogger<TelegramService> logger)
        {
            _telegramToken = telegramBotToken;
            _telegramBaseUrl = $"{telegramBaseUrl}{_telegramToken}";
            _telegramFileBaseUrl = telegramBaseUrl;
           _logger = logger;

            // Define a Polly retry policy: 3 retries with exponential backoff
            _retryPolicy = Policy
                .Handle<FlurlHttpException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning("Retry {RetryCount} encountered an error: {Exception}. Waiting {TimeSpan} before next retry.", retryCount, exception, timeSpan);
                    });
        }

        //private static readonly AsyncAdvancedCircuitBreakerSyntax 
        public async Task SendMessageAsync(string chatId, string message)
        {
            var sendMessageUrl = $"{_telegramBaseUrl}/sendMessage";
            var payload = new { chat_id = chatId, text = message };

            _logger.LogInformation("Sending message to chat {ChatId}: {Message}", chatId, message);

            try
            {
                var response = await _retryPolicy.ExecuteAsync(async () =>
                {
                    var result = await sendMessageUrl.PostJsonAsync(payload);
                    _logger.LogInformation("Message sent successfully to chat {ChatId}", chatId);
                    return result;
                });

                _logger.LogInformation("Response: {Response}", response);
            }
            catch (FlurlHttpException ex)
            {
                var errorResponse = await ex.GetResponseStringAsync();
                _logger.LogError("Error sending message to chat {ChatId}: {ErrorResponse}", chatId, errorResponse);
                throw;
            }
        }
        public async Task SendDocumentStreamAsync(string chatId, Stream documentStream, string fileName)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(documentStream), "document", fileName);

                var response = await $"{_telegramFileBaseUrl}/bot{_telegramToken}/sendDocument"
                    .SetQueryParam("chat_id", chatId)
                    .PostAsync(content);

                response.ResponseMessage.EnsureSuccessStatusCode();
            }
        }

        public async Task<string> DownloadFileAsync(string fileId)
        {
            // Step 1: Get file info
            var client = new RestClient($"{_telegramBaseUrl}{_telegramToken}/getFile?file_id={fileId}");
            var request = new RestRequest();
            var response = await client.GetAsync(request);

            // Parse the response to extract the file path
            var filePath = JObject.Parse(response.Content)["result"]?["file_path"]?.ToString();

            if (filePath != null)
            {
                // Step 2: Download the file from Telegram servers
                var downloadUrl = $"{_telegramFileBaseUrl}/file/bot{_telegramToken}/{filePath}";
                var downloadClient = new RestClient(downloadUrl);
                var downloadResponse = await downloadClient.DownloadDataAsync(new RestRequest());

                // Save the file locally (you can change the path based on your server)
                var localFilePath = Path.Combine("your-local-directory", Path.GetFileName(filePath));
                await File.WriteAllBytesAsync(localFilePath, downloadResponse);

                return localFilePath;
            }

            return null;
        }
    

    public async Task SendPhotoAsync(string chatId, string photoUrl, string? caption = null)
        {
            var sendPhotoUrl = $"{_telegramBaseUrl}/sendPhoto";
            var payload = new { chat_id = chatId, photo = photoUrl, caption = caption };

            _logger.LogInformation("Sending photo to chat {ChatId}: {PhotoUrl}", chatId, photoUrl);

            await _retryPolicy.ExecuteAsync(async () =>
            {
                await sendPhotoUrl.PostJsonAsync(payload);
                _logger.LogInformation("Photo sent successfully to chat {ChatId}", chatId);
            });
        }

        public async Task SendVideoAsync(string chatId, string videoUrl, string? caption = null)
        {
            var sendVideoUrl = $"{_telegramBaseUrl}/sendVideo";
            var payload = (chat_id: chatId, video: videoUrl, caption: caption);

            _logger.LogInformation("Sending video to chat {ChatId}: {VideoUrl}", chatId, videoUrl);

            await _retryPolicy.ExecuteAsync(async () =>
            {
                await sendVideoUrl.PostJsonAsync(payload);
                _logger.LogInformation("Video sent successfully to chat {ChatId}", chatId);
            });
        }

        public async Task SendVideoStreamAsync(string chatId, Stream videoStream, string fileName)
        {
            using (var content = new MultipartFormDataContent())
            {
                // Add the video stream to the content with the appropriate file name
                content.Add(new StreamContent(videoStream), "video", fileName);

                // Make the HTTP POST request to Telegram API for sending the video
                var response = await $"{_telegramBaseUrl}/bot{_telegramToken}/sendVideo"
                    .SetQueryParam("chat_id", chatId)
                    .PostAsync(content);

                // Ensure the response is successful, otherwise throw an exception
                response.ResponseMessage.EnsureSuccessStatusCode();
            }
        }


        public async Task SendDocumentAsync(string chatId, string documentUrl, string? caption = null)
        {
            var sendDocumentUrl = $"{_telegramBaseUrl}/sendDocument";
            var payload = (chat_id: chatId, document: documentUrl, caption: caption);

            _logger.LogInformation("Sending document to chat {ChatId}: {DocumentUrl}", chatId, documentUrl);

            await _retryPolicy.ExecuteAsync(async () =>
            {
                await sendDocumentUrl.PostJsonAsync(payload);
                _logger.LogInformation("Document sent successfully to chat {ChatId}", chatId);
            });
        }

        public async Task SendAudioAsync(string chatId, string audioUrl, string? caption = null)
        {
            var sendAudioUrl = $"{_telegramBaseUrl}/sendAudio";
            var payload = (chat_id: chatId, audio: audioUrl, caption: caption);

            _logger.LogInformation("Sending audio to chat {ChatId}: {AudioUrl}", chatId, audioUrl);

            await _retryPolicy.ExecuteAsync(async () =>
            {
                await sendAudioUrl.PostJsonAsync(payload);
                _logger.LogInformation("Audio sent successfully to chat {ChatId}", chatId);
            });
        }

        public async Task<string> GetUpdatesAsync()
        {
            var updatesUrl = $"{_telegramBaseUrl}/getUpdates";

            _logger.LogInformation("Fetching updates");

            return await _retryPolicy.ExecuteAsync(async () =>
            {
                var result = await updatesUrl.GetStringAsync();
                _logger.LogInformation("Updates fetched successfully");
                return result;
            });
        }

        public async Task DeleteMessageAsync(string chatId, string messageId)
        {
            var deletePayload = new { chat_id = chatId, message_id = messageId };

            _logger.LogInformation("Deleting message {MessageId} from chat {ChatId}", messageId, chatId);

            await _retryPolicy.ExecuteAsync(async () =>
            {
                await $"{_telegramBaseUrl}/deleteMessage".PostJsonAsync(deletePayload);
                _logger.LogInformation("Message {MessageId} deleted successfully from chat {ChatId}", messageId, chatId);
            });
        }
    }
}
