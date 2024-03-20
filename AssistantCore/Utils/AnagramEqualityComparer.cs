using System;
using System.Collections.Generic;

namespace AssistantCore.Utils
{
    public class AnagramEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y) => getCanonicalString(x) == getCanonicalString(y);

        public int GetHashCode(string obj) => getCanonicalString(obj).GetHashCode();

        private string getCanonicalString(string word)
        {
            char[] wordChars = word.ToCharArray();
            Array.Sort(wordChars);
            return new string(wordChars);
        }
    }
}
