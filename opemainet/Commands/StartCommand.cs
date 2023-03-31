using Telegram.Bot;
using Telegram.Bot.Types;

namespace opemainet.Commands;
public class StartCommand : ICommandBot
{
    public string Nome => "/start";

    public async void Executar(ITelegramBotClient botClient, long chatId, Update? update = null)
    {
        string text = "Bem vindo ao bot!";
        await botClient.SendTextMessageAsync(chatId, text);
    }

}
