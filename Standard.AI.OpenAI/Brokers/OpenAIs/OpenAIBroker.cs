// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RESTFulSense.Clients;
using Standard.AI.OpenAI.Models.Configurations;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker : IOpenAIBroker
    {
        private readonly OpenAIConfigurations openAIConfigurations;
        private readonly IRESTFulApiFactoryClient apiClient;
        private readonly HttpClient httpClient;

        public OpenAIBroker(OpenAIConfigurations openAIConfigurations)
        {
            this.openAIConfigurations = openAIConfigurations;
            this.httpClient = SetupHttpClient();
            this.apiClient = SetupApiClient();
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);

        private async ValueTask<T> PostAsync<T>(string relativeUrl, T content) =>
            await this.apiClient.PostContentAsync(relativeUrl, content);

        private async ValueTask<TResult> PostAsync<TRequest, TResult>(string relativeUrl, TRequest content)
        {
            return await this.apiClient.PostContentAsync<TRequest, TResult>(
                relativeUrl,
                content,
                mediaType: "application/json",
                ignoreDefaultValues: true);
        }

        private async ValueTask<TResult> PostFormAsync<TRequest, TResult>(string relativeUrl, TRequest content)
            where TRequest : class
        {
            return await this.apiClient.PostFormAsync<TRequest, TResult>(
                relativeUrl,
                content);
        }

        private async ValueTask<T> PutAsync<T>(string relativeUrl, T content) =>
            await this.apiClient.PutContentAsync(relativeUrl, content);

        private async ValueTask<T> DeleteAsync<T>(string relativeUrl) =>
            await this.apiClient.DeleteContentAsync<T>(relativeUrl);

        private HttpClient SetupHttpClient()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress =
                    new Uri(uriString: this.openAIConfigurations.ApiUrl),
            };

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    scheme: "Bearer",
                    parameter: this.openAIConfigurations.ApiKey);

            httpClient.DefaultRequestHeaders.Add(
                name: "OpenAI-Organization",
                value: this.openAIConfigurations.OrganizationId);

            return httpClient;
        }

        private IRESTFulApiFactoryClient SetupApiClient() =>
            new RESTFulApiFactoryClient(this.httpClient);
    }
}
