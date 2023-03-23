// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Models.Exceptions
{
    internal class ModelDependencyException : Xeption
    {
        public ModelDependencyException(Xeption innerException)
            : base(message: "Model dependency error occurred, contact support.",
                  innerException)
        { }
    }
}