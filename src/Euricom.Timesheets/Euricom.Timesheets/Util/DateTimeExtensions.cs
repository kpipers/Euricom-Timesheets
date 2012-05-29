using System;

namespace Euricom.Timesheets.Util
{
    public static class DateTimeExtensions
    {
        public static double UnixTicks(this DateTime datetime)
        {
            var unixEpoch = new DateTime(1970, 1, 1);
            var timeSpan = new TimeSpan(datetime.ToUniversalTime().Ticks - unixEpoch.Ticks);
            return timeSpan.TotalMilliseconds;
        }
    }
}