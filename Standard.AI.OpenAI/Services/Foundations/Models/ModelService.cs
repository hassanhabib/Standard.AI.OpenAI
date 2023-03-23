// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalModels;
using Standard.AI.OpenAI.Models.Services.Foundations.Models;

namespace Standard.AI.OpenAI.Services.Foundations.Models
{
    internal partial class ModelService : IModelService
    {
        private readonly IOpenAIBroker openAiBroker;

        public ModelService(IOpenAIBroker openAiBroker) =>
            this.openAiBroker = openAiBroker;

        public async ValueTask<Model[]> GetModelsAsync()
        {
            ExternalModelsResult result = await this.openAiBroker.GetAllModelsAsync();
            ExternalModel[] externalModels = result.Data;
            Model[] models = ConvertToModels(externalModels);

            return models;
        }

        private static Model[] ConvertToModels(ExternalModel[] externalModels)
        {
            IEnumerable<Model> models =
                externalModels.Select(selector: ConvertToModel);

            Model[] modelArray = models.ToArray();

            return modelArray;
        }

        private static Model ConvertToModel(ExternalModel externalModel) =>
            new Model
            {
                Id = externalModel.Id,
                Object = externalModel.Object,
                Created = externalModel.Created,
                OwnedBy = externalModel.OwnedBy,
                Permission = ConvertToPermissions(externalModel.Permission),
                Root = externalModel.Root
            };

        private static Permission[] ConvertToPermissions(ExternalPermission[] externalPermissions)
        {
            IEnumerable<Permission> permissions =
                externalPermissions.Select(selector: ConvertToPermission);

            Permission[] permissionsArray = permissions.ToArray();

            return permissionsArray;
        }

        private static Permission ConvertToPermission(ExternalPermission externalPermission) =>
            new Permission
            {
                Id = externalPermission.Id,
                Object = externalPermission.Object,
                Created = externalPermission.Created,
                AllowCreateEngine = externalPermission.AllowCreateEngine,
                AllowSampling = externalPermission.AllowSampling,
                AllowLogprobs = externalPermission.AllowLogprobs,
                AllowSearchIndices = externalPermission.AllowSearchIndices,
                AllowView = externalPermission.AllowView,
                AllowFineTuning = externalPermission.AllowFineTuning,
                Organization = externalPermission.Organization,
                IsBlocking = externalPermission.IsBlocking
            };
    }
}