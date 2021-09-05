using System;

namespace D1den.Calculation
{
    /// <summary>
    /// Additional math functions
    /// </summary>
    public static class MathA
    {
        /// <summary>
        /// Aligning a value to a range
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns>Value in range</returns>
        public static double Clamp(double value, double minValue, double maxValue)
        {
            if (value >= minValue && value <= maxValue)
                return value;
            else if (value < minValue)
                return minValue;
            else
                return maxValue;
        }
        /// <summary>
        /// Aligning a value to a range
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns>Value in range</returns>
        public static int Clamp(int value, int minValue, int maxValue)
        {
            if (value >= minValue && value <= maxValue)
                return value;
            else if (value < minValue)
                return minValue;
            else
                return maxValue;
        }
        /// <summary>
        /// Convert degree to radian
        /// </summary>
        /// <param name="degreeAngle">Angle in degrees</param>
        /// <returns>Angle in radians</returns>
        public static double DegreeToRadian(double degreeAngle)
        {
            return degreeAngle * Math.PI / 180.0;
        }
        /// <summary>
        /// Convert radian to degree
        /// </summary>
        /// <param name="radianAngle">Angle in radians</param>
        /// <returns>Angle in degrees</returns>
        public static double RadianToDegree(double radianAngle)
        {
            return radianAngle * 180.0 / Math.PI; ;
        }
        static double CompareRelativeError(double x, double y)
        {
            return Math.Abs(x - y) / Math.Max(Math.Abs(x), Math.Abs(y));
        }
        /// <summary>
        /// Compare numbers with some margin of error
        /// </summary>
        /// <param name="x">First number</param>
        /// <param name="y">Second number</param>
        /// <param name="delta">Comparison error</param>
        /// <returns>Result of сomparison</returns>
        public static bool CompareAlmostEqual(double x, double y, double delta)
        {
            return x == y || CompareRelativeError(x, y) < delta;
        }

    }
}
