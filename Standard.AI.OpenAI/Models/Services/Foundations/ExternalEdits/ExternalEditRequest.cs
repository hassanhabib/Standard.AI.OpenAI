// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalEdits
{
    internal class ExternalEditRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("instruction")]
        public string Instruction { get; set; }

        [JsonProperty("n")]
        public int NumberOfEdits { get; set; }

        [JsonProperty("temperature")]
        public decimal Temperature { get; set; }

        [JsonProperty("top_p")]
        public double ProbabilityMass { get; set; }
    }
}
