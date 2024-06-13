// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions
{
    public class FineTuneValidationException : Xeption
    {
        public FineTuneValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}