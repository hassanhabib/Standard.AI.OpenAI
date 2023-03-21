// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using OpenAI.NET.Clients.OpenAIs;
using OpenAI.NET.Models.Configurations;

namespace OpenAI.NET.Tests.Integration.APIs.Completions
{
    public partial class CompletionsApiTests
    {
        private readonly IOpenAIClient openAIClient;

        public CompletionsApiTests()
        {
            var openAIConfigurations = new ApiConfigurations
            {
                ApiKey = Environment.GetEnvironmentVariable("ApiKey"),
                OrganizationId = Environment.GetEnvironmentVariable("OrgId"),
                ApiUrl = "https://api.openai.com/"
            };

            this.openAIClient = new OpenAIClient(openAIConfigurations);
        }
    }
}
