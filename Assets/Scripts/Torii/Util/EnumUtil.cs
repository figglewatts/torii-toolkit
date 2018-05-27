using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Util
{
    public static class EnumUtil
    {
        /// <summary>
        /// Parse a string value to an enum. Case insensitive.
        /// </summary>
        /// <typeparam name="T">The Type of the enum</typeparam>
        /// <param name="value">The string value to parse</param>
        /// <returns>The parsed enum value</returns>
        public static T Parse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
