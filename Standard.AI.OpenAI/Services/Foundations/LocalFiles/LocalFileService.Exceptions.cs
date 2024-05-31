// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions;
using Xeptions;

namespace Standard.AI.OpenAI.Services.Foundations.LocalFiles
{
    internal partial class LocalFileService
    {
        private delegate Stream ReturningStreamFunction();

        private Stream TryCatch(ReturningStreamFunction returningStreamFunction)
        {
            try
            {
                return returningStreamFunction();
            }
            catch (InvalidLocalFileException invalidFileException)
            {
                throw new LocalFileValidationException(invalidFileException);
            }
            catch (ArgumentException argumentException)
            {
                var invalidFileException = new InvalidLocalFileException(argumentException);

                throw createLocalFileDependencyValidationException(
                    invalidFileException);
            }
            catch (PathTooLongException pathTooLongException)
            {
                var invalidFileException = new InvalidLocalFileException(pathTooLongException);

                throw createLocalFileDependencyValidationException(
                    invalidFileException);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                var notFoundFileException =
                    new NotFoundLocalFileException(fileNotFoundException);

                throw createLocalFileDependencyValidationException(
                    notFoundFileException);
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                var notFoundFileException =
                    new NotFoundLocalFileException(directoryNotFoundException);

                throw createLocalFileDependencyValidationException(
                    notFoundFileException);
            }
            catch (IOException ioException)
            {
                var failedFileException =
                    new FailedLocalFileDependencyException(ioException);

                throw new LocalFileDependencyException(failedFileException);
            }
            catch (NotSupportedException notSupportedException)
            {
                var failedFileException =
                    new FailedLocalFileDependencyException(notSupportedException);

                throw new LocalFileDependencyException(failedFileException);
            }
            catch (Exception exception)
            {
                var failedLocalFileServiceException =
                    new FailedLocalFileServiceException(exception);

                throw new LocalFileServiceException(failedLocalFileServiceException);
            }
        }

        private static LocalFileDependencyValidationException createLocalFileDependencyValidationException(Xeption innerException)
        {
            return new LocalFileDependencyValidationException(
                message: "Local file dependency validation error occurred, fix the errors and try again.",
                innerException);
        }
    }
}
