using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    static class Extensions
    {
        public static bool In(this int number, params int[] numbers)
        {
            return numbers.Contains(number);
        }
    }
}
