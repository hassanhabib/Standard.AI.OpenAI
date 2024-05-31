// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIModels
{
    public partial class AIModelServiceTests
    {
        [Fact]
        private async Task ShouldThrowDependencyExceptionOnRetrieveAIModelByNameIfUrlNotFoundAsync()
        {
            // given
            string someAIModelName = CreateRandomString();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationAIModelException =
                new InvalidConfigurationAIModelException(
                    message: "Invalid AI Model configuration error occurred, contact support.",
                        innerException: httpResponseUrlNotFoundException);

            var expectedAIModelDependencyException =
                createAIModelDependencyException(
                        innerException: invalidConfigurationAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAIModelByIdAsync(someAIModelName))
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(someAIModelName);

            AIModelDependencyException
                actualAIModelDependencyException =
                    await Assert.ThrowsAsync<AIModelDependencyException>(
                        retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelName),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        private async Task ShouldThrowDependencyExceptionOnRetrieveAIModelByNameIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            string someAIModelName = CreateRandomString();

            var unauthorizedAIModelException =
                new UnauthorizedAIModelException(
                    message: "Unauthorized AI Model error occurred, fix errors and try again.",
                        innerException: unauthorizedException);

            var expectedAIModelDependencyException =
                createAIModelDependencyException(
                        innerException: unauthorizedAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                 broker.GetAIModelByIdAsync(someAIModelName))
                     .ThrowsAsync(unauthorizedException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(someAIModelName);

            AIModelDependencyException
                actualAIModelDependencyException =
                    await Assert.ThrowsAsync<AIModelDependencyException>(
                        retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelName),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnRetrieveAIModelByNameIfNotFoundOccurredAsync()
        {
            // given
            string someAIModelName = CreateRandomString();

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException();

            var notFoundAIModelException =
                new NotFoundAIModelException(
                    message: "AI Model not found.",
                        innerException: httpResponseNotFoundException);

            var expectedAIModelDependencyValidationException =
                new AIModelDependencyValidationException(
                    message: "AI Model dependency validation error occurred, fix errors and try again.",
                        innerException: notFoundAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAIModelByIdAsync(someAIModelName))
                    .ThrowsAsync(httpResponseNotFoundException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(someAIModelName);

            AIModelDependencyValidationException
                actualAIModelDependencyValidationException =
                    await Assert.ThrowsAsync<AIModelDependencyValidationException>(
                        retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyValidationException.Should().BeEquivalentTo(
                expectedAIModelDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelName),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnRetrieveAIModelByNameIfBadRequestOccurredAsync()
        {
            // given
            string someAIModelName = CreateRandomString();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidAIModelException =
                new InvalidAIModelException(
                    message: "AI Model is invalid.",
                        innerException: httpResponseBadRequestException);

            var expectedAIModelDependencyValidationException =
                new AIModelDependencyValidationException(
                    message: "AI Model dependency validation error occurred, fix errors and try again.",
                        innerException: invalidAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAIModelByIdAsync(someAIModelName))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(aiModelName: someAIModelName);

            AIModelDependencyValidationException
                actualAIModelDependencyValidationException =
                    await Assert.ThrowsAsync<AIModelDependencyValidationException>(
                        retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyValidationException.Should().BeEquivalentTo(
                expectedAIModelDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelName),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnRetrieveAIModelByNameIfTooManyRequestsOccurredAsync()
        {
            // given
            string someAIModelName = CreateRandomString();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallAIModelException =
                new ExcessiveCallAIModelException(
                    message: "Excessive call error occurred, limit your calls.",
                        innerException: httpResponseTooManyRequestsException);

            var expectedAIModelDependencyValidationException =
                new AIModelDependencyValidationException(
                    message: "AI Model dependency validation error occurred, fix errors and try again.",
                        innerException: excessiveCallAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                 broker.GetAIModelByIdAsync(someAIModelName))
                     .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(someAIModelName);

            AIModelDependencyValidationException actualAIModelDependencyValidationException =
                await Assert.ThrowsAsync<AIModelDependencyValidationException>(
                    retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyValidationException.Should().BeEquivalentTo(
                expectedAIModelDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelName),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyExceptionOnRetrieveAIModelByNameIfHttpResponseErrorOccurredAsync()
        {
            // given
            string someAIModelName = CreateRandomString();

            var httpResponseException =
                new HttpResponseException();

            var failedServerAIModelException =
                new FailedServerAIModelException(
                    message: "Failed AI Model server error occurred, contact support",
                        innerException: httpResponseException);

            var expectedAIModelDependencyException =
                createAIModelDependencyException(
                        innerException: failedServerAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                 broker.GetAIModelByIdAsync(someAIModelName))
                     .ThrowsAsync(httpResponseException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(someAIModelName);

            AIModelDependencyException actualAIModelDependencyException =
                await Assert.ThrowsAsync<AIModelDependencyException>(
                    retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelName),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowServiceExceptionOnRetrieveAIModelByNameIfServiceErrorOccurredAsync()
        {
            // given
            string someAIModelName = CreateRandomString();
            var serviceException = new Exception();

            var failedAIModelServiceException =
                new FailedAIModelServiceException(
                    message: "Failed AI Model Service Exception occurred, please contact support for assistance.",
                        innerException: serviceException);

            var expectedAIModelServiceException =
                new AIModelServiceException(
                    message: "AI Model service error occurred, contact support.",
                        innerException: failedAIModelServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAIModelByIdAsync(someAIModelName))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(someAIModelName);

            AIModelServiceException actualAIModelServiceException =
                await Assert.ThrowsAsync<AIModelServiceException>(
                    retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelServiceException.Should().BeEquivalentTo(
                expectedAIModelServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelName),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}