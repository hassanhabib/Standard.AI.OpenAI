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
                throw CreateAIModelValidationException(
                    invalidAIModelException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationAIModelException =
                    CreateInvalidConfigurationAIModelException(
                        httpResponseUrlNotFoundException);

                throw CreateAIModelDependencyException(
                    invalidConfigurationAIModelException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIModelException =
                    CreateUnauthorizedAIModelException(
                        httpResponseUnauthorizedException);

                throw CreateAIModelDependencyException(
                    unauthorizedAIModelException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIModelException =
                    CreateUnauthorizedAIModelException(
                        httpResponseForbiddenException);

                throw CreateAIModelDependencyException(
                    unauthorizedAIModelException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundAIModelException =
                    new NotFoundAIModelException(
                        message: "AI Model not found.", 
                        httpResponseNotFoundException);

                throw CreateAIModelDependencyValidationException(
                    notFoundAIModelException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidAIModelException =
                    new InvalidAIModelException(
                        message: "AI Model is invalid.", 
                        httpResponseBadRequestException);

                throw CreateAIModelDependencyValidationException(
                    invalidAIModelException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAIModelException =
                    CreateExcessiveCallAIModelException(
                        httpResponseTooManyRequestsException);

                throw CreateAIModelDependencyValidationException(
                    excessiveCallAIModelException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIModelException =
                    CreateFailedServerAIModelException(
                        httpResponseException);

                throw CreateAIModelDependencyException(
                    failedServerAIModelException);
            }
            catch (Exception exception)
            {
                var failedAIModelServiceException =
                    CreateFailedAIModelServiceException(
                        exception);

                throw CreateAIModelServiceException(
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
                    CreateInvalidConfigurationAIModelException(
                        httpResponseUrlNotFoundException);

                throw CreateAIModelDependencyException(
                    invalidConfigurationAIModelException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIModelException =
                    CreateUnauthorizedAIModelException(
                        httpResponseUnauthorizedException);

                throw CreateAIModelDependencyException(
                    unauthorizedAIModelException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIModelException =
                    CreateUnauthorizedAIModelException(
                        httpResponseForbiddenException);

                throw CreateAIModelDependencyException(
                    unauthorizedAIModelException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAIModelException =
                    CreateExcessiveCallAIModelException(
                        httpResponseTooManyRequestsException);

                throw CreateAIModelDependencyValidationException(
                    excessiveCallAIModelException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIModelException =
                    CreateFailedServerAIModelException(
                        httpResponseException);

                throw CreateAIModelDependencyException(
                    failedServerAIModelException);
            }
            catch (Exception exception)
            {
                var failedAIModelServiceException =
                    CreateFailedAIModelServiceException(
                        exception);

                throw CreateAIModelServiceException(
                    failedAIModelServiceException);
            }
        }

        private static NotFoundAIModelException CreateNotFoundAIModelException(Exception innerException)
        {
            return new NotFoundAIModelException(
                message: "AI Model not found.", 
                innerException);
        }

        private static AIModelValidationException CreateAIModelValidationException(Xeption innerException)
        {
            return new AIModelValidationException(
                message: "AI Model validation error occurred, fix errors and try again.",
                innerException);
        }

        private static InvalidConfigurationAIModelException CreateInvalidConfigurationAIModelException(Exception innerException)
        {
            return new InvalidConfigurationAIModelException(
                message: "Invalid AI Model configuration error occurred, contact support.",
                innerException);
        }

        private static AIModelDependencyException CreateAIModelDependencyException(Xeption innerException)
        {
            return new AIModelDependencyException(
                message: "AI Model dependency error occurred, contact support.",
                innerException);
        }

        private static UnauthorizedAIModelException CreateUnauthorizedAIModelException(Exception innerException)
        {
            return new UnauthorizedAIModelException(
                message: "Unauthorized AI Model error occurred, fix errors and try again.",
                innerException);
        }

        private static AIModelDependencyValidationException CreateAIModelDependencyValidationException(Xeption innerException)
        {
            return new AIModelDependencyValidationException(
                message: "AI Model dependency validation error occurred, fix errors and try again.",
                innerException);
        }

        private static ExcessiveCallAIModelException CreateExcessiveCallAIModelException(Exception innerException)
        {
            return new ExcessiveCallAIModelException(
                message: "Excessive call error occurred, limit your calls.",
                innerException);
        }

        private static FailedServerAIModelException CreateFailedServerAIModelException(Exception innerException)
        {
            return new FailedServerAIModelException(
                message: "Failed AI Model server error occurred, contact support",
                innerException);
        }

        private static FailedAIModelServiceException CreateFailedAIModelServiceException(Exception innerException)
        {
            return new FailedAIModelServiceException(
                message: "Failed AI Model Service Exception occurred, please contact support for assistance.",
                innerException);
        }

        private static AIModelServiceException CreateAIModelServiceException(Xeption innerException)
        {
            return new AIModelServiceException(
                message: "AI Model service error occurred, contact support.",
                innerException);
        }
    }
}