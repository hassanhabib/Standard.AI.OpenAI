// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;

namespace Standard.AI.OpenAI.Services.Foundations.AIModels
{
    internal class AIModelService : IAIModelService
    {
        private readonly IOpenAIBroker openAIBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public AIModelService(IOpenAIBroker openAIBroker, IDateTimeBroker dateTimeBroker)
        {
            this.openAIBroker = openAIBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<IEnumerable<AIModel>> RetrieveAllAIModelsAsync() =>
            throw new System.NotImplementedException();
    }
}
