using opemainet.Commands;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Serilog;
using Telegram.Bot.Types;

namespace Control;
public class MessageHandler
{
    private readonly IList<opemainet.Commands.ICommandBot> _commands;
    private readonly ILogger _logger;
    private OpenAiControl _control;

    public MessageHandler(ILogger logger)
    {
        _commands = new List<opemainet.Commands.ICommandBot>
        {
            new StartCommand()
        };

        _logger = logger;
        _control = new OpenAiControl(logger);
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message.Chat.Type == ChatType.Group)
            return;

        if (update.Type != UpdateType.Message)
            return;

        if (update.Message!.Type != MessageType.Text)
            return;

        var chatId = update.Message.Chat.Id;
        var messageText = update.Message.Text;

        if (!messageText.StartsWith("/"))
        {
            // not a command, handle as normal message
            string response = await _control.GetSpeakAsync(messageText);
            await SendMessageAsync(chatId, response, update, botClient);
            return;
        }

        var commandName = messageText.Split(" ")[0];
        var command = _commands.FirstOrDefault(x => x.Nome == commandName);
        if (command == null)
        {
            // command not found
            return;
        }

        command.Executar(botClient, chatId);
    }

    private async Task SendMessageAsync(long chatId, string text, Telegram.Bot.Types.Update update, ITelegramBotClient botClient)
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
