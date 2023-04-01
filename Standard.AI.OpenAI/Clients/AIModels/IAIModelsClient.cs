// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;

namespace Standard.AI.OpenAI.Clients.AIModels
{
    internal interface IAIModelsClient
    {
        ValueTask<IEnumerable<AIModel>> RetrieveAIModelsAsync();
    }
}
