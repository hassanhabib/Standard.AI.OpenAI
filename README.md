# Standard.AI.OpenAI

<div align=center>
	<img width="300" src="https://raw.githubusercontent.com/hassanhabib/OpenAI.NET/main/Standard.AI.OpenAI/artificial-intelligence.png" />
</div>

<br />

[![.NET](https://github.com/hassanhabib/OpenAI.NET/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hassanhabib/OpenAI.NET/actions/workflows/dotnet.yml)
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
https://platform.openai.com/signup

### API Keys
Once you've created an OpenAI account. Now, go ahead and generate an API key from the following link:
https://platform.openai.com/account/api-keys

### Hello, World!
Once you've completed the aforementioned steps, let's write our very first program with Standard.AI.OpenAI as follows:

#### Standard.AI.OpenAI Nuget
Find our nuget package.

#### Program.cs
The following example demonstrate how you can write your first Completions program.

```csharp
var openAiApiConfigurations = new ApiConfigurations
{
	ApiKey = "YOUR_API_KEY_HERE",
	OrganizationId = "YOUR_OPTIONAL_ORG_ID_HERE"
}

var openAiClient = new OpenAIClient(openAiApiConfigurations);

var inputCompletion = new Completion
{
	Request = new CompletionRequest
	{
		Prompts = new string[] {"Human: Hello!"}
		Model = "text-davinci-003"
	};
}

Completion resultCompletion =
	await this.openAiClient.Completions.PostCompletionAsync(
		inputCompletion);

Array.ForEach(resultCompletion.Response.Prompts, Console.WriteLine);

```

## How to Contribute
If you want to contribute to this project please review the following documents:
- [The Standard](https://github.com/hassanhabib/The-Standard)
- [C# Coding Standard](https://github.com/hassanhabib/CSharpCodingStandard)
- [The Team Standard](https://github.com/hassanhabib/The-Standard-Team)

If you have a question make sure you either open an issue or join our [The Standard Community](https://discord.com/invite/vdPZ7hS52X) discord server.

## Live-Sessions
Our live-sessions are schedules on [The Standard Calendar](https://tinyurl.com/the-standard-calendar) make sure you adjust the time to your city/timezone to to be able to join us.

We broadcast on multiple platforms:
<br /> YouTube
<br /> LinkedIn
<br /> Twitch
<br /> Twitter
<br /> Facebook

### Past-Sessions
Here's our live sessions to show you how this library was developed from scratch:

<img width="20" src="https://camo.githubusercontent.com/3d319610708f36a324d601b85d0d29c4e90344a8768f45c05136b92a877fd755/68747470733a2f2f7777772e7365617263686d61726b6574696e676175737472616c69612e636f6d2e61752f77702d636f6e74656e742f75706c6f6164732f323031372f31302f6f726967696e616c5f696d616765735f596f75547562652e706e67" target="_blank" style="max-width: 100%;"  />[OPENAI000: Getting Started](https://www.youtube.com/watch?v=JQnTpGV-7YA)&nbsp;

<img width="20" src="https://camo.githubusercontent.com/3d319610708f36a324d601b85d0d29c4e90344a8768f45c05136b92a877fd755/68747470733a2f2f7777772e7365617263686d61726b6574696e676175737472616c69612e636f6d2e61752f77702d636f6e74656e742f75706c6f6164732f323031372f31302f6f726967696e616c5f696d616765735f596f75547562652e706e67" target="_blank" style="max-width: 100%;"  /> [OPENAI001: API Integrations](https://www.youtube.com/watch?v=2eN4ht2uESo)&nbsp;

<img width="20" src="https://camo.githubusercontent.com/3d319610708f36a324d601b85d0d29c4e90344a8768f45c05136b92a877fd755/68747470733a2f2f7777772e7365617263686d61726b6574696e676175737472616c69612e636f6d2e61752f77702d636f6e74656e742f75706c6f6164732f323031372f31302f6f726967696e616c5f696d616765735f596f75547562652e706e67" target="_blank" style="max-width: 100%;"  /> [OPENAI002: API Integrations](https://youtube.com/live/IrBGCAyLmmQ?si=EnSIkaIECMiOmarE)&nbsp;

<img width="20" src="https://camo.githubusercontent.com/3d319610708f36a324d601b85d0d29c4e90344a8768f45c05136b92a877fd755/68747470733a2f2f7777772e7365617263686d61726b6574696e676175737472616c69612e636f6d2e61752f77702d636f6e74656e742f75706c6f6164732f323031372f31302f6f726967696e616c5f696d616765735f596f75547562652e706e67" target="_blank" style="max-width: 100%;"  /> [OPENAI003: API Integrations](https://youtube.com/live/SZHBt0SW2EY?si=EnSIkaIECMiOmarE)&nbsp;

<img width="20" src="https://camo.githubusercontent.com/3d319610708f36a324d601b85d0d29c4e90344a8768f45c05136b92a877fd755/68747470733a2f2f7777772e7365617263686d61726b6574696e676175737472616c69612e636f6d2e61752f77702d636f6e74656e742f75706c6f6164732f323031372f31302f6f726967696e616c5f696d616765735f596f75547562652e706e67" target="_blank" style="max-width: 100%;"  /> [OPENAI004: Completion Service](https://youtube.com/live/z0BlU3KVr7E?si=EnSIkaIECMiOmarE)&nbsp;

<img width="20" src="https://camo.githubusercontent.com/3d319610708f36a324d601b85d0d29c4e90344a8768f45c05136b92a877fd755/68747470733a2f2f7777772e7365617263686d61726b6574696e676175737472616c69612e636f6d2e61752f77702d636f6e74656e742f75706c6f6164732f323031372f31302f6f726967696e616c5f696d616765735f596f75547562652e706e67" target="_blank" style="max-width: 100%;"  /> [OPENAI005: Completion Service](https://youtube.com/live/wMC1I85Zjmo?si=EnSIkaIECMiOmarE)&nbsp;

<img width="20" src="https://camo.githubusercontent.com/3d319610708f36a324d601b85d0d29c4e90344a8768f45c05136b92a877fd755/68747470733a2f2f7777772e7365617263686d61726b6574696e676175737472616c69612e636f6d2e61752f77702d636f6e74656e742f75706c6f6164732f323031372f31302f6f726967696e616c5f696d616765735f596f75547562652e706e67" target="_blank" style="max-width: 100%;"  /> [OPENAI006: Developing Completion Clients](https://youtube.com/live/aENHthpFTvI?si=EnSIkaIECMiOmarE)&nbsp;

<img width="20" src="https://camo.githubusercontent.com/3d319610708f36a324d601b85d0d29c4e90344a8768f45c05136b92a877fd755/68747470733a2f2f7777772e7365617263686d61726b6574696e676175737472616c69612e636f6d2e61752f77702d636f6e74656e742f75706c6f6164732f323031372f31302f6f726967696e616c5f696d616765735f596f75547562652e706e67" target="_blank" style="max-width: 100%;"  /> [OPENAI007: Acceptance & Integration Tests](https://youtube.com/live/fzlF2k7SM?si=EnSIkaIECMiOmarE)&nbsp;

<img width="20" src="https://camo.githubusercontent.com/3d319610708f36a324d601b85d0d29c4e90344a8768f45c05136b92a877fd755/68747470733a2f2f7777772e7365617263686d61726b6574696e676175737472616c69612e636f6d2e61752f77702d636f6e74656e742f75706c6f6164732f323031372f31302f6f726967696e616c5f696d616765735f596f75547562652e706e67" target="_blank" style="max-width: 100%;"  /> [OPENAI008: Acceptance & Integration Tests](https://www.youtube.com/live/86bfBmkUMFo?feature=share)&nbsp;

<img width="20" src="https://camo.githubusercontent.com/3d319610708f36a324d601b85d0d29c4e90344a8768f45c05136b92a877fd755/68747470733a2f2f7777772e7365617263686d61726b6574696e676175737472616c69612e636f6d2e61752f77702d636f6e74656e742f75706c6f6164732f323031372f31302f6f726967696e616c5f696d616765735f596f75547562652e706e67" target="_blank" style="max-width: 100%;"  /> [OPENAI009: Publishing Nuget Package](https://www.youtube.com/live/saU_5peAC-Y?feature=share)&nbsp;