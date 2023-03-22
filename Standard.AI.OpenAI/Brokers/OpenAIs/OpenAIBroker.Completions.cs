// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Models.Services.Foundations.ExternalCompletions;

namespace OpenAI.NET.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<ExternalCompletionResponse> PostCompletionRequestAsync(ExternalCompletionRequest completionRequest) =>
            await PostAsync<ExternalCompletionRequest, ExternalCompletionResponse>(relativeUrl: "v1/completions", completionRequest);
    }
}