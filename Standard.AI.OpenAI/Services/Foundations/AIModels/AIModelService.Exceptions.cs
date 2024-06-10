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
                    new InvalidConfigurationAIModelException(httpResponseUrlNotFoundException);

                throw CreateAIModelDependencyException(
                    invalidConfigurationAIModelException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(httpResponseUnauthorizedException);

                throw CreateAIModelDependencyException(
                    unauthorizedAIModelException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(httpResponseForbiddenException);

                throw CreateAIModelDependencyException(
                    unauthorizedAIModelException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundAIModelException =
                    new NotFoundAIModelException(httpResponseNotFoundException);

                throw CreateAIModelDependencyValidationException(
                    notFoundAIModelException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidAIModelException =
                    new InvalidAIModelException(httpResponseBadRequestException);

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
                    new FailedServerAIModelException(httpResponseException);

                throw CreateAIModelDependencyException(
                    failedServerAIModelException);
            }
            catch (Exception exception)
            {
                var failedAIModelServiceException =
                    new FailedAIModelServiceException(exception);

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
                    new InvalidConfigurationAIModelException(httpResponseUrlNotFoundException);

                throw CreateAIModelDependencyException(
                    invalidConfigurationAIModelException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(httpResponseUnauthorizedException);

                throw CreateAIModelDependencyException(
                    unauthorizedAIModelException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(httpResponseForbiddenException);

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
                    new FailedServerAIModelException(httpResponseException);

                throw CreateAIModelDependencyException(
                    failedServerAIModelException);
            }
            catch (Exception exception)
            {
                var failedAIModelServiceException =
                    new FailedAIModelServiceException(exception);

                throw CreateAIModelServiceException(
                    failedAIModelServiceException);
            }
        }

        private static AIModelValidationException CreateAIModelValidationException(Xeption innerException)
        {
            return new AIModelValidationException(
                message: "AI Model validation error occurred, fix errors and try again.",
                innerException);
        }

        private static AIModelDependencyException CreateAIModelDependencyException(Xeption innerException)
        {
            return new AIModelDependencyException(
                message: "AI Model dependency error occurred, contact support.",
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

        private static AIModelServiceException CreateAIModelServiceException(Xeption innerException)
        {
            return new AIModelServiceException(
                message: "AI Model service error occurred, contact support.",
                innerException);
        }
    }
}