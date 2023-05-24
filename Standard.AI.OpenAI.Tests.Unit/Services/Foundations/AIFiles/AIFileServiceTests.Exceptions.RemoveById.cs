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
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfUrlNotFoundAsync()
        {
            // given
            string someFileId = CreateRandomString();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationFileException =
                new InvalidConfigurationAIFileException(
                    httpResponseUrlNotFoundException);

            var expectedFileDependencyException =
                new AIFileDependencyException(
                    invalidConfigurationFileException);

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
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            string someFileId = CreateRandomString();

            var unauthorizedFileException =
                new UnauthorizedAIFileException(unauthorizedException);

            var expectedFileDependencyException =
                new AIFileDependencyException(unauthorizedFileException);

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
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfFileNotFoundOccurredAsync()
        {
            // given
            string someFileId = CreateRandomString();

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException();

            var notFoundFileException =
                new NotFoundAIFileException(
                    httpResponseNotFoundException);

            var expectedFileDependencyValidationException =
                new AIFileDependencyValidationException(
                    notFoundFileException);

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
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfBadRequestOccurredAsync()
        {
            // given
            string someFileId = CreateRandomString();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidFileException =
                new InvalidAIFileException(
                    httpResponseBadRequestException);

            var expectedFileDependencyValidationException =
                new AIFileDependencyValidationException(
                    invalidFileException);

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
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfTooManyRequestsOccurredAsync()
        {
            // given
            string someFileId = CreateRandomString();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallFileException =
                new ExcessiveCallAIFileException(
                    httpResponseTooManyRequestsException);

            var expectedFileDependencyValidationException =
                new AIFileDependencyValidationException(
                    excessiveCallFileException);

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
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfHttpResponseErrorOccurredAsync()
        {
            // given
            string someFileId = CreateRandomString();
            var httpResponseException = new HttpResponseException();

            var failedServerFileException =
                new FailedServerAIFileException(
                    httpResponseException);

            var expectedFileDependencyException =
                new AIFileDependencyException(failedServerFileException);

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
        public async Task ShouldThrowServiceExceptionOnRemoveByIdIfServiceErrorOccurredAsync()
        {
            // given
            string someFileId = CreateRandomString();
            var serviceException = new Exception();

            var failedFileServiceException =
                new FailedAIFileServiceException(serviceException);

            var expectedFileServiceException =
                new AIFileServiceException(
                    failedFileServiceException);

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
    }
}