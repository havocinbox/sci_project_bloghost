using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bloghost.UI.Web.Infrastructure
{
    public class Const
    {
        public static IEnumerable<string> Months()
        {
            return new List<string>(12)
                    {
                        "January",
                        "February",
                        "March",
                        "April",
                        "May",
                        "June",
                        "July",
                        "August",
                        "September",
                        "October",
                        "November",
                        "December",
                    };

        }

        public static IEnumerable<string> Days()
        {
            var result = new List<string>(31);
            for (var numberDay = 1; numberDay <= 31; numberDay++)
            {
                result.Add(numberDay.ToString());
            }
            return result;
        }

        public static IEnumerable<string> Years()
        {
            var count = DateTime.Now.Year - 1900;
            var result = new List<string>(count);
            for (var year = 1900; year <= DateTime.Now.Year; year++)
            {
                result.Add(year.ToString());
            }
            return result;
        }
    }
}