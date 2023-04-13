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
        [Fact]
        public async Task ShouldThrowValidationExceptionOnUploadIfAIFileIsNull()
        {
            // given
            AIFile noAIFile = null;
            var nullAIFileException = new NullAIFileException();

            var expectedAIFileValidationException =
                new AIFileValidationException(nullAIFileException);

            // when
            ValueTask<AIFile> uploadFileTask = this.aiFileService.UploadFileAsync(noAIFile);

            AIFileValidationException actualAIFileValidationException =
                await Assert.ThrowsAsync<AIFileValidationException>(uploadFileTask.AsTask);

            // then
            actualAIFileValidationException.Should().BeEquivalentTo(
                expectedAIFileValidationException);

            this.openAiBrokerMock.Verify(broker =>
                broker.PostFileFormAsync(It.IsAny<ExternalAIFileRequest>()),
                    Times.Never);

            this.openAiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
