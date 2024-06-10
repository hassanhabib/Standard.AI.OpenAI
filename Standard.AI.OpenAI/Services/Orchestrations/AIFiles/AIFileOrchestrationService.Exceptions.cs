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
                throw CreateAIFileOrchestrationValidationException(
                    nullAIFileOrchestrationException);
            }
            catch (InvalidAIFileOrchestrationException invalidAIFileOrchestrationException)
            {
                throw CreateAIFileOrchestrationValidationException(
                    invalidAIFileOrchestrationException);
            }
            catch (LocalFileValidationException localFileValidationException)
            {
                throw CreateAIFileOrchestrationDependencyValidationException(
                    localFileValidationException.InnerException as Xeption);
            }
            catch (LocalFileDependencyValidationException localFileDependencyValidationException)
            {
                throw CreateAIFileOrchestrationDependencyValidationException(
                    localFileDependencyValidationException.InnerException as Xeption);
            }
            catch (LocalFileDependencyException localFileDependencyException)
            {
                throw CreateAIFileOrchestrationDependencyException(
                    localFileDependencyException.InnerException as Xeption);
            }
            catch (LocalFileServiceException localFileServiceException)
            {
                throw CreateAIFileOrchestrationDependencyException(
                    localFileServiceException.InnerException as Xeption);
            }
            catch (AIFileValidationException aIFileValidationException)
            {
                throw CreateAIFileOrchestrationDependencyValidationException(
                    aIFileValidationException.InnerException as Xeption);
            }
            catch (AIFileDependencyValidationException aIFileDependencyValidationException)
            {
                throw CreateAIFileOrchestrationDependencyValidationException(
                    aIFileDependencyValidationException.InnerException as Xeption);
            }
            catch (AIFileDependencyException aIFileDependencyException)
            {
                throw CreateAIFileOrchestrationDependencyException(
                    aIFileDependencyException.InnerException as Xeption);
            }
            catch (AIFileServiceException aIFileServiceException)
            {
                throw CreateAIFileOrchestrationDependencyException(
                    aIFileServiceException.InnerException as Xeption);
            }
            catch (Exception exception)
            {
                var failedAIFileOrchestrationServiceException =
                    new FailedAIFileOrchestrationServiceException(
                        message: "Failed AI file service error occurred, contact support.",
                        exception);

                throw CreateAIFileOrchestrationServiceException(
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
                throw CreateAIFileOrchestrationDependencyException(
                    aIFileDependencyException.InnerException as Xeption);
            }
            catch (AIFileDependencyValidationException aIFileDependencyValidationException)
            {
                throw CreateAIFileOrchestrationDependencyValidationException(
                    aIFileDependencyValidationException.InnerException as Xeption);
            }
            catch (AIFileServiceException aIFileServiceException)
            {
                throw CreateAIFileOrchestrationDependencyException(
                    aIFileServiceException.InnerException as Xeption);
            }
            catch (Exception exception)
            {
                var failedAIFileOrchestrationServiceException =
                    new FailedAIFileOrchestrationServiceException(
                        message: "Failed AI file service error occurred, contact support.",
                        exception);

                throw CreateAIFileOrchestrationServiceException(
                    failedAIFileOrchestrationServiceException);
            }
        }

        private static AIFileOrchestrationValidationException CreateAIFileOrchestrationValidationException(Xeption innerException)
        {
            return new AIFileOrchestrationValidationException(
                message: "AI file validation error occurred, fix errors and try again.",
                innerException);
        }

        private static AIFileOrchestrationDependencyValidationException CreateAIFileOrchestrationDependencyValidationException(Xeption innerException)
        {
            return new AIFileOrchestrationDependencyValidationException(
                message: "AI file dependency validation error occurred, fix errors and try again.",
                innerException);
        }

        private static AIFileOrchestrationDependencyException CreateAIFileOrchestrationDependencyException(Xeption innerException)
        {
            return new AIFileOrchestrationDependencyException(
                message: "AI File dependency error occurred, contact support.",
                innerException);
        }

        private static AIFileOrchestrationServiceException CreateAIFileOrchestrationServiceException(Xeption innerException)
        {
            return new AIFileOrchestrationServiceException(
                message: "AI File error occurred, contact support.",
                innerException);
        }

        private static NullAIFileOrchestrationException CreateNullAIFileOrchestrationException() 
        {
            return new NullAIFileOrchestrationException(
                message: "AI file is null.");
        }

        private static InvalidAIFileOrchestrationException CreateInvalidAIFileOrchestrationException()
        {
            return new InvalidAIFileOrchestrationException(
                 message: "AI file is invalid.");
        }
    }
}
