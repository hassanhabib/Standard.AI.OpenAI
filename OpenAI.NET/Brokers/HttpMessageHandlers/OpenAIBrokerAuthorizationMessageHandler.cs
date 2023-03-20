using Microsoft.AspNetCore.Authentication.JwtBearer;
using OpenAI.NET.Models.Configurations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAI.NET.Brokers.HttpMessageHandlers
{
    internal class OpenAIBrokerAuthorizationMessageHandler : DelegatingHandler
    {
        private const string OpenAIOrganizationIdHeaderKey = "OpenAI-Organization";
        private readonly OpenAIApiConfigurations apiConfigurations;

        public OpenAIBrokerAuthorizationMessageHandler(OpenAIApiConfigurations apiConfigurations)
        {
            this.apiConfigurations = apiConfigurations;
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.AddRequestHeaders(request);

            return base.Send(request, cancellationToken);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.AddRequestHeaders(request);

            return base.SendAsync(request, cancellationToken);
        }

        private void AddRequestHeaders(HttpRequestMessage request)
        {
            request.Headers.Add(
                name: OpenAIOrganizationIdHeaderKey,
                value: this.apiConfigurations.OrganizationId);

            request.Headers.Authorization ??=
                new AuthenticationHeaderValue(
                    scheme: JwtBearerDefaults.AuthenticationScheme,
                    parameter: this.apiConfigurations.ApiKey);
        }
    }
}
