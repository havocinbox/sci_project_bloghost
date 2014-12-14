using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Bloghost.UI.Web.Infrastructure
{
    public static class VariablesFactory
    {
        public static IEnumerable<SelectListItem> YearSelectListItems
        {
            get
            {
                var result = new List<SelectListItem>(100) { new SelectListItem { Selected = true, Text = "- Year -", Value = "- Year -" } };
                for (var year = DateTime.Now.Year; year >= 1900; year--)
                {
                    result.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = Const.Years().ToArray()[year - 1900],
                        Value = Const.Years().ToArray()[year - 1900]
                    });
                }
                return result;
            }
        }

        public static IEnumerable<SelectListItem> MonthSelectListItems
        {
            get
            {
                var result = new List<SelectListItem>(12) { new SelectListItem { Selected = true, Text = "- Month -", Value = "- Month -" } };
                for (var numberMonth = 1; numberMonth <= 12; numberMonth++)
                {
                    result.Add(new SelectListItem
                        {
                            Selected = false,
                            Text = Const.Months().ToArray()[numberMonth -1],
                            Value = numberMonth.ToString(CultureInfo.InvariantCulture)
                        });
                }
                return result;
            }
        }

        public static IEnumerable<SelectListItem> DaySelectListItems
        {
            get
            {
                var result = new List<SelectListItem>(31) { new SelectListItem { Selected = true, Text = "- Day -", Value = "- Day -" } };
                for (var day = 1; day <= 31; day++)
                {
                    result.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = Const.Days().ToArray()[day - 1],
                        Value = Const.Days().ToArray()[day - 1]
                    });
                }
                return result;
            }
        }
    }
}