// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.AIModels
{
    public partial class AIModelsApiTests
    {
        [Fact(Skip = "This test is only for releases")]
        public async Task ShouldRetrieveAllAIModelsAsync()
        {
            // given . when
            IEnumerable<AIModel> responseAIModels =
                await this.openAIClient.AIModels.RetrieveAIModelsAsync();

            // then
            Assert.NotNull(responseAIModels);
        }
    }
}
