// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;

namespace Standard.AI.OpenAI.Services.Foundations.AIFiles
{
    internal interface IAIFileService
    {
        ValueTask<AIFile> UploadFileAsync(AIFile file);
        ValueTask<AIFile> RemoveFileByIdAsync(string fileId);
        ValueTask<IEnumerable<AIFileResponse>> RetrieveAllFilesAsync();
    }
}
