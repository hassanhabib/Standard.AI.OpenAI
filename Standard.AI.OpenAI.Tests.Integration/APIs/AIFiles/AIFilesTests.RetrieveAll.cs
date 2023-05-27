// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.AIFiles
{
    public partial class AIFilesTests
    {
        [Fact(Skip = "This test is only for releases")]
        public async Task ShouldRetrieveAllAIFilesAsync()
        {
            // given & when
            IEnumerable<AIFileResponse> responseAIFiles =
                await this.openAIClient.AIFiles.RetrieveAllFilesAsync();

            // then
            Assert.NotNull(responseAIFiles);
        }
    }
}
