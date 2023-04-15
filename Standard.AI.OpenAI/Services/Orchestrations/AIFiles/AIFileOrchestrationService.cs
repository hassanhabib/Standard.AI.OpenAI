// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Services.Foundations.LocalFiles;

namespace Standard.AI.OpenAI.Services.Orchestrations.AIFiles
{
    internal class AIFileOrchestrationService : IAIFileOrchestrationService
    {
        private readonly ILocalFileService localFileService;
        private readonly IAIFileService aiFileService;

        public AIFileOrchestrationService(
            ILocalFileService localFileService,
            IAIFileService aiFileService)
        {
            this.localFileService = localFileService;
            this.aiFileService = aiFileService;
        }

        public ValueTask<AIFile> UploadFileAsync(AIFile aiFile)
        {
            throw new System.NotImplementedException();
        }
    }
}
