using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace OpenAI.NET.Infrastructure.Build
{
    internal static class Workflows
    {
        /// <summary>
        /// Method handles the creation of GitHub pull request and master build script
        /// </summary>
        public static void GitHubPipeline()
        {
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

                new RestoreTask
                {
                    Name = "Restoring Packages"
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
        }

        public static void Dependabot()
        {
            var adoNetClient = new ADotNetClient();

            var dependabot = new Dependabot()
            {
                Version = 2,
                Updates = new List<Update>()
                {
                    new Update
                    {
                        PackageEcosystem = "nuget",
                        Directory = "/",
                        Schedule = new Schedule
                        {
                            Interval = "daily"
                        },
                        OpenPullRequestsLimit = 5

                    },
                    new Update
                    {
                        PackageEcosystem = "github-actions",
                        Directory = "/",
                        Schedule = new Schedule
                        {
                            Interval = "daily"
                        },
                        OpenPullRequestsLimit = 1
                    }
                }
            };

            adoNetClient.SerializeAndWriteToFile(
                adoPipeline: dependabot,
                path: "../../../../.github/dependabot.yml");
        }
    }

    #region Models should be taken from the ADOTNET package when updated.
    public class Dependabot
    {
        [YamlMember(Alias = "version")]
        public int Version { get; set; }

        [YamlMember(Alias = "updates")]
        public List<Update> Updates { get; set; }
    }

    public class Update
    {
        [YamlMember(Alias = "package-ecosystem")]
        public string PackageEcosystem { get; set; }

        [YamlMember(Alias = "directory")]
        public string Directory { get; set; }

        [YamlMember(Alias = "schedule")]
        public Schedule Schedule { get; set; }

        [YamlMember(Alias = "open-pull-requests-limit")]
        public int OpenPullRequestsLimit { get; set; }
    }

    public class Schedule
    {
        [YamlMember(Alias = "interval")]
        public string Interval { get; set; }
    }
    #endregion // ADOTNET
}
