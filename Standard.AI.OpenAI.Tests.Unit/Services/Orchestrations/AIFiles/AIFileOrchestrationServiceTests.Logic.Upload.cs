// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Orchestrations.AIFiles
{
    public partial class AIFileOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldUploadFileAsync()
        {
            // given
            Stream noStream = null;
            AIFile randomAIFile = CreateRandomAIFile();
            AIFile inputAIFile = randomAIFile;
            inputAIFile.Request.Content = noStream;
            Stream randomStream = CreateRandomStream();
            Stream readStream = randomStream;
            AIFile expectedInputAIFile = inputAIFile.DeepClone();
            expectedInputAIFile.Request.Content = readStream;
            AIFile uploadedAIFile = expectedInputAIFile;
            AIFile expectedAIFile = expectedInputAIFile.DeepClone();

            this.localFileServiceMock.Setup(service =>
                service.ReadFile(inputAIFile.Request.Name))
                    .Returns(readStream);

            this.aiFileServiceMock.Setup(service =>
                service.UploadFileAsync(It.Is(
                    SameAIFileAs(expectedInputAIFile))))
                        .ReturnsAsync(uploadedAIFile);

            // when
            AIFile actualAIFile =
                await this.aiFileOrchestrationService.UploadFileAsync(
                    inputAIFile);

            // then
            actualAIFile.Should().BeEquivalentTo(expectedInputAIFile);

            this.localFileServiceMock.Verify(service =>
                service.ReadFile(inputAIFile.Request.Name),
                    Times.Once);

            this.aiFileServiceMock.Verify(service =>
                service.UploadFileAsync(It.Is(
                    SameAIFileAs(expectedInputAIFile))),
                        Times.Once);

            this.localFileServiceMock.VerifyNoOtherCalls();
            this.aiFileServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldUploadStreamAsync()
        {
            // given
            AIFile randomAIFile = CreateRandomAIFile();
            AIFile inputAIFile = randomAIFile;
            AIFile uploadedAIFile = inputAIFile;
            AIFile expectedAIFile = uploadedAIFile.DeepClone();

            this.aiFileServiceMock.Setup(service =>
                service.UploadFileAsync(inputAIFile))
                    .ReturnsAsync(uploadedAIFile);

            // when
            AIFile actualAIFile =
                await this.aiFileOrchestrationService.UploadFileAsync(
                    inputAIFile);

            // then
            actualAIFile.Should().BeEquivalentTo(expectedAIFile);

            this.aiFileServiceMock.Verify(service =>
                service.UploadFileAsync(inputAIFile),
                    Times.Once);

            this.localFileServiceMock.Verify(service =>
                service.ReadFile(It.IsAny<string>()),
                    Times.Never);

            this.aiFileServiceMock.VerifyNoOtherCalls();
            this.localFileServiceMock.VerifyNoOtherCalls();
        }
    }
}
