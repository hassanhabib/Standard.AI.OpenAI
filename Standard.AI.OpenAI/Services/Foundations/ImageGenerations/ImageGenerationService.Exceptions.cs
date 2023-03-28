// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.ImageGenerations
{
    internal partial class ImageGenerationService
    {
        private delegate ValueTask<ImageGeneration> ReturningImageGenerationFunction();

        private async ValueTask<ImageGeneration> TryCatch(ReturningImageGenerationFunction returningImageGenerationFunction)
        {
            try
            {
                return await returningImageGenerationFunction();
            }
            catch (NullImageGenerationException nullImageGenerationException)
            {
                throw new ImageGenerationValidationException(nullImageGenerationException);
            }
            catch (InvalidImageGenerationException invalidImageGenerationException)
            {
                throw new ImageGenerationValidationException(invalidImageGenerationException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationImageGenerationException =
                    new InvalidConfigurationImageGenerationException(httpResponseUrlNotFoundException);

                throw new ImageGenerationDependencyException(invalidConfigurationImageGenerationException);
            }
        }
    }
}