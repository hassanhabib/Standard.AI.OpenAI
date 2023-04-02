// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial interface IOpenAIBroker
    {
        ValueTask<ExternalCompletionResponse> PostCompletionRequestAsync(
            ExternalCompletionRequest completionRequest);

        ValueTask<Stream> PostCompletionRequestWithStreamResponseAsync(
            ExternalCompletionRequest completionRequest,
            CancellationToken cancellationToken);
    }
}