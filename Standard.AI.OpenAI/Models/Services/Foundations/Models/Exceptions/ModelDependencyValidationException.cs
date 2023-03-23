// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Models.Exceptions
{
    internal class ModelDependencyValidationException : Xeption
    {
        public ModelDependencyValidationException(Xeption innerException)
            : base(message: "Model dependency validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}