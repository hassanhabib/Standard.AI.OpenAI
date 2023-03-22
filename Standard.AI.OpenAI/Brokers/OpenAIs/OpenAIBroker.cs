// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Configurations;
using RESTFulSense.Clients;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker : IOpenAIBroker
    {
        private readonly ApiConfigurations apiConfigurations;
        private readonly IRESTFulApiFactoryClient apiClient;
        private readonly HttpClient httpClient;

        public OpenAIBroker(ApiConfigurations apiConfigurations)
        {
            this.apiConfigurations = apiConfigurations;
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
                ignoreNulls: true);
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
                    new Uri(uriString: this.apiConfigurations.ApiUrl),
            };

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    scheme: "Bearer",
                    parameter: this.apiConfigurations.ApiKey);

            httpClient.DefaultRequestHeaders.Add(
                name: "OpenAI-Organization",
                value: this.apiConfigurations.OrganizationId);

            return httpClient;
        }

        private IRESTFulApiFactoryClient SetupApiClient() =>
            new RESTFulApiFactoryClient(this.httpClient);
    }
}
