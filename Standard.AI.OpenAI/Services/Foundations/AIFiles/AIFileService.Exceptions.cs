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
                throw new AIFileValidationException(
                    message: "AI file validation error occurred, fix errors and try again.", 
                    nullAIFileException);
            }
            catch (InvalidAIFileException invalidFileException)
            {
                throw new AIFileValidationException(
                    message: "AI file validation error occurred, fix errors and try again.", 
                    invalidFileException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationFileException =
                    new InvalidConfigurationAIFileException(
                        message: "Invalid AI file configuration error occurred, contact support.", 
                        httpResponseUrlNotFoundException);

                throw createAIFileDependencyException(
                    invalidConfigurationFileException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIFileException =
                    new UnauthorizedAIFileException(
                        message: "Unauthorized AI file request, fix errors and try again.", 
                        httpResponseUnauthorizedException);

                throw createAIFileDependencyException(
                    unauthorizedAIFileException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIFileException =
                    new UnauthorizedAIFileException(
                        message: "Unauthorized AI file request, fix errors and try again.", 
                        httpResponseForbiddenException);

                throw createAIFileDependencyException(
                    unauthorizedAIFileException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundAIFileException =
                    new NotFoundAIFileException(
                        message: "Not found AI file error occurred, fix errors and try again.", 
                        httpResponseNotFoundException);

                throw createAIFileDependencyValidationException(
                    notFoundAIFileException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidAIFileException =
                    new InvalidAIFileException(
                        message: "Invalid AI file error occurred, fix errors and try again.", 
                        httpResponseBadRequestException);

                throw createAIFileDependencyValidationException(
                    invalidAIFileException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAIFileException =
                    new ExcessiveCallAIFileException(
                        message: "Excessive call error occurred, limit your calls.", 
                        httpResponseTooManyRequestsException);

                throw createAIFileDependencyValidationException(
                    excessiveCallAIFileException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIFileException =
                    new FailedServerAIFileException(
                        message: "Failed AI file server error occurred, contact support.", 
                        httpResponseException);

                throw createAIFileDependencyException(
                    failedServerAIFileException);
            }
            catch (Exception exception)
            {
                var failedAIFileServiceException =
                    new FailedAIFileServiceException(
                        message: "Failed AI file service error occurred, contact support.", 
                        exception);

                throw new AIFileServiceException(
                    message: "AI file service error occurred, contact support.", 
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
                    new InvalidConfigurationAIFileException(
                        message: "Invalid AI file configuration error occurred, contact support.", 
                        httpResponseUrlNotFoundException);

                throw createAIFileDependencyException(
                    invalidConfigurationAIFileException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIFileException =
                    new UnauthorizedAIFileException(
                        message: "Unauthorized AI file request, fix errors and try again.", 
                        httpResponseUnauthorizedException);

                throw createAIFileDependencyException(
                    unauthorizedAIFileException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIFileException =
                    new UnauthorizedAIFileException(
                        message: "Unauthorized AI file request, fix errors and try again.", 
                        httpResponseForbiddenException);

                throw createAIFileDependencyException(
                    unauthorizedAIFileException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAIFileException =
                    new ExcessiveCallAIFileException(
                        message: "Excessive call error occurred, limit your calls.", 
                        httpResponseTooManyRequestsException);

                throw createAIFileDependencyValidationException(
                    excessiveCallAIFileException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIFileException =
                    new FailedServerAIFileException(
                        message: "Failed AI file server error occurred, contact support.", 
                        httpResponseException);

                throw createAIFileDependencyException(
                    failedServerAIFileException);
            }
            catch (Exception exception)
            {
                var failedAIFileServiceException =
                    new FailedAIFileServiceException(
                        message: "Failed AI file service error occurred, contact support.", 
                        exception);

                throw new AIFileServiceException(
                   message: "AI file service error occurred, contact support.",
                   failedAIFileServiceException);
            }
        }

        private static AIFileDependencyException createAIFileDependencyException(Xeption innerException)
        {
            return new AIFileDependencyException(
                message: "AI file dependency error occurred, contact support.",
                innerException);
        }

        private static AIFileDependencyValidationException createAIFileDependencyValidationException(Xeption innerException)
        {
            return new AIFileDependencyValidationException(
                message: "AI file dependency validation error occurred, contact support.",
                innerException);
        }
    }
}