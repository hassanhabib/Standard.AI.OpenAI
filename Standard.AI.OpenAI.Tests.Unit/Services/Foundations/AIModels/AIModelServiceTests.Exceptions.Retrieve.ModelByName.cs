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
        public async Task ShouldThrowDependencyExceptionOnRetrieveAIModelByIdIfUrlNotFoundAsync()
        {
            // given
            var someAiModelId = CreateRandomString();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationAIModelException =
                new InvalidConfigurationAIModelException(
                    httpResponseUrlNotFoundException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(
                    invalidConfigurationAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAIModelByIdAsync(someAiModelId))
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<AIModel> retrieveAIModelByIdTask =
               this.aiModelService.RetrieveAIModelByIdAsync(aiModelName: someAiModelId);

            AIModelDependencyException
                actualAIModelDependencyException =
                    await Assert.ThrowsAsync<AIModelDependencyException>(
                        retrieveAIModelByIdTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAiModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAIModelByIdIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            var someAiModelId = CreateRandomString();

            var unauthorizedAIModelException =
                new UnauthorizedAIModelException(unauthorizedException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(unauthorizedAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                 broker.GetAIModelByIdAsync(someAiModelId))
                     .ThrowsAsync(unauthorizedException);

            // when
            ValueTask<AIModel> retrieveAIModelByIdTask =
               this.aiModelService.RetrieveAIModelByIdAsync(aiModelName: someAiModelId);

            AIModelDependencyException
                actualAIModelDependencyException =
                    await Assert.ThrowsAsync<AIModelDependencyException>(
                        retrieveAIModelByIdTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAiModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAIModelByIdIfTooManyRequestsOccurredAsync()
        {
            // given
            var someAiModelId = CreateRandomString();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallAIModelException =
                new ExcessiveCallAIModelException(
                    httpResponseTooManyRequestsException);

            var expectedAIModelDependencyValidationException =
                new AIModelDependencyValidationException(
                    excessiveCallAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                 broker.GetAIModelByIdAsync(someAiModelId))
                     .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<AIModel> retrieveAIModelByIdTask =
               this.aiModelService.RetrieveAIModelByIdAsync(aiModelName: someAiModelId);

            AIModelDependencyValidationException actualAIModelDependencyValidationException =
                await Assert.ThrowsAsync<AIModelDependencyValidationException>(
                    retrieveAIModelByIdTask.AsTask);

            // then
            actualAIModelDependencyValidationException.Should().BeEquivalentTo(
                expectedAIModelDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAiModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAIModelByIdIfServerErrorOccurredAsync()
        {
            // given
            var someAiModelId = CreateRandomString();

            var httpResponseException =
                new HttpResponseException();

            var failedServerAIModelException =
                new FailedServerAIModelException(
                    httpResponseException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(
                    failedServerAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                 broker.GetAIModelByIdAsync(someAiModelId))
                     .ThrowsAsync(httpResponseException);

            // when
            ValueTask<AIModel> retrieveAIModelByIdTask =
               this.aiModelService.RetrieveAIModelByIdAsync(aiModelName: someAiModelId);

            AIModelDependencyException actualAIModelDependencyException =
                await Assert.ThrowsAsync<AIModelDependencyException>(
                    retrieveAIModelByIdTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAiModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAIModelByIdIfServiceErrorOccurredAsync()
        {
            // given
            var someAiModelId = CreateRandomString();
            var serviceException = new Exception();

            var failedAIModelServiceException =
                new FailedAIModelServiceException(serviceException);

            var expectedAIModelServiceException =
                new AIModelServiceException(failedAIModelServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAIModelByIdAsync(someAiModelId))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<AIModel> retrieveAIModelByIdTask =
               this.aiModelService.RetrieveAIModelByIdAsync(aiModelName: someAiModelId);

            AIModelServiceException actualAIModelServiceException =
                await Assert.ThrowsAsync<AIModelServiceException>(
                    retrieveAIModelByIdTask.AsTask);

            // then
            actualAIModelServiceException.Should().BeEquivalentTo(
                expectedAIModelServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAiModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}
