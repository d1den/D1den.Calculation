using System;

namespace D1den.Calculation
{
    /// <summary>
    /// Дополнительные математические функции
    /// </summary>
    public static class MathA
    {
        /// <summary>
        /// Выравнивание значения по диапазону
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="minValue">Минимально допустимое значение</param>
        /// <param name="maxValue">Максимально допустимое значение</param>
        /// <returns>Значение, соответсвующее диапазону</returns>
        public static double Clamp(double value, double minValue, double maxValue)
        {
            if (value >= minValue && value <= maxValue)
            {
                return value;
            }
            else if (value < minValue)
            {
                return minValue;
            }
            else
            {
                return maxValue;
            }
        }

        /// <summary>
        /// Выравнивание значения по диапазону
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="minValue">Минимально допустимое значение</param>
        /// <param name="maxValue">Максимально допустимое значение</param>
        /// <returns>Значение, соответсвующее диапазону</returns>
        public static int Clamp(int value, int minValue, int maxValue)
        {
            if (value >= minValue && value <= maxValue)
            {
                return value;
            }
            else if (value < minValue)
            {
                return minValue;
            }
            else
            {
                return maxValue;
            }
        }
        /// <summary>
        /// Преобразование градусов к радианам
        /// </summary>
        /// <param name="degreeAngle">Угол в градусах</param>
        /// <returns>Радианы</returns>
        public static double DegreeToRadian(double degreeAngle)
        {
            return (degreeAngle * Math.PI) / 180.0;
        }

        /// <summary>
        /// Преобразование радиан к градусам
        /// </summary>
        /// <param name="radianAngle">Угол в радианах</param>
        /// <returns>Градусы</returns>
        public static double RadianToDegree(double radianAngle)
        {
            return (radianAngle * 180.0) / Math.PI; ;
        }
    }
}
