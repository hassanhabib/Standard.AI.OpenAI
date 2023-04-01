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
                throw new ImageGenerationClientValidationException(
                    imageGenerationValidationException.InnerException as Xeption);
            }
            catch (ImageGenerationDependencyValidationException imageGenerationDependencyValidationException)
            {
                throw new ImageGenerationClientValidationException(
                    imageGenerationDependencyValidationException.InnerException as Xeption);
            }
            catch (ImageGenerationDependencyException imageGenerationDependencyException)
            {
                throw new ImageGenerationClientDependencyException(
                    imageGenerationDependencyException.InnerException as Xeption);
            }
            catch (ImageGenerationServiceException imageGenerationServiceException)
            {
                throw new ImageGenerationClientServiceException(
                    imageGenerationServiceException.InnerException as Xeption);
            }
        }
    }
}