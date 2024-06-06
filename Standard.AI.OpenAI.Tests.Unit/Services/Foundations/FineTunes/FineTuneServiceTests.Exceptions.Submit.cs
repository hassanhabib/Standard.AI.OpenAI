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
        private async Task ShouldThrowDependencyExceptionOnSubmitIfUrlNotFoundAsync()
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidFineTuneConfigurationException =
                new InvalidFineTuneConfigurationException(
                    message: "Invalid fine tune configuration error ocurred, contact support.",
                        innerException: httpResponseUrlNotFoundException);

            var expectedFineTuneDependencyException =
                new FineTuneDependencyException(
                    message: "Fine tune dependency error ocurred, contact support.",
                        innerException: invalidFineTuneConfigurationException);

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
        private async Task ShouldThrowDependencyExceptionOnSubmitIfUnauthorizedExceptionAsync(
            Exception unauthorizedException)
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();

            var unauthorizedFineTuneException =
                new UnauthorizedFineTuneException(
                    message: "Unauthorized fine tune request, fix errors and try again.",
                        innerException: unauthorizedException);

            var expectedFineTuneDependencyException =
                new FineTuneDependencyException(
                    message: "Fine tune dependency error ocurred, contact support.",
                        innerException: unauthorizedFineTuneException);

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
        private async Task ShouldThrowDependencyValidationExceptionOnSubmitIfBadRequestErrorOccursAsync()
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidFineTuneException =
                new InvalidFineTuneException(
                    message: "Fine tune is invalid.",
                        innerException: httpResponseBadRequestException);

            var expectedFineTuneDependencyValidationException =
                new FineTuneDependencyValidationException(
                    message: "Fine tune dependency validation error occurred, fix errors and try again",
                        innerException: invalidFineTuneException);

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
        private async Task ShouldThrowDependencyValidationExceptionOnSubmitIfTooManyRequestsErrorOccursAsync()
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallFineTuneException =
                new ExcessiveCallFineTuneException(
                    message: "Excessive call error occurred, limit your calls.",
                        innerException: httpResponseTooManyRequestsException);

            var expectedFineTuneDependencyValidationException =
                new FineTuneDependencyValidationException(
                    message: "Fine tune dependency validation error occurred, fix errors and try again",
                        innerException: excessiveCallFineTuneException);

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
        private async Task ShouldThrowDependencyExceptionOnSubmitIfServerErrorOccursAsync()
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();

            var httpResponseException =
                new HttpResponseException();

            var failedServerFineTuneException =
                new FailedServerFineTuneException(
                    message: "Failed fine tune server error occurred, contact support.",
                        innerException: httpResponseException);

            var expectedFineTuneDependencyException =
                new FineTuneDependencyException(
                    message: "Fine tune dependency error ocurred, contact support.",
                        innerException: failedServerFineTuneException);

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
        private async Task ShouldThrowServiceExceptionOnSubmitIfExceptionOccursAsync()
        {
            // given
            FineTune someFineTune = CreateRandomFineTune();
            var serviceException = new Exception();

            var failedFineTuneServiceException =
                new FailedFineTuneServiceException(
                    message: "Failed fine tune error occurred, contact support.",
                        innerException: serviceException);

            var expectedFineTuneServiceException =
                new FineTuneServiceException(
                    message: "Fine tune error ocurred, contact support.",
                        innerException: failedFineTuneServiceException);

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