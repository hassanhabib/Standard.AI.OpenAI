// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

namespace Standard.AI.OpenAI.Models.Services.Foundations.Models
{
    public class Model
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int Created { get; set; }
        public string OwnedBy { get; set; }
        public Permission[] Permission { get; set; }
        public string Root { get; set; }
    }
}
