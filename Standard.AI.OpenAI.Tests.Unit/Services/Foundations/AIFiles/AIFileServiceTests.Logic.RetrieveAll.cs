// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIFiles;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveFilesAsync()
        {
            // given
            List<dynamic> filesRandomPropertiesList = CreateRandomFilesPropertiesList();

            var externalAIFilesResult = new ExternalAIFilesResult
            {
                Files = filesRandomPropertiesList.Select(item =>
                    new ExternalAIFileResponse
                    {
                        Id = item.Id,
                        Object = item.Object,
                        Bytes = item.Bytes,
                        CreatedDate = item.Created,
                        FileName = item.FileName,
                        Purpose = item.Purpose,
                        Deleted = item.Deleted,
                        Status = item.ExternalStatus,
                        StatusDetails = item.StatusDetails
                    }).ToArray()
            };

            IEnumerable<AIFileResponse> randomAIFiles = filesRandomPropertiesList.Select(item =>
                new AIFileResponse
                {
                    Id = item.Id,
                    Type = item.Type,
                    Size = item.Size,
                    CreatedDate = item.CreatedDate,
                    Name = item.Name,
                    Purpose = item.Purpose,
                    Deleted = item.Deleted,
                    Status = item.Status,
                    StatusDetails = item.StatusDetails
                }).ToArray();

            IEnumerable<AIFileResponse> expectedAIFiles = randomAIFiles;

            filesRandomPropertiesList.ForEach(item =>
            {
                int created = item.Created;

                this.dateTimeBrokerMock.Setup(broker =>
                    broker.ConvertToDateTimeOffSet(created))
                        .Returns(item.CreatedDate);
            });

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllFilesAsync())
                    .ReturnsAsync(externalAIFilesResult);

            // when
            IEnumerable<AIFileResponse> actualAIFiles =
                await this.aiFileService.RetrieveAllFilesAsync();

            // then
            actualAIFiles.Should().BeEquivalentTo(expectedAIFiles);

            filesRandomPropertiesList.ForEach(item =>
            {
                int created = item.Created;

                this.dateTimeBrokerMock.Verify(broker =>
                    broker.ConvertToDateTimeOffSet(created),
                        Times.Once);
            });

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllFilesAsync(),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}
