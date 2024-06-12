// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions
{
    public class FineTuneDependencyException : Xeption
    {
        public FineTuneDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}