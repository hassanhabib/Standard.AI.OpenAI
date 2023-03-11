using System.Collections.Generic;
using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

var adoNetClient = new ADotNetClient();

var githubPipeline = new GithubPipeline
{
    Name = "OpenAI.NET Build",

    OnEvents = new Events
    {
        Push = new PushEvent
        {
            Branches = new string[] { "main" }
        },

        PullRequest = new PullRequestEvent
        {
            Branches = new string[] { "main" }
        }
    },

    Jobs = new Jobs
    {
        Build = new BuildJob
        {
            RunsOn = BuildMachines.WindowsLatest,

            Steps = new List<GithubTask>
            {
                new CheckoutTaskV2
                {
                    Name = "Pulling Code"
                },

                new SetupDotNetTaskV1
                {
                    Name = "Installing .NET",

                    TargetDotNetVersion = new TargetDotNetVersion
                    {
                        DotNetVersion = "7.0.201"
                    }
                },

                new DotNetBuildTask
                {
                    Name = "Building Solution"
                },

                new TestTask
                {
                    Name = "Running Tests"
                }
            }
        }
    }
};

adoNetClient.SerializeAndWriteToFile(
    adoPipeline: githubPipeline,
    path: "../../../../.github/workflows/dotnet.yml");
