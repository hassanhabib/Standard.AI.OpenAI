// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions
{
    public class FailedServerFineTuneException : Xeption
    {
        public FailedServerFineTuneException(Exception innerException)
            : base(message: "Failed fine tune server error occurred, contact support.",
                  innerException)
        { }
    }
}
