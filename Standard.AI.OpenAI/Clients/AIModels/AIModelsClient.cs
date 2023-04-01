// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.AIModels.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions;
using Standard.AI.OpenAI.Services.Foundations.AIModels;
using Xeptions;

namespace Standard.AI.OpenAI.Clients.AIModels
{
    public class AIModelsClient : IAIModelsClient
    {
        private readonly IAIModelService aIModelService;

        public AIModelsClient(IAIModelService aIModelService) =>
            this.aIModelService = aIModelService;

        public async ValueTask<IEnumerable<AIModel>> RetrieveAIModelsAsync()
        {
            try
            {
                return await this.aIModelService.RetrieveAllAIModelsAsync();
            }
            catch (AIModelDependencyValidationException aiModelDependencyValidationException)
            {
                throw new AIModelClientValidationException(
                    aiModelDependencyValidationException.InnerException as Xeption);
            }
            catch (AIModelDependencyException aiModelDependencyException)
            {
                throw new AIModelClientValidationException(
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
