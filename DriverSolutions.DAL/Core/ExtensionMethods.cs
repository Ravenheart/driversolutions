using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.DAL
{
    public static class ExtensionMethods
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek? startOfWeek = null)
        {
            if (startOfWeek == null)
                startOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            int diff = dt.DayOfWeek - startOfWeek.Value;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime StartOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
        }

        /// <summary>
        /// Returns the last occurance of the specified day
        /// </summary>
        /// <param name="dt">Start date</param>
        /// <param name="day">Day to find</param>
        /// <param name="ignoreCurrent">Ignore the current day if it matches</param>
        /// <returns></returns>
        public static DateTime PreviousOccurance(this DateTime dt, DayOfWeek day, bool ignoreCurrent = true)
        {
            if (!ignoreCurrent && dt.DayOfWeek == day)
                return dt.AddDays(-7);

            while (dt.DayOfWeek != day)
            {
                dt = dt.AddDays(-1);
            }

            return dt;
        }

        /// <summary>
        /// Removes any illegal characters from the fileName and returns a safe one
        /// </summary>
        /// <param name="fileName">FileName</param>
        /// <returns></returns>
        public static string SafeFileName(string fileName)
        {
            char[] buffer = new char[fileName.Length];
            int position = 0;

            char[] illegal = Path.GetInvalidFileNameChars();
            for (int i = 0; i < fileName.Length; i++)
            {
                if (!illegal.Contains(fileName[i]))
                    buffer[position++] = fileName[i];
            }

            return new string(buffer, 0, position);
        }
    }
}
