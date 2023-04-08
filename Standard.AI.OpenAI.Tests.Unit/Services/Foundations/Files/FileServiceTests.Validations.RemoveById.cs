// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;
using Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Files
{
    public partial class FileServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfIdIsInvalidAsync(
            string invalidId)
        {
            // given
            string invalidFileId = invalidId;

            var invalidFileException =
                new InvalidFileException();

            invalidFileException.AddData(
                key: nameof(File.Id),
                values: "Value is required");

            var expectedFileValidationException =
                new FileValidationException(invalidFileException);

            // when
            ValueTask<File> removeFileByIdTask =
                this.fileService.RemoveFileByIdAsync(invalidFileId);

            FileValidationException actualFileValidationException =
                await Assert.ThrowsAsync<FileValidationException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileValidationException.Should().BeEquivalentTo(
                expectedFileValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}