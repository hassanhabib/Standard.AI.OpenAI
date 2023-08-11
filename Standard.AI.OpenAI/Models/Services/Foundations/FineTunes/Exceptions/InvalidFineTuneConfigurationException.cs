// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions
{
    public class InvalidFineTuneConfigurationException : Xeption
    {
        public InvalidFineTuneConfigurationException(Exception innerException)
            : base(
                message: "Invalid fine tune configuration error ocurred, contact support.",
                    innerException: innerException)
        { }

        public InvalidFineTuneConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}