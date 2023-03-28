// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;

namespace Standard.AI.OpenAI.Services.Foundations.ImageGenerations
{
    internal class ImageGenerationService : IImageGenerationService
    {
        private readonly IOpenAIBroker openAIBroker;

        public ImageGenerationService(IOpenAIBroker openAIBroker) =>
            this.openAIBroker = openAIBroker;

        public async ValueTask<ImageGeneration> GenerateImageAsync(ImageGeneration imageGeneration)
        {
            ExternalImageGenerationResponse externalImageGenerationResponse =
                await PostImageGenerationRequestAsync(imageGeneration);

            return ConvertToImageGeneration(imageGeneration, externalImageGenerationResponse);
        }

        private async ValueTask<ExternalImageGenerationResponse> PostImageGenerationRequestAsync(
            ImageGeneration imageGeneration)
        {
            ExternalImageGenerationRequest externalImageGenerationRequest =
                ConvertToImageGenerationRequest(imageGeneration);

            ExternalImageGenerationResponse externalImageGenerationResponse =
                await this.openAIBroker.PostImageGenerationRequestAsync(externalImageGenerationRequest);

            return externalImageGenerationResponse;
        }

        private static ExternalImageGenerationRequest ConvertToImageGenerationRequest(ImageGeneration imageGeneration)
        {
            return new ExternalImageGenerationRequest
            {
                Prompt = imageGeneration.Request.Prompt,
                ImagesToGenerate = imageGeneration.Request.ImagesToGenerate,
                ImageSize = imageGeneration.Request.ImageSize,
                ResponseFormat = imageGeneration.Request.ResponseFormat,
                User = imageGeneration.Request.User
            };
        }

        private static ImageGeneration ConvertToImageGeneration(
            ImageGeneration imageGeneration,
            ExternalImageGenerationResponse externalImageGenerationResponse)
        {
            imageGeneration.Response = new ImageGenerationResponse
            {
                Created = externalImageGenerationResponse.Created,

                Results = externalImageGenerationResponse.Results.Select(result =>
                {
                    return new ImageGenerationResult
                    {
                        ImageUrl = result.ImageUrl,
                        Base64EncodedJsonImage = result.Base64EncodedJsonImage
                    };
                }).ToArray()
            };

            return imageGeneration;
        }
    }
}