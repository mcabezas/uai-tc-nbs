using System;

namespace Time
{
    public class Time : ITime
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

    }
}