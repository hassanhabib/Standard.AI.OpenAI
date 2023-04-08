// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;

namespace Standard.AI.OpenAI.Services.Foundations.AIFiles
{
    internal partial class AIFileService : IAIFileService
    {
        private readonly IOpenAIBroker openAIBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public AIFileService(
            IOpenAIBroker openAIBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.openAIBroker = openAIBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<AIFile> UploadFileAsync(AIFile file)
        {
            throw new System.NotImplementedException();
        }
    }
}
