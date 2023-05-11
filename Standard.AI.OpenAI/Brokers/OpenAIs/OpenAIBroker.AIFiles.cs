// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIFiles;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        private const string FilesRelativeUrl = "v1/files";

        public async ValueTask<ExternalAIFileResponse> PostFileFormAsync(
            ExternalAIFileRequest externalFileRequest)
        {
            return await PostFormAsync<ExternalAIFileRequest, ExternalAIFileResponse>(
                relativeUrl: FilesRelativeUrl,
                externalFileRequest);
        }

        public async ValueTask<ExternalAIFilesResult> GetAllFilesAsync() =>
            await GetAsync<ExternalAIFilesResult>(FilesRelativeUrl);

        public async ValueTask<ExternalAIFileResponse> DeleteFileByIdAsync(string fileId) =>
            await DeleteAsync<ExternalAIFileResponse>(relativeUrl: $"{FilesRelativeUrl}/{fileId}");
    }
}