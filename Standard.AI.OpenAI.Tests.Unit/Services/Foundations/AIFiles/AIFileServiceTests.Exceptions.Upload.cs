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
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIFiles;
using Xeptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
    {
        [Fact]
        private async Task ShouldThrowDependencyExceptionOnUploadIfUrlNotFoundAsync()
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();

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
                broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()))
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileService.UploadFileAsync(someAIFile);

            AIFileDependencyException actualFileDependencyException =
                await Assert.ThrowsAsync<AIFileDependencyException>(
                    uploadFileTask.AsTask);

            // then
            actualFileDependencyException.Should().BeEquivalentTo(
                expectedFileDependencyException);

            this.openAIBrokerMock.Verify(broker =>
               broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()),
                   Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        private async Task ShouldThrowDependencyExceptionOnUploadIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();

            var unauthorizedFileException =
                new UnauthorizedAIFileException(
                    message: "Unauthorized AI file request, fix errors and try again.",
                        innerException: unauthorizedException);

            var expectedFileDependencyException =
                createAIFileDependencyException(
                        innerException: unauthorizedFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()))
                    .ThrowsAsync(unauthorizedException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileService.UploadFileAsync(someAIFile);

            AIFileDependencyException actualFileDependencyException =
                await Assert.ThrowsAsync<AIFileDependencyException>(
                    uploadFileTask.AsTask);

            // then
            actualFileDependencyException.Should().BeEquivalentTo(
                expectedFileDependencyException);

            this.openAIBrokerMock.Verify(broker =>
               broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()),
                   Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnUploadIfBadRequestErrorOccurredAsync()
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var notFoundFileException =
                new InvalidAIFileException(
                    message: "Invalid AI file error occurred, fix errors and try again.",
                        innerException: httpResponseBadRequestException);

            var expectedFileDependencyValidationException =
                new AIFileDependencyValidationException(
                    message: "AI file dependency validation error occurred, contact support.",
                        innerException: notFoundFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileService.UploadFileAsync(someAIFile);

            AIFileDependencyValidationException actualFileDependencyValidationException =
                await Assert.ThrowsAsync<AIFileDependencyValidationException>(
                    uploadFileTask.AsTask);

            // then
            actualFileDependencyValidationException.Should().BeEquivalentTo(
                expectedFileDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
               broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()),
                   Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowServiceExceptionOnUploadIfServicetErrorOccurredAsync()
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();
            var serviceException = new Exception();

            var failedAIFileServiceException =
                new FailedAIFileServiceException(
                    message: "Failed AI file service error occurred, contact support.",
                        innerException: serviceException);

            var expectedFileServiceException =
                new AIFileServiceException(
                    message: "AI file service error occurred, contact support.",
                        innerException: failedAIFileServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileService.UploadFileAsync(someAIFile);

            AIFileServiceException actualAIFileServiceException =
                await Assert.ThrowsAsync<AIFileServiceException>(
                    uploadFileTask.AsTask);

            // then
            actualAIFileServiceException.Should().BeEquivalentTo(
                expectedFileServiceException);

            this.openAIBrokerMock.Verify(broker =>
               broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()),
                   Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }


    }
}