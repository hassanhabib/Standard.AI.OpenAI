// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;

namespace Standard.AI.OpenAI.Brokers.DateTimes
{
    public class DateTimeBroker : IDateTimeBroker
    {
        public DateTimeOffset ConvertToDateTimeOffSet(int totalSeconds) =>
            DateTimeOffset.FromUnixTimeSeconds(totalSeconds);
    }
}
