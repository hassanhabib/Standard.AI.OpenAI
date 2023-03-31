// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial interface IOpenAIBroker
    {
        ValueTask<ExternalAudioTranscriptionResponse> PostAudioTranscriptionRequestAsync(
            ExternalAudioTranscriptionRequest externalAudioTranscriptionRequest);
    }
}
