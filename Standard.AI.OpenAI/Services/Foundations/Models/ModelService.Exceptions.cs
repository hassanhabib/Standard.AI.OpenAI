// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.Models;
using Standard.AI.OpenAI.Models.Services.Foundations.Models.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.Models
{
    internal partial class ModelService : IModelService
    {
        private delegate ValueTask<Model[]> ReturningModelArrayFunction();

        private static async ValueTask<Model[]> TryCatch(ReturningModelArrayFunction returningModelArrayFunction)
        {
            try
            {
                return await returningModelArrayFunction();
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedModelException =
                    new UnauthorizedModelException(httpResponseUnauthorizedException);

                throw new ModelDependencyException(unauthorizedModelException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedModelException =
                    new UnauthorizedModelException(httpResponseForbiddenException);

                throw new ModelDependencyException(unauthorizedModelException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationModelException =
                    new InvalidConfigurationModelException(httpResponseUrlNotFoundException);

                throw new ModelDependencyException(invalidConfigurationModelException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallModelException =
                    new ExcessiveCallModelException(httpResponseTooManyRequestsException);

                throw new ModelDependencyValidationException(excessiveCallModelException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerModelException =
                    new FailedServerModelException(httpResponseException);

                throw new ModelDependencyException(failedServerModelException);
            }
            catch (Exception exception)
            {
                var failedModelServiceException =
                    new FailedModelServiceException(exception);

                throw new ModelServiceException(failedModelServiceException);
            }
        }
    }
}
