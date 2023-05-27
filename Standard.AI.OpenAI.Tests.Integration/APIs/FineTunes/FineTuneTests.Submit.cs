// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.FineTunes
{
    public partial class FineTuneTests
    {
        [Fact(Skip = "This test is only for releases")]
        public async Task ShouldSubmitFineTuneAsync()
        {
            // given
            AIFile randomAIFile = 
                await SubmitRandomFileAsync();
            
            var fineTune = new FineTune();
            fineTune.Request = new FineTuneRequest();
            
            fineTune.Request.FileId = 
                randomAIFile.Response.Id;

            // when
            FineTune fineTuneResult =
                await this.openAIClient.FineTuneClient
                    .SubmitFineTuneAsync(fineTune);

            // then
            Assert.NotNull(fineTuneResult);
        }
    }
}
