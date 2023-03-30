// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.ImageGenerations
{
    public partial class ImageGenerationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnGenerateIfImageGenerationIsNullAsync()
        {
            // given
            ImageGeneration nullImageGeneration = null;

            var nullImageGenerationException =
                new NullImageGenerationException();

            var expectedImageGenerationValidationException =
                new ImageGenerationValidationException(nullImageGenerationException);

            // when
            ValueTask<ImageGeneration> generateImageTask =
                this.imageGenerationService.GenerateImageAsync(
                    nullImageGeneration);

            ImageGenerationValidationException actualImageGenerationValidationException =
                await Assert.ThrowsAsync<ImageGenerationValidationException>(
                    generateImageTask.AsTask);

            // then
            actualImageGenerationValidationException.Should().BeEquivalentTo(
                expectedImageGenerationValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnGenerateIfRequestIsNullAsync()
        {
            // given
            var invalidImageGeneration = new ImageGeneration();
            invalidImageGeneration.Request = null;

            var invalidImageGenerationException =
                new InvalidImageGenerationException();

            invalidImageGenerationException.AddData(
                key: nameof(ImageGeneration.Request),
                values: "Value is required");

            var expectedImageGenerationValidationException =
                new ImageGenerationValidationException(invalidImageGenerationException);

            // when
            ValueTask<ImageGeneration> generateImageTask =
                this.imageGenerationService.GenerateImageAsync(invalidImageGeneration);

            ImageGenerationValidationException actualImageGenerationValidationException =
                await Assert.ThrowsAsync<ImageGenerationValidationException>(
                    generateImageTask.AsTask);

            // then
            actualImageGenerationValidationException.Should().BeEquivalentTo(
                expectedImageGenerationValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnGenerateIfRequestIsInvalidAsync(string invalidText)
        {
            // given
            var invalidImageGeneration = new ImageGeneration
            {
                Request = new ImageGenerationRequest
                {
                    Prompt = invalidText
                }
            };

            var invalidImageGenerationException =
                new InvalidImageGenerationException();

            invalidImageGenerationException.AddData(
                key: nameof(ImageGeneration.Request.Prompt),
                    values: "Value is required");

            var expectedImageGenerationValidationException =
                new ImageGenerationValidationException(invalidImageGenerationException);

            // when
            ValueTask<ImageGeneration> generateImageTask =
                this.imageGenerationService.GenerateImageAsync(invalidImageGeneration);

            ImageGenerationValidationException actualImageGenerationValidationException =
                await Assert.ThrowsAsync<ImageGenerationValidationException>(
                    generateImageTask.AsTask);

            // then
            actualImageGenerationValidationException.Should().BeEquivalentTo(
                expectedImageGenerationValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}