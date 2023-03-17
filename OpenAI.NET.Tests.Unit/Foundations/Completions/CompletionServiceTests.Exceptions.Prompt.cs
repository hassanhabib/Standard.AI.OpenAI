// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Specialized;
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
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnPromptIfUnAuthorizedAsync()
        {
            // given
            Completion someCompletion = CreateRandomCompletion();

            var httpResponseUnauthorizedException =
                new HttpResponseUnauthorizedException();

            var unauthorizedCompletionException =
                new UnauthorizedCompletionException(
                    httpResponseUnauthorizedException);

            var expectedCompletionDependencyException =
                new CompletionDependencyException(
                    unauthorizedCompletionException);

            this.openAiBrokerMock.Setup(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()))
                        .ThrowsAsync(httpResponseUnauthorizedException);

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
    }
}
