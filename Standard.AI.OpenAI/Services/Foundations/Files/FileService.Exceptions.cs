// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

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
        }
    }
}