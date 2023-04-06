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
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ShouldThrowValidationExceptionOnReadIfFilePathIsInvalid(
            string invalidFilePath)
        {
            // given
            var invalidFileException =
                new InvalidLocalFileException();

            invalidFileException.AddData(
                key: "FilePath",
                "Value is required");

            var expectedFileValidationException =
                new LocalFileValidationException(invalidFileException);

            // when
            Action readFileAction = () =>
                this.localFileService.ReadFile(invalidFilePath);

            LocalFileValidationException actualFileValidationException =
                Assert.Throws<LocalFileValidationException>(readFileAction);

            // then
            actualFileValidationException.Should().BeEquivalentTo(
                expectedFileValidationException);

            this.fileBrokerMock.Verify(broker =>
                broker.ReadFile(It.IsAny<string>()),
                    Times.Never);

            this.fileBrokerMock.VerifyNoOtherCalls();
        }
    }
}
