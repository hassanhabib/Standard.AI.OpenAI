// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

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
        private readonly IOpenAIBroker openAIBroker;
        private readonly IDateTimeBroker dateTimeBroker;

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

            return externalAIModelsResult.AIModels.Select(externalAIModel =>
                new AIModel
                {
                    Name = externalAIModel.Id,
                    CreatedDate = this.dateTimeBroker.ConvertToDateTimeOffSet(externalAIModel.Created),
                    Type = externalAIModel.Object,
                    OwnedBy = externalAIModel.OwnedBy,
                    Parent = externalAIModel.Parent,
                    OriginModel = externalAIModel.Root,

                    Permissions = externalAIModel.Permissions.Select(
                        externalPermission =>
                        {
                            return new AIModelPermission
                            {
                                Id = externalPermission.Id,
                                Type = externalPermission.Object,
                                CreatedDate = this.dateTimeBroker.ConvertToDateTimeOffSet(externalPermission.Created),
                                AllowCreateEngine = externalPermission.AllowCreateEngine,
                                AllowSampling = externalPermission.AllowSampling,
                                AllowLogProbabilities = externalPermission.AllowLogprobs,
                                AllowSearchIndices = externalPermission.AllowSearchIndices,
                                AllowView = externalPermission.AllowView,
                                AllowFineTuning = externalPermission.AllowFineTuning,
                                Organization = externalPermission.Organization,
                                IsBlocking = externalPermission.IsBlocking
                            };
                        }).ToArray()
                }).ToArray();
        });
    }
}
