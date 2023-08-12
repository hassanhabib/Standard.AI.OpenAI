// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Completions
{
    public partial class CompletionServiceTests
    {
        [Fact]
        private async Task ShouldThrowValidationExceptionOnPromptIfCompletionIsNullAsync()
        {
            // given
            Completion nullCompletion = null;
            var nullCompletionException = new NullCompletionException();

            var exceptedCompletionValidationException =
                new CompletionValidationException(
                    message: "Completion validation error occurred, fix errors and try again.",
                        innerException: nullCompletionException);

            // when
            ValueTask<Completion> promptCompletionTask =
                this.completionService.PromptCompletionAsync(nullCompletion);

            CompletionValidationException actualCompletionValidationException =
                await Assert.ThrowsAsync<CompletionValidationException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionValidationException.Should()
                .BeEquivalentTo(exceptedCompletionValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowValidationExceptionOnPromptIfRequestIsNullAsync()
        {
            // given
            var invalidCompletion = new Completion();
            invalidCompletion.Request = null;

            var invalidCompletionException =
                new InvalidCompletionException();

            invalidCompletionException.AddData(
                key: nameof(Completion.Request),
                values: "Value is required");

            var expectedCompletionValidationException =
                new CompletionValidationException(
                    message: "Completion validation error occurred, fix errors and try again.",
                        innerException: invalidCompletionException);

            // when
            ValueTask<Completion> promptCompletionTask =
                this.completionService.PromptCompletionAsync(invalidCompletion);

            CompletionValidationException actualCompletionValidationException =
                await Assert.ThrowsAsync<CompletionValidationException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionValidationException.Should()
                .BeEquivalentTo(expectedCompletionValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        private async Task ShouldThrowValidationExceptionOnPromptIfCompletionIsInvalidAsync(string invalidText)
        {
            // given
            var completion = new Completion
            {
                Request = new CompletionRequest
                {
                    Model = invalidText
                }
            };

            var invalidCompletionException = new InvalidCompletionException();

            invalidCompletionException.AddData(
                key: nameof(CompletionRequest.Model),
                values: "Value is required");

            invalidCompletionException.AddData(
                key: nameof(CompletionRequest.Prompts),
                values: "Value is required");

            var expectedCompletionValidationException =
                new CompletionValidationException(
                    message: "Completion validation error occurred, fix errors and try again.",
                        innerException: invalidCompletionException);

            // when
            ValueTask<Completion> promptCompletionTask =
                this.completionService.PromptCompletionAsync(completion);

            CompletionValidationException actualCompletionValidationException =
                await Assert.ThrowsAsync<CompletionValidationException>(promptCompletionTask.AsTask);

            // then
            actualCompletionValidationException.Should().BeEquivalentTo(
                expectedCompletionValidationException);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowValidationExceptionOnPromptIfPromptIsEmptyAsync()
        {
            // given
            var completion = new Completion
            {
                Request = new CompletionRequest
                {
                    Model = GetRandomString(),
                    Prompts = Array.Empty<string>()
                }
            };

            var invalidCompletionException = new InvalidCompletionException();

            invalidCompletionException.AddData(
                key: nameof(CompletionRequest.Prompts),
                values: "Value is required");

            var expectedCompletionValidationException =
                new CompletionValidationException(
                    message: "Completion validation error occurred, fix errors and try again.",
                        innerException: invalidCompletionException);

            // when
            ValueTask<Completion> promptCompletionTask =
                this.completionService.PromptCompletionAsync(completion);

            CompletionValidationException actualCompletionValidationException =
                await Assert.ThrowsAsync<CompletionValidationException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionValidationException.Should().BeEquivalentTo(
                expectedCompletionValidationException);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}