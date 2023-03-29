// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;

namespace Standard.AI.OpenAI.Brokers.DateTimes
{
    public class DateTimeBroker : IDateTimeBroker
    {
        public DateTimeOffset ConvertToDateTimeOffSet(uint totalSeconds) =>
            DateTimeOffset.FromUnixTimeSeconds(totalSeconds);
    }
}
