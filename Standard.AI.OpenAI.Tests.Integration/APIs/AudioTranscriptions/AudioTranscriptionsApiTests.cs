// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.AudioTranscriptions
{
    public partial class AudioTranscriptionsApiTests
    {
        private readonly IOpenAIClient openAIClient;

        public AudioTranscriptionsApiTests()
        {
            var openAIConfigurations = new OpenAIConfigurations
            {
                ApiKey = Environment.GetEnvironmentVariable("ApiKey"),
                OrganizationId = Environment.GetEnvironmentVariable("OrgId"),
                ApiUrl = "https://api.openai.com/"
            };

            this.openAIClient = new OpenAIClient(openAIConfigurations);
        }
    }
}
