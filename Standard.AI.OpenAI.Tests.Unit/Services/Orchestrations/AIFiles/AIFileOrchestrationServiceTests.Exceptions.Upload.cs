﻿// ---------------------------------------------------------------------------------- 
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
        private async Task ShouldThrowDependencyValidationExceptionOnUploadIfDependencyValidationErrorOccursAsync(
            Xeption dependencyValidationException)
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();

            var expectedAIFileOrchestrationDependencyValidationException =
                new AIFileOrchestrationDependencyValidationException(
                    message: "AI file dependency validation error occurred, fix errors and try again.",
                        innerException: dependencyValidationException.InnerException as Xeption);

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

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        private async Task ShouldThrowDependencyExceptionOnUploadIfDependencyValidationErrorOccursAsync(
            Xeption dependencyValidationException)
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();

            var expectedAIFileOrchestrationDependencyException =
                new AIFileOrchestrationDependencyException(
                    message: "AI File dependency error occurred, contact support.",
                        innerException: dependencyValidationException.InnerException as Xeption);

            this.aiFileServiceMock.Setup(service =>
                service.UploadFileAsync(It.IsAny<AIFile>()))
                    .ThrowsAsync(dependencyValidationException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileOrchestrationService.UploadFileAsync(
                    someAIFile);

            AIFileOrchestrationDependencyException
                actualAIFileOrchestrationDependencyException =
                    await Assert.ThrowsAsync<AIFileOrchestrationDependencyException>(
                        uploadFileTask.AsTask);

            // then
            actualAIFileOrchestrationDependencyException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationDependencyException);

            this.aiFileServiceMock.Verify(service =>
                service.UploadFileAsync(It.IsAny<AIFile>()),
                    Times.Once);

            this.localFileServiceMock.Verify(service =>
                service.ReadFile(It.IsAny<string>()),
                    Times.Never);

            this.aiFileServiceMock.VerifyNoOtherCalls();
            this.localFileServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowServiceExceptionOnUploadIfExceptionOccursAsync()
        {
            // given
            AIFile someAIFile = CreateRandomAIFile();
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
                service.UploadFileAsync(It.IsAny<AIFile>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileOrchestrationService.UploadFileAsync(
                    someAIFile);

            AIFileOrchestrationServiceException actualAIFileOrchestrationServiceException =
                await Assert.ThrowsAsync<AIFileOrchestrationServiceException>(
                    uploadFileTask.AsTask);

            // then
            actualAIFileOrchestrationServiceException.Should().BeEquivalentTo(
                expectedAIFileOrchestrationServiceException);

            this.aiFileServiceMock.Verify(service =>
                service.UploadFileAsync(It.IsAny<AIFile>()),
                    Times.Once);

            this.localFileServiceMock.Verify(service =>
                service.ReadFile(It.IsAny<string>()),
                    Times.Never);

            this.aiFileServiceMock.VerifyNoOtherCalls();
            this.localFileServiceMock.VerifyNoOtherCalls();
        }
    }
}