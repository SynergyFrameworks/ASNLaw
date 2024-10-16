using TelegramChatGptApi.Application.Interfaces;
using TelegramChatGptApi.Application.Services;
using TelegramChatGptApi.Application.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var telegramToken = configuration["Telegram:BotToken"];
        var telegramApiUrl = configuration["Telegram:ApiUrl"];
        var openAIApiKey = configuration["OpenAI:ApiKey"];
        var openAiApiUrl = configuration["OpenAI:ApiUrl"];

        services.AddSingleton<ITelegramService>(provider =>
            new TelegramService(telegramToken, telegramApiUrl, provider.GetRequiredService<ILogger<TelegramService>>()));

        services.AddSingleton<IChatGptService>(provider =>
            new ChatGptService(openAIApiKey, openAiApiUrl, provider.GetRequiredService<ILogger<ChatGptService>>()));

        services.AddSingleton<GenerateMessageUseCase>();
        services.AddSingleton<GenerateDocumentUseCase>();
        services.AddSingleton<GenerateImageUseCase>();
        services.AddSingleton<GenerateVideoUseCase>();

        return services;
    }
}
