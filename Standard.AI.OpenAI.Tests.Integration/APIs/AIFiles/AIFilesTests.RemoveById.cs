// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.AIFiles
{
    public partial class AIFilesTests
    {
        [Fact(Skip = "This test is only for releases")]
        public async Task ShouldRemoveFileByIdAsync()
        {
            // given
            MemoryStream memoryStream = CreateRandomStream();

            var aiFile = new AIFile
            {
                Request = new AIFileRequest()
                {
                    Name = "Test",
                    Content = memoryStream,
                    Purpose = "fine-tune"
                }
            };

            AIFile uploadedFile = 
                await this.openAIClient.AIFiles.UploadFileAsync(
                    aiFile);

            // when
            AIFile responseFile =
                await this.openAIClient.AIFiles.RemoveFileByIdAsync(
                    uploadedFile.Response.Id);

            // then
            Assert.NotNull(responseFile.Response);
        }
    }
}