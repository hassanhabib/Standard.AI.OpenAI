// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions;
using Xeptions;

namespace Standard.AI.OpenAI.Services.Foundations.AIFiles
{
    internal partial class AIFileService
    {
        private delegate ValueTask<AIFile> ReturningAIFileFunction();
        private delegate ValueTask<IEnumerable<AIFileResponse>> ReturningAIFilesFunction();

        private async ValueTask<AIFile> TryCatch(ReturningAIFileFunction returningAIFileFunction)
        {
            try
            {
                return await returningAIFileFunction();
            }
            catch (NullAIFileException nullAIFileException)
            {
                throw CreateAIFileValidationException(
                    nullAIFileException);
            }
            catch (InvalidAIFileException invalidFileException)
            {
                throw CreateAIFileValidationException(
                    invalidFileException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationFileException =
                    CreateInvalidConfigurationAIFileException(
                        httpResponseUrlNotFoundException);

                throw CreateAIFileDependencyException(
                    invalidConfigurationFileException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIFileException =
                    CreateUnauthorizedAIFileException(
                        httpResponseUnauthorizedException);

                throw CreateAIFileDependencyException(
                    unauthorizedAIFileException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIFileException =
                    CreateUnauthorizedAIFileException(
                        httpResponseForbiddenException);

                throw CreateAIFileDependencyException(
                    unauthorizedAIFileException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundAIFileException =
                    new NotFoundAIFileException(
                        message: "Not found AI file error occurred, fix errors and try again.", 
                        httpResponseNotFoundException);

                throw CreateAIFileDependencyValidationException(
                    notFoundAIFileException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidAIFileException =
                    new InvalidAIFileException(
                        message: "Invalid AI file error occurred, fix errors and try again.", 
                        httpResponseBadRequestException);

                throw CreateAIFileDependencyValidationException(
                    invalidAIFileException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAIFileException =
                    CreateExcessiveCallAIFileException(
                        httpResponseTooManyRequestsException);

                throw CreateAIFileDependencyValidationException(
                    excessiveCallAIFileException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIFileException =
                    CreateFailedServerAIFileException(
                        httpResponseException);

                throw CreateAIFileDependencyException(
                    failedServerAIFileException);
            }
            catch (Exception exception)
            {
                var failedAIFileServiceException =
                    CreateFailedAIFileServiceException(
                        exception);

                throw CreateAIFileServiceException(
                    failedAIFileServiceException);
            }
        }

        private async ValueTask<IEnumerable<AIFileResponse>> TryCatch(ReturningAIFilesFunction returningAIFilesFunction)
        {
            try
            {
                return await returningAIFilesFunction();
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationAIFileException =
                    CreateInvalidConfigurationAIFileException(
                        httpResponseUrlNotFoundException);

                throw CreateAIFileDependencyException(
                    invalidConfigurationAIFileException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIFileException =
                    CreateUnauthorizedAIFileException(
                        httpResponseUnauthorizedException);

                throw CreateAIFileDependencyException(
                    unauthorizedAIFileException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIFileException =
                    CreateUnauthorizedAIFileException(
                        httpResponseForbiddenException);

                throw CreateAIFileDependencyException(
                    unauthorizedAIFileException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAIFileException =
                    CreateExcessiveCallAIFileException(
                        httpResponseTooManyRequestsException);

                throw CreateAIFileDependencyValidationException(
                    excessiveCallAIFileException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIFileException =
                    CreateFailedServerAIFileException(
                        httpResponseException);

                throw CreateAIFileDependencyException(
                    failedServerAIFileException);
            }
            catch (Exception exception)
            {
                var failedAIFileServiceException =
                    CreateFailedAIFileServiceException(
                        exception);

                throw CreateAIFileServiceException(
                   failedAIFileServiceException);
            }
        }

        private static AIFileDependencyException CreateAIFileDependencyException(Xeption innerException)
        {
            return new AIFileDependencyException(
                message: "AI file dependency error occurred, contact support.",
                innerException);
        }

        private static AIFileDependencyValidationException CreateAIFileDependencyValidationException(Xeption innerException)
        {
            return new AIFileDependencyValidationException(
                message: "AI file dependency validation error occurred, contact support.",
                innerException);
        }

        private static AIFileServiceException CreateAIFileServiceException(Xeption innerException)
        {
            return new AIFileServiceException(
                message: "AI file service error occurred, contact support.",
                innerException);
        }

        private static AIFileValidationException CreateAIFileValidationException(Xeption innerException)
        {
            throw new AIFileValidationException(
                message: "AI file validation error occurred, fix errors and try again.",
                innerException);
        }

        private static ExcessiveCallAIFileException CreateExcessiveCallAIFileException(Xeption innerException)
        {
            return new ExcessiveCallAIFileException(
                message: "Excessive call error occurred, limit your calls.",
                innerException);
        }

        private static FailedAIFileServiceException CreateFailedAIFileServiceException(Exception innerException)
        {
            return new FailedAIFileServiceException(
                message: "Failed AI file service error occurred, contact support.",
                innerException);
        }

        private static FailedServerAIFileException CreateFailedServerAIFileException(Exception innerException)
        {
            return new FailedServerAIFileException(
                message: "Failed AI file server error occurred, contact support.",
                innerException);
        }

        private static InvalidConfigurationAIFileException CreateInvalidConfigurationAIFileException(Exception innerException)
        {
            return new InvalidConfigurationAIFileException(
                message: "Invalid AI file configuration error occurred, contact support.",
                innerException);
        }

        private static UnauthorizedAIFileException CreateUnauthorizedAIFileException(Exception innerException)
        {
            return new UnauthorizedAIFileException(
                message: "Unauthorized AI file request, fix errors and try again.",
                innerException);
        }

    }
}