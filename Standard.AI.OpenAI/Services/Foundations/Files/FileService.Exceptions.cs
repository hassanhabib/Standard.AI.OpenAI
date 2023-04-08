// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;
using Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.Files
{
    internal partial class FileService
    {
        private delegate ValueTask<File> ReturningFileFunction();

        private async ValueTask<File> TryCatch(ReturningFileFunction returningFileFunction)
        {
            try
            {
                return await returningFileFunction();
            }
            catch (InvalidFileException invalidFileException)
            {
                throw new FileValidationException(invalidFileException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationFileException =
                    new InvalidConfigurationFileException(httpResponseUrlNotFoundException);

                throw new FileDependencyException(invalidConfigurationFileException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedFileException =
                    new UnauthorizedFileException(httpResponseUnauthorizedException);

                throw new FileDependencyException(unauthorizedFileException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedFileException =
                    new UnauthorizedFileException(httpResponseForbiddenException);

                throw new FileDependencyException(unauthorizedFileException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundFileException =
                    new NotFoundFileException(httpResponseNotFoundException);

                throw new FileDependencyValidationException(notFoundFileException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidFileException =
                    new InvalidFileException(httpResponseBadRequestException);

                throw new FileDependencyValidationException(invalidFileException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallFileException =
                    new ExcessiveCallFileException(httpResponseTooManyRequestsException);

                throw new FileDependencyValidationException(excessiveCallFileException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerFileException =
                    new FailedServerFileException(httpResponseException);

                throw new FileDependencyException(failedServerFileException);
            }
            catch (Exception exception)
            {
                var failedFileServiceException =
                    new FailedFileServiceException(exception);

                throw new FileServiceException(failedFileServiceException);
            }
        }
    }
}