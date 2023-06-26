// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.AIFiles.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Orchestrations.AIFiles.Exceptions;
using Standard.AI.OpenAI.Services.Orchestrations.AIFiles;
using Xeptions;

namespace Standard.AI.OpenAI.Clients.AIFiles
{
    internal class AIFilesClient : IAIFilesClient
    {
        private readonly IAIFileOrchestrationService aiFileOrchestrationService;

        public AIFilesClient(IAIFileOrchestrationService aiFileOrchestrationService) =>
            this.aiFileOrchestrationService = aiFileOrchestrationService;

        public async ValueTask<AIFile> UploadFileAsync(AIFile aiFile)
        {
            try
            {
                return await this.aiFileOrchestrationService.UploadFileAsync(aiFile);
            }
            catch (AIFileOrchestrationValidationException aiFileOrchestrationValidationException)
            {
                throw new AIFileClientValidationException(
                    aiFileOrchestrationValidationException.InnerException as Xeption);
            }
            catch (AIFileOrchestrationDependencyValidationException aiFileOrchestrationDependencyValidationException)
            {
                throw new AIFileClientValidationException(
                    aiFileOrchestrationDependencyValidationException.InnerException as Xeption);
            }
            catch (AIFileOrchestrationDependencyException aiFileOrchestrationDependencyException)
            {
                throw new AIFileClientDependencyException(
                    aiFileOrchestrationDependencyException.InnerException as Xeption);
            }
            catch (AIFileOrchestrationServiceException aiFileOrchestrationServiceException)
            {
                throw new AIFileClientServiceException(
                    aiFileOrchestrationServiceException.InnerException as Xeption);
            }
        }

        public async ValueTask<IEnumerable<AIFileResponse>> RetrieveAllFilesAsync()
        {
            try
            {
                return await this.aiFileOrchestrationService.RetrieveAllFilesAsync();
            }
            catch (AIFileOrchestrationDependencyValidationException aiFileOrchestrationDependencyValidationException)
            {
                throw new AIFileClientValidationException(
                    aiFileOrchestrationDependencyValidationException.InnerException as Xeption);
            }
            catch (AIFileOrchestrationDependencyException aiFileOrchestrationDependencyException)
            {
                throw new AIFileClientDependencyException(
                    aiFileOrchestrationDependencyException.InnerException as Xeption);
            }
            catch (AIFileOrchestrationServiceException aiFileOrchestrationServiceException)
            {
                throw new AIFileClientServiceException(
                    aiFileOrchestrationServiceException.InnerException as Xeption);
            }
        }

        public async ValueTask<AIFile> RemoveFileByIdAsync(string fileId)
        {
            try
            {
                return await this.aiFileOrchestrationService.RemoveFileByIdAsync(fileId);
            }
            catch (AIFileOrchestrationValidationException aiFileOrchestrationValidationException)
            {
                throw new AIFileClientValidationException(
                    aiFileOrchestrationValidationException.InnerException as Xeption);
            }
            catch (AIFileOrchestrationDependencyValidationException aiFileOrchestrationDependencyValidationException)
            {
                throw new AIFileClientValidationException(
                    aiFileOrchestrationDependencyValidationException.InnerException as Xeption);
            }
            catch (AIFileOrchestrationDependencyException aiFileOrchestrationDependencyException)
            {
                throw new AIFileClientDependencyException(
                    aiFileOrchestrationDependencyException.InnerException as Xeption);
            }
            catch (AIFileOrchestrationServiceException aiFileOrchestrationServiceException)
            {
                throw new AIFileClientServiceException(
                    aiFileOrchestrationServiceException.InnerException as Xeption);
            }
        }
    }
}
