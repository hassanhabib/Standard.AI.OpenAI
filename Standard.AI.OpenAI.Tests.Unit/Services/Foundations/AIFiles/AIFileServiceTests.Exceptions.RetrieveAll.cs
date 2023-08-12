// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
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
        private async Task ShouldThrowDependencyExceptionOnRetrieveAllIfUrlNotFoundAsync()
        {
            // given
            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationAIFileException =
                new InvalidConfigurationAIFileException(
                    message: "Invalid AI file configuration error occurred, contact support.",
                        innerException: httpResponseUrlNotFoundException);

            var expectedAIFileDependencyException =
                new AIFileDependencyException(
                    message: "AI file dependency error occurred, contact support.",
                        innerException: invalidConfigurationAIFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllFilesAsync())
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<IEnumerable<AIFileResponse>> getAllFilesTask =
                this.aiFileService.RetrieveAllFilesAsync();

            AIFileDependencyException actualAIFileDependencyException =
                await Assert.ThrowsAsync<AIFileDependencyException>(
                    getAllFilesTask.AsTask);

            // then
            actualAIFileDependencyException.Should().BeEquivalentTo(
                expectedAIFileDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllFilesAsync(),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        private async Task ShouldThrowDependencyExceptionOnRetrieveAllIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            var unauthorizedAIFileException =
                new UnauthorizedAIFileException(
                    message: "Unauthorized AI file request, fix errors and try again.",
                        innerException: unauthorizedException);

            var expectedAIFileDependencyException =
                new AIFileDependencyException(
                    message: "AI file dependency error occurred, contact support.",
                        innerException: unauthorizedAIFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllFilesAsync())
                    .ThrowsAsync(unauthorizedException);

            // when
            ValueTask<IEnumerable<AIFileResponse>> getAllFilesTask =
                this.aiFileService.RetrieveAllFilesAsync();

            AIFileDependencyException actualAIFileDependencyException =
                await Assert.ThrowsAsync<AIFileDependencyException>(
                    getAllFilesTask.AsTask);

            // then
            actualAIFileDependencyException.Should().BeEquivalentTo(
                expectedAIFileDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllFilesAsync(),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllIfTooManyRequestsOccurredAsync()
        {
            // given
            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallAIFileException =
                new ExcessiveCallAIFileException(
                    message: "Excessive call error occurred, limit your calls.",
                        innerException: httpResponseTooManyRequestsException);

            var expectedAIFileDependencyValidationException =
                new AIFileDependencyValidationException(
                    message: "AI file dependency validation error occurred, contact support.",
                        innerException: excessiveCallAIFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllFilesAsync())
                    .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<IEnumerable<AIFileResponse>> retrieveAllFilesTask =
                this.aiFileService.RetrieveAllFilesAsync();

            AIFileDependencyValidationException actualAIFileDependencyValidationException =
                await Assert.ThrowsAsync<AIFileDependencyValidationException>(
                    retrieveAllFilesTask.AsTask);

            // then
            actualAIFileDependencyValidationException.Should().BeEquivalentTo(
                expectedAIFileDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllFilesAsync(),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyExceptionOnRetrieveAllIfHttpResponseErrorOccurredAsync()
        {
            // given
            var httpResponseException =
                new HttpResponseException();

            var failedServerAIFileException =
                new FailedServerAIFileException(
                    message: "Failed AI file server error occurred, contact support.",
                        innerException: httpResponseException);

            var expectedAIFileDependencyException =
                new AIFileDependencyException(
                    message: "AI file dependency error occurred, contact support.",
                        innerException: failedServerAIFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllFilesAsync())
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<IEnumerable<AIFileResponse>> retrieveAllFilesTask =
                this.aiFileService.RetrieveAllFilesAsync();

            AIFileDependencyException actualAIFileDependencyException =
                await Assert.ThrowsAsync<AIFileDependencyException>(
                    retrieveAllFilesTask.AsTask);

            // then
            actualAIFileDependencyException.Should().BeEquivalentTo(
                expectedAIFileDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllFilesAsync(),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccurredAsync()
        {
            // given
            var serviceException = new Exception();

            var failedAIFileServiceException =
                new FailedAIFileServiceException(
                    message: "Failed AI file service error occurred, contact support.",
                        innerException: serviceException);

            var expectedAIFileServiceException =
                new AIFileServiceException(
                    message: "AI file service error occurred, contact support.",
                        innerException: failedAIFileServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllFilesAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<IEnumerable<AIFileResponse>> retrieveAllFilesTask =
                this.aiFileService.RetrieveAllFilesAsync();

            AIFileServiceException actualAIFileServiceException =
                await Assert.ThrowsAsync<AIFileServiceException>(
                    retrieveAllFilesTask.AsTask);

            // then
            actualAIFileServiceException.Should().BeEquivalentTo(
                expectedAIFileServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllFilesAsync(),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}