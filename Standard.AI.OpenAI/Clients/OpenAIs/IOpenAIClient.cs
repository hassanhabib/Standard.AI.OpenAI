// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using OpenAI.NET.Clients.Completions;

namespace OpenAI.NET.Clients.OpenAIs
{
    public interface IOpenAIClient
    {
        ICompletionsClient Completions { get; set; }
    }
}
