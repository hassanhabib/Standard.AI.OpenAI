// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIModels;

namespace Standard.AI.OpenAI.Services.Foundations.AIModels
{
    internal partial class AIModelService : IAIModelService
    {
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly IOpenAIBroker openAIBroker;

        public AIModelService(
            IOpenAIBroker openAIBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.openAIBroker = openAIBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<IEnumerable<AIModel>> RetrieveAllAIModelsAsync() =>
        TryCatch(async () =>
        {
            ExternalAIModelsResult externalAIModelsResult =
                await this.openAIBroker.GetAllAIModelsAsync();

            return externalAIModelsResult.AIModels.Select(ConvertToAIModel).ToArray();
        });

        public ValueTask<AIModel> RetrieveAIModelByNameAsync(string aiModelName) =>
        TryCatch(async () =>
        {
            ValidateAIModelName(aiModelName);

            ExternalAIModel externalAIModel =
                await this.openAIBroker.GetAIModelByIdAsync(aiModelName);

            return ConvertToAIModel(externalAIModel);
        });

        private AIModel ConvertToAIModel(ExternalAIModel externalAIModel)
        {
            return new AIModel
            {
                Name = externalAIModel.Id,
                CreatedDate = this.dateTimeBroker.ConvertToDateTimeOffSet(externalAIModel.Created),
                Type = externalAIModel.Object,
                OwnedBy = externalAIModel.OwnedBy,
                Parent = externalAIModel.Parent,
                OriginModel = externalAIModel.Root,
                Permissions = externalAIModel.Permissions.Select(ConvertToAIModelPermission).ToArray()
            };
        }

        private AIModelPermission ConvertToAIModelPermission(ExternalAIModelPermission externalAIModelPermission)
        {
            return new AIModelPermission
            {
                Id = externalAIModelPermission.Id,
                Type = externalAIModelPermission.Object,
                CreatedDate = this.dateTimeBroker.ConvertToDateTimeOffSet(externalAIModelPermission.Created),
                AllowCreateEngine = externalAIModelPermission.AllowCreateEngine,
                AllowSampling = externalAIModelPermission.AllowSampling,
                AllowLogProbabilities = externalAIModelPermission.AllowLogprobs,
                AllowSearchIndices = externalAIModelPermission.AllowSearchIndices,
                AllowView = externalAIModelPermission.AllowView,
                AllowFineTuning = externalAIModelPermission.AllowFineTuning,
                Organization = externalAIModelPermission.Organization,
                IsBlocking = externalAIModelPermission.IsBlocking
            };
        }
    }
}