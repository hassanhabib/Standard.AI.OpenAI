// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Services.Foundations.LocalFiles;

namespace Standard.AI.OpenAI.Services.Orchestrations.AIFiles
{
    internal partial class AIFileOrchestrationService : IAIFileOrchestrationService
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

        public ValueTask<AIFile> UploadFileAsync(AIFile aiFile) =>
        TryCatch(async () =>
        {
            ValidateAIFile(aiFile);

            return aiFile.Request.Content switch
            {
                { } => await this.aiFileService.UploadFileAsync(aiFile),
                _ => await ReadUploadFileAsync(aiFile)
            };
        });

        public ValueTask<IEnumerable<AIFileResponse>> RetrieveAllFilesAsync() =>
        TryCatch(async () => await this.aiFileService.RetrieveAllFilesAsync());

        private async ValueTask<AIFile> ReadUploadFileAsync(AIFile aiFile)
        {
            Stream readStream = this.localFileService.ReadFile(aiFile.Request.Name);
            aiFile.Request.Content = readStream;

            return await this.aiFileService.UploadFileAsync(aiFile);
        }

        public ValueTask<AIFile> RemoveFileByIdAsync(string fileId) =>
        TryCatch(async () =>
        {
            ValidateAIFileIdNotNull(fileId);
            
            AIFile aiFile = await this.aiFileService.RemoveFileByIdAsync(fileId);
            
            return aiFile;
        });
    }
}