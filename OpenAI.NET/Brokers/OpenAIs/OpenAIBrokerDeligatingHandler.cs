using OpenAI.NET.Models.Configurations;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAI.NET.Brokers.OpenAIs
{
    internal class OpenAIBrokerDeligatingHandler : DelegatingHandler
    {
        private readonly OpenAIApiConfigurations apiConfigurations;

        public OpenAIBrokerDeligatingHandler(OpenAIApiConfigurations apiConfigurations)
        {
            this.apiConfigurations = apiConfigurations;
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.AddRequiredHeaders(request);

            return base.Send(request, cancellationToken);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.AddRequiredHeaders(request);

            return base.SendAsync(request, cancellationToken);
        }

        private void AddRequiredHeaders(HttpRequestMessage request)
        {
            request.Headers.Authorization ??=
                new AuthenticationHeaderValue(
                    scheme: "Bearer",
                    parameter: this.apiConfigurations.ApiKey);

            request.Headers.Add(
                name: "OpenAI-Organization",
                value: this.apiConfigurations.OrganizationId);
        }
    }
}
