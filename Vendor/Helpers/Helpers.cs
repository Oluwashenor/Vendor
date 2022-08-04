using System;
using System.Collections.Generic;

namespace Vendor.Helpers
{
    public class Helpers
    {
        public List<string> EnumLooper<T>()
        {
            var result = new List<string>();
            foreach (var item in Enum.GetNames(typeof(T)))
            {
                result.Add(item);
            }
            return result;
        }
    }
}
