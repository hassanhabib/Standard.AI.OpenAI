// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Models.ExternalCompletions;

namespace OpenAI.NET.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<CompletionResponse> PostCompletionRequestAsync( string completionRequest) =>
            await PostAsync<string, CompletionResponse>(relativeUrl: "v1/completions", completionRequest);
    }
}