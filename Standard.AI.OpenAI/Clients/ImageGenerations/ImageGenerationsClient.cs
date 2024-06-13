// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.ImageGenerations.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions;
using Standard.AI.OpenAI.Services.Foundations.ImageGenerations;
using Xeptions;

namespace Standard.AI.OpenAI.Clients.ImageGenerations
{
    internal class ImageGenerationsClient : IImageGenerationsClient
    {
        private readonly IImageGenerationService imageGenerationService;

        public ImageGenerationsClient(IImageGenerationService imageGenerationService) =>
            this.imageGenerationService = imageGenerationService;

        public async ValueTask<ImageGeneration> GenerateImageAsync(ImageGeneration imageGeneration)
        {
            try
            {
                return await this.imageGenerationService.GenerateImageAsync(imageGeneration);
            }
            catch (ImageGenerationValidationException imageGenerationValidationException)
            {
                throw CreateImageGenerationClientValidationException(
                    imageGenerationValidationException.InnerException as Xeption);
            }
            catch (ImageGenerationDependencyValidationException imageGenerationDependencyValidationException)
            {
                throw CreateImageGenerationClientValidationException(
                    imageGenerationDependencyValidationException.InnerException as Xeption);
            }
            catch (ImageGenerationDependencyException imageGenerationDependencyException)
            {
                throw CreateImageGenerationClientDependencyException(
                    imageGenerationDependencyException.InnerException as Xeption);
            }
            catch (ImageGenerationServiceException imageGenerationServiceException)
            {
                throw CreateImageGenerationClientServiceException(
                    imageGenerationServiceException.InnerException as Xeption);
            }
        }

        private static ImageGenerationClientValidationException CreateImageGenerationClientValidationException(
            Xeption innerException)
        {
            return new ImageGenerationClientValidationException(
                message: "Image generation client validation error occurred, fix errors and try again.",
                innerException);
        }

        private static ImageGenerationClientDependencyException CreateImageGenerationClientDependencyException(
            Xeption innerException)
        {
            return new ImageGenerationClientDependencyException(
                message: "Image generation client dependency error occurred, contact support.",
                innerException);
        }

        private static ImageGenerationClientServiceException CreateImageGenerationClientServiceException(
            Xeption innerException)
        {
            return new ImageGenerationClientServiceException(
                message: "Image generation client service error occurred, contact support.",
                innerException);
        }
    }
}