using System;
using System.Collections.Generic;
using System.Text;

namespace d1den.MathLibrary
{
    /// <summary>
    /// Точка в 3-д пространстве
    /// </summary>
    public readonly struct Point3D
    {
        private readonly double _x, _y, _z;
        /// <summary>
        /// X-координата точки
        /// </summary>
        public double X { get { return _x; } }
        /// <summary>
        /// Y-координата точки
        /// </summary>
        public double Y { get { return _y; } }
        /// <summary>
        /// Z-координата точки
        /// </summary>
        public double Z { get { return _z; } }
        /// <summary>
        /// Создание точки по координатам
        /// </summary>
        public Point3D(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        /// <summary>
        /// Создание точки по массиву её координат
        /// </summary>
        /// <param name="pointArray">Массив координат точки</param>
        public Point3D(double[] pointArray)
        {
            if (pointArray.Length != 3)
            {
                var ex = new ArgumentException("Array length != 3");
                ex.Data.Add("ArrayLength", pointArray.Length);
                throw ex;
            }
            _x = pointArray[0];
            _y = pointArray[1];
            _z = pointArray[2];
        }

        /// <summary>
        /// Получить расстрояние между двумя точками
        /// </summary>
        /// <param name="point1">Первая точка</param>
        /// <param name="point2">Вторая точка</param>
        /// <returns>Расстояние</returns>
        public static double GetDistance(Point3D point1, Point3D point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2.0) + Math.Pow(point2.Y - point1.Y, 2.0) + Math.Pow(point2.Z - point1.Z, 2.0));
        }
        /// <summary>
        /// Получить расстрояние до другой точки
        /// </summary>
        /// <param name="point">Вторая точка</param>
        /// <returns>Расстояние</returns>
        public double GetDistance(Point3D point)
        {
            return GetDistance(this, point);
        }
        /// <summary>
        /// Получить строковое представление точки
        /// </summary>
        /// <returns>Строка точки</returns>
        public override string ToString()
        {
            return string.Format("{0:F3}; {1:F3}; {2:F3}", _x, _y, _z);
        }
    }
}
