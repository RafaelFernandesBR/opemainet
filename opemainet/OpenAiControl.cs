using OpenAI;
using OpenAI.Models;
using Serilog;
using System.Diagnostics;

namespace Control;
public class OpenAiControl
{
    private readonly ILogger _logger;

    public OpenAiControl(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<string> GetSpeakAsync(string speak)
    {
        try
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var api = new OpenAI.OpenAIClient(new OpenAIAuthentication(Environment.GetEnvironmentVariable("opemAItokem")));
            var result = await api.CompletionsEndpoint.CreateCompletionAsync(speak, temperature: 0.9, maxTokens: 3500, model: Model.Davinci);

            stopwatch.Stop();
            _logger.Information("Tempo de execução: {0}", stopwatch.Elapsed);

            return result.ToString();
        }
        catch (Exception ex)
        {
            _logger.Error("Erro: " + ex.Message);
            return "erro, tente novamente mais tarde.";
        }

    }
}
