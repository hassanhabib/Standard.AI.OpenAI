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
        public async Task ShouldThrowDependencyExceptionOnRetrieveIfUrlNotFoundAsync()
        {
            // given
            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationAIModelException =
                new InvalidConfigurationAIModelException(
                    httpResponseUrlNotFoundException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(
                    invalidConfigurationAIModelException);

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            var unauthorizedAIModelException =
                new UnauthorizedAIModelException(unauthorizedException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(unauthorizedAIModelException);

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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveIfTooManyRequestsOccurredAsync()
        {
            // given
            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallAIModelException =
                new ExcessiveCallAIModelException(
                    httpResponseTooManyRequestsException);

            var expectedAIModelDependencyValidationException =
                new AIModelDependencyValidationException(
                    excessiveCallAIModelException);

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveIfServerErrorOccurredAsync()
        {
            // given
            var httpResponseException =
                new HttpResponseException();

            var failedServerAIModelException =
                new FailedServerAIModelException(
                    httpResponseException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(
                    failedServerAIModelException);

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
        public async Task ShouldThrowServiceExceptionOnRetrieveAllModelsIfServiceErrorOccurredAsync()
        {
            // given
            var serviceException = new Exception();

            var failedAIModelServiceException =
                new FailedAIModelServiceException(serviceException);

            var expectedAIModelServiceException =
                new AIModelServiceException(failedAIModelServiceException);

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
