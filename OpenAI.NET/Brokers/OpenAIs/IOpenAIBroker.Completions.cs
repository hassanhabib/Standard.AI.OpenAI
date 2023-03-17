// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using OpenAI.NET.Models.ExternalCompletions;
using System.Threading.Tasks;

namespace OpenAI.NET.Brokers.OpenAIs
{
    internal partial interface IOpenAIBroker
    {
        ValueTask<CompletionResponse> PostCompletionRequestAsync(CompletionRequest completionRequest);
    }
}