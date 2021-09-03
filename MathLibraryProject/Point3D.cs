using System;

namespace D1den.EngineeringMath
{
    /// <summary>
    /// Точка в 3-д пространстве
    /// </summary>
    [Serializable]
    public readonly struct Point3D : IEquatable<Point3D>
    {
        #region Поля и свойства
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
        #endregion

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
                var ex = new ArgumentException("Array can not convert to Point3D! Array length != 3");
                ex.Data.Add("ArrayLength", pointArray.Length);
                throw ex;
            }
            _x = pointArray[0];
            _y = pointArray[1];
            _z = pointArray[2];
        }

        #region Методы
        /// <summary>
        /// Преобразовать точку к массиву координат
        /// </summary>
        /// <returns>Массив координат</returns>
        public double[] ToArray()
        {
            return new double[] { _x, _y, _z };
        }

        /// <summary>
        /// Получить расстрояние до другой точки
        /// </summary>
        /// <param name="point">Вторая точка</param>
        /// <returns>Расстояние</returns>
        public double GetDistance(Point3D point)
        {
            return Math.Sqrt(Math.Pow(point.X - X, 2.0) + Math.Pow(point.Y - Y, 2.0) + Math.Pow(point.Z - Z, 2.0));
        }

        /// <summary>
        /// Получить строковое представление точки
        /// </summary>
        /// <returns>Строка точки</returns>
        public override string ToString()
        {
            return string.Format("{0:F2}; {1:F2}; {2:F2}", _x, _y, _z);
        }

        /// <summary>
        /// Сравнение точек в виде объектов
        /// </summary>
        /// <param name="obj">Упакованный объект точки</param>
        /// <returns>Результат сравнения</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Point3D point2)
                return Equals(point2);
            else
                return false;
        }

        /// <summary>
        /// Сравнение точек
        /// </summary>
        /// <param name="other">Точка для сравнения</param>
        /// <returns>Результат сравнения</returns>
        public bool Equals(Point3D other)
        {
            if (other._x == this._x && other._y == this._y && other._z == this._z)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Алгоритм создания хэш-кода
        /// </summary>
        /// <returns>Хэш-код</returns>
        public override int GetHashCode()
        {
            double result = _x * 1000.0 + _y * 100 + _z * 10;
            if (result > int.MaxValue)
                result /= 1.0E+9;
            else if (result > -1.0 && result < 1.0)
                result *= 1.0E+9;
            return (int)Math.Round(result);
        }
        #endregion

        #region Операторы
        /// <summary>
        /// Преобразовать массив координат к точке
        /// </summary>
        /// <param name="pointArray">Массив координат</param>
        public static implicit operator Point3D(double[] pointArray)
        {
            return new Point3D(pointArray);
        }

        /// <summary>
        /// Преобразовать точку к массиву координат
        /// </summary>
        /// <param name="point">Точка</param>
        public static explicit operator double[](Point3D point)
        {
            return point.ToArray();
        }

        /// <summary>
        /// Проверка на равенство точек
        /// </summary>
        /// <param name="point1">Точка 1</param>
        /// <param name="point2">Точка 2</param>
        /// <returns>Результат сравнения</returns>
        public static bool operator ==(Point3D point1, Point3D point2)
        {
            return point1.Equals(point2);
        }

        /// <summary>
        /// Проверка на неравенство точек
        /// </summary>
        /// <param name="point1">Точка 1</param>
        /// <param name="point2">Точка 2</param>
        /// <returns>Результат сравнения</returns>
        public static bool operator !=(Point3D point1, Point3D point2)
        {
            return !point1.Equals(point2);
        }
        #endregion
    }
}
