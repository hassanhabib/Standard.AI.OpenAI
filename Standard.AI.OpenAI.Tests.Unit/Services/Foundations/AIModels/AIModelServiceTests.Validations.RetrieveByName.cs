// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIModels
{
    public partial class AIModelServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        private async Task ShouldThrowValidationExceptionOnRetrieveModelByNameIfModelNameIsInvalidAsync(
            string invalidAIModelName)
        {
            // given
            var invalidAIModelException =
                new InvalidAIModelException(message: "AI Model is invalid.");

            invalidAIModelException.AddData(
                key: nameof(AIModel.Name),
                values: "Value is required");

            var expectedAIModelValidationException =
                new AIModelValidationException(
                    message: "AI Model validation error occurred, fix errors and try again.",
                        innerException: invalidAIModelException);

            // when
            ValueTask<AIModel> retrieveAIModelByNameTask =
                this.aiModelService.RetrieveAIModelByNameAsync(invalidAIModelName);

            AIModelValidationException actualAIModelValidationException =
                await Assert.ThrowsAsync<AIModelValidationException>(
                    retrieveAIModelByNameTask.AsTask);

            // then
            actualAIModelValidationException.Should()
                .BeEquivalentTo(expectedAIModelValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(
                    It.IsAny<string>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}