using System;
using System.Globalization;

namespace AssistantCore.Algorithms
{
    public class DateTimeTests
    {
        public int ParseStringDate()
        {
            string date = "202204201400";

            var result = DateTime.TryParseExact(date, "yyyyMMddHHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsed);              

            Console.WriteLine(parsed.ToLongDateString());

            return int.MinValue;
        }
    }
}
