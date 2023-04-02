// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.ChatCompletions
{
    public partial class ChatCompletionServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnSendIfChatCompletionIsNullAsync()
        {
            // given
            ChatCompletion nullChatCompletion = null;

            var nullChatCompletionException =
                new NullChatCompletionException();

            var expectedChatCompletionValidationException =
                new ChatCompletionValidationException(nullChatCompletionException);

            // when
            ValueTask<ChatCompletion> sendChatCompletionTask =
                this.chatCompletionService.SendChatCompletionAsync(
                    nullChatCompletion);

            ChatCompletionValidationException actualChatCompletionValidationException =
                await Assert.ThrowsAsync<ChatCompletionValidationException>(
                    sendChatCompletionTask.AsTask);

            // then
            actualChatCompletionValidationException.Should()
                .BeEquivalentTo(expectedChatCompletionValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnSendIfRequestIsNullAsync()
        {
            // given
            var invalidChatCompletion = new ChatCompletion();
            invalidChatCompletion.Request = null;

            var invalidChatCompletionException =
                new InvalidChatCompletionException();

            invalidChatCompletionException.AddData(
                key: nameof(ChatCompletion.Request),
                values: "Value is required");

            var expectedChatCompletionValidationException =
                new ChatCompletionValidationException(invalidChatCompletionException);

            // when
            ValueTask<ChatCompletion> sendChatCompletionTask =
                this.chatCompletionService.SendChatCompletionAsync(invalidChatCompletion);

            ChatCompletionValidationException actualChatCompletionValidationException =
                await Assert.ThrowsAsync<ChatCompletionValidationException>(sendChatCompletionTask.AsTask);

            // then
            actualChatCompletionValidationException.Should()
                .BeEquivalentTo(expectedChatCompletionValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnSendIfRequestIsInvalidAsync(string invalidText)
        {
            // given
            var invalidChatCompletion = new ChatCompletion()
            {
                Request = new ChatCompletionRequest
                {
                    Model = invalidText,
                    Messages = null
                }
            };

            var invalidChatCompletionException =
                new InvalidChatCompletionException();

            invalidChatCompletionException.AddData(
                key: nameof(ChatCompletion.Request.Model),
                values: "Value is required");

            invalidChatCompletionException.AddData(
                key: nameof(ChatCompletion.Request.Messages),
                values: "Value is required");

            var expectedChatCompletionValidationException =
                new ChatCompletionValidationException(invalidChatCompletionException);

            // when
            ValueTask<ChatCompletion> sendChatCompletionTask =
                this.chatCompletionService.SendChatCompletionAsync(invalidChatCompletion);

            ChatCompletionValidationException actualChatCompletionValidationException =
                await Assert.ThrowsAsync<ChatCompletionValidationException>(sendChatCompletionTask.AsTask);

            // then
            actualChatCompletionValidationException.Should()
                .BeEquivalentTo(expectedChatCompletionValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
