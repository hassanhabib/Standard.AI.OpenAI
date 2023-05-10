// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIFiles;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
    {
        [Fact]
        public async Task ShouldRemoveFileByIdAsync()
        {
            // given
            string randomString = CreateRandomString();
            string randomFileId = randomString;
            string inputFileId = randomFileId;

            dynamic randomFileProperties =
                CreateRandomFileProperties();

            var randomExternalAIFileResponse = new ExternalAIFileResponse
            {
                Id = randomFileProperties.Id,
                Object = randomFileProperties.Object,
                Deleted = randomFileProperties.Deleted,
            };

            var randomAIFileResponse = new AIFileResponse
            {
                Id = randomFileProperties.Id,
                Type = randomFileProperties.Type,
                Deleted = randomFileProperties.Deleted
            };

            var randomAIFile = new AIFile
            {
                Response = randomAIFileResponse
            };

            ExternalAIFileResponse removedExternalAIFileResponse = randomExternalAIFileResponse.DeepClone();
            AIFile expectedAIFile = randomAIFile;

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(inputFileId))
                    .ReturnsAsync(removedExternalAIFileResponse);

            // when
            AIFile actualFile = await this.aiFileService.RemoveFileByIdAsync(inputFileId);

            // then
            actualFile.Should().BeEquivalentTo(expectedAIFile);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(inputFileId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}