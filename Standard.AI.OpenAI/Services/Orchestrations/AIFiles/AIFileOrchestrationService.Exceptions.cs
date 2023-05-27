// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions;
using Standard.AI.OpenAI.Models.Services.Orchestrations.AIFiles.Exceptions;
using Xeptions;

namespace Standard.AI.OpenAI.Services.Orchestrations.AIFiles
{
    internal partial class AIFileOrchestrationService
    {
        private delegate ValueTask<AIFile> ReturningAIFileFunction();
        private delegate ValueTask<IEnumerable<AIFileResponse>> ReturningAIFilesFunction();

        private async ValueTask<AIFile> TryCatch(ReturningAIFileFunction returningAIFileFunction)
        {
            try
            {
                return await returningAIFileFunction();
            }
            catch (NullAIFileOrchestrationException nullAIFileOrchestrationException)
            {
                throw new AIFileOrchestrationValidationException(
                    nullAIFileOrchestrationException);
            }
            catch (InvalidAIFileOrchestrationException invalidAIFileOrchestrationException)
            {
                throw new AIFileOrchestrationValidationException(invalidAIFileOrchestrationException);
            }
            catch (LocalFileValidationException localFileValidationException)
            {
                throw new AIFileOrchestrationDependencyValidationException(
                    localFileValidationException.InnerException as Xeption);
            }
            catch (LocalFileDependencyValidationException localFileDependencyValidationException)
            {
                throw new AIFileOrchestrationDependencyValidationException(
                    localFileDependencyValidationException.InnerException as Xeption);
            }
            catch (LocalFileDependencyException localFileDependencyException)
            {
                throw new AIFileOrchestrationDependencyException(
                    localFileDependencyException.InnerException as Xeption);
            }
            catch (LocalFileServiceException localFileServiceException)
            {
                throw new AIFileOrchestrationDependencyException(
                    localFileServiceException.InnerException as Xeption);
            }
            catch (AIFileValidationException aIFileValidationException)
            {
                throw new AIFileOrchestrationDependencyValidationException(
                    aIFileValidationException.InnerException as Xeption);
            }
            catch (AIFileDependencyValidationException aIFileDependencyValidationException)
            {
                throw new AIFileOrchestrationDependencyValidationException(
                    aIFileDependencyValidationException.InnerException as Xeption);
            }
            catch (AIFileDependencyException aIFileDependencyException)
            {
                throw new AIFileOrchestrationDependencyException(
                    aIFileDependencyException.InnerException as Xeption);
            }
            catch (AIFileServiceException aIFileServiceException)
            {
                throw new AIFileOrchestrationDependencyException(
                    aIFileServiceException.InnerException as Xeption);
            }
            catch (Exception exception)
            {
                var failedAIFileOrchestrationServiceException =
                    new FailedAIFileOrchestrationServiceException(
                        exception);

                throw new AIFileOrchestrationServiceException(
                    failedAIFileOrchestrationServiceException);
            }
        }

        private async ValueTask<IEnumerable<AIFileResponse>> TryCatch(ReturningAIFilesFunction returningAIFilesFunction)
        {
            try
            {
                return await returningAIFilesFunction();
            }
            catch (AIFileDependencyException aIFileDependencyException)
            {
                throw new AIFileOrchestrationDependencyException(
                    aIFileDependencyException.InnerException as Xeption);
            }
            catch (AIFileDependencyValidationException aIFileDependencyValidationException)
            {
                throw new AIFileOrchestrationDependencyValidationException(
                    aIFileDependencyValidationException.InnerException as Xeption);
            }
            catch (AIFileServiceException aIFileServiceException)
            {
                throw new AIFileOrchestrationDependencyException(
                    aIFileServiceException.InnerException as Xeption);
            }
            catch (Exception exception)
            {
                var failedAIFileOrchestrationServiceException =
                    new FailedAIFileOrchestrationServiceException(
                        exception);

                throw new AIFileOrchestrationServiceException(
                    failedAIFileOrchestrationServiceException);
            }
        }
    }
}
