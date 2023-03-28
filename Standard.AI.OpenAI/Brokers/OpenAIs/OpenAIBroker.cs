// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RESTFulSense.Clients;
using RESTFulSense.Services;
using Standard.AI.OpenAI.Models.Configurations;

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

        private async ValueTask<TResult> PostAsync<TResult>(string relativeUrl, HttpContent content)
        {
            // TODO: We make us of the httpClient directly since atm RESTFulSense does not have an overload
            //       which accept HttpContent.
            //       Tracked by: <https://github.com/hassanhabib/RESTFulSense/issues/71>

            HttpResponseMessage responseMessage = await httpClient.PostAsync(relativeUrl, content);
            await ValidationService.ValidateHttpResponseAsync(responseMessage);

            return await DeserializeResponseContentAsync<TResult>(responseMessage);

            static async ValueTask<T> DeserializeResponseContentAsync<T>(HttpResponseMessage responseMessage) =>
                JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
        }

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
