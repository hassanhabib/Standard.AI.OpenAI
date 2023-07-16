// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.AIFiles.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;

namespace Standard.AI.OpenAI.Clients.AIFiles
{
    public interface IAIFilesClient
    {
        /// <exception cref="AIFileClientValidationException"/>
        /// <exception cref="AIFileClientDependencyException"/>
        /// <exception cref="AIFileClientServiceException"/>
        ValueTask<AIFile> UploadFileAsync(AIFile aiFile);
        ValueTask<IEnumerable<AIFileResponse>> RetrieveAllFilesAsync();
        
        /// <exception cref="AIFileClientValidationException"/>
        /// <exception cref="AIFileClientDependencyException"/>
        /// <exception cref="AIFileClientServiceException"/>
        ValueTask<AIFile> RemoveFileByIdAsync(string fileId);
    }
}
