// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
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
                new InvalidAIFileException();

            invalidFileException.AddData(
                key: nameof(AIFile.Response.Id),
                values: "Value is required");

            var expectedFileValidationException =
                new AIFileValidationException(invalidFileException);

            // when
            ValueTask<AIFile> removeFileByIdTask =
                this.aiFileService.RemoveFileByIdAsync(invalidFileId);

            AIFileValidationException actualFileValidationException =
                await Assert.ThrowsAsync<AIFileValidationException>(
                    removeFileByIdTask.AsTask);

            // then
            actualFileValidationException.Should().BeEquivalentTo(
                expectedFileValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(It.IsAny<string>()),
                    Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}