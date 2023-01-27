using Serilog;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Control;
public class Bot
{
    private TelegramBotClient _botClient;
    private ReceiverOptions _receiverOptions;
    private MessageHandler _messageHandler;
    private readonly ILogger _logger;
    private ErrorHandler _ErrorHandler;

    public Bot(string token, ILogger logger)
    {
        _logger = logger;
        _botClient = new TelegramBotClient(token);
        _receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }
        };

        _messageHandler = new MessageHandler(logger);
        _ErrorHandler = new ErrorHandler(logger);
    }

    public async Task Start()
    {
        _botClient.StartReceiving(
            _messageHandler.HandleUpdateAsync,
            _ErrorHandler.HandleErrorAsync,
            _receiverOptions);

        var me = await _botClient.GetMeAsync();
        _logger.Debug($"Start listening for @{me.Username}");
    }

}
