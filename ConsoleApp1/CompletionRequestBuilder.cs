using ConsoleApp1;
using FluentBuilder;

namespace OpenAI.NET.Models.Completions
{
    [AutoGenerateBuilder(typeof(CompletionRequest))]
    public partial class CompletionRequestBuilder
    {
        public CompletionRequestBuilder WithPrompt(string prompt)
        {
            return WithPrompt(new[] { prompt });
        }

        public CompletionRequestBuilder WithModel(ModelAsEnum model)
        {
            var modelAsString = model.TryGetDescription(out var value) ? value : model.ToString();

            return WithModel(modelAsString);
        }
    }
}