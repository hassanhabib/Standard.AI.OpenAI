// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial interface IOpenAIBroker
    {
        ValueTask<ExternalImageGenerationResponse> PostImageGenerationRequestAsync(
            ExternalImageGenerationRequest externalImageGenerationRequest);
    }
}