// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions;

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
                throw new AIModelValidationException(invalidAIModelException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationAIModelException =
                    new InvalidConfigurationAIModelException(httpResponseUrlNotFoundException);

                throw new AIModelDependencyException(invalidConfigurationAIModelException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(httpResponseUnauthorizedException);

                throw new AIModelDependencyException(unauthorizedAIModelException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(httpResponseForbiddenException);

                throw new AIModelDependencyException(unauthorizedAIModelException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundAIModelException =
                    new NotFoundAIModelException(httpResponseNotFoundException);

                throw new AIModelDependencyValidationException(notFoundAIModelException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidAIModelException =
                    new InvalidAIModelException(httpResponseBadRequestException);

                throw new AIModelDependencyValidationException(invalidAIModelException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAIModelException =
                    new ExcessiveCallAIModelException(httpResponseTooManyRequestsException);

                throw new AIModelDependencyValidationException(excessiveCallAIModelException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIModelException =
                    new FailedServerAIModelException(httpResponseException);

                throw new AIModelDependencyException(failedServerAIModelException);
            }
            catch (Exception exception)
            {
                var failedAIModelServiceException =
                    new FailedAIModelServiceException(exception);

                throw new AIModelServiceException(
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

                throw new AIModelDependencyException(invalidConfigurationAIModelException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(httpResponseUnauthorizedException);

                throw new AIModelDependencyException(unauthorizedAIModelException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIModelException =
                    new UnauthorizedAIModelException(httpResponseForbiddenException);

                throw new AIModelDependencyException(unauthorizedAIModelException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAIModelException =
                    new ExcessiveCallAIModelException(httpResponseTooManyRequestsException);

                throw new AIModelDependencyValidationException(excessiveCallAIModelException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIModelException =
                    new FailedServerAIModelException(httpResponseException);

                throw new AIModelDependencyException(failedServerAIModelException);
            }
            catch (Exception exception)
            {
                var failedAIModelServiceException =
                    new FailedAIModelServiceException(exception);

                throw new AIModelServiceException(
                    failedAIModelServiceException);
            }
        }
    }
}