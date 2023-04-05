// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;

namespace Standard.AI.OpenAI.Services.Foundations.AIModels
{
    internal interface IAIModelService
    {
        ValueTask<IEnumerable<AIModel>> RetrieveAllAIModelsAsync();
        ValueTask<AIModel> RetrieveAIModelByNameAsync(string aiModelName);
    }
}