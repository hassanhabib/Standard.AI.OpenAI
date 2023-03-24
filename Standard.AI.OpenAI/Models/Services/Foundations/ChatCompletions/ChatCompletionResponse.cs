// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions
{
    public class ChatCompletionResponse
    {
        public string Id { get; set; }

        public string Object { get; set; }

        public int Created { get; set; }

        public ChatCompletionChoice[] Choices { get; set; }

        public ChatCompletionUsage Usage { get; set; }
    }
}
