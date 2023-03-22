// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Models.Services.Foundations.ExternalCompletions;

namespace OpenAI.NET.Brokers.OpenAIs
{
    internal partial interface IOpenAIBroker
    {
        ValueTask<ExternalCompletionResponse> PostCompletionRequestAsync(ExternalCompletionRequest completionRequest);
    }
}