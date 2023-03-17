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
    }
}
