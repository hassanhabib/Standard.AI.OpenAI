// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions;
using Xeptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
    {
        [Fact]
        private async Task ShouldThrowDependencyExceptionOnRemoveByIdIfUrlNotFoundAsync()
        {
            // given
            string someFileId = CreateRandomString();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationFileException =
                new InvalidConfigurationAIFileException(
                    message: "Invalid AI file configuration error occurred, contact support.",
                        innerException: httpResponseUrlNotFoundException);

            var expectedFileDependencyException =
                createAIFileDependencyException(
                        innerException: invalidConfigurationFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.aiFileService.RemoveFileByIdAsync(someFileId);

            AIFileDependencyException actualFileDependencyException =
                await Assert.ThrowsAsync<AIFileDependencyException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyException.Should().BeEquivalentTo(
                expectedFileDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        private async Task ShouldThrowDependencyExceptionOnRemoveByIdIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            string someFileId = CreateRandomString();

            var unauthorizedFileException =
                new UnauthorizedAIFileException(
                    message: "Unauthorized AI file request, fix errors and try again.",
                        innerException: unauthorizedException);

            var expectedFileDependencyException =
                createAIFileDependencyException(
                        innerException: unauthorizedFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(unauthorizedException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.aiFileService.RemoveFileByIdAsync(someFileId);

            AIFileDependencyException actualFileDependencyException =
                await Assert.ThrowsAsync<AIFileDependencyException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyException.Should().BeEquivalentTo(
                expectedFileDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfFileNotFoundOccurredAsync()
        {
            // given
            string someFileId = CreateRandomString();

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException();

            var notFoundFileException =
                new NotFoundAIFileException(
                    message: "Not found AI file error occurred, fix errors and try again.",
                        innerException: httpResponseNotFoundException);

            var expectedFileDependencyValidationException =
                createAIFileDependencyValidationException(
                        innerException: notFoundFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(httpResponseNotFoundException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.aiFileService.RemoveFileByIdAsync(someFileId);

            AIFileDependencyValidationException actualFileDependencyValidationException =
                await Assert.ThrowsAsync<AIFileDependencyValidationException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyValidationException.Should().BeEquivalentTo(
                expectedFileDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfBadRequestOccurredAsync()
        {
            // given
            string someFileId = CreateRandomString();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidFileException =
                new InvalidAIFileException(
                    message: "Invalid AI file error occurred, fix errors and try again.",
                        innerException: httpResponseBadRequestException);

            var expectedFileDependencyValidationException =
                createAIFileDependencyValidationException(
                        innerException: invalidFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.aiFileService.RemoveFileByIdAsync(someFileId);

            AIFileDependencyValidationException actualFileDependencyValidationException =
                await Assert.ThrowsAsync<AIFileDependencyValidationException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyValidationException.Should().BeEquivalentTo(
                expectedFileDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfTooManyRequestsOccurredAsync()
        {
            // given
            string someFileId = CreateRandomString();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallFileException =
                new ExcessiveCallAIFileException(
                    message: "Excessive call error occurred, limit your calls.",
                        innerException: httpResponseTooManyRequestsException);

            var expectedFileDependencyValidationException =
                createAIFileDependencyValidationException(
                        innerException: excessiveCallFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.aiFileService.RemoveFileByIdAsync(someFileId);

            AIFileDependencyValidationException actualFileDependencyValidationException =
                await Assert.ThrowsAsync<AIFileDependencyValidationException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyValidationException.Should().BeEquivalentTo(
                expectedFileDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyExceptionOnRemoveByIdIfHttpResponseErrorOccurredAsync()
        {
            // given
            string someFileId = CreateRandomString();
            var httpResponseException = new HttpResponseException();

            var failedServerFileException =
                new FailedServerAIFileException(
                    message: "Failed AI file server error occurred, contact support.",
                    innerException: httpResponseException);

            var expectedFileDependencyException =
                createAIFileDependencyException(
                        innerException: failedServerFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.aiFileService.RemoveFileByIdAsync(someFileId);

            AIFileDependencyException actualFileDependencyException =
                await Assert.ThrowsAsync<AIFileDependencyException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyException.Should().BeEquivalentTo(
                expectedFileDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowServiceExceptionOnRemoveByIdIfServiceErrorOccurredAsync()
        {
            // given
            string someFileId = CreateRandomString();
            var serviceException = new Exception();

            var failedFileServiceException =
                new FailedAIFileServiceException(
                    message: "Failed AI file service error occurred, contact support.",
                        innerException: serviceException);

            var expectedFileServiceException =
                createAIFileValidationException(
                        innerException: failedFileServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.aiFileService.RemoveFileByIdAsync(someFileId);

            AIFileServiceException actualFileServiceException =
                await Assert.ThrowsAsync<AIFileServiceException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileServiceException.Should().BeEquivalentTo(
                expectedFileServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
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