// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalFiles;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<ExternalFile> DeleteFileByIdAsync(string fileId) =>
            await DeleteAsync<ExternalFile>(relativeUrl: $"v1/files/{fileId}");
    }
}