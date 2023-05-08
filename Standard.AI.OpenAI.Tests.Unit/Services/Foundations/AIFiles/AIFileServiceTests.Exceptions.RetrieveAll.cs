// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfUrlNotFoundAsync()
        {
            // given
            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationAIFileException =
                new InvalidConfigurationAIFileException(
                    httpResponseUrlNotFoundException);

            var expectedAIFileDependencyException =
                new AIFileDependencyException(
                    invalidConfigurationAIFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllFilesAsync())
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<IEnumerable<AIFileResponse>> getAllFilesTask =
               this.aiFileService.RetrieveAllFilesAsync();

            AIFileDependencyException
                actualAIFileDependencyException =
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
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfUnauthorizedAsync(
           HttpResponseException unauthorizedException)
        {
            // given
            var unauthorizedAIFileException =
                new UnauthorizedAIFileException(unauthorizedException);

            var expectedAIFileDependencyException =
                new AIFileDependencyException(unauthorizedAIFileException);

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfTooManyRequestsOccurredAsync()
        {
            // given
            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallAIFileException =
                new ExcessiveCallAIFileException(
                    httpResponseTooManyRequestsException);

            var expectedAIFileDependencyException =
                new AIFileDependencyException(
                    excessiveCallAIFileException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllFilesAsync())
                    .ThrowsAsync(httpResponseTooManyRequestsException);

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfServerErrorOccurredAsync()
        {
            // given
            var httpResponseException =
                new HttpResponseException();

            var failedServerAIFileException =
                new FailedServerAIFileException(
                    httpResponseException);

            var expectedAIFileDependencyException =
                new AIFileDependencyException(
                    failedServerAIFileException);

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
    }
}
