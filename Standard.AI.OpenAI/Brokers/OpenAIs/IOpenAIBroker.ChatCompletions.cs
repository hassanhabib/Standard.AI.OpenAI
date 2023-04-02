// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial interface IOpenAIBroker
    {
        ValueTask<ExternalChatCompletionResponse> PostChatCompletionRequestAsync(
            ExternalChatCompletionRequest externalChatCompletionRequest);

        ValueTask<Stream> PostChatCompletionRequestWithStreamResponseAsync(
            ExternalChatCompletionRequest externalChatCompletionRequest,
            CancellationToken cancellationToken);

    }
}
