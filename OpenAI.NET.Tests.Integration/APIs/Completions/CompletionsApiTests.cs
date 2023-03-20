// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OpenAI.NET.Clients.OpenAIs;
using OpenAI.NET.Models.Configurations;

namespace OpenAI.NET.Tests.Integration.APIs.Completions
{
    public partial class CompletionsApiTests
    {
        private readonly IOpenAIClient openAIClient;

        public CompletionsApiTests()
        {
            ApiConfigurations openAIConfigurations =
                GetApiConfigurationsFromUserSecrets() ??
                GetApiConfigurationsFromEnvironmentVariables();

            this.openAIClient = new OpenAIClient(openAIConfigurations);
        }

        private ApiConfigurations GetApiConfigurationsFromUserSecrets()
        {
            ApiConfigurations apiConfigurations = default!;

            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureAppConfiguration(configBuilder =>
            {
                configBuilder.AddUserSecrets(userSecretsId: "318ba1a7-5a3f-472f-9d57-cd5430e2c958");
                IConfiguration config = configBuilder.Build();
                apiConfigurations = config.Get<ApiConfigurations>();
            });
            
            builder.Build();

            return apiConfigurations;
        }

        private ApiConfigurations GetApiConfigurationsFromEnvironmentVariables()
        {
            ApiConfigurations openAIConfigurations = new ApiConfigurations
            {
                ApiKey = Environment.GetEnvironmentVariable(variable: "ApiKey"),
                OrganizationId = Environment.GetEnvironmentVariable(variable: "OrgId"),
                ApiUrl = "https://api.openai.com/"
            };

            return openAIConfigurations;
        }
    }
}

