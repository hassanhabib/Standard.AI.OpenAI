// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Files
{
    public partial class FileServiceTests
    {
        [Fact]
        public async Task ShouldRemoveFileByIdAsync()
        {
            // given
            string randomString = GetRandomString();
            string randomFileId = randomString;
            string inputFileId = randomFileId;

            dynamic fileRandomProperties =
                CreateRandomFileProperties();

            var randomExternalAIFileResponse = new ExternalAIFileResponse
            {
                Id = fileRandomProperties.Id,
                Object = fileRandomProperties.Object,
                Deleted = fileRandomProperties.Deleted,
            };

            var randomAIFileResponse = new AIFileResponse
            {
                Id = fileRandomProperties.Id,
                Type = fileRandomProperties.Type,
                Deleted = fileRandomProperties.Deleted
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
            AIFile actualFile = await this.fileService.RemoveFileByIdAsync(
                inputFileId);

            // then
            actualFile.Should().BeEquivalentTo(expectedAIFile);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(inputFileId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}