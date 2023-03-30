// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Standard.AI.OpenAI.Clients.ChatCompletions;
using Standard.AI.OpenAI.Clients.Completions;
using Standard.AI.OpenAI.Clients.ImageGenerations;

namespace Standard.AI.OpenAI.Clients.OpenAIs
{
    public interface IOpenAIClient
    {
        ICompletionsClient Completions { get; }
        IChatCompletionsClient ChatCompletions { get; }
        IImageGenerationsClient ImageGenerations { get; }
    }
}
