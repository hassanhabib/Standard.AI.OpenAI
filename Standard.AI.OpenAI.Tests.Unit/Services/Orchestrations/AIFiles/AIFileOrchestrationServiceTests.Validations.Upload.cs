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
        public async Task ShouldThrowValidationExceptionOnUploadIfFileIsNullAsync()
        {
            // given
            AIFile nullAIFile = null;

            var nullAIFileOrchestrationException =
                new NullAIFileOrchestrationException();

            var expectedAIFileOrchestrationValidationException =
                new AIFileOrchestrationValidationException(
                    nullAIFileOrchestrationException);

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
    }
}
