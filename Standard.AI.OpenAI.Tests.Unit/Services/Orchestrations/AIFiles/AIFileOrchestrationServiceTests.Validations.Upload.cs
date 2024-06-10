// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Orchestrations.AIFiles.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Orchestrations.AIFiles
{
    public partial class AIFileOrchestrationServiceTests
    {
        [Fact]
        private async Task ShouldThrowValidationExceptionOnUploadIfFileIsNullAsync()
        {
            // given
            AIFile nullAIFile = null;

            var nullAIFileOrchestrationException =
                new NullAIFileOrchestrationException(
                    message: "AI file is null.");

            var expectedAIFileOrchestrationValidationException =
                new AIFileOrchestrationValidationException(
                    message: "AI file validation error occurred, fix errors and try again.",
                        innerException: nullAIFileOrchestrationException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileOrchestrationService.UploadFileAsync(
                    nullAIFile);

            AIFileOrchestrationValidationException
                actualAIFileOrchestrationValidatioNException =
                    await Assert.ThrowsAsync<AIFileOrchestrationValidationException>(
                        uploadFileTask.AsTask);

            // then
            actualAIFileOrchestrationValidatioNException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationValidationException);

            this.localFileServiceMock.Verify(service =>
                service.ReadFile(It.IsAny<string>()),
                    Times.Never);

            this.aiFileServiceMock.Verify(service =>
                service.UploadFileAsync(It.IsAny<AIFile>()),
                    Times.Never);

            this.localFileServiceMock.VerifyNoOtherCalls();
            this.aiFileServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowValidationExceptionOnUploadIfRequestIsNullAsync()
        {
            // given
            var invalidAIFile = new AIFile();

            var invalidAIFileOrchestrationException = 
                new InvalidAIFileOrchestrationException(
                    message: "AI file is invalid.");

            invalidAIFileOrchestrationException.AddData(
                key: nameof(AIFileRequest),
                values: "Object is required");

            var expectedAIFileOrchestrationValidationException =
                new AIFileOrchestrationValidationException(
                    message: "AI file validation error occurred, fix errors and try again.",
                        innerException: invalidAIFileOrchestrationException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileOrchestrationService.UploadFileAsync(
                    invalidAIFile);

            AIFileOrchestrationValidationException
                actualAIFileOrchestrationValidatioNException =
                    await Assert.ThrowsAsync<AIFileOrchestrationValidationException>(
                        uploadFileTask.AsTask);

            // then
            actualAIFileOrchestrationValidatioNException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationValidationException);

            this.localFileServiceMock.Verify(service =>
                service.ReadFile(It.IsAny<string>()),
                    Times.Never);

            this.aiFileServiceMock.Verify(service =>
                service.UploadFileAsync(It.IsAny<AIFile>()),
                    Times.Never);

            this.localFileServiceMock.VerifyNoOtherCalls();
            this.aiFileServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        private async Task ShouldThrowValidationExceptionIfRequestIsInvalidAsync(
            string invalidName)
        {
            // given
            AIFile randomAIFile = CreateRandomAIFile();
            AIFile invalidAIFile = randomAIFile;
            invalidAIFile.Request.Name = invalidName;

            var invalidAIFileOrchestrationException =
                new InvalidAIFileOrchestrationException(
                    message: "AI file is invalid.");

            invalidAIFileOrchestrationException.AddData(
                key: nameof(AIFileRequest.Name),
                "Value is required");

            var expectedAIFileOrchestrationValidationException =
                new AIFileOrchestrationValidationException(
                    message: "AI file validation error occurred, fix errors and try again.",
                        innerException: invalidAIFileOrchestrationException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileOrchestrationService.UploadFileAsync(
                    invalidAIFile);

            AIFileOrchestrationValidationException
                actualAIFileOrchestrationValidatioNException =
                    await Assert.ThrowsAsync<AIFileOrchestrationValidationException>(
                        uploadFileTask.AsTask);

            // then
            actualAIFileOrchestrationValidatioNException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationValidationException);

            this.localFileServiceMock.Verify(service =>
                service.ReadFile(It.IsAny<string>()),
                    Times.Never);

            this.aiFileServiceMock.Verify(service =>
                service.UploadFileAsync(It.IsAny<AIFile>()),
                    Times.Never);

            this.localFileServiceMock.VerifyNoOtherCalls();
            this.aiFileServiceMock.VerifyNoOtherCalls();
        }
    }
}