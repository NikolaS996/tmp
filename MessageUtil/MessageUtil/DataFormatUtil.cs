using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageUtil
{
    public class DataFormatUtil
    {
        public static string GetFormatedDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //This one differs from the first one for adding milliseconds in string
        //But as it's independent from the other format - it can be changed to suit needs better
        public static string GetFormatedLongDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
