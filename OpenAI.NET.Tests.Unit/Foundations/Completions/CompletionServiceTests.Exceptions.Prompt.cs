// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using OpenAI.NET.Models.Completions;
using OpenAI.NET.Models.Completions.Exceptions;
using OpenAI.NET.Models.ExternalCompletions;
using RESTFulSense.Exceptions;
using Xunit;

namespace OpenAI.NET.Tests.Unit.Foundations.Completions
{
    public partial class CompletionServiceTests
    {
        [Theory]
        [MemberData(nameof(UnAuthorizationExceptions))]
        public async Task ShouldThrowDependencyExceptionOnPromptIfUnAuthorizedAsync(
            HttpResponseException unAuthorizationException)
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var unauthorizedCompletionException =
                new UnauthorizedCompletionException(
                    unAuthorizationException);

            var expectedCompletionDependencyException =
                new CompletionDependencyException(
                    unauthorizedCompletionException);

            this.openAiBrokerMock.Setup(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()))
                        .ThrowsAsync(unAuthorizationException);

            // when
            ValueTask<Completion> promptCompletionTask =
                this.completionService.PromptCompletionAsync(someCompletion);

            CompletionDependencyException actualCompletionDependencyException =
                await Assert.ThrowsAsync<CompletionDependencyException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionDependencyException.Should().BeEquivalentTo(
                expectedCompletionDependencyException);

            this.openAiBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnPromptIfCompletionNotFoundAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException();

            var notFoundCompletionException =
                new NotFoundCompletionException(
                    httpResponseNotFoundException);

            var expectedCompletionDependencyValidationException =
                new CompletionDependencyValidationException(
                    notFoundCompletionException);

            this.openAiBrokerMock.Setup(broker =>
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

            this.openAiBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnPromptIfBadRequestOccursAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidCompletionException =
                new InvalidCompletionException(httpResponseBadRequestException);

            var expectedCompletionDependencyValidationException =
                new CompletionDependencyValidationException(invalidCompletionException);

            this.openAiBrokerMock.Setup(broker => broker.PostCompletionRequestAsync(
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

            this.openAiBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnPromptIfUrlNotFoundErrorOccursAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationCompletionException =
                new InvalidConfigurationCompletionException(
                    httpResponseUrlNotFoundException);

            var expectedCompletionDependencyException =
                new CompletionDependencyException(
                    invalidConfigurationCompletionException);

            this.openAiBrokerMock.Setup(broker => broker.PostCompletionRequestAsync(
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

            this.openAiBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnPromptIfTooManyRequestsOccurredAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallCompletionException =
                new ExcessiveCallCompletionException(
                    httpResponseTooManyRequestsException);

            var expectedCompletionDependencyValidationException =
                new CompletionDependencyValidationException(
                    excessiveCallCompletionException);

            this.openAiBrokerMock.Setup(broker => broker.PostCompletionRequestAsync(
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

            this.openAiBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnPromptIfHttpErrorOccursAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var httpResponseException =
                new HttpResponseException();

            var failedServerCompletionException =
                new FailedServerCompletionException(httpResponseException);

            var expectedCompletionDependencyException =
                new CompletionDependencyException(
                    failedServerCompletionException);

            this.openAiBrokerMock.Setup(broker => broker.PostCompletionRequestAsync(
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

            this.openAiBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Once);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
