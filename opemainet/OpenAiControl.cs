using OpenAI;
using OpenAI.Models;
using System.Diagnostics;

namespace Control;
public class OpenAiControl
{
    public static async Task<string> GetSpeakAsync(string speak)
    {
        try
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var api = new OpenAI.OpenAIClient(new OpenAIAuthentication(Environment.GetEnvironmentVariable("opemAItokem")));
            var result = await api.CompletionsEndpoint.CreateCompletionAsync(speak, temperature: 0.7, max_tokens: 3000, model: Model.Davinci);
            string n = result.ToString();

            stopwatch.Stop();
            Console.WriteLine("Tempo de execução: {0}", stopwatch.Elapsed);

            return n;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
            return "erro, tente novamente mais tarde.";
        }

    }
}