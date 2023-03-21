// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Net.Mime;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using OpenAI.NET.Clients.OpenAIs;
using OpenAI.NET.Models.Configurations;
using OpenAI.NET.Models.Services.Foundations.Completions;
using OpenAI.NET.Models.Services.Foundations.ExternalCompletions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace OpenAI.NET.Tests.Acceptance.Clients.Completions
{
    public partial class CompletionClientTests
    {
        [Fact]
        public async Task ShouldPromptCompletionAsync()
        {
            // given
            var testData = new CompletionRequestTestData();

            this.ConfigureWireMockServer("/v1/completions", testData);

            // when
            Completion actualCompletion =
                await this.openAIClient.Completions.PromptCompletionAsync(
                    testData.InputCompletion);

            // then
            actualCompletion.Should().BeEquivalentTo(testData.ExpectedCompletion);
        }


        [Fact]
        public async Task ShouldPromptCompletionAsyncWhenCreatedManually()
        {
            // given
            var testData = new CompletionRequestTestData();

            this.apiConfigurations = new OpenAIApiConfigurations
            {
                ApiUrl = this.wireMockServer.Url,
                ApiKey = CreateRandomString(),
                OrganizationId = CreateRandomString(),
            };

            this.ConfigureWireMockServer("/v1/completions", testData);

            this.openAIClient = new OpenAIClient(apiConfigurations);

            // when
            Completion actualCompletion =
                await this.openAIClient.Completions.PromptCompletionAsync(
                    testData.InputCompletion);

            // then
            actualCompletion.Should().BeEquivalentTo(testData.ExpectedCompletion);
        }

        private void ConfigureWireMockServer(string url, CompletionRequestTestData testData)
        {
            var fullUrl = new Uri(baseUri: new Uri(this.apiConfigurations.ApiUrl), relativeUri: url);
            this.wireMockServer
                .Given(
                    Request.Create()
                        .WithPath(url)
                        .WithHeader(HeaderNames.Authorization, $"{JwtBearerDefaults.AuthenticationScheme} {this.apiConfigurations.ApiKey}")
                        .WithHeader(GlobalConstants.OpenAIOrgIdKey, $"{this.apiConfigurations.OrganizationId}")
                        .WithHeader(HeaderNames.ContentType, $"{MediaTypeNames.Application.Json}; charset=utf-8")
                        .WithBody(JsonConvert.SerializeObject(
                            testData.CompletionRequest,
                            testData.JsonSerializationSettings)))
                .RespondWith(
                    Response
                        .Create()
                        .WithBodyAsJson(testData.CompletionResponse));
        }
          
        private class CompletionRequestTestData
        {
            public CompletionRequestTestData()
            {
                RandomCompletion = CreateRandomCompletion();
                InputCompletion = RandomCompletion;
                CompletionRequest= ConvertToCompletionRequest(InputCompletion);
                CompletionResponse = CreateRandomExternalCompletionResponse();
                ExpectedCompletion = ConvertToCompletion(InputCompletion, CompletionResponse);
                JsonSerializationSettings = new JsonSerializerSettings();
                JsonSerializationSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            }

            public Completion RandomCompletion { get; private set; }

            public Completion InputCompletion { get; private set; }

            public ExternalCompletionRequest CompletionRequest { get; private set; }
            
            public ExternalCompletionResponse CompletionResponse { get; private set; }
            
            public Completion ExpectedCompletion { get; private set; }

            public JsonSerializerSettings JsonSerializationSettings { get; private set; }
        }
    }
}
