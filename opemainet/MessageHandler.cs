using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Serilog;

namespace Control;
public class MessageHandler
{
    private OpenAiControl _control;
    private readonly ILogger _logger;

    public MessageHandler(ILogger logger)
    {
        _control = new OpenAiControl(logger);
        _logger = logger;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type != UpdateType.Message)
            return;

        if (update.Message!.Type != MessageType.Text)
            return;

        var chatId = update.Message.Chat.Id;
        var messageText = update.Message.Text;

        switch (messageText)
        {
            case "/start":
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Welcome to my bot!");
                break;
            default:
                string response = await _control.GetSpeakAsync(messageText);
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: response);
                break;
        }
    }
}
