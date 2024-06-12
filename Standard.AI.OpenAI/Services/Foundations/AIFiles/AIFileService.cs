// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIFiles;

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

        public ValueTask<AIFile> UploadFileAsync(AIFile file) =>
        TryCatch(async () =>
        {
            ValidateAIFile(file);
            ExternalAIFileResponse externalAIFileResponse = await PostFileAsync(file);
            file.Response = ConvertToFileResponse(externalAIFileResponse);

            return file;
        });

        public ValueTask<AIFile> RemoveFileByIdAsync(string fileId) =>
        TryCatch(async () =>
        {
            ValidateFileId(fileId);
            ExternalAIFileResponse removedFile = await this.openAIBroker.DeleteFileByIdAsync(fileId);

            return ConvertToFile(removedFile);
        });

        public ValueTask<IEnumerable<AIFileResponse>> RetrieveAllFilesAsync() =>
        TryCatch(async () =>
        {
            ExternalAIFilesResult externalAIFilesResult = await this.openAIBroker.GetAllFilesAsync();

            return externalAIFilesResult.Files.Select(ConvertToFileResponse).ToArray();
        });

        private async ValueTask<ExternalAIFileResponse> PostFileAsync(AIFile file)
        {
            var externalAIFileRequest = new ExternalAIFileRequest()
            {
                File = file.Request.Content,
                Purpose = file.Request.Purpose,
                FileName = file.Request.Name
            };

            return await this.openAIBroker.PostFileFormAsync(externalAIFileRequest);
        }

        private static AIFile ConvertToFile(ExternalAIFileResponse externalAIFileResponse)
        {
            return new AIFile
            {
                Response = ConvertToDeletedFileResponse(externalAIFileResponse)
            };
        }

        private static AIFileResponse ConvertToDeletedFileResponse(ExternalAIFileResponse externalAIFileResponse)
        {
            return new AIFileResponse
            {
                Id = externalAIFileResponse.Id,
                Type = externalAIFileResponse.Object,
                Size = externalAIFileResponse.Bytes,
                Name = externalAIFileResponse.FileName,
                Purpose = externalAIFileResponse.Purpose,
                Deleted = externalAIFileResponse.Deleted,
                Status = ConvertToAIFileStatus(externalAIFileResponse.Status),
                StatusDetails = externalAIFileResponse.StatusDetails
            };
        }

        private AIFileResponse ConvertToFileResponse(ExternalAIFileResponse externalAIFileResponse)
        {
            return new AIFileResponse
            {
                Id = externalAIFileResponse.Id,
                Type = externalAIFileResponse.Object,
                Size = externalAIFileResponse.Bytes,
                Name = externalAIFileResponse.FileName,
                Purpose = externalAIFileResponse.Purpose,
                Deleted = externalAIFileResponse.Deleted,
                Status = ConvertToAIFileStatus(externalAIFileResponse.Status),
                StatusDetails = externalAIFileResponse.StatusDetails,

                CreatedDate =
                    this.dateTimeBroker.ConvertToDateTimeOffSet(externalAIFileResponse.CreatedDate),
            };
        }

        private static AIFileStatus ConvertToAIFileStatus(string externalStatus)
        {
            return externalStatus?.ToLowerInvariant() switch
            {
                "uploaded" => AIFileStatus.Uploaded,
                "processed" => AIFileStatus.Processed,
                "error" => AIFileStatus.Error,
                _ => AIFileStatus.Unknown
            };
        }
    }
}