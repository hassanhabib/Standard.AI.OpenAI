using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions
{
    internal class InvalidAIModelNameException : Xeption
    {
        public InvalidAIModelNameException()
            : base(message: "AI Model Name is invalid.")
        { }
    }
}
