using System;
using System.Linq;

namespace AssistantCore.Algorithms
{
    public class StringTests
    {
        public int ParseFromString(string ids)
        {
            var splitted = ids.Split('/', StringSplitOptions.RemoveEmptyEntries);

            var converted = splitted.Select(s => int.Parse(s));
            #region Display
            Console.WriteLine(converted.Count());
            foreach (var item in converted)
            {
                Console.WriteLine(item);
            }
            #endregion
            return converted.Count();
        }
    }
}
