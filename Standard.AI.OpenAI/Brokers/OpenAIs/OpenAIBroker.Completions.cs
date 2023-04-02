// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<ExternalCompletionResponse> PostCompletionRequestAsync(
            ExternalCompletionRequest completionRequest)
        {
            return await PostAsync<ExternalCompletionRequest, ExternalCompletionResponse>(
                relativeUrl: "v1/completions",
                completionRequest);
        }

        public async ValueTask<Stream> PostCompletionRequestWithStreamResponseAsync(
            ExternalCompletionRequest completionRequest,
            CancellationToken cancellationToken)
        {
            return await PostWithStreamResponseAsync(
                relativeUrl: "v1/completions",
                completionRequest,
                cancellationToken);
        }
    }
}