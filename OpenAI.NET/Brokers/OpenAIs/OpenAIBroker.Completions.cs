// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using OpenAI.NET.Models.ExternalCompletions;
using System.Threading.Tasks;

namespace OpenAI.NET.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<CompletionResponse> PostCompletionRequestAsync(CompletionRequest completionRequest) =>
            await PostAsync<CompletionRequest, CompletionResponse>(relativeUrl: "v1/completions", completionRequest);
    }
}