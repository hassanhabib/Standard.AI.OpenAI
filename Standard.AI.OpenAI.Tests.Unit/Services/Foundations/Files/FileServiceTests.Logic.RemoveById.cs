// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalFiles;
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

            var randomExternalFile = new ExternalFile
            {
                Id = fileRandomProperties.Id,
                Object = fileRandomProperties.Object,
                Deleted = fileRandomProperties.Deleted,
            };

            var randomFile = new File()
            {
                Id = fileRandomProperties.Id,
                Type = fileRandomProperties.Type,
                Deleted = fileRandomProperties.Deleted
            };

            ExternalFile removedExternalFile = randomExternalFile.DeepClone();
            File expectedFile = randomFile;

            this.openAIBrokerMock.Setup(broker =>
                broker.DeleteFileByIdAsync(inputFileId))
                    .ReturnsAsync(removedExternalFile);

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