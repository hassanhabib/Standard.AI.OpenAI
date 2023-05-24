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
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyErrorOccursAsync(
            Xeption dependencyException)
        {
            // given
            var expectedAIFileOrchestrationDependencyException =
                new AIFileOrchestrationDependencyException(
                    dependencyException.InnerException as Xeption);

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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllIfDependencyValidationErrorOccursAsync()
        {
            // given
            var someInnerException = new Xeption();
            var dependencyValidationException = 
                new AIFileDependencyValidationException(someInnerException);

            var expectedAIFileOrchestrationDependencyValidationException =
                new AIFileOrchestrationDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

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
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfExceptionOccursAsync()
        {
            // given
            var serviceException = new Exception();

            var failedAIFileOrchestrationServiceException =
                new FailedAIFileOrchestrationServiceException(serviceException);

            var expectedAIFileOrchestrationServiceException =
                new AIFileOrchestrationServiceException(
                    failedAIFileOrchestrationServiceException);

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
