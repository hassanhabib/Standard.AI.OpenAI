// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using OpenAI.NET.Brokers;
using OpenAI.NET.Clients.Completions;
using OpenAI.NET.Services.Foundations.Completions;

using System;
using System.IO;

namespace OpenAI.NET.Clients.OpenAIs
{
    public class OpenAIClient : IOpenAIClient
    {
        public OpenAIClient()
        {
            IHost host = RegisterServices();
            this.ServiceProvider = host.Services;
            InitializeClients(host);
        }

        public ICompletionsClient Completions { get; set; }

        public IServiceProvider ServiceProvider { get; private set; }

        private void InitializeClients(IHost host) =>
            Completions = host.Services.GetRequiredService<ICompletionsClient>();

        private static IHost RegisterServices()
        {
            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureAppConfiguration((ctx, builder) =>
            {
                var hostingEnvironment = ctx.HostingEnvironment;
                var contentPath = hostingEnvironment.ContentRootPath;
                var environmentName = hostingEnvironment.EnvironmentName;
                builder
                    .AddJsonFile(Path.Combine(contentPath, "appsettings.json"));
            });

            builder.ConfigureServices((ctx, services) =>
            {
                services.AddBrokers(ctx.Configuration, "OpenAIApiConfiguration");
                services.AddTransient<ICompletionService, CompletionService>();
                services.AddTransient<ICompletionsClient, CompletionsClient>();
            });

            IHost host = builder.Build();
            
            return host;
        }

    }
}
