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

                throw new LocalFileDependencyValidationException(invalidFileException);
            }
            catch (PathTooLongException pathTooLongException)
            {
                var invalidFileException = new InvalidLocalFileException(pathTooLongException);

                throw new LocalFileDependencyValidationException(invalidFileException);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                var notFoundFileException =
                    new NotFoundLocalFileException(fileNotFoundException);

                throw new LocalFileDependencyValidationException(notFoundFileException);
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                var notFoundFileException =
                    new NotFoundLocalFileException(directoryNotFoundException);

                throw new LocalFileDependencyValidationException(notFoundFileException);
            }
            catch (IOException ioException)
            {
                var failedFileException =
                    new FailedLocalFileDependencyException(ioException);

                throw CreateLocalFileDependencyException(
                    failedFileException);
            }
            catch (NotSupportedException notSupportedException)
            {
                var failedFileException =
                    new FailedLocalFileDependencyException(notSupportedException);

                throw CreateLocalFileDependencyException(
                    failedFileException);
            }
            catch (Exception exception)
            {
                var failedLocalFileServiceException =
                    new FailedLocalFileServiceException(exception);

                throw new LocalFileServiceException(failedLocalFileServiceException);
            }
        }

        private static LocalFileDependencyException CreateLocalFileDependencyException(Xeption innerException)
        {
            return new LocalFileDependencyException(
                message: "Local file dependency error occurred, contact support.",
                innerException);
        }
    }
}
