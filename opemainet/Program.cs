using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class Program
{
    private static async Task Main(string[] args)
    {
        var botClient = new TelegramBotClient(Environment.GetEnvironmentVariable("tokem"));

        using var cts = new CancellationTokenSource();

        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { } // receive all update types
        };
        botClient.StartReceiving(
       HandleUpdateAsync,
        HandleErrorAsync,
        receiverOptions,
        cancellationToken: cts.Token);

        var me = await botClient.GetMeAsync();

        Console.WriteLine($"Start listening for @{me.Username}");

        //definindo para fechar o programa no terminal com ctrl+c
        var cancellationTokenSource = new CancellationTokenSource();
        AppDomain.CurrentDomain.ProcessExit += (s, e) => cancellationTokenSource.Cancel();
        Console.CancelKeyPress += (s, e) => cancellationTokenSource.Cancel();
        await Task.Delay(-1, cancellationTokenSource.Token).ContinueWith(t => { });

        // Send cancellation request to stop bot
        cts.Cancel();

        async Task MsgSendAsync(long chatId, string text, Update update, CancellationToken cancellationToken)
        {
            try
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            replyToMessageId: update.Message.MessageId,
            cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type != UpdateType.Message)
                return;
            // Only process text messages
            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            string Username = update.Message.Chat.Username;

            switch (messageText)
            {
                case "/start":
                    await MsgSendAsync(chatId, "Bem-vindo ao meu bot!", update, cancellationToken);
                    break;
                default:
                    string get = await Control.OpenAiControl.GetSpeakAsync(messageText);
                    await MsgSendAsync(chatId, get, update, cancellationToken);
                    break;
            }
        }
    }

    static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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
