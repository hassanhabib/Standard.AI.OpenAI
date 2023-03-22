// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace OpenAI.NET.Models.Configurations
{
    public class HttpPolicySettings
    {
        public bool EnableRetry { get; set; }

        public int NumberOfRetries { get; set; }

        public List<object> WaitingAmongRetries { get; set; } = new List<object>();

        public bool EnableCircuitBreaker { get; set; }
        
        public int MaxNumberOffailures { get; set; }

        public object BreakCircuitFor { get; set; }

        public object Timeout { get; set; }
    }
}
