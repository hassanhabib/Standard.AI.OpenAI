// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.AIModels
{
    internal partial class AIModelService
    {

        private delegate ValueTask<IEnumerable<AIModel>> ReturningAIModelsFunction();

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
        }
    }
}
