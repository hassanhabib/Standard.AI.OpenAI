// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.LocalFiles
{
    public partial class LocalFileServiceTests
    {
        [Theory]
        [MemberData(nameof(FileValidationExceptions))]
        public void ShouldThrowDependencyValidationExceptionOnReadIfDepedencyErrorOccurs(
            Exception dependencyValidationException)
        {
            // given
            string someFilePath = CreateRandomFilePath();
            var invalidFileException = new InvalidLocalFileException(dependencyValidationException);

            var expectedFileDependencyValidationException =
                new LocalFileDependencyValidationException(invalidFileException);

            this.fileBrokerMock.Setup(broker =>
                broker.ReadFile(someFilePath))
                    .Throws(dependencyValidationException);

            // when
            Action readFileAction = () =>
                this.localFileService.ReadFile(someFilePath);

            LocalFileDependencyValidationException actualFileDependencyValidationException =
                Assert.Throws<LocalFileDependencyValidationException>(readFileAction);

            // then
            actualFileDependencyValidationException.Should().BeEquivalentTo(
                expectedFileDependencyValidationException);

            this.fileBrokerMock.Verify(broker =>
                broker.ReadFile(It.IsAny<string>()),
                    Times.Once);

            this.fileBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FileNotFoundExceptions))]
        public void ShouldThrowDependencyExceptionOnReadIfFileNotFoundExceptionOccurs(
            Exception fileNotFoundException)
        {
            // given
            string someFilePath = CreateRandomFilePath();

            var notFoundLocalFileException =
                new NotFoundLocalFileException(fileNotFoundException);

            var expectedFileDependencyValidationException =
                new LocalFileDependencyValidationException(notFoundLocalFileException);

            this.fileBrokerMock.Setup(broker =>
                broker.ReadFile(someFilePath))
                    .Throws(fileNotFoundException);

            // when
            Action readFileAction = () =>
                this.localFileService.ReadFile(someFilePath);

            LocalFileDependencyValidationException actualFileDependencyValidationException =
                Assert.Throws<LocalFileDependencyValidationException>(readFileAction);

            // then
            actualFileDependencyValidationException.Should().BeEquivalentTo(
                expectedFileDependencyValidationException);

            this.fileBrokerMock.Verify(broker =>
                broker.ReadFile(It.IsAny<string>()),
                    Times.Once);

            this.fileBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FileDependencyExceptions))]
        public void ShouldThrowDependencyExceptionOnReadIfFileErrorOccurs(
            Exception fileDependencyException)
        {
            // given
            string someFilePath = CreateRandomFilePath();

            var failedLocalFileDependencyException =
                new FailedLocalFileDependencyException(fileDependencyException);

            var expectedFileDependencyException =
                new LocalFileDependencyException(failedLocalFileDependencyException);

            this.fileBrokerMock.Setup(broker =>
                broker.ReadFile(someFilePath))
                    .Throws(fileDependencyException);

            // when
            Action readFileAction = () =>
                this.localFileService.ReadFile(someFilePath);

            LocalFileDependencyException actualFileDependencyException =
                Assert.Throws<LocalFileDependencyException>(readFileAction);

            // then
            actualFileDependencyException.Should().BeEquivalentTo(
                expectedFileDependencyException);

            this.fileBrokerMock.Verify(broker =>
                broker.ReadFile(It.IsAny<string>()),
                    Times.Once);

            this.fileBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowFailedServiceException()
        {
            // given            
            string someFilePath = CreateRandomFilePath();
            var serviceException = new Exception();

            var failedLocalFileServiceException =
                new FailedLocalFileServiceException(serviceException);

            var expectedLocalFileServiceException =
                new LocalFileServiceException(failedLocalFileServiceException);

            this.fileBrokerMock.Setup(broker =>
                broker.ReadFile(It.IsAny<string>()))
                    .Throws(serviceException);

            // when
            Action readFileAction = () =>
                this.localFileService.ReadFile(someFilePath);

            LocalFileServiceException actualLocalFileServiceException =
              Assert.Throws<LocalFileServiceException>(readFileAction);

            // then
            actualLocalFileServiceException.Should().BeEquivalentTo(
                expectedLocalFileServiceException);

            this.fileBrokerMock.Verify(broker =>
                broker.ReadFile(It.IsAny<string>()),
                    Times.Once);

            this.fileBrokerMock.VerifyNoOtherCalls();
        }
    }
}
