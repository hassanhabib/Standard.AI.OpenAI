// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Completions
{
    public partial class CompletionServiceTests
    {
        [Fact]
        private async Task ShouldThrowDependencyExceptionOnPromptIfUrlNotFoundErrorOccursAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationCompletionException =
                new InvalidConfigurationCompletionException(
                    message: "Invalid configuration error occurred, fix errors and try again.",
                        innerException: httpResponseUrlNotFoundException);

            var expectedCompletionDependencyException =
                new CompletionDependencyException(
                    message: "Completion dependency error occurred, contact support.",
                        innerException: invalidConfigurationCompletionException);

            this.openAIBrokerMock.Setup(broker => broker.PostCompletionRequestAsync(
                It.IsAny<ExternalCompletionRequest>()))
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<Completion> promptCompletionTask =
               this.completionService.PromptCompletionAsync(someCompletion);

            CompletionDependencyException actualCompletionException =
                await Assert.ThrowsAsync<CompletionDependencyException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionException.Should().BeEquivalentTo(
                expectedCompletionDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        private async Task ShouldThrowDependencyExceptionOnPromptIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var unauthorizedCompletionException =
                new UnauthorizedCompletionException(
                    message: "Unauthorized completion request, fix errors and try again.",
                        innerException: unauthorizedException);

            var expectedCompletionDependencyException =
                new CompletionDependencyException(
                    message: "Completion dependency error occurred, contact support.",
                        innerException: unauthorizedCompletionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()))
                        .ThrowsAsync(unauthorizedException);

            // when
            ValueTask<Completion> promptCompletionTask =
                this.completionService.PromptCompletionAsync(someCompletion);

            CompletionDependencyException actualCompletionDependencyException =
                await Assert.ThrowsAsync<CompletionDependencyException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionDependencyException.Should().BeEquivalentTo(
                expectedCompletionDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnPromptIfCompletionNotFoundAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException();

            var notFoundCompletionException =
                new NotFoundCompletionException(
                    message: "Not found completion error occurred, fix errors and try again.",
                        innerException: httpResponseNotFoundException);

            var expectedCompletionDependencyValidationException =
                new CompletionDependencyValidationException(
                    message: "Completion dependency validation error occurred, fix errors and try again.",
                        innerException: notFoundCompletionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()))
                        .ThrowsAsync(httpResponseNotFoundException);

            // when
            ValueTask<Completion> promptCompletionTask =
                this.completionService.PromptCompletionAsync(someCompletion);

            CompletionDependencyValidationException actualCompletionDependencyException =
                await Assert.ThrowsAsync<CompletionDependencyValidationException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionDependencyException.Should().BeEquivalentTo(
                expectedCompletionDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnPromptIfBadRequestOccursAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidCompletionException =
                new InvalidCompletionException(
                    message: "Invalid completion error occurred, fix errors and try again.",
                        innerException: httpResponseBadRequestException);

            var expectedCompletionDependencyValidationException =
                new CompletionDependencyValidationException(
                    message: "Completion dependency validation error occurred, fix errors and try again.",
                        innerException: invalidCompletionException);

            this.openAIBrokerMock.Setup(broker => broker.PostCompletionRequestAsync(
                It.IsAny<ExternalCompletionRequest>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<Completion> promptCompletionTask =
               this.completionService.PromptCompletionAsync(someCompletion);

            CompletionDependencyValidationException actualCompletionDependencyException =
                await Assert.ThrowsAsync<CompletionDependencyValidationException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionDependencyException.Should().BeEquivalentTo(
                expectedCompletionDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnPromptIfTooManyRequestsOccurredAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallCompletionException =
                new ExcessiveCallCompletionException(
                    message: "Excessive call error occurred, limit your calls.",
                        innerException: httpResponseTooManyRequestsException);

            var expectedCompletionDependencyValidationException =
                new CompletionDependencyValidationException(
                    message: "Completion dependency validation error occurred, fix errors and try again.",
                        innerException: excessiveCallCompletionException);

            this.openAIBrokerMock.Setup(broker => broker.PostCompletionRequestAsync(
                It.IsAny<ExternalCompletionRequest>()))
                    .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<Completion> promptCompletionTask =
               this.completionService.PromptCompletionAsync(someCompletion);

            CompletionDependencyValidationException actualCompletionDependencyValidationException =
                await Assert.ThrowsAsync<CompletionDependencyValidationException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionDependencyValidationException.Should().BeEquivalentTo(
                expectedCompletionDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyExceptionOnPromptIfHttpErrorOccursAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var httpResponseException =
                new HttpResponseException();

            var failedServerCompletionException =
                new FailedServerCompletionException(
                    message: "Failed server completion error occurred, contact support.",
                        innerException: httpResponseException);

            var expectedCompletionDependencyException =
                new CompletionDependencyException(
                    message: "Completion dependency error occurred, contact support.",
                        innerException: failedServerCompletionException);

            this.openAIBrokerMock.Setup(broker => broker.PostCompletionRequestAsync(
                It.IsAny<ExternalCompletionRequest>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<Completion> promptCompletionTask =
               this.completionService.PromptCompletionAsync(someCompletion);

            CompletionDependencyException actualCompletionException =
                await Assert.ThrowsAsync<CompletionDependencyException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionException.Should().BeEquivalentTo(
                expectedCompletionDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowServiceExceptionOnPromptIfServiceErrorOccursAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();
            var serviceException = new Exception();

            var failedCompletionServiceException =
                new FailedCompletionServiceException(
                    message: "Failed completion error occurred, contact support.",
                        innerException: serviceException);

            var expectedCompletionServiceException =
                new CompletionServiceException(
                    message: "Completion service error occurred, contact support.",
                        innerException: failedCompletionServiceException);

            this.openAIBrokerMock.Setup(broker => broker.PostCompletionRequestAsync(
                It.IsAny<ExternalCompletionRequest>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Completion> promptCompletionTask =
               this.completionService.PromptCompletionAsync(someCompletion);

            CompletionServiceException actualCompletionServiceException =
                await Assert.ThrowsAsync<CompletionServiceException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionServiceException.Should().BeEquivalentTo(
                expectedCompletionServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}