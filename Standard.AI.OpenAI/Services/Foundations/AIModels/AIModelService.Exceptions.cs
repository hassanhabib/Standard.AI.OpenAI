// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions;
using Xeptions;

namespace Standard.AI.OpenAI.Services.Foundations.AIModels
{
    internal partial class AIModelService
    {
        private delegate ValueTask<AIModel> ReturningAIModelFunction();
        private delegate ValueTask<IEnumerable<AIModel>> ReturningAIModelsFunction();

        private async ValueTask<AIModel> TryCatch(ReturningAIModelFunction returningAIModelFunction)
        {
            try
            {
                return await returningAIModelFunction();
            }
            catch (InvalidAIModelException invalidAIModelException)
            {
                throw new AIModelValidationException(
                    message: "AI Model validation error occurred, fix errors and try again.", 
                    invalidAIModelException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationAIModelException =
                    new InvalidConfigurationAIModelException(
                        message: "Invalid AI Model configuration error occurred, contact support.", 
                        httpResponseUrlNotFoundException);

                throw createAIModelDependencyException(
                    invalidConfigurationAIModelException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(
                        message: "Unauthorized AI Model error occurred, fix errors and try again.", 
                        httpResponseUnauthorizedException);

                throw createAIModelDependencyException(
                    unauthorizedAIModelException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(
                        message: "Unauthorized AI Model error occurred, fix errors and try again.", 
                        httpResponseForbiddenException);

                throw createAIModelDependencyException(
                    unauthorizedAIModelException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundAIModelException =
                    new NotFoundAIModelException(
                        message: "AI Model not found.", 
                        httpResponseNotFoundException);

                throw new AIModelDependencyValidationException(
                    message: "AI Model dependency validation error occurred, fix errors and try again.",
                    notFoundAIModelException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidAIModelException =
                    new InvalidAIModelException(
                        message: "AI Model is invalid.", 
                        httpResponseBadRequestException);

                throw new AIModelDependencyValidationException(
                    message: "AI Model dependency validation error occurred, fix errors and try again.", 
                    invalidAIModelException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAIModelException =
                    new ExcessiveCallAIModelException(
                        message: "Excessive call error occurred, limit your calls.", 
                        httpResponseTooManyRequestsException);

                throw new AIModelDependencyValidationException(
                    message: "AI Model dependency validation error occurred, fix errors and try again.", 
                    excessiveCallAIModelException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIModelException =
                    new FailedServerAIModelException(
                        message: "Failed AI Model server error occurred, contact support", 
                        httpResponseException);

                throw createAIModelDependencyException(
                    failedServerAIModelException);
            }
            catch (Exception exception)
            {
                var failedAIModelServiceException =
                    new FailedAIModelServiceException(
                        message: "Failed AI Model Service Exception occurred, please contact support for assistance.", 
                        exception);

                throw new AIModelServiceException(
                    message: "AI Model service error occurred, contact support.",
                    failedAIModelServiceException);
            }
        }

        private async ValueTask<IEnumerable<AIModel>> TryCatch(ReturningAIModelsFunction returningAIModelsFunction)
        {
            try
            {
                return await returningAIModelsFunction();
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationAIModelException =
                    new InvalidConfigurationAIModelException(
                        message: "Invalid AI Model configuration error occurred, contact support.",
                        httpResponseUrlNotFoundException);

                throw createAIModelDependencyException(
                    invalidConfigurationAIModelException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(
                        message: "Unauthorized AI Model error occurred, fix errors and try again.", 
                        httpResponseUnauthorizedException);

                throw createAIModelDependencyException(
                    unauthorizedAIModelException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(
                        message: "Unauthorized AI Model error occurred, fix errors and try again.", 
                        httpResponseForbiddenException);

                throw createAIModelDependencyException(
                    unauthorizedAIModelException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAIModelException =
                    new ExcessiveCallAIModelException(
                        message: "Excessive call error occurred, limit your calls.", 
                        httpResponseTooManyRequestsException);

                throw new AIModelDependencyValidationException(
                    message: "AI Model dependency validation error occurred, fix errors and try again.", 
                    excessiveCallAIModelException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIModelException =
                    new FailedServerAIModelException(
                        message: "Failed AI Model server error occurred, contact support", 
                        httpResponseException);

                throw createAIModelDependencyException(
                    failedServerAIModelException);
            }
            catch (Exception exception)
            {
                var failedAIModelServiceException =
                    new FailedAIModelServiceException(
                        message: "Failed AI Model Service Exception occurred, please contact support for assistance.", 
                        exception);

                throw new AIModelServiceException(
                    message: "AI Model service error occurred, contact support.",
                    failedAIModelServiceException);
            }
        }

        private static AIModelDependencyException createAIModelDependencyException(Xeption innerException)
        {
            return new AIModelDependencyException(
                message: "AI Model dependency error occurred, contact support.",
                innerException);
        }
    }
}