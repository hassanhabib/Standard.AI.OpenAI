// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;

namespace Standard.AI.OpenAI.Services.Foundations.Files
{
    internal class FileService : IFileService
    {
        private readonly IOpenAIBroker openAIBroker;

        public FileService(IOpenAIBroker openAIBroker) =>
            this.openAIBroker = openAIBroker;

        public ValueTask<File> RemoveFileByIdAsync(string fileId) =>
            throw new System.NotImplementedException();
    }
}