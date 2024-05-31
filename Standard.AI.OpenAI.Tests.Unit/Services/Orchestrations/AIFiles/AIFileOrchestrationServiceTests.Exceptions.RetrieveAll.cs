// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions;
using Standard.AI.OpenAI.Models.Services.Orchestrations.AIFiles.Exceptions;
using Xeptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Orchestrations.AIFiles
{
    public partial class AIFileOrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(AIFileServiceDependencyExceptions))]
        private async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyErrorOccursAsync(
            Xeption dependencyException)
        {
            // given
            var expectedAIFileOrchestrationDependencyException =
                new AIFileOrchestrationDependencyException(
                    message: "AI File dependency error occurred, contact support.",
                        innerException: dependencyException.InnerException as Xeption);

            this.aiFileServiceMock.Setup(service =>
                service.RetrieveAllFilesAsync())
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<IEnumerable<AIFileResponse>> retrieveAllFilesTask =
                this.aiFileOrchestrationService.RetrieveAllFilesAsync();

            AIFileOrchestrationDependencyException
                actualAIFileOrchestrationDependencyException =
                    await Assert.ThrowsAsync<AIFileOrchestrationDependencyException>(
                        retrieveAllFilesTask.AsTask);

            // then
            actualAIFileOrchestrationDependencyException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationDependencyException);

            this.aiFileServiceMock.Verify(service =>
                service.RetrieveAllFilesAsync(),
                Times.Once);

            this.aiFileServiceMock.VerifyNoOtherCalls();
            this.localFileServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllIfDependencyValidationErrorOccursAsync()
        {
            // given
            var someInnerException = new Xeption();


            var dependencyValidationException =
                createAIFileDependencyValidationException(
                        innerException: someInnerException);

            var expectedAIFileOrchestrationDependencyValidationException =
                new AIFileOrchestrationDependencyValidationException(
                    message: "AI file dependency validation error occurred, fix errors and try again.",
                        innerException: dependencyValidationException.InnerException as Xeption);

            this.aiFileServiceMock.Setup(service =>
                service.RetrieveAllFilesAsync())
                    .ThrowsAsync(dependencyValidationException);

            // when
            ValueTask<IEnumerable<AIFileResponse>> retrieveAllFilesTask =
                this.aiFileOrchestrationService.RetrieveAllFilesAsync();

            AIFileOrchestrationDependencyValidationException
                actualAIFileOrchestrationDependencyValidationException =
                    await Assert.ThrowsAsync<AIFileOrchestrationDependencyValidationException>(
                        retrieveAllFilesTask.AsTask);

            // then
            actualAIFileOrchestrationDependencyValidationException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationDependencyValidationException);

            this.aiFileServiceMock.Verify(service =>
                service.RetrieveAllFilesAsync(),
                    Times.Once);

            this.aiFileServiceMock.VerifyNoOtherCalls();
            this.localFileServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowServiceExceptionOnRetrieveAllIfExceptionOccursAsync()
        {
            // given
            var serviceException = new Exception();

            var failedAIFileOrchestrationServiceException =
                new FailedAIFileOrchestrationServiceException(
                    message: "Failed AI file service error occurred, contact support.",
                        innerException: serviceException);

            var expectedAIFileOrchestrationServiceException =
                new AIFileOrchestrationServiceException(
                    message: "AI File error occurred, contact support.",
                        innerException: failedAIFileOrchestrationServiceException);

            this.aiFileServiceMock.Setup(service =>
                service.RetrieveAllFilesAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<IEnumerable<AIFileResponse>> retrieveAllFilesTask =
                this.aiFileOrchestrationService.RetrieveAllFilesAsync();

            AIFileOrchestrationServiceException actualAIFileOrchestrationServiceException =
                await Assert.ThrowsAsync<AIFileOrchestrationServiceException>(
                    retrieveAllFilesTask.AsTask);

            // then
            actualAIFileOrchestrationServiceException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationServiceException);

            this.aiFileServiceMock.Verify(service =>
                service.RetrieveAllFilesAsync(),
                Times.Once);

            this.aiFileServiceMock.VerifyNoOtherCalls();
            this.localFileServiceMock.VerifyNoOtherCalls();
        }
    }
}