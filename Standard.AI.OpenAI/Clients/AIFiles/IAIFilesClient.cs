// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;

namespace Standard.AI.OpenAI.Clients.AIFiles
{
    public interface IAIFilesClient
    {
        ValueTask<AIFile> UploadFileAsync(AIFile aiFile);
        ValueTask<IEnumerable<AIFileResponse>> RetrieveAllFilesAsync();
        ValueTask<AIFile> RemoveFileByIdAsync(string fileId);
    }
}
