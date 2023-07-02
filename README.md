# Standard.AI.OpenAI

![Standard.AI.OpenAI](https://raw.githubusercontent.com/hassanhabib/Standard.AI.OpenAI/main/Standard.AI.OpenAI/standard-ai-openai-cover-big.png)


[![.NET](https://github.com/hassanhabib/OpenAI.NET/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hassanhabib/OpenAI.NET/actions/workflows/dotnet.yml)
[![Nuget](https://img.shields.io/nuget/v/Standard.AI.OpenAI?logo=nuget)](https://www.nuget.org/packages/Standard.AI.OpenAI)
![Nuget](https://img.shields.io/nuget/dt/Standard.AI.OpenAI?color=blue&label=Downloads)
[![The Standard - COMPLIANT](https://img.shields.io/badge/The_Standard-COMPLIANT-2ea44f)](https://github.com/hassanhabib/The-Standard)
[![The Standard Community](https://img.shields.io/discord/934130100008538142?color=%237289da&label=The%20Standard%20Community&logo=Discord)](https://discord.gg/vdPZ7hS52X)

## Introduction
Standard.AI.OpenAI is a Standard-Compliant .NET library built on top of OpenAI API RESTful endpoints to enable software engineers to develop AI-Powered solutions in .NET.

## Standard-Compliance
This library was built according to The Standard. The library follows engineering principles, patterns and tooling as recommended by The Standard.

This library is also a community effort which involved many nights of pair-programming, test-driven development and in-depth exploration research and design discussions.

## How to use this library?
In order to use this library there are prerequists that you must complete before you can write your first AI-Powered C#.NET program. These steps are as follows:

### OpenAI Account
You must create an OpenAI account with the following link:
[Click here](https://platform.openai.com/signup)

### Nuget Package 
Install the Standard.AI.OpenAI library in your project.
Use the method best suited for your development preference listed at the Nuget Link above or below.

[![Nuget](https://img.shields.io/nuget/v/Standard.AI.OpenAI?logo=nuget)](https://www.nuget.org/packages/Standard.AI.OpenAI)

### API Keys
Once you've created an OpenAI account. Now, go ahead and generate an API key from the following link:
[Click here](https://platform.openai.com/account/api-keys)

### Hello, World!
Once you've completed the aforementioned steps, let's write our very first program with Standard.AI.OpenAI as follows:

### Completions
The following example demonstrate how you can write your first Completions program.

#### Program.cs
```csharp
using System;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;

namespace ExampleOpenAIDotNet
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var openAIConfigurations = new OpenAIConfigurations
            {
                ApiKey = "YOUR_API_KEY_HERE",
                OrganizationId = "YOUR_OPTIONAL_ORG_ID_HERE"
            };

            var openAIClient = new OpenAIClient(openAIConfigurations);

            var inputCompletion = new Completion
            {
                Request = new CompletionRequest
                {
                    Prompts = new string[] { "Human: Hello!" },

                    Model = "text-davinci-003"
                }
            };

            Completion resultCompletion =
                await openAIClient.Completions.PromptCompletionAsync(
                    inputCompletion);

            Array.ForEach(
                resultCompletion.Response.Choices, 
                choice => Console.WriteLine(choice.Text));
        }
    }
}
```

### Chat Completions
The following example demonstrate how you can write your first Chat Completions program.

#### Program.cs
```csharp
using System;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

namespace ExampleOpenAIDotNet
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var openAIConfigurations = new OpenAIConfigurations
            {
                ApiKey = "YOUR_API_KEY_HERE",
                OrganizationId = "YOUR_OPTIONAL_ORG_ID_HERE"
            };

            var openAIClient = new OpenAIClient(openAIConfigurations);

            var chatCompletion = new ChatCompletion
            {
                Request = new ChatCompletionRequest
                {
                    Model = "gpt-3.5-turbo",
                    Messages = new ChatCompletionMessage[]
                    {
                        new ChatCompletionMessage
                        {
                            Content = "What is c#?",
                            Role = "user",
                        }
                    },
                }
            };

            ChatCompletion resultChatCompletion =
                await openAIClient.ChatCompletions.SendChatCompletionAsync(
                    chatCompletion);

            Array.ForEach(
                resultChatCompletion.Response.Choices,
                choice => Console.WriteLine(
                    value: $"{choice.Message.Role}: {choice.Message.Content}"));
        }
    }
}
```

### Fine-Tunes
The following example demonstrate how you can write your first Fine-tunes program.

#### Program.cs
```csharp
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;

namespace Examples.Standard.AI.OpenAI.Clients.FineTunes
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var openAIConfigurations = new OpenAIConfigurations
            {
                ApiKey = "YOUR_API_KEY_HERE",
                ApiUrl = "https://api.openai.com"
            };

            IOpenAIClient openAIClient =
                new OpenAIClient(openAIConfigurations);

            MemoryStream memoryStream = CreateRandomStream();

            var aiFile = new AIFile
            {
                Request = new AIFileRequest
                {
                    Name = "Test",
                    Content = memoryStream,
                    Purpose = "fine-tune"
                }
            };

            AIFile file = await openAIClient.AIFiles
                .UploadFileAsync(aiFile);

            var fineTune = new FineTune();
            fineTune.Request = new FineTuneRequest();

            fineTune.Request.FileId =
                file.Response.Id;

            FineTune fineTuneResult =
                await openAIClient.FineTuneClient
                    .SubmitFineTuneAsync(fineTune);

            Console.WriteLine(fineTuneResult);
        }

        private static MemoryStream CreateRandomStream()
        {
            string content = "{\"prompt\": \"<prompt text>\", \"completion\": \"<ideal generated text>\"}";

            return new MemoryStream(Encoding.UTF8.GetBytes(content));
        }
    }
}
```

#### Exceptions

Standard.AI.OpenAI may throw following exceptions:

| Exception Name | When it will be thrown |
| --- | --- |
| `ChatCompletionClientValidationException` | This exception is thrown when a validation error occurs while using the chat completion client. For example, if required data is missing or invalid. |
| `ChatCompletionClientDependencyException` | This exception is thrown when a dependency error occurs while using the chat completion client. For example, if a required dependency is unavailable or incompatible. |
| `ChatCompletionClientServiceException` | This exception is thrown when a service error occurs while using the chat completion client. For example, if there is a problem with the server or any other service failure. |
| `FineTuneClientValidationException` | This exception is thrown when a validation error occurs while using the fine-tunes client. For example, if required data is missing or invalid. |
| `FineTuneClientDependencyException` | This exception is thrown when a dependency error occurs while using the fine-tunes client. For example, if a required dependency is unavailable or incompatible. |
| `FineTuneClientDependencyException` | This exception is thrown when a service error occurs while using the fine-tunes client. For example, if there is a problem with the server or any other service failure. |


## How to Contribute
If you want to contribute to this project please review the following documents:
- [The Standard](https://github.com/hassanhabib/The-Standard)
- [C# Coding Standard](https://github.com/hassanhabib/CSharpCodingStandard)
- [The Team Standard](https://github.com/hassanhabib/The-Standard-Team)

If you have a question make sure you either open an issue or join our [The Standard Community](https://discord.com/invite/vdPZ7hS52X) discord server.

## Live-Sessions
Our live-sessions are scheduled on [The Standard Calendar](https://tinyurl.com/the-standard-calendar) make sure you adjust the time to your city/timezone to be able to join us.

We broadcast on multiple platforms:

[YouTube](https://www.youtube.com/@HassanHabib)

[LinkedIn](https://www.linkedin.com/in/hassanrezkhabib/)

[Twitch](https://www.twitch.tv/binhabib)

[Twitter](https://twitter.com/HassanRezkHabib)

[Facebook](https://www.facebook.com/hassan.rezk.habib)

### Past-Sessions
Here's our live sessions to show you how this library was developed from scratch:

[Standard.AI.OpenAI YouTube Playlist](https://www.youtube.com/watch?v=JQnTpGV-7YA&list=PLan3SCnsISTTl_MnGP6B78Nfu9Ix8G4mU)