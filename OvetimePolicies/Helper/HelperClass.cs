using System.Globalization;

namespace OvetimePolicies.Helper
{
    public static class HelperClass
    {
        /// <summary>
        /// Convert Custome date string to datetime
        /// </summary>
        /// <param name="date">string persian date</param>
        /// <returns></returns>
        public static DateTime ConverDate(string date)
        {
            var persianYear = date.Substring(0, 4);
            var persianMonth= date.Substring(4, 2);
            var persianDay = date.Substring(6, 2);
            PersianCalendar persianCalendar = new PersianCalendar();

            return persianCalendar.ToDateTime(int.Parse(persianYear), int.Parse(persianMonth), int.Parse(persianMonth), 0, 0,
                0, 0);
        }
    }
}
