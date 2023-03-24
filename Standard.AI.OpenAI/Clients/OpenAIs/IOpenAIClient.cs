// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Standard.AI.OpenAI.Clients.Completions;

namespace Standard.AI.OpenAI.Clients.OpenAIs
{
    public interface IOpenAIClient
    {
        ICompletionsClient Completions { get; }
    }
}
