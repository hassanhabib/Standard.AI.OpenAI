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
                throw new LocalFileValidationException(
                    message: "Local file validation error occurred, fix error and try again.", 
                    invalidFileException);
            }
            catch (ArgumentException argumentException)
            {
                var invalidFileException =
                    CreateInvalidLocalFileException(
                        argumentException);

                throw CreateLocalFileDependencyValidationException(
                    invalidFileException);
            }
            catch (PathTooLongException pathTooLongException)
            {
                var invalidFileException =
                    CreateInvalidLocalFileException(
                        pathTooLongException);

                throw CreateLocalFileDependencyValidationException(
                    invalidFileException);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                var notFoundFileException =
                    CreateNotFoundLocalFileException(
                        fileNotFoundException);

                throw CreateLocalFileDependencyValidationException(
                    notFoundFileException);
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                var notFoundFileException =
                    CreateNotFoundLocalFileException(
                        directoryNotFoundException);

                throw CreateLocalFileDependencyValidationException(
                    notFoundFileException);
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

                throw new LocalFileServiceException(
                    message: "Local file service error occurred, contact support.", 
                    failedLocalFileServiceException);
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

        private static LocalFileDependencyValidationException CreateLocalFileDependencyValidationException(Xeption innerException)
        {
            return new LocalFileDependencyValidationException(
                message: "Local file dependency validation error occurred, fix the errors and try again.",
                innerException);
        }

        private static NotFoundLocalFileException CreateNotFoundLocalFileException(Exception innerException)
        {
            return new NotFoundLocalFileException(
                message: "Not found local file error occurred, fix error and try again.",
                innerException);
        }

    }
}
