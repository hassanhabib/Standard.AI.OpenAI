// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

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
        public async Task ShouldRemoveFileByIdAsync()
        {
            // given
            string inputFileId = CreateRandomFileId();
            string expectedFileId = inputFileId.DeepClone();
            AIFile expectedAIFile = CreateRandomAIFile();
            expectedAIFile.Response.Deleted = true;

            this.aiFileServiceMock.Setup(service =>
                service.RemoveFileByIdAsync(inputFileId))
                    .ReturnsAsync(expectedAIFile);

            // when 
            AIFile actualAIFile = 
                await this.aiFileOrchestrationService.RemoveFileByIdAsync(inputFileId);

            // then
            actualAIFile.Should().BeEquivalentTo(expectedAIFile);

            this.aiFileServiceMock.Verify(service =>
                service.RemoveFileByIdAsync(It.Is(
                    SameFileIdAs(expectedFileId))),
                    Times.Once);

            this.localFileServiceMock.VerifyNoOtherCalls();
            this.aiFileServiceMock.VerifyNoOtherCalls();
        }
    }
}