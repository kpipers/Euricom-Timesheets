using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Euricom.Timesheets.Models
{
    public class HomeModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Month")]
        public int Month { get; set; }

        public IEnumerable<SelectListItem> Months
        {
            get
            {
                var months = DateTimeFormatInfo.InvariantInfo.MonthNames.Select((monthName, index) => new SelectListItem
                {
                    Value = (index + 1).ToString(),
                    Text = monthName
                }).ToList();

                months.RemoveAt(12); // 13th item is empty

                return months;
            }
        }

        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }

        public IEnumerable<SelectListItem> Years
        {
            get
            {

                var currentYear = DateTime.Now.Year.ToString();

                return new List<SelectListItem>
                           {
                               new SelectListItem {Value = currentYear, Text = currentYear}
                           };
            }
        }
    }
}