// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

using RESTFulSense.Clients;

using System;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace OpenAI.NET.Brokers.OpenAIs
{
    internal partial class OpenAIBroker : IOpenAIBroker
    {
        private readonly IRESTFulApiFactoryClient apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenAIBroker"/> class.
        /// </summary>
        /// <param name="apiClient">The RESTFulSenses Api Client.</param>
        public OpenAIBroker(IHttpClientFactory httpClientFactory)
        {
            HttpClient httpClient = httpClientFactory.CreateClient(nameof(OpenAIBroker));
            this.apiClient = new RESTFulApiFactoryClient(httpClient);
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
                mediaType: MediaTypeNames.Application.Json,
                ignoreNulls: true);
        }


        private async ValueTask<T> PutAsync<T>(string relativeUrl, T content) =>
            await this.apiClient.PutContentAsync(relativeUrl, content);

        private async ValueTask<T> DeleteAsync<T>(string relativeUrl) =>
            await this.apiClient.DeleteContentAsync<T>(relativeUrl);
    }
}
