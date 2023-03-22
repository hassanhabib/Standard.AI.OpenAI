// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.Completions
{
    public partial class CompletionsApiTests
    {
        [Fact(Skip = "This test is only for releases")]
        public async Task ShouldPromptCompletionAsync()
        {
            // given
            var inputCompletion = new Completion
            {
                Request = new CompletionRequest
                {
                    Model = "text-davinci-003",
                    Prompt = new string[]
                    {
                        "Human: What's the tallest building in the world?\n",
                        "AI: "
                    }
                }
            };

            // when
            Completion responseCompletion =
                await this.openAIClient.Completions.PromptCompletionAsync(
                    inputCompletion);

            // then
            Assert.NotNull(responseCompletion.Response);
        }
    }
}
