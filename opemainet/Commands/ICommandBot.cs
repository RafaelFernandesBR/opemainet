using Telegram.Bot;

namespace opemainet.Commands;
public interface ICommandBot
{
    string Nome { get; }
    void Executar(ITelegramBotClient botClient, long chatId, string? msg = null);
}
