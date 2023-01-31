using Telegram.Bot;
using Telegram.Bot.Types;

namespace opemainet.Commands;
public class StartCommand : ICommandBot
{
    public string Nome => "/start";

    public void Executar(ITelegramBotClient botClient, long chatId, Update? update = null)
    {
        string text = "Bem vindo ao bot!";
        botClient.SendTextMessageAsync(chatId, text);
    }

}
