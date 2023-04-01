// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
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
            DateTimeOffset randomDateTime = GetRandomDate();
            int randomDateNumber = GetRandomDateNumber();

            dynamic randomImageGenerationProperties = CreateRandomImageGenerationProperties(
                createdDate: randomDateTime,
                createdDateNumber: randomDateNumber);

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
                Created = randomImageGenerationProperties.CreatedDate,

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

            this.dateTimeBrokerMock.Setup(broker =>
                broker.ConvertToDateTimeOffSet(randomDateNumber))
                    .Returns(randomDateTime);

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

            this.dateTimeBrokerMock.Verify(broker =>
                broker.ConvertToDateTimeOffSet(randomDateNumber),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}