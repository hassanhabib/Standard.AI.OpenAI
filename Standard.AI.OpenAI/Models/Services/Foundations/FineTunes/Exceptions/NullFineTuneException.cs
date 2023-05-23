// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions
{
    public class NullFineTuneException : Xeption
    {
        public NullFineTuneException()
            : base(message: "Fine tune is null.")
        { }
    }
}
