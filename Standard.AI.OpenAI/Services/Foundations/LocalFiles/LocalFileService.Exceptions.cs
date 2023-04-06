// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions;

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
            catch (InvalidFileException invalidFileException)
            {
                throw new FileValidationException(invalidFileException);
            }
            catch (ArgumentException argumentException)
            {
                var invalidFileException = new InvalidFileException(argumentException);

                throw new FileDependencyValidationException(invalidFileException);
            }
            catch (PathTooLongException pathTooLongException)
            {
                var invalidFileException = new InvalidFileException(pathTooLongException);

                throw new FileDependencyValidationException(invalidFileException);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                var notFoundFileException =
                    new NotFoundFileException(fileNotFoundException);

                throw new FileDependencyValidationException(notFoundFileException);
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                var notFoundFileException =
                    new NotFoundFileException(directoryNotFoundException);

                throw new FileDependencyValidationException(notFoundFileException);
            }
            catch (IOException ioException)
            {
                var failedFileException =
                    new FailedFileException(ioException);

                throw new FileDependencyException(failedFileException);
            }
            catch (NotSupportedException notSupportedException)
            {
                var failedFileException =
                    new FailedFileException(notSupportedException);

                throw new FileDependencyException(failedFileException);
            }
            catch (Exception exception)
            {
                var failedLocalFileServiceException =
                    new FailedLocalFileServiceException(exception);

                throw new LocalFileServiceException(failedLocalFileServiceException);
            }
        }
    }
}
