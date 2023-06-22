// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
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
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveFileIfDependencyValidationErrorOccursAsync(
            Xeption dependencyValidationException)
        {
            // given 
            string someFileId = CreateRandomFileId();

            var expectedAIFileOrchestrationDependencyValidationException =
                new AIFileOrchestrationDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

            this.aiFileServiceMock.Setup(service => 
                service.RemoveFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(dependencyValidationException);

            // when
            ValueTask<AIFile> removeFileTask = 
                this.aiFileOrchestrationService.RemoveFileByIdAsync(
                    someFileId);

            AIFileOrchestrationDependencyValidationException 
                actualAIFileOrchestrationDependencyValidationException =
                    await Assert.ThrowsAsync<AIFileOrchestrationDependencyValidationException>(
                        removeFileTask.AsTask);

            // then
            actualAIFileOrchestrationDependencyValidationException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationDependencyValidationException);

            this.aiFileServiceMock.Verify(service => 
                service.RemoveFileByIdAsync(It.IsAny<string>()), 
                    Times.Once);

            this.aiFileServiceMock.VerifyNoOtherCalls();
            this.localFileServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRemoveFileIfDependencyExceptionErrorOccursAsync(
            Xeption dependencyException)
        {
            // given 
            string someFileId = CreateRandomFileId();

            var expectedAIFileOrchestrationDependencyException =
                new AIFileOrchestrationDependencyException(
                    dependencyException.InnerException as Xeption);

            this.aiFileServiceMock.Setup(service => 
                service.RemoveFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<AIFile> removeFileTask =
                this.aiFileOrchestrationService.RemoveFileByIdAsync(
                    someFileId);

            AIFileOrchestrationDependencyException 
                actualAIFileOrchestrationDependencyException =
                    await Assert.ThrowsAsync<AIFileOrchestrationDependencyException>(
                        removeFileTask.AsTask);

            // then
            actualAIFileOrchestrationDependencyException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationDependencyException);

            this.aiFileServiceMock.Verify(service => 
                service.RemoveFileByIdAsync(It.IsAny<string>()), 
                    Times.Once);

            this.aiFileServiceMock.VerifyNoOtherCalls();
            this.localFileServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveFileIfExceptionOccursAsync()
        {
            // given 
            string someFileId = CreateRandomFileId();
            var serviceException = new Exception();

            var failedAIFileOrchestrationServiceException = 
                new FailedAIFileOrchestrationServiceException(serviceException);
            
            var expectedAIFileOrchestrationServiceException = 
                new AIFileOrchestrationServiceException(failedAIFileOrchestrationServiceException);

            this.aiFileServiceMock.Setup(service => 
                service.RemoveFileByIdAsync(It.IsAny<string>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<AIFile> removeFileTask = 
                this.aiFileOrchestrationService.RemoveFileByIdAsync(someFileId);

            AIFileOrchestrationServiceException actualAIFileOrchestrationServiceException =
                await Assert.ThrowsAsync<AIFileOrchestrationServiceException>(
                    removeFileTask.AsTask);

            // then
            actualAIFileOrchestrationServiceException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationServiceException);

            this.aiFileServiceMock.Verify(service => 
                service.RemoveFileByIdAsync(It.IsAny<string>()), 
                Times.Once);

            this.aiFileServiceMock.VerifyNoOtherCalls();
            this.localFileServiceMock.VerifyNoOtherCalls();
        }
    }
}