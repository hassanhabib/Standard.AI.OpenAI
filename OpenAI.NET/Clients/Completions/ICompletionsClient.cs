// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Models.Services.Foundations.Completions;

namespace OpenAI.NET.Clients.Completions
{
    public interface ICompletionsClient
    {
        ValueTask<Completion> PromptCompletionAsync(Completion completion);
    }
}
