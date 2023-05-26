// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions
{
    public class InvalidFineTuneException : Xeption
    {
        public InvalidFineTuneException()
            : base(message: "Fine tune is invalid.")
        { }

        public InvalidFineTuneException(Exception innerException)
            : base(
                message: "Fine tune is invalid.",
                innerException)
        { }
    }

}
