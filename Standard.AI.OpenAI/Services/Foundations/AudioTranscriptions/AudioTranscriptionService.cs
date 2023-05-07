// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;

namespace Standard.AI.OpenAI.Services.Foundations.AudioTranscriptions
{
    internal partial class AudioTranscriptionService : IAudioTranscriptionService
    {
        private readonly IOpenAIBroker openAIBroker;

        public AudioTranscriptionService(IOpenAIBroker openAIBroker) =>
            this.openAIBroker = openAIBroker;

        public ValueTask<AudioTranscription> SendAudioTranscriptionAsync(AudioTranscription audioTranscription) =>
            throw new System.NotImplementedException();
    }
}
