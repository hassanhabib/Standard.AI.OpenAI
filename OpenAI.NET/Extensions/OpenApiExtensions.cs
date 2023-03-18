// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using OpenAI.NET.Brokers.OpenAIs;
using OpenAI.NET.Clients.Completions;
using OpenAI.NET.Clients.OpenAIs;
using OpenAI.NET.Models.Configurations;
using OpenAI.NET.Services.Foundations.Completions;

namespace OpenAI.NET.Extensions
{
    public static class OpenApiExtensions
    {
        public static IServiceCollection UseOpenAiNet(
            this IServiceCollection services,
            ApiConfigurations apiConfigurations)
        {
            services.AddTransient<IOpenAIBroker, OpenAIBroker>();
            services.AddTransient<ICompletionService, CompletionService>();
            services.AddTransient<ICompletionsClient, CompletionsClient>();
            services.AddTransient<IOpenAIClient, OpenAIClient>();
            services.AddSingleton(options => apiConfigurations);

            return services;
        }
    }
}