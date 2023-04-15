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
        public async Task ShouldThrowDependencyExceptionOnRetrieveAIModelByNameIfUrlNotFoundAsync()
        {
            // given
            string someAIModelId = CreateRandomString();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationAIModelException =
                new InvalidConfigurationAIModelException(
                    httpResponseUrlNotFoundException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(
                    invalidConfigurationAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAIModelByIdAsync(someAIModelId))
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(aiModelName: someAIModelId);

            AIModelDependencyException
                actualAIModelDependencyException =
                    await Assert.ThrowsAsync<AIModelDependencyException>(
                        retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAIModelByNameIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            string someAIModelId = CreateRandomString();

            var unauthorizedAIModelException =
                new UnauthorizedAIModelException(unauthorizedException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(unauthorizedAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                 broker.GetAIModelByIdAsync(someAIModelId))
                     .ThrowsAsync(unauthorizedException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(aiModelName: someAIModelId);

            AIModelDependencyException
                actualAIModelDependencyException =
                    await Assert.ThrowsAsync<AIModelDependencyException>(
                        retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAIModelByNameIfNotFoundOccurredAsync()
        {
            // given
            string someAIModelId = CreateRandomString();

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException();

            var notFoundAIModelException =
                new NotFoundAIModelException(
                    httpResponseNotFoundException);

            var expectedAIModelDependencyValidationException =
                new AIModelDependencyValidationException(
                    notFoundAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAIModelByIdAsync(someAIModelId))
                    .ThrowsAsync(httpResponseNotFoundException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(aiModelName: someAIModelId);

            AIModelDependencyValidationException
                actualAIModelDependencyValidationException =
                    await Assert.ThrowsAsync<AIModelDependencyValidationException>(
                        retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyValidationException.Should().BeEquivalentTo(
                expectedAIModelDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAIModelByNameIfBadRequestOccurredAsync()
        {
            // given
            string someAIModelId = CreateRandomString();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidAIModelException =
                new InvalidAIModelException(
                    httpResponseBadRequestException);

            var expectedAIModelDependencyValidationException =
                new AIModelDependencyValidationException(
                    invalidAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAIModelByIdAsync(someAIModelId))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(aiModelName: someAIModelId);

            AIModelDependencyValidationException
                actualAIModelDependencyValidationException =
                    await Assert.ThrowsAsync<AIModelDependencyValidationException>(
                        retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyValidationException.Should().BeEquivalentTo(
                expectedAIModelDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAIModelByNameIfTooManyRequestsOccurredAsync()
        {
            // given
            string someAIModelId = CreateRandomString();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallAIModelException =
                new ExcessiveCallAIModelException(
                    httpResponseTooManyRequestsException);

            var expectedAIModelDependencyValidationException =
                new AIModelDependencyValidationException(
                    excessiveCallAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                 broker.GetAIModelByIdAsync(someAIModelId))
                     .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(aiModelName: someAIModelId);

            AIModelDependencyValidationException actualAIModelDependencyValidationException =
                await Assert.ThrowsAsync<AIModelDependencyValidationException>(
                    retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyValidationException.Should().BeEquivalentTo(
                expectedAIModelDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAIModelByNameIfServerErrorOccurredAsync()
        {
            // given
            string someAIModelId = CreateRandomString();

            var httpResponseException =
                new HttpResponseException();

            var failedServerAIModelException =
                new FailedServerAIModelException(
                    httpResponseException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(
                    failedServerAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                 broker.GetAIModelByIdAsync(someAIModelId))
                     .ThrowsAsync(httpResponseException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(aiModelName: someAIModelId);

            AIModelDependencyException actualAIModelDependencyException =
                await Assert.ThrowsAsync<AIModelDependencyException>(
                    retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAIModelByNameIfServiceErrorOccurredAsync()
        {
            // given
            string someAIModelId = CreateRandomString();
            var serviceException = new Exception();

            var failedAIModelServiceException =
                new FailedAIModelServiceException(serviceException);

            var expectedAIModelServiceException =
                new AIModelServiceException(failedAIModelServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAIModelByIdAsync(someAIModelId))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
               this.aiModelService.RetrieveAIModelByNameAsync(aiModelName: someAIModelId);

            AIModelServiceException actualAIModelServiceException =
                await Assert.ThrowsAsync<AIModelServiceException>(
                    retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelServiceException.Should().BeEquivalentTo(
                expectedAIModelServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(someAIModelId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}