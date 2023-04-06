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
        [MemberData(nameof(FileExceptions))]
        public void ShouldThrowDependencyValidationExceptionOnReadIfDepedencyErrorOccurs(
            Exception dependencyValidationException)
        {
            // given
            string someFilePath = CreateRandomFilePath();
            var invalidFileException = new InvalidFileException(dependencyValidationException);

            var expectedFileDependencyValidationException =
                new FileDependencyValidationException(invalidFileException);

            this.fileBrokerMock.Setup(broker =>
                broker.ReadFile(someFilePath))
                    .Throws(dependencyValidationException);

            // when
            Action readFileAction = () =>
                this.localFileService.ReadFile(someFilePath);

            FileDependencyValidationException actualFileDependencyValidationException =
                Assert.Throws<FileDependencyValidationException>(readFileAction);

            // then
            actualFileDependencyValidationException.Should().BeEquivalentTo(
                expectedFileDependencyValidationException);

            this.fileBrokerMock.Verify(broker =>
                broker.ReadFile(It.IsAny<string>()),
                    Times.Once);

            this.fileBrokerMock.VerifyNoOtherCalls();
        }
    }
}
