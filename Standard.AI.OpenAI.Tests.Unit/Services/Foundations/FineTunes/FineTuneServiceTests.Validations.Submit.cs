// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.FineTunes
{
    public partial class FineTuneServiceTests
    {

        [Fact]
        public async Task ShouldThrowValidationExceptionOnSubmitIfFineTuneIsNullAsync()
        {
            // given
            FineTune nullFineTune = null;

            var nullFineTuneException =
                new NullFineTuneException();

            var expectedFineTuneValidationException =
                new FineTuneValidationException(nullFineTuneException);

            // when
            ValueTask<FineTune> sendFineTuneTask =
                this.fineTuneService.SubmitFineTuneAsync(
                    nullFineTune);

            FineTuneValidationException actualFineTuneValidationException =
                await Assert.ThrowsAsync<FineTuneValidationException>(
                    sendFineTuneTask.AsTask);

            // then
            actualFineTuneValidationException.Should()
                .BeEquivalentTo(expectedFineTuneValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFineTuneAsync(
                    It.IsAny<ExternalFineTuneRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnSubmitIfFineTuneRequestIsNullAsync()
        {
            // given
            FineTuneRequest nullFineTuneRequest = null;

            FineTune fineTune = new FineTune
            {
                Request = nullFineTuneRequest
            };

            var invalidFineTuneException =
                new InvalidFineTuneException();

            invalidFineTuneException.AddData(
                key: nameof(FineTune.Request),
                values: "Value is required");

            var expectedFineTuneValidationException =
                new FineTuneValidationException(invalidFineTuneException);

            // when
            ValueTask<FineTune> sendFineTuneTask =
                this.fineTuneService.SubmitFineTuneAsync(
                    fineTune);

            FineTuneValidationException actualFineTuneValidationException =
                await Assert.ThrowsAsync<FineTuneValidationException>(
                    sendFineTuneTask.AsTask);

            // then
            actualFineTuneValidationException.Should()
                .BeEquivalentTo(expectedFineTuneValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFineTuneAsync(
                    It.IsAny<ExternalFineTuneRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
