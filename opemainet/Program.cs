using Control;

namespace OpenAiNet;
public class Program
{
    private static async Task Main(string[] args)
    {
        var bot = new Bot(Environment.GetEnvironmentVariable("tokem"), LoggerConfig.CreateLogger());
        bot.Start();

        // Handle application shutdown
        var cancellationTokenSource = new CancellationTokenSource();
        AppDomain.CurrentDomain.ProcessExit += (s, e) => cancellationTokenSource.Cancel();
        Console.CancelKeyPress += (s, e) => cancellationTokenSource.Cancel();
        await Task.Delay(-1, cancellationTokenSource.Token).ContinueWith(t => { });
    }
}
