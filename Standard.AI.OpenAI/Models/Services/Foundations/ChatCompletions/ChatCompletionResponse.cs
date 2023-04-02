// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions
{
    public class ChatCompletionResponse
    {
        public string Id { get; set; }

        public string Object { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public ChatCompletionChoice[] Choices { get; set; }

        public ChatCompletionUsage Usage { get; set; }
    }
}
