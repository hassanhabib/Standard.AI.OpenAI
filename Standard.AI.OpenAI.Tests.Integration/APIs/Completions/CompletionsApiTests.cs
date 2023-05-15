﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.Completions
{
    public partial class CompletionsApiTests
    {
        private readonly IOpenAIClient openAIClient;

        public CompletionsApiTests()
        {
            IConfiguration config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            OpenAIConfigurations openAIConfigurations =
                config.GetSection(key: "OpenAI").Get<OpenAIConfigurations>();

            this.openAIClient = new OpenAIClient(openAIConfigurations);
        }
    }
}
