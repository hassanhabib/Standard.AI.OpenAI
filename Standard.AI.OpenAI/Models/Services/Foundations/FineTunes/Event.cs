// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes
{
    public class Event
    {
        public string Type { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public int CreatedDate { get; set; }
    }
}
