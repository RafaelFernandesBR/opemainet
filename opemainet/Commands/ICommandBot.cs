using Telegram.Bot;
using Telegram.Bot.Types;

namespace opemainet.Commands;
public interface ICommandBot
{
    string Nome { get; }
    void Executar(ITelegramBotClient botClient, long chatId, Update? update = null);
}
