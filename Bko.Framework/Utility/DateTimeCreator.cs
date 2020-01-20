using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bko.Framework.Utility
{
    public static class DateTimeCreator
    {
        public static DateTime With0Am(DateTime dt)
        {
            return new DateTime(dt.Date.Year, dt.Date.Month, dt.Date.Day, 0, 0, 0);
        }
        public static DateTime With0Pm(DateTime dt)
        {
            return new DateTime(dt.Date.Year, dt.Date.Month, dt.Date.Day, 23, 59, 59);
        }
    }
}
