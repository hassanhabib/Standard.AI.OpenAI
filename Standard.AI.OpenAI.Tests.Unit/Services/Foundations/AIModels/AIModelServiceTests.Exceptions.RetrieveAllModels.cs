// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
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
        private async Task ShouldThrowDependencyExceptionOnRetrieveIfUrlNotFoundAsync()
        {
            // given
            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationAIModelException =
                new InvalidConfigurationAIModelException(
                    message: "Invalid AI Model configuration error occurred, contact support.",
                        innerException: httpResponseUrlNotFoundException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(
                    message: "AI Model dependency error occurred, contact support.",
                        innerException: invalidConfigurationAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllAIModelsAsync())
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<IEnumerable<AIModel>> getAllAIModelsTask =
               this.aiModelService.RetrieveAllAIModelsAsync();

            AIModelDependencyException
                actualAIModelDependencyException =
                    await Assert.ThrowsAsync<AIModelDependencyException>(
                        getAllAIModelsTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllAIModelsAsync(),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        private async Task ShouldThrowDependencyExceptionOnRetrieveIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            var unauthorizedAIModelException =
                new UnauthorizedAIModelException(
                    message: "Unauthorized AI Model error occurred, fix errors and try again.",
                        innerException: unauthorizedException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(
                    message: "AI Model dependency error occurred, contact support.",
                        innerException: unauthorizedAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllAIModelsAsync())
                    .ThrowsAsync(unauthorizedException);

            // when
            ValueTask<IEnumerable<AIModel>> getAllAIModelsTask =
               this.aiModelService.RetrieveAllAIModelsAsync();

            AIModelDependencyException actualAIModelDependencyException =
                await Assert.ThrowsAsync<AIModelDependencyException>(
                    getAllAIModelsTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllAIModelsAsync(),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnRetrieveIfTooManyRequestsOccurredAsync()
        {
            // given
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
                broker.GetAllAIModelsAsync())
                    .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<IEnumerable<AIModel>> retrieveAllAIModelsTask =
                this.aiModelService.RetrieveAllAIModelsAsync();

            AIModelDependencyValidationException actualAIModelDependencyValidationException =
                await Assert.ThrowsAsync<AIModelDependencyValidationException>(
                    retrieveAllAIModelsTask.AsTask);

            // then
            actualAIModelDependencyValidationException.Should().BeEquivalentTo(
                expectedAIModelDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllAIModelsAsync(),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyExceptionOnRetrieveIfServerErrorOccurredAsync()
        {
            // given
            var httpResponseException =
                new HttpResponseException();

            var failedServerAIModelException =
                new FailedServerAIModelException(
                    message: "Failed AI Model server error occurred, contact support",
                        innerException: httpResponseException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(
                    message: "AI Model dependency error occurred, contact support.",
                        innerException: failedServerAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllAIModelsAsync())
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<IEnumerable<AIModel>> retrieveAllAIModelsTask =
                this.aiModelService.RetrieveAllAIModelsAsync();

            AIModelDependencyException actualAIModelDependencyException =
                await Assert.ThrowsAsync<AIModelDependencyException>(
                    retrieveAllAIModelsTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllAIModelsAsync(),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowServiceExceptionOnRetrieveAllModelsIfServiceErrorOccurredAsync()
        {
            // given
            var serviceException = new Exception();

            var failedAIModelServiceException =
                new FailedAIModelServiceException(
                    message: "Failed AI Model Service Exception occurred, please contact support for assistance.",
                        innerException: serviceException);

            var expectedAIModelServiceException =
                createAIModelServiceException(
                        innerException: failedAIModelServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllAIModelsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<IEnumerable<AIModel>> retrieveAllAIModelsTask =
                this.aiModelService.RetrieveAllAIModelsAsync();

            AIModelServiceException actualAIModelServiceException =
                await Assert.ThrowsAsync<AIModelServiceException>(
                    retrieveAllAIModelsTask.AsTask);

            // then
            actualAIModelServiceException.Should().BeEquivalentTo(
                expectedAIModelServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllAIModelsAsync(),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}