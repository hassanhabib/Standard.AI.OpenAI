// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIFiles;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnUploadIfAIFileIsNull()
        {
            // given
            AIFile nullAIFile = null;
            var nullAIFileException = new NullAIFileException();

            var expectedAIFileValidationException =
                new AIFileValidationException(nullAIFileException);

            // when
            ValueTask<AIFile> uploadFileTask = this.aiFileService.UploadFileAsync(nullAIFile);

            AIFileValidationException actualAIFileValidationException =
                await Assert.ThrowsAsync<AIFileValidationException>(uploadFileTask.AsTask);

            // then
            actualAIFileValidationException.Should().BeEquivalentTo(
                expectedAIFileValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()),
                    Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnUploadIfAIFileRequestIsNull()
        {
            // given
            var invalidAIFile = new AIFile();
            invalidAIFile.Request = null;

            var invalidAIFileException =
                new InvalidAIFileException();

            invalidAIFileException.AddData(
                key: nameof(AIFile.Request),
                values: "Value is required");

            var expectedAIFileValidationException =
                new AIFileValidationException(invalidAIFileException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileService.UploadFileAsync(invalidAIFile);

            AIFileValidationException actualAIFileValidationException =
                await Assert.ThrowsAsync<AIFileValidationException>(uploadFileTask.AsTask);

            // then
            actualAIFileValidationException.Should().BeEquivalentTo(
                expectedAIFileValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()),
                    Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnUploadIfAIFileRequestIsInvalid(string invalidString)
        {
            // given
            var invalidAIFile = new AIFile();

            invalidAIFile.Request = new AIFileRequest
            {
                Name = invalidString,
                Purpose = invalidString,
                Content = null
            };

            var invalidAIFileException =
                new InvalidAIFileException();

            invalidAIFileException.AddData(
                key: nameof(AIFileRequest.Name),
                values: "Value is required");

            invalidAIFileException.AddData(
                key: nameof(AIFileRequest.Content),
                values: "Value is required");

            invalidAIFileException.AddData(
                key: nameof(AIFileRequest.Purpose),
                values: "Value is required");

            var expectedAIFileValidationException =
                new AIFileValidationException(invalidAIFileException);

            // when
            ValueTask<AIFile> uploadFileTask =
                this.aiFileService.UploadFileAsync(invalidAIFile);

            AIFileValidationException actualAIFileValidationException =
                await Assert.ThrowsAsync<AIFileValidationException>(uploadFileTask.AsTask);

            // then
            actualAIFileValidationException.Should().BeEquivalentTo(
                expectedAIFileValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()),
                    Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
