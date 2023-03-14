// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.NET.Models.ExternalCompletions
{
    public class CompletionResponse
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("_object")]
        public string _object { get; set; }
        [JsonProperty("created")]
        public int created { get; set; }
        [JsonProperty("model")]
        public string model { get; set; }
        publ[JsonProperty("choices")]ic Choice
            [] choices { get; set; }
        [JsonProperty("usage")]
        public Usage usage { get; set; }
    }

    public class Usage
    {
        [JsonProperty("prompt_tokens")]
        public int prompt_tokens { get; set; }
        [JsonProperty("completion_tokens")]
        public int completion_tokens { get; set; }
        [JsonProperty("total_tokens")]
        public int total_tokens { get; set; }
    }

    public class Choice
    {
        [JsonProperty("text")]
        public string text { get; set; }
        [JsonProperty("index")]
        public int index { get; set; }
        [JsonProperty("logprobs")]
        public object logprobs { get; set; }
        [JsonProperty("finish_reason")]
        public string finish_reason { get; set; }
    }

}