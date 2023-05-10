// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Force.DeepCloner;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AudioTranscriptions
{
    public partial class AudioTranscriptionServiceTests
    {
        [Fact]
        public async Task ShouldSendAudioTranscriptionAsync()
        {
            // given
            dynamic audioTranscriptionProperties = CreateRandomAudioTranscriptionProperties();

            var randomAudioTranscriptionRequest = new AudioTranscriptionRequest()
            {
                Content = audioTranscriptionProperties.Content,
                FileName = audioTranscriptionProperties.FileName,
                Model = audioTranscriptionProperties.Model,
                Prompt = audioTranscriptionProperties.Prompt,
                Temperature = audioTranscriptionProperties.Temperature,
                Language = audioTranscriptionProperties.Language,
            };

            var randomAudioTranscriptionResponse = new AudioTranscriptionResponse()
            {
                Text = audioTranscriptionProperties.Text
            };

            var randomAudioTranscription = new AudioTranscription()
            {
                Request = randomAudioTranscriptionRequest
            };

            var randomExternalAudioTranscriptionRequest = new ExternalAudioTranscriptionRequest()
            {
                File = audioTranscriptionProperties.ExternalFile,
                FileName = audioTranscriptionProperties.FileName,
                Model = audioTranscriptionProperties.Model,
                Prompt = audioTranscriptionProperties.Prompt,
                Temperature = audioTranscriptionProperties.Temperature,
                Language = audioTranscriptionProperties.Language
            };

            var randomExternalAudioTranscriptionResponse = new ExternalAudioTranscriptionResponse()
            {
                Text = audioTranscriptionProperties.Text
            };

            AudioTranscription inputAudioTranscription = randomAudioTranscription;
            AudioTranscription expectedAudioTranscription = inputAudioTranscription.DeepClone();
            expectedAudioTranscription.Response = randomAudioTranscriptionResponse;

            ExternalAudioTranscriptionRequest mappedExternalAudioTranscriptionRequest =
                randomExternalAudioTranscriptionRequest;

            ExternalAudioTranscriptionResponse returnedExternalAudioTranscriptionResponse =
                randomExternalAudioTranscriptionResponse;

            this.openAIBrokerMock.Setup(broker =>
                broker.PostAudioTranscriptionRequestAsync(It.Is(
                    SameExternalAudioTranscriptionRequestAs(mappedExternalAudioTranscriptionRequest))))
                    .ReturnsAsync(returnedExternalAudioTranscriptionResponse);

            // when
            AudioTranscription audioTranscription = await this.audioTranscriptionService
                .SendAudioTranscriptionAsync(inputAudioTranscription);

            // then
            this.openAIBrokerMock.Verify(broker =>
                broker.PostAudioTranscriptionRequestAsync(It.Is(
                    SameExternalAudioTranscriptionRequestAs(mappedExternalAudioTranscriptionRequest))),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}
