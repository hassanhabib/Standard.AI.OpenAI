// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.ImageGenerations
{
    public partial class ImageGenerationServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnGenerateIfUrlNotFoundAsync()
        {
            // given
            ImageGeneration someImageGeneration = CreateRandomImageGeneration();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationImageGenerationException =
                new InvalidConfigurationImageGenerationException(
                    httpResponseUrlNotFoundException);

            var expectedImageGenerationDependencyException =
                new ImageGenerationDependencyException(
                    invalidConfigurationImageGenerationException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()))
                        .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<ImageGeneration> generateImageTask =
                this.imageGenerationService.GenerateImageAsync(someImageGeneration);

            ImageGenerationDependencyException actualImageGenerationDependencyException =
                await Assert.ThrowsAsync<ImageGenerationDependencyException>(
                    generateImageTask.AsTask);

            // then
            actualImageGenerationDependencyException.Should().BeEquivalentTo(
                expectedImageGenerationDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}