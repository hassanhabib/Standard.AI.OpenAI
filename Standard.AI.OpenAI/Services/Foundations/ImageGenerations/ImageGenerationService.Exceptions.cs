// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
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
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedImageGenerationException =
                    new UnauthorizedImageGenerationException(httpResponseUnauthorizedException);

                throw new ImageGenerationDependencyException(unauthorizedImageGenerationException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedImageGenerationException =
                    new UnauthorizedImageGenerationException(httpResponseForbiddenException);

                throw new ImageGenerationDependencyException(unauthorizedImageGenerationException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundImageGenerationException =
                    new NotFoundImageGenerationException(httpResponseNotFoundException);

                throw new ImageGenerationDependencyValidationException(notFoundImageGenerationException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidImageGenerationException =
                    new InvalidImageGenerationException(httpResponseBadRequestException);

                throw new ImageGenerationDependencyValidationException(invalidImageGenerationException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallImageGenerationException =
                    new ExcessiveCallImageGenerationException(httpResponseTooManyRequestsException);

                throw new ImageGenerationDependencyValidationException(excessiveCallImageGenerationException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerImageGenerationException =
                    new FailedServerImageGenerationException(httpResponseException);

                throw new ImageGenerationDependencyException(failedServerImageGenerationException);
            }
            catch (Exception exception)
            {
                var failedImageGenerationServiceException =
                    new FailedImageGenerationServiceException(exception);

                throw new ImageGenerationServiceException(failedImageGenerationServiceException);
            }
        }
    }
}