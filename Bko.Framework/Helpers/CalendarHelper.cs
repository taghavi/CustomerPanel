using System;
using System.Globalization;

namespace Framework.Helpers
{
    public static class CalendarHelper
    {
        public static NameValue ToShamsi(this DateTime dateTime)
        {
            try
            {

                PersianCalendar pc = new PersianCalendar();
                int pcYear = pc.GetYear(dateTime);
                int pcMonth = pc.GetMonth(dateTime);

                int pcDay = pc.GetDayOfMonth(dateTime);
                string convertedDate = string.Format("{0}/{1}/{2}", pcYear, pcMonth < 10 ? "0" + pcMonth.ToString() : pcMonth.ToString(), pcDay < 10 ? "0" + pcDay.ToString() : pcDay.ToString());

                return new NameValue(GetPersianDate(pc.GetDayOfWeek(dateTime)), convertedDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static NameValue ToShamsiLong(this DateTime dateTime)
        {
            try
            {

                PersianCalendar pc = new PersianCalendar();
                int pcYear = pc.GetYear(dateTime);
                int pcMonth = pc.GetMonth(dateTime);

                int pcDay = pc.GetDayOfMonth(dateTime);
                int pcHour = pc.GetHour(dateTime);
                int pcMin = pc.GetMinute(dateTime);
                int pcSec = pc.GetSecond(dateTime);
                string convertedDate = string.Format("{0}-{1}-{2} {3}:{4}:{5}", pcYear,
                    pcMonth < 10 ? "0" + pcMonth.ToString() : pcMonth.ToString(), 
                    pcDay < 10 ? "0" + pcDay.ToString() : pcDay.ToString(),
                    pcHour < 10 ? "0" + pcHour.ToString() : pcHour.ToString(),
                    pcMin < 10 ? "0" + pcMin.ToString() : pcMin.ToString(),
                    pcSec < 10 ? "0" + pcSec.ToString() : pcSec.ToString());

                return new NameValue(GetPersianDate(pc.GetDayOfWeek(dateTime)), convertedDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GetPersianDate(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "جمعه";

                case DayOfWeek.Saturday:
                    return "شنبه";

                case DayOfWeek.Sunday:
                    return "یکشنبه";

                case DayOfWeek.Monday:
                    return "دوشنبه";

                case DayOfWeek.Tuesday:
                    return "سه شنبه";

                case DayOfWeek.Wednesday:
                    return "چهارشنبه";
                case DayOfWeek.Thursday:
                    return "پنجشنبه";

            }

            return "";
        }

        public static string GetTwoDatesIntervals(DateTime fromDate, DateTime toDate)
        {
            try
            {
                NameValue from = ToShamsi(fromDate);
                NameValue to = ToShamsi(toDate);
                return string.Format("از {0} {1} تا {2} {3}", from.Name, from.Value, to.Name, to.Value);
            }
            catch (Exception ex)
            {
                return "";
            }


        }


    }
}