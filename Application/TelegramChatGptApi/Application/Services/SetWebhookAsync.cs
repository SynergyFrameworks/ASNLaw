using RestSharp;
using System.Threading.Tasks;
//ngrok http https://localhost:5001

public class TelegramWebhookService
{
    private readonly string _botToken = "YOUR_BOT_TOKEN";

    public async Task SetWebhookAsync(string webhookUrl)
    {
        var client = new RestClient($"https://api.telegram.org/bot{_botToken}/setWebhook");
        var request = new RestRequest();

        request.AddParameter("url", webhookUrl);

        var response = await client.PostAsync(request);
        Console.WriteLine(response.Content);
    }

    public async Task DeleteWebhookAsync()
    {
        var client = new RestClient($"https://api.telegram.org/bot{_botToken}/deleteWebhook");
        var request = new RestRequest();

        var response = await client.PostAsync(request);
        Console.WriteLine(response.Content);
    }




}
