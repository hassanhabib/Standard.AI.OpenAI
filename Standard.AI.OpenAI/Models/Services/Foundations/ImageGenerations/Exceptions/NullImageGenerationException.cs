// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class NullImageGenerationException : Xeption
    {
        public NullImageGenerationException(string message)
            : base(message)
        { }

        public NullImageGenerationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}