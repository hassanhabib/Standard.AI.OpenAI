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
                var invalidFileException =
                    CreateInvalidLocalFileException(
                        argumentException);

                throw new LocalFileDependencyValidationException(invalidFileException);
            }
            catch (PathTooLongException pathTooLongException)
            {
                var invalidFileException =
                    CreateInvalidLocalFileException(
                        pathTooLongException);

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
                    CreateFailedLocalFileDependencyException(
                        ioException);

                throw CreateLocalFileDependencyException(
                    failedFileException);
            }
            catch (NotSupportedException notSupportedException)
            {
                var failedFileException =
                    CreateFailedLocalFileDependencyException(
                        notSupportedException);

                throw CreateLocalFileDependencyException(
                    failedFileException);
            }
            catch (Exception exception)
            {
                var failedLocalFileServiceException =
                    new FailedLocalFileServiceException(
                        message: "Failed local file service error occurred, contact support.", 
                        exception);

                throw new LocalFileServiceException(failedLocalFileServiceException);
            }
        }


        private static FailedLocalFileDependencyException CreateFailedLocalFileDependencyException(Exception innerException)
        {
            return new FailedLocalFileDependencyException(
                message: "Failed local file error occurred, contact support.",
                innerException);
        }

        private static InvalidLocalFileException CreateInvalidLocalFileException(Exception innerException)
        {
            return new InvalidLocalFileException(
                message: "Invalid local file error occurred, fix error and try again.",
                innerException);
        }

        private static LocalFileDependencyException CreateLocalFileDependencyException(Xeption innerException)
        {
            return new LocalFileDependencyException(
                message: "Local file dependency error occurred, contact support.",
                innerException);
        }











    }
}
