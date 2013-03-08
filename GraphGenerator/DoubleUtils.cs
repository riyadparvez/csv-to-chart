using System;

namespace GraphGenerator
{
    static class DoubleUtils
    {
        public static double HalfFloor(double num)
        {
            double threshold = (int)num + 0.5;
            if (num > threshold)
            {
                return Math.Floor(num);
            }
            else
            {
                return threshold;
            }
        }
    }
}
