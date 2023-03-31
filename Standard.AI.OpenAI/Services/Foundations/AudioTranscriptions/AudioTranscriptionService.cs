﻿// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions;

namespace Standard.AI.OpenAI.Services.Foundations.AudioTranscriptions
{
    internal partial class AudioTranscriptionService : IAudioTranscriptionService
    {
        private readonly IOpenAIBroker openAIBroker;

        public AudioTranscriptionService(IOpenAIBroker openAIBroker) =>
            this.openAIBroker = openAIBroker;

        public ValueTask<AudioTranscription> SendAudioTranscriptionAsync(AudioTranscription audioTranscription) =>
        TryCatch(async () =>
        {
            ValidateAudioTranscriptionOnSend(audioTranscription);

            ExternalAudioTranscriptionRequest externalAudioTranscriptionRequest =
                ConvertToExternalAudioTranscriptionRequest(audioTranscription);

            ExternalAudioTranscriptionResponse externalAudioTranscriptionResponse =
                await this.openAIBroker.PostAudioTranscriptionRequestAsync(externalAudioTranscriptionRequest);

            return ConvertToAudioTranscription(audioTranscription, externalAudioTranscriptionResponse);
        });

        private static ExternalAudioTranscriptionRequest ConvertToExternalAudioTranscriptionRequest(
            AudioTranscription audioTranscription)
        {
            return new ExternalAudioTranscriptionRequest
            {
                FilePath = audioTranscription.Request.FilePath,
                Model = audioTranscription.Request.Model,
                ResponseFormat = audioTranscription.Request.ResponseFormat,
                Prompt = audioTranscription.Request.Prompt,
                Temperature = audioTranscription.Request.Temperature,
                Language = audioTranscription.Request.Language
            };
        }

        private static AudioTranscription ConvertToAudioTranscription(
            AudioTranscription audioTranscription,
            ExternalAudioTranscriptionResponse externalAudioTranscriptionResponse)
        {
            audioTranscription.Response = new AudioTranscriptionResponse
            {
                Text = externalAudioTranscriptionResponse.Text
            };

            return audioTranscription;
        }
    }
}
