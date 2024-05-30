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
                throw createAIFileValidationException(
                    nullAIFileException);
            }
            catch (InvalidAIFileException invalidFileException)
            {
                throw createAIFileValidationException(
                    invalidFileException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationFileException =
                    createInvalidConfigurationAIFileException(
                        httpResponseUrlNotFoundException);

                throw createAIFileDependencyException(
                    invalidConfigurationFileException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIFileException =
                    createUnauthorizedAIFileException(
                        httpResponseUnauthorizedException);

                throw createAIFileDependencyException(
                    unauthorizedAIFileException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIFileException =
                    createUnauthorizedAIFileException(
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
                    createExcessiveCallAIFileException(
                        httpResponseTooManyRequestsException);

                throw createAIFileDependencyValidationException(
                    excessiveCallAIFileException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIFileException =
                    createFailedServerAIFileException(
                        httpResponseException);

                throw createAIFileDependencyException(
                    failedServerAIFileException);
            }
            catch (Exception exception)
            {
                var failedAIFileServiceException =
                    createFailedAIFileServiceException(
                        exception);

                throw createAIFileServiceException(
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
                    createInvalidConfigurationAIFileException(
                        httpResponseUrlNotFoundException);

                throw createAIFileDependencyException(
                    invalidConfigurationAIFileException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAIFileException =
                    createUnauthorizedAIFileException(
                        httpResponseUnauthorizedException);

                throw createAIFileDependencyException(
                    unauthorizedAIFileException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAIFileException =
                    createUnauthorizedAIFileException(
                        httpResponseForbiddenException);

                throw createAIFileDependencyException(
                    unauthorizedAIFileException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAIFileException =
                    createExcessiveCallAIFileException(
                        httpResponseTooManyRequestsException);

                throw createAIFileDependencyValidationException(
                    excessiveCallAIFileException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAIFileException =
                    createFailedServerAIFileException(
                        httpResponseException);

                throw createAIFileDependencyException(
                    failedServerAIFileException);
            }
            catch (Exception exception)
            {
                var failedAIFileServiceException =
                    createFailedAIFileServiceException(
                        exception);

                throw createAIFileServiceException(
                   failedAIFileServiceException);
            }
        }

    }
}