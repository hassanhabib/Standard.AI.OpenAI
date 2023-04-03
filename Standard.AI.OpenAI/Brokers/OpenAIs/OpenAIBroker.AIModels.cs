// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIModels;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<ExternalAIModelsResult> GetAllAIModelsAsync() =>
             await GetAsync<ExternalAIModelsResult>(relativeUrl: "v1/models");

        public async ValueTask<ExternalAIModel> GetAIModelByIdAsync(string aiModelId) =>
            await GetAsync<ExternalAIModel>(relativeUrl: $"v1/models/{aiModelId}");
    }
}