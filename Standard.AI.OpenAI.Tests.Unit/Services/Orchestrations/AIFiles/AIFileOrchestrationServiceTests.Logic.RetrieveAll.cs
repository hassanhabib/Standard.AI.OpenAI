// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
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
        public async Task ShouldRetrieveAllFilesAsync()
        {
            // given
            IEnumerable<AIFileResponse> randomAIFiles = CreateRandomAIFileResponses();
            IEnumerable<AIFileResponse> expectedAIFiles = randomAIFiles.DeepClone();

            this.aiFileServiceMock.Setup(service =>
                service.RetrieveAllFilesAsync())
                    .ReturnsAsync(randomAIFiles);

            // when
            IEnumerable<AIFileResponse> actualAIFiles =
                await this.aiFileOrchestrationService.RetrieveAllFilesAsync();

            // then
            actualAIFiles.Should().BeEquivalentTo(expectedAIFiles);

            this.aiFileServiceMock.Verify(service =>
                service.RetrieveAllFilesAsync(),
                    Times.Once);

            this.localFileServiceMock.VerifyNoOtherCalls();
            this.aiFileServiceMock.VerifyNoOtherCalls();
        }
    }
}
