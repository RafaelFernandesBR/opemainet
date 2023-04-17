using Control;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Telegram.Bot;

namespace OpenAiNet
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((builder, services) =>
                {
                    services.AddSingleton<ILogger>(LoggerConfig.CreateLogger());
                    services.AddSingleton<OpenAiControl>();
                    services.AddSingleton<Bot>(provider =>
                        new Bot(new TelegramBotClient(Environment.GetEnvironmentVariable("tokem")),
                                provider.GetService<ILogger>(),
                                provider.GetService<OpenAiControl>()));
                })
                .Build();

            var bot = host.Services.GetRequiredService<Bot>();
            bot.Start();

            // Handle application shutdown
            var cancellationTokenSource = new CancellationTokenSource();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => cancellationTokenSource.Cancel();
            Console.CancelKeyPress += (s, e) => cancellationTokenSource.Cancel();
            await Task.Delay(-1, cancellationTokenSource.Token).ContinueWith(t => { });
        }
    }
}
