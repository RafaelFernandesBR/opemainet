using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Serilog;
using Telegram.Bots.Http;
using Telegram.Bots;

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
                await SendMessageAsync(chatId, "Bem vindo ao bot!", update, botClient);
                break;
            default:
                string response = await _control.GetSpeakAsync(messageText);
                await SendMessageAsync(chatId, response, update, botClient);
                break;
        }
    }

    private async Task SendMessageAsync(long chatId, string text, Update update, ITelegramBotClient botClient)
    {
        try
        {
            await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            replyToMessageId: update.Message.MessageId);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
        }
    }

}
