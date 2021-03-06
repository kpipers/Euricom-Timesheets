﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Euricom.Timesheets.Models.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Timesheet : Entity
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }

        [JsonProperty(PropertyName = "month")]
        public int Month { get; set; }

        [JsonProperty(PropertyName = "timesheetDays")]
        public IEnumerable<TimesheetDay> WorkingDays { get; set; }
    }
}