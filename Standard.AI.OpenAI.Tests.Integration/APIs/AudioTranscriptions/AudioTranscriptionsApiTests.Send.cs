// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.AudioTranscriptions
{
    public partial class AudioTranscriptionsApiTests
    {
        [Theory(Skip = "This test is only for releases")]
        [InlineData(@"APIs\AudioTranscriptions\assets\OPENAI000_Getting_Started_15_sec.mp3")]
        [InlineData(@"APIs\AudioTranscriptions\assets\OPENAI000_Getting_Started_30_sec.mp3")]
        public async Task ShouldSendAudioTranscriptionAsync(string filePath)
        {
            // given
            var inputAudioTranscription = new AudioTranscription
            {
                Request = new AudioTranscriptionRequest
                {
                    Model = AudioTranscriptionModel.Whisper1,
                    FilePath = filePath
                }
            };

            // when
            AudioTranscription responseAudioTranscription =
                await this.openAIClient.AudioTranscriptions.SendAudioTranscriptionAsync(
                    inputAudioTranscription);

            // then
            Assert.NotNull(responseAudioTranscription.Response);
            Assert.NotNull(responseAudioTranscription.Response.Text);
        }
    }
}