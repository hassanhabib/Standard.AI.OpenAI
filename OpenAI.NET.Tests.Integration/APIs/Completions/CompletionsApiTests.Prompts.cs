// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Models.Services.Foundations.Completions;
using Xunit;

namespace OpenAI.NET.Tests.Integration.APIs.Completions
{
    public partial class CompletionsApiTests
    {
        [Fact]
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
