using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Control;
public class Bot
{
    private TelegramBotClient _botClient;
    private CancellationTokenSource _cts;
    private ReceiverOptions _receiverOptions;
    private MessageHandler _messageHandler;

    public Bot(string token)
    {
        _botClient = new TelegramBotClient(token);
        _cts = new CancellationTokenSource();
        _receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }
        };

        _messageHandler = new MessageHandler();
    }

    public async Task Start()
    {
        _botClient.StartReceiving(
            _messageHandler.HandleUpdateAsync,
            Control.ErrorHandler.HandleErrorAsync,
            _receiverOptions,
            _cts.Token);

        var me = await _botClient.GetMeAsync();
        Console.WriteLine($"Start listening for @{me.Username}");

        // set up cancellation for closing the program
        var cancellationTokenSource = new CancellationTokenSource();
        AppDomain.CurrentDomain.ProcessExit += (s, e) => cancellationTokenSource.Cancel();
        Console.CancelKeyPress += (s, e) => cancellationTokenSource.Cancel();
        await Task.Delay(-1, cancellationTokenSource.Token).ContinueWith(t => { });

        _cts.Cancel();
    }

    public async Task SendMessageAsync(long chatId, string text, Update update)
    {
        try
        {
            await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            replyToMessageId: update.Message.MessageId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
