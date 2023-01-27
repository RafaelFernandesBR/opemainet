using Control;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot;

namespace OpenAiNet;
public class Program
{
    private static async Task Main(string[] args)
    {
        var bot = new Bot(Environment.GetEnvironmentVariable("tokem"));
        bot.Start();

        var messageHandler = new MessageHandler();

        // Handle application shutdown
        var cancellationTokenSource = new CancellationTokenSource();
        AppDomain.CurrentDomain.ProcessExit += (s, e) => cancellationTokenSource.Cancel();
        Console.CancelKeyPress += (s, e) => cancellationTokenSource.Cancel();
        await Task.Delay(-1, cancellationTokenSource.Token).ContinueWith(t => { });
    }

}
