// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Collections.Generic;
using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

namespace OpenAI.NET.Infrastructure.Build
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Creation of the GitHub pull request and master build pipeline.
            Workflows.GitHubPipeline();

            // Dependabot to keep the actions and packages updated to the latest versions.
            Workflows.Dependabot();
        }
    }
}