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
            throw new System.NotImplementedException();
    }
}