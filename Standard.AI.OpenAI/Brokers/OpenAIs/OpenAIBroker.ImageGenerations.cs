// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<ExternalImageGenerationResponse> PostImageGenerationRequestAsync(
            ExternalImageGenerationRequest externalImageGenerationRequest)
        {
            return await PostAsync<ExternalImageGenerationRequest, ExternalImageGenerationResponse>(
                relativeUrl: "v1/images/generations",
                content: externalImageGenerationRequest);
        }
    }
}