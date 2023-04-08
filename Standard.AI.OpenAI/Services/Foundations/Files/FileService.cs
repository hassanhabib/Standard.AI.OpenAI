// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;

namespace Standard.AI.OpenAI.Services.Foundations.Files
{
    internal partial class FileService : IFileService
    {
        private readonly IOpenAIBroker openAIBroker;

        public FileService(IOpenAIBroker openAIBroker) =>
            this.openAIBroker = openAIBroker;

        public ValueTask<File> RemoveFileByIdAsync(string fileId) =>
        TryCatch(async () =>
        {
            ValidateFileId(fileId);
            ExternalAIFileResponse removedFile = await this.openAIBroker.DeleteFileByIdAsync(fileId);

            return ConvertToFile(removedFile);
        });

        private static File ConvertToFile(ExternalAIFileResponse externalFile)
        {
            return new File()
            {
                Id = externalFile.Id,
                Type = externalFile.Object,
                Deleted = externalFile.Deleted
            };
        }
    }
}