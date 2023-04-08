// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;
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

            var randomFile = new File
            {
                Id = fileRandomProperties.Id,
                Type = fileRandomProperties.Type,
                Deleted = fileRandomProperties.Deleted
            };

            ExternalAIFileResponse removedExternalAIFileResponse = randomExternalAIFileResponse.DeepClone();
            File expectedFile = randomFile;

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(inputFileId))
                    .ReturnsAsync(removedExternalAIFileResponse);

            // when
            File actualFile = await this.fileService.RemoveFileByIdAsync(
                inputFileId);

            // then
            actualFile.Should().BeEquivalentTo(expectedFile);

            this.openAIBrokerMock.Verify(broker =>
                broker.DeleteFileByIdAsync(inputFileId),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}