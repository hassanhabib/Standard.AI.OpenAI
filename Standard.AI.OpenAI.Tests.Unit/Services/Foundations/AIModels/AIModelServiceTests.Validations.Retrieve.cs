using System.Threading.Tasks;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions;
using Xunit;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using FluentAssertions;
using Microsoft.Extensions.Hosting;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIModels
{
    public partial class AIModelServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveModelByNameIfModelNameIsInvalidAsync(string invalidAIModelName)
        {
            // given
            var invalidAIModelException =
                new InvalidAIModelException();

            var expectedAIModelValidationException =
                new AIModelValidationException(invalidAIModelException);

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
