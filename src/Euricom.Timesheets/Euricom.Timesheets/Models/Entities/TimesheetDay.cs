using System;
using Newtonsoft.Json;
using Euricom.Timesheets.Util;
using Newtonsoft.Json.Converters;

namespace Euricom.Timesheets.Models.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TimesheetDay
    {
        [JsonProperty(PropertyName = "Date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "IsWeekend")]
        public bool IsWeekend { get; set; }

        [JsonProperty(PropertyName = "WorkingDay")]
        public string WorkingDay { get; set; }
    }
}