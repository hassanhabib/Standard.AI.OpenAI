// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.AIModels
{
    public partial class AIModelsApiTests
    {
        private readonly IOpenAIClient openAIClient;

        public AIModelsApiTests()
        {
            var openAIConfigurations = new OpenAIConfigurations
            {
                ApiKey = Environment.GetEnvironmentVariable("OpenAIApiKey"),
                OrganizationId = Environment.GetEnvironmentVariable("OpenAIOrgId"),
                ApiUrl = "https://api.openai.com/"
            };

            this.openAIClient = new OpenAIClient(openAIConfigurations);
        }
    }
}
