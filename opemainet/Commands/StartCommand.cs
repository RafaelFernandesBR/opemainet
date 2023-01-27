using Telegram.Bot;

namespace opemainet.Commands;
public class StartCommand : ICommandBot
{
    public string Nome => "/start";

    public void Executar(ITelegramBotClient botClient, long chatId, string? msg = null)
    {
        string text = "Bem vindo ao bot!";
        botClient.SendTextMessageAsync(chatId, text);
    }

}
