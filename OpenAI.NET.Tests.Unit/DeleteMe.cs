// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Brokers.OpenAIs;
using OpenAI.NET.Models.Configurations;
using OpenAI.NET.Models.ExternalCompletions;
using Xunit;

namespace OpenAI.NET.Tests.Unit
{
    public partial class DeleteMe
    {
        [Fact]
        public void ShouldBeTrue() => Assert.True(condition: true);

        [Fact]
        public async Task ShouldDoStuffAndDeleteMeTooAsync()
        {
            var apiConfiguration = new ApiConfigurations
            {
                ApiKey = this.apiKey,
                ApiUrl = "https://api.openai.com/"
            };

            var openAIBroker = new OpenAIBroker(apiConfiguration);

            var completionRequest = new CompletionRequest
            {
                Model = "text-davinci-003",
                Prompt = new string[] { "Human: Hello", "AI:" },
                MaxTokens = 5,
                Temperature = 0.9,
                CompletionsPerPrompt = 1
            };

            CompletionResponse completionResponse =
                await openAIBroker.PostCompletionRequestAsync(completionRequest);

            Assert.NotNull(completionResponse);
        }
    }
}