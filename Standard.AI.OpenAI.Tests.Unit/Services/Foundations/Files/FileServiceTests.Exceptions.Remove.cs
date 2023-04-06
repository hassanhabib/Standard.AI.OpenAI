// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;
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
            ValueTask<File> removeFileByIdTask =
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
            ValueTask<File> removeFileByIdTask =
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
    }
}