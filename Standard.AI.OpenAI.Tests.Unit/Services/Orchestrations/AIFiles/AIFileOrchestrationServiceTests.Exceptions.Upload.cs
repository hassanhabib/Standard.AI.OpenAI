// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Orchestrations.AIFiles.Exceptions;
using Xeptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Orchestrations.AIFiles
{
    public partial class AIFileOrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnUploadIfDependencyValidationErrorOccursAsync(
            Xeption dependencyValidationException)
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();

            var expectedAIFileOrchestrationDependencyValidationException =
                new AIFileOrchestrationDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

            this.aiFileServiceMock.Setup(service =>
                service.UploadFileAsync(It.IsAny<AIFile>()))
                    .ThrowsAsync(dependencyValidationException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileOrchestrationService.UploadFileAsync(
                    someAIFile);

            AIFileOrchestrationDependencyValidationException
                actualAIFileOrchestrationDependencyValidationException =
                    await Assert.ThrowsAsync<AIFileOrchestrationDependencyValidationException>(
                        uploadFileTask.AsTask);

            // then
            actualAIFileOrchestrationDependencyValidationException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationDependencyValidationException);

            this.aiFileServiceMock.Verify(service =>
                service.UploadFileAsync(It.IsAny<AIFile>()),
                    Times.Once);

            this.localFileServiceMock.Verify(service =>
                service.ReadFile(It.IsAny<string>()),
                    Times.Never);

            this.localFileServiceMock.VerifyNoOtherCalls();
            this.aiFileServiceMock.VerifyNoOtherCalls();
        }
    }
}
