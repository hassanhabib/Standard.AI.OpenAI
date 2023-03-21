using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;

using OpenAI.NET.Models.Configurations;


namespace OpenAI.NET.Brokers.HttpMessageHandlers
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
            AddRequiredHeaders(request);

            return base.Send(request, cancellationToken);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AddRequiredHeaders(request);

            return base.SendAsync(request, cancellationToken);
        }

        private void AddRequiredHeaders(HttpRequestMessage request)
        {
            request.Headers.Authorization ??=
                new AuthenticationHeaderValue(
                    scheme: JwtBearerDefaults.AuthenticationScheme,
                    parameter: apiConfigurations.ApiKey);

            request.Headers.Add(
                name: GlobalConstants.OpenAIOrgIdKey,
                value: apiConfigurations.OrganizationId);
        }
    }
}
