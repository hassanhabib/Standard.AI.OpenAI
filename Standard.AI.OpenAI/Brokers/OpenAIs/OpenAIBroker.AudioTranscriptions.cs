// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<ExternalAudioTranscriptionResponse> PostAudioTranscriptionRequestAsync(
            ExternalAudioTranscriptionRequest externalAudioTranscriptionRequest)
        {
            return await this.PostFormAsync<ExternalAudioTranscriptionRequest, ExternalAudioTranscriptionResponse>(
                relativeUrl: "v1/audio/transcriptions",
                content: externalAudioTranscriptionRequest);
        }
    }
}