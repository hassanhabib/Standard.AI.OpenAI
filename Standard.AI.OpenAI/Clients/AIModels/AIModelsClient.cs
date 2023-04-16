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
                throw new AIModelClientValidationException(
                    aiModelDependencyValidationException.InnerException as Xeption);
            }
            catch (AIModelDependencyException aiModelDependencyException)
            {
                throw new AIModelClientDependencyException(
                    aiModelDependencyException.InnerException as Xeption);
            }
            catch (AIModelServiceException aiModelServiceException)
            {
                throw new AIModelClientServiceException(
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
                throw new AIModelClientValidationException(
                    aIModelValidationException.InnerException as Xeption);
            }
            catch (AIModelDependencyValidationException aiModelDependencyValidationException)
            {
                throw new AIModelClientValidationException(
                    aiModelDependencyValidationException.InnerException as Xeption);
            }
            catch (AIModelDependencyException aiModelDependencyException)
            {
                throw new AIModelClientDependencyException(
                    aiModelDependencyException.InnerException as Xeption);
            }
            catch (AIModelServiceException aiModelServiceException)
            {
                throw new AIModelClientServiceException(
                    aiModelServiceException.InnerException as Xeption);
            }
        }
    }
}
