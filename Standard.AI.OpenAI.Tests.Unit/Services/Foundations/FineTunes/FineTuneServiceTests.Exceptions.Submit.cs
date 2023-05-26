// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.FineTunes
{
    public partial class FineTuneServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnSubmitIfUrlNotFoundAsync()
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidFineTuneConfigurationException =
                new InvalidFineTuneConfigurationException(
                    httpResponseUrlNotFoundException);

            var expectedFineTuneDependencyException =
                new FineTuneDependencyException(
                    invalidFineTuneConfigurationException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostFineTuneAsync(It.IsAny<ExternalFineTuneRequest>()))
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<FineTune> submitFineTuneTask =
                this.fineTuneService.SubmitFineTuneAsync(someFineTune);

            FineTuneDependencyException actualFineTuneDependencyException =
                await Assert.ThrowsAsync<FineTuneDependencyException>(
                    submitFineTuneTask.AsTask);

            // then
            actualFineTuneDependencyException.Should().BeEquivalentTo(
                expectedFineTuneDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFineTuneAsync(It.IsAny<ExternalFineTuneRequest>()),
                    Times.Once());

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        public async Task ShouldThrowDependencyExceptionOnSubmitIfUnauthorizedExceptionAsync(
            Exception unauthorizedException)
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();

            var unauthorizedFineTuneException =
                new UnauthorizedFineTuneException(
                    unauthorizedException);

            var expectedFineTuneDependencyException =
                new FineTuneDependencyException(
                    unauthorizedFineTuneException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostFineTuneAsync(It.IsAny<ExternalFineTuneRequest>()))
                    .ThrowsAsync(unauthorizedException);

            // when
            ValueTask<FineTune> submitFineTuneTask =
                this.fineTuneService.SubmitFineTuneAsync(someFineTune);

            FineTuneDependencyException actualFineTuneDependencyException =
                await Assert.ThrowsAsync<FineTuneDependencyException>(
                    submitFineTuneTask.AsTask);

            // then
            actualFineTuneDependencyException.Should().BeEquivalentTo(
                expectedFineTuneDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFineTuneAsync(It.IsAny<ExternalFineTuneRequest>()),
                    Times.Once());

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
