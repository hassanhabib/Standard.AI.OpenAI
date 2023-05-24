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
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnUploadIfUrlNotFoundAsync()
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationFileException =
                new InvalidConfigurationAIFileException(
                    httpResponseUrlNotFoundException);

            var expectedFileDependencyException =
                new AIFileDependencyException(
                    invalidConfigurationFileException);

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
        public async Task ShouldThrowDependencyExceptionOnUploadIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();

            var unauthorizedFileException =
                new UnauthorizedAIFileException(unauthorizedException);

            var expectedFileDependencyException =
                new AIFileDependencyException(
                    unauthorizedFileException);

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
        public async Task ShouldThrowDependencyValidationExceptionOnUploadIfBadRequestErrorOccurredAsync()
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var notFoundFileException =
                new InvalidAIFileException(
                    httpResponseBadRequestException);

            var expectedFileDependencyValidationException =
                new AIFileDependencyValidationException(
                    notFoundFileException);

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
        public async Task ShouldThrowServiceExceptionOnUploadIfServicetErrorOccurredAsync()
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();
            var serviceException = new Exception();

            var failedAIFileServiceException =
                new FailedAIFileServiceException(
                    serviceException);

            var expectedFileServiceException =
                new AIFileServiceException(
                    failedAIFileServiceException);

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
