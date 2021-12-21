using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CymaticLabs.InfluxDB.Studio.Util
{
    public class DateTimeUtil
    {
       public static long UnixTime(DateTime dateTime)
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (dateTime - epochStart).Ticks * 100;
        }
       public static DateTime UnixTimeToDateTime(long unixTime)
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return new DateTime((epochStart).Ticks + unixTime / 100);
        }
    }
}
