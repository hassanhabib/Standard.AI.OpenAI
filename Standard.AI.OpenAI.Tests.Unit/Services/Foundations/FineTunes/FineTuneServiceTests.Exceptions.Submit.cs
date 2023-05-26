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
                broker.PostFineTuneAsync(
                    It.IsAny<ExternalFineTuneRequest>()),
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
                broker.PostFineTuneAsync(
                    It.IsAny<ExternalFineTuneRequest>()),
                        Times.Once());

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnSubmitIfBadRequestErrorOccursAsync()
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidFineTuneException =
                new InvalidFineTuneException(httpResponseBadRequestException);

            var expectedFineTuneDependencyValidationException =
                new FineTuneDependencyValidationException(invalidFineTuneException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostFineTuneAsync(It.IsAny<ExternalFineTuneRequest>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<FineTune> submitFineTuneTask =
                this.fineTuneService.SubmitFineTuneAsync(someFineTune);

            FineTuneDependencyValidationException actualFineTuneDependencyValidationException =
                await Assert.ThrowsAsync<FineTuneDependencyValidationException>(
                    submitFineTuneTask.AsTask);

            // then
            actualFineTuneDependencyValidationException.Should().BeEquivalentTo(
                expectedFineTuneDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFineTuneAsync(
                    It.IsAny<ExternalFineTuneRequest>()),
                        Times.Once());

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnSubmitIfTooManyRequestsErrorOccursAsync()
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallFineTuneException =
                new ExcessiveCallFineTuneException(httpResponseTooManyRequestsException);

            var expectedFineTuneDependencyValidationException =
                new FineTuneDependencyValidationException(excessiveCallFineTuneException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostFineTuneAsync(It.IsAny<ExternalFineTuneRequest>()))
                    .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<FineTune> submitFineTuneTask =
                this.fineTuneService.SubmitFineTuneAsync(someFineTune);

            FineTuneDependencyValidationException actualFineTuneDependencyValidationException =
                await Assert.ThrowsAsync<FineTuneDependencyValidationException>(
                    submitFineTuneTask.AsTask);

            // then
            actualFineTuneDependencyValidationException.Should().BeEquivalentTo(
                expectedFineTuneDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFineTuneAsync(
                    It.IsAny<ExternalFineTuneRequest>()),
                        Times.Once());

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnSubmitIfServerErrorOccursAsync()
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();

            var httpResponseException =
                new HttpResponseException();

            var failedServerFineTuneException =
                new FailedServerFineTuneException(httpResponseException);

            var expectedFineTuneDependencyException =
                new FineTuneDependencyException(failedServerFineTuneException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostFineTuneAsync(It.IsAny<ExternalFineTuneRequest>()))
                    .ThrowsAsync(httpResponseException);

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
                broker.PostFineTuneAsync(
                    It.IsAny<ExternalFineTuneRequest>()),
                        Times.Once());

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnSubmitIfExceptionOccursAsync()
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();
            var serviceException = new Exception();

            var failedFineTuneServiceException =
                new FailedFineTuneServiceException(serviceException);

            var expectedFineTuneServiceException =
                new FineTuneServiceException(failedFineTuneServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostFineTuneAsync(It.IsAny<ExternalFineTuneRequest>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<FineTune> submitFineTuneTask =
                this.fineTuneService.SubmitFineTuneAsync(someFineTune);

            FineTuneServiceException actualFineTuneServiceException =
                await Assert.ThrowsAsync<FineTuneServiceException>(
                    submitFineTuneTask.AsTask);

            // then
            actualFineTuneServiceException.Should().BeEquivalentTo(
                expectedFineTuneServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFineTuneAsync(
                    It.IsAny<ExternalFineTuneRequest>()),
                        Times.Once());

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
