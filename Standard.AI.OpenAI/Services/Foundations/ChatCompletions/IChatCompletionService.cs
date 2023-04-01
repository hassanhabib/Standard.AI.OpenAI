// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

namespace Standard.AI.OpenAI.Services.Foundations.ChatCompletions
{
    internal interface IChatCompletionService
    {
        ValueTask<ChatCompletion> SendChatCompletionAsync(ChatCompletion chatCompletion);
    }
}
