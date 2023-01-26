using Control;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot;

namespace OpenAiNet;
public class Program
{
    private static async Task Main(string[] args)
    {
        var bot = new Bot("1820018763:AAHTTC5m_AvjaGoo8_sinIfTZ0HHRW3HK2c");
        bot.Start();

        var messageHandler = new MessageHandler();

        // Handle application shutdown
        var cancellationTokenSource = new CancellationTokenSource();
        AppDomain.CurrentDomain.ProcessExit += (s, e) => cancellationTokenSource.Cancel();
        Console.CancelKeyPress += (s, e) => cancellationTokenSource.Cancel();
        await Task.Delay(-1, cancellationTokenSource.Token).ContinueWith(t => { });
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}
