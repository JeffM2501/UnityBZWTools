using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Types
{
    public static class Utilities
    {
        public static string[] GetStringList<T>(IEnumerable<T> inList)
        {
            List<string> outList = new List<string>();
            foreach (var o in inList)
                outList.Add(o.ToString());

            return outList.ToArray();
        }
    }
}
