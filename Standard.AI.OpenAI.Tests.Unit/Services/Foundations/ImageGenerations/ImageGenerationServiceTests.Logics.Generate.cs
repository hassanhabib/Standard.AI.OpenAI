// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.ImageGenerations
{
    public partial class ImageGenerationServiceTests
    {
        [Fact]
        public async Task ShouldGenerateImageAsync()
        {
            // given
            dynamic randomImageGenerationProperties = CreateRandomImageGenerationProperties();

            var randomImageGenerationRequest = new ImageGenerationRequest
            {
                Prompt = randomImageGenerationProperties.Prompt,
                ImagesToGenerate = randomImageGenerationProperties.ImagesToGenerate,
                ImageSize = randomImageGenerationProperties.ImageSize,
                ResponseFormat = randomImageGenerationProperties.ResponseFormat,
                User = randomImageGenerationProperties.User
            };

            var randomImageGenerationResponse = new ImageGenerationResponse
            {
                Created = randomImageGenerationProperties.Created,

                Results = ((dynamic[])randomImageGenerationProperties.Results).Select(result =>
                {
                    return new ImageGenerationResult
                    {
                        ImageUrl = result.ImageUrl,
                        Base64EncodedJsonImage = result.Base64EncodedJsonImage
                    };
                }).ToArray()
            };

            var randomImageGeneration = new ImageGeneration
            {
                Request = randomImageGenerationRequest
            };

            var randomExternalImageGenerationRequest = new ExternalImageGenerationRequest
            {
                Prompt = randomImageGenerationProperties.Prompt,
                ImagesToGenerate = randomImageGenerationProperties.ImagesToGenerate,
                ImageSize = randomImageGenerationProperties.ImageSize,
                ResponseFormat = randomImageGenerationProperties.ResponseFormat,
                User = randomImageGenerationProperties.User
            };

            var randomExternalImageGenerationResponse = new ExternalImageGenerationResponse
            {
                Created = randomImageGenerationProperties.Created,

                Results = ((dynamic[])randomImageGenerationProperties.Results).Select(result =>
                {
                    return new ExternalImageGenerationResult
                    {
                        ImageUrl = result.ImageUrl,
                        Base64EncodedJsonImage = result.Base64EncodedJsonImage
                    };
                }).ToArray()
            };

            ImageGeneration inputImageGeneration = randomImageGeneration;
            ImageGeneration expectedImageGeneration = inputImageGeneration.DeepClone();
            expectedImageGeneration.Response = randomImageGenerationResponse;

            ExternalImageGenerationRequest mappedExternalImageGenerationRequest =
                randomExternalImageGenerationRequest;

            ExternalImageGenerationResponse returnedExternalImageGenerationResponse =
                randomExternalImageGenerationResponse;

            this.openAIBrokerMock.Setup(broker =>
                broker.PostImageGenerationRequestAsync(It.Is(
                    SameExternalImageGenerationRequestAs(mappedExternalImageGenerationRequest))))
                        .ReturnsAsync(returnedExternalImageGenerationResponse);

            // when
            ImageGeneration actualImageGeneration =
                await this.imageGenerationService.GenerateImageAsync(inputImageGeneration);

            // then
            actualImageGeneration.Should().BeEquivalentTo(expectedImageGeneration);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(It.Is(
                    SameExternalImageGenerationRequestAs(mappedExternalImageGenerationRequest))),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}