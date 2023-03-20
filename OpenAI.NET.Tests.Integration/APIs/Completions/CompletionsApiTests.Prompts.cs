// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Clients.OpenAIs;
using OpenAI.NET.Models.Services.Foundations.Completions;
using Xunit;

namespace OpenAI.NET.Tests.Integration.APIs.Completions
{
    public partial class CompletionsApiTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ShouldPromptCompletionAsync(bool resolveFromDI)
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

            if (resolveFromDI)
            {
                this.openAIClient = new OpenAIClient();
            }

            // when
            Completion responseCompletion =
                await this.openAIClient.Completions.PromptCompletionAsync(
                    inputCompletion);

            // then
            Assert.NotNull(responseCompletion.Response);
        }
    }
}
