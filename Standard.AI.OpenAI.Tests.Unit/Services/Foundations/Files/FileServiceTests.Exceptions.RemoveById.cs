// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Files
{
    public partial class FileServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfUrlNotFoundAsync()
        {
            // given
            string someFileId = GetRandomString();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationFileException =
                new InvalidConfigurationFileException(
                    httpResponseUrlNotFoundException);

            var expectedFileDependencyException =
                new FileDependencyException(
                    invalidConfigurationFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.fileService.RemoveFileByIdAsync(someFileId);

            FileDependencyException actualFileDependencyException =
                await Assert.ThrowsAsync<FileDependencyException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyException.Should().BeEquivalentTo(
                expectedFileDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            string someFileId = GetRandomString();

            var unauthorizedFileException =
                new UnauthorizedFileException(unauthorizedException);

            var expectedFileDependencyException =
                new FileDependencyException(unauthorizedFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(unauthorizedException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.fileService.RemoveFileByIdAsync(someFileId);

            FileDependencyException actualFileDependencyException =
                await Assert.ThrowsAsync<FileDependencyException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyException.Should().BeEquivalentTo(
                expectedFileDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfFileNotFoundOccurredAsync()
        {
            // given
            string someFileId = GetRandomString();

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException();

            var notFoundFileException =
                new NotFoundFileException(
                    httpResponseNotFoundException);

            var expectedFileDependencyValidationException =
                new FileDependencyValidationException(
                    notFoundFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(httpResponseNotFoundException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.fileService.RemoveFileByIdAsync(someFileId);

            FileDependencyValidationException actualFileDependencyValidationException =
                await Assert.ThrowsAsync<FileDependencyValidationException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyValidationException.Should().BeEquivalentTo(
                expectedFileDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfBadRequestOccurredAsync()
        {
            // given
            string someFileId = GetRandomString();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidFileException =
                new InvalidFileException(
                    httpResponseBadRequestException);

            var expectedFileDependencyValidationException =
                new FileDependencyValidationException(
                    invalidFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.fileService.RemoveFileByIdAsync(someFileId);

            FileDependencyValidationException actualFileDependencyValidationException =
                await Assert.ThrowsAsync<FileDependencyValidationException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyValidationException.Should().BeEquivalentTo(
                expectedFileDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfTooManyRequestsOccurredAsync()
        {
            // given
            string someFileId = GetRandomString();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallFileException =
                new ExcessiveCallFileException(
                    httpResponseTooManyRequestsException);

            var expectedFileDependencyValidationException =
                new FileDependencyValidationException(
                    excessiveCallFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.fileService.RemoveFileByIdAsync(someFileId);

            FileDependencyValidationException actualFileDependencyValidationException =
                await Assert.ThrowsAsync<FileDependencyValidationException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyValidationException.Should().BeEquivalentTo(
                expectedFileDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfHttpResponseErrorOccurredAsync()
        {
            // given
            string someFileId = GetRandomString();
            var httpResponseException = new HttpResponseException();

            var failedServerFileException =
                new FailedServerFileException(
                    httpResponseException);

            var expectedFileDependencyException =
                new FileDependencyException(failedServerFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.fileService.RemoveFileByIdAsync(someFileId);

            FileDependencyException actualFileDependencyException =
                await Assert.ThrowsAsync<FileDependencyException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileDependencyException.Should().BeEquivalentTo(
                expectedFileDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveByIdIfServiceErrorOccurredAsync()
        {
            // given
            string someFileId = GetRandomString();
            var serviceException = new Exception();

            var failedFileServiceException =
                new FailedFileServiceException(serviceException);

            var expectedFileServiceException =
                new FileServiceException(
                    failedFileServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.fileService.RemoveFileByIdAsync(someFileId);

            FileServiceException actualFileServiceException =
                await Assert.ThrowsAsync<FileServiceException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileServiceException.Should().BeEquivalentTo(
                expectedFileServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}