using System;
using System.Collections.Generic;
using System.Text;

namespace ColorPicker.BaseCore.Extensions
{
    internal static class NumericExtensions
    {
        public static float Clamp(this float self, float min, float max)
        {
            if (max < min)
            {
                return max;
            }
            else if (self < min)
            {
                return min;
            }
            else if (self > max)
            {
                return max;
            }

            return self;
        }
    }
}
