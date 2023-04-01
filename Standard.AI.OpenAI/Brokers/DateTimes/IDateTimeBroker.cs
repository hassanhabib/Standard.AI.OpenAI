// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;

namespace Standard.AI.OpenAI.Brokers.DateTimes
{
    public interface IDateTimeBroker
    {
        DateTimeOffset ConvertToDateTimeOffSet(int totalSeconds);
    }
}
