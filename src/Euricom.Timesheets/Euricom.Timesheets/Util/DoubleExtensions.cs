using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Euricom.Timesheets.Util
{
    public static class DoubleExtensions
    {
        public static DateTime FromUnixTicks(this double ticks)
        {
            var date = new DateTime(1970, 1, 1);
            date = date.AddSeconds(ticks);
            return date;
        }
    }
}