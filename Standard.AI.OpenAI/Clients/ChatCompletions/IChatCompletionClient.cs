// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

namespace Standard.AI.OpenAI.Clients.ChatCompletions
{
    public interface IChatCompletionClient
    {
        ValueTask<ChatCompletion> PostChatCompletionAsync(ChatCompletion chatCompletion);
    }
}
