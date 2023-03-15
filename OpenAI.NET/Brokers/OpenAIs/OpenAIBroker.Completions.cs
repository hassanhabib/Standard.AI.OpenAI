// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Models.ExternalCompletions;

namespace OpenAI.NET.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<CompletionResponse> PostCompletionRequestAsync( CompletionRequest completionRequest) =>
            await PostAsync<CompletionRequest, CompletionResponse>(relativeUrl: "v1/completions", completionRequest);
    }
}