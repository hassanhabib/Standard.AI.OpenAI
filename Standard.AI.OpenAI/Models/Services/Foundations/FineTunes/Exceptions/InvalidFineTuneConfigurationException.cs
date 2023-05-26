// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions
{
    internal class InvalidFineTuneConfigurationException : Xeption
    {
        public InvalidFineTuneConfigurationException(Exception innerException)
            : base(message: "Invalid fine tune configuration error ocurred, contact support.",
                  innerException)
        { }
    }
}
