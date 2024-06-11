// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.AIModels.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions;
using Standard.AI.OpenAI.Services.Foundations.AIModels;
using Xeptions;

namespace Standard.AI.OpenAI.Clients.AIModels
{
    internal class AIModelsClient : IAIModelsClient
    {
        private readonly IAIModelService aiModelService;

        public AIModelsClient(IAIModelService aiModelService) =>
            this.aiModelService = aiModelService;

        /// <inheritdoc />
        public async ValueTask<IEnumerable<AIModel>> RetrieveAIModelsAsync()
        {
            try
            {
                return await this.aiModelService.RetrieveAllAIModelsAsync();
            }
            catch (AIModelDependencyValidationException aiModelDependencyValidationException)
            {
                throw CreateAIModelClientValidationException(
                    aiModelDependencyValidationException.InnerException as Xeption);
            }
            catch (AIModelDependencyException aiModelDependencyException)
            {
                throw CreateAIModelClientDependencyException(
                    aiModelDependencyException.InnerException as Xeption);
            }
            catch (AIModelServiceException aiModelServiceException)
            {
                throw CreateAIModelClientServiceException(
                    aiModelServiceException.InnerException as Xeption);
            }
        }

        public async ValueTask<AIModel> RetrieveAIModelByNameAsync(string aiModelName)
        {
            try
            {
                return await this.aiModelService.RetrieveAIModelByNameAsync(aiModelName);
            }
            catch (AIModelValidationException aIModelValidationException)
            {
                throw CreateAIModelClientValidationException(
                    aIModelValidationException.InnerException as Xeption);
            }
            catch (AIModelDependencyValidationException aiModelDependencyValidationException)
            {
                throw CreateAIModelClientValidationException(
                    aiModelDependencyValidationException.InnerException as Xeption);
            }
            catch (AIModelDependencyException aiModelDependencyException)
            {
                throw CreateAIModelClientDependencyException(
                    aiModelDependencyException.InnerException as Xeption);
            }
            catch (AIModelServiceException aiModelServiceException)
            {
                throw CreateAIModelClientServiceException(
                    aiModelServiceException.InnerException as Xeption);
            }
        }

        private static AIModelClientValidationException CreateAIModelClientValidationException(Xeption innerException)
        {
            return new AIModelClientValidationException(
                message: "AI model client validation error occurred, fix errors and try again.",
                innerException);
        }

        private static AIModelClientDependencyException CreateAIModelClientDependencyException(Xeption innerException)
        {
            return new AIModelClientDependencyException(
                message: "AI model client dependency error occurred, contact support.",
                innerException);
        }

        private static AIModelClientServiceException CreateAIModelClientServiceException(Xeption innerException)
        {
            return new AIModelClientServiceException(
                message: "AI Model client service error occurred, contact support.",
                innerException);
        }
    }
}
