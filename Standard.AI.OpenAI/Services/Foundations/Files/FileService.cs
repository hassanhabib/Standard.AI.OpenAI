// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;

namespace Standard.AI.OpenAI.Services.Foundations.Files
{
    internal partial class FileService : IFileService
    {
        private readonly IOpenAIBroker openAIBroker;

        public FileService(IOpenAIBroker openAIBroker) =>
            this.openAIBroker = openAIBroker;

        public ValueTask<AIFile> RemoveFileByIdAsync(string fileId) =>
        TryCatch(async () =>
        {
            ValidateFileId(fileId);
            ExternalAIFileResponse removedFile = await this.openAIBroker.DeleteFileByIdAsync(fileId);

            return ConvertToFile(removedFile);
        });

        private static AIFile ConvertToFile(ExternalAIFileResponse externalAIFileResponse)
        {
            return new AIFile
            {
                Response = ConvertToFileResponse(externalAIFileResponse)
            };
        }

        private static AIFileResponse ConvertToFileResponse(ExternalAIFileResponse externalAIFileResponse)
        {
            return new AIFileResponse
            {
                Id = externalAIFileResponse.Id,
                Type = externalAIFileResponse.Object,
                Size = externalAIFileResponse.Bytes,
                Name = externalAIFileResponse.FileName,
                Purpose = externalAIFileResponse.Purpose,
            };
        }
    }
}