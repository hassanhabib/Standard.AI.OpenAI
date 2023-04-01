// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;

namespace Standard.AI.OpenAI.Services.Foundations.ImageGenerations
{
    internal partial class ImageGenerationService : IImageGenerationService
    {
        private readonly IOpenAIBroker openAIBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public ImageGenerationService(IOpenAIBroker openAIBroker, IDateTimeBroker dateTimeBroker)
        {
            this.openAIBroker = openAIBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<ImageGeneration> GenerateImageAsync(ImageGeneration imageGeneration) =>
        TryCatch(async () =>
        {
            ValidateImageGenerationOnGenerate(imageGeneration);

            ExternalImageGenerationResponse externalImageGenerationResponse =
                await PostImageGenerationRequestAsync(imageGeneration);

            return ConvertToImageGeneration(imageGeneration, externalImageGenerationResponse);
        });

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

        private ImageGeneration ConvertToImageGeneration(
            ImageGeneration imageGeneration,
            ExternalImageGenerationResponse externalImageGenerationResponse)
        {
            imageGeneration.Response = new ImageGenerationResponse
            {
                Created = this.dateTimeBroker.ConvertToDateTimeOffSet(externalImageGenerationResponse.Created),

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