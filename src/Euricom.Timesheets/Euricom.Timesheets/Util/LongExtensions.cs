using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Euricom.Timesheets.Util
{
    public static class LongExtensions
    {
        public static DateTime FromUnixTicks(this long ticks)
        {
            var date = new DateTime(1970, 1, 1);
            date = date.AddMilliseconds(ticks);
            return date;
        }
    }
}