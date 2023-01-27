using Telegram.Bot.Exceptions;
using Telegram.Bot;
using Serilog;

namespace Control;
public class ErrorHandler
{
    private readonly ILogger _logger;

    public ErrorHandler(ILogger logger)
    {
        _logger = logger;
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.Error(ErrorMessage);
        return Task.CompletedTask;
    }
}
