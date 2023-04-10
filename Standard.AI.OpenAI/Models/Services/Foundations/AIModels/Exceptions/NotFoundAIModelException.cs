using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions
{
    public class NotFoundAIModelException : Xeption
    {
        public NotFoundAIModelException(Exception innerException)
            : base(message: "Model does not exist error occurred, check that the model name is correct.", innerException)
        { }
    }
}
