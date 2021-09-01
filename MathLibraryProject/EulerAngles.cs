using System;
using System.Collections.Generic;
using System.Text;

namespace d1den.MathLibrary
{
    /// <summary>
    /// Единицы измерения углов
    /// </summary>
    [Serializable]
    public enum AngleUnits
    {
        /// <summary>
        /// Радианы
        /// </summary>
        Radians = 0,
        /// <summary>
        /// Градусы
        /// </summary>
        Degrees = 1
    }

    /// <summary>
    /// Порядок осей поворота
    /// </summary>
    /// <remarks>Тип углов Эйлера напрямую зависит от порядка осей поворота,
    /// так как итоговая матрица поворота получается
    /// за счёт последовательного перемножения матриц поворота вокруг каждой оси.
    /// <see href="https://wiki2.wiki/wiki/euler_angles">Углы Эйлера</see>
    /// </remarks>
    [Serializable]
    public enum RotationAxisOrder
    {
        /// <summary>
        /// Поворот по осям XYZ. Углы Тейта-Брайна
        /// </summary>
        XYZ = 0,
        /// <summary>
        /// Поворот по осям ZXZ
        /// </summary>
        ZXZ = 1,
        /// <summary>
        /// Поворот по осям ZYZ
        /// </summary>
        ZYZ = 2
    }

    /// <summary>
    /// Углы Эйлера 
    /// </summary>
    /// <remarks>
    /// Определяет для использования углы Эйлера, которые можно задавать как в ручную, 
    /// так и из матрицы поворота твёрдого тела.
    /// Углы Эйлера описывают ориентацию твёрдого тела в пространстве наиболее понятным человеку образом, так как 
    /// имеют всего три угла. 
    /// <see href="https://wiki2.wiki/wiki/euler_angles">Углы Эйлера</see>
    /// </remarks>
    [Serializable]
    public readonly struct EulerAngles : IEquatable<EulerAngles>
    {
        private readonly double _alpha;
        private readonly double _betta;
        private readonly double _gamma;
        private readonly AngleUnits _angleUnits;
        private readonly RotationAxisOrder _rotationAxisOrder;

        /// <summary>
        /// Угол прецессии
        /// </summary>
        public double Alpha { get { return _alpha; } }

        /// <summary>
        /// Угол нутации
        /// </summary>
        public double Betta { get { return _betta; } }

        /// <summary>
        /// Угол собственного вращения
        /// </summary>
        public double Gamma { get { return _gamma; } }

        /// <summary>
        /// Единицы измерения углов
        /// </summary>
        public AngleUnits AngleUnits { get { return _angleUnits; } }
        
        /// <summary>
        /// Порядок осей поворота
        /// </summary>
        public RotationAxisOrder RotationAxisOrder { get { return _rotationAxisOrder; } }

        /// <summary>
        /// Объект углов Эйлера в ГРАДУСАХ
        /// </summary>
        /// <param name="alpha">Угол прецессии</param>
        /// <param name="betta">Угол нутации</param>
        /// <param name="gamma">Угол собственного вращения</param>
        public EulerAngles(double alpha, double betta, double gamma)
        {
            _alpha = alpha;
            _betta = betta;
            _gamma = gamma;
            _angleUnits = AngleUnits.Degrees;
            _rotationAxisOrder = RotationAxisOrder.XYZ;
        }

        /// <summary>
        /// Объект углов Эйлера с настройкой единиц измерения углов
        /// </summary>
        /// <param name="alpha">Угол прецессии</param>
        /// <param name="betta">Угол нутации</param>
        /// <param name="gamma">Угол собственного вращения</param>
        /// <param name="angleUnits">Единицы измерения угла</param>
        /// <param name="rotationAxisOrder">Порядок осей поворота</param>
        public EulerAngles(double alpha, double betta, double gamma, AngleUnits angleUnits, RotationAxisOrder rotationAxisOrder) : this(alpha, betta, gamma)
        { 
            _angleUnits = angleUnits;
            _rotationAxisOrder = rotationAxisOrder;
        }

        /// <summary>
        /// Объект углов Эйлера из массива значений углов в ГРАДУСАХ
        /// </summary>
        /// <param name="eulerAnglesArray">Массив значений углов</param>
        /// <exception cref="ArgumentException">Если длина массива значений != 3</exception>
        public EulerAngles(double[] eulerAnglesArray)
        {
            if (eulerAnglesArray.Length != 3)
            {
                var ex = new ArgumentException("Array`s length != 3");
                ex.Data.Add("Array length", eulerAnglesArray.Length);
                throw ex;
            }
            _alpha = eulerAnglesArray[0];
            _betta = eulerAnglesArray[1];
            _gamma = eulerAnglesArray[2];
            _angleUnits = AngleUnits.Degrees;
            _rotationAxisOrder = RotationAxisOrder.XYZ;
        }

        /// <summary>
        /// Объект углов Эйлера из массива значений углов
        /// </summary>
        /// <param name="eulerAnglesArray">Массив значений углов</param>
        /// <param name="angleUnits">Единицы измерения угла</param>
        /// <param name="rotationAxisOrder">Порядок осей поворота</param>
        public EulerAngles(double[] eulerAnglesArray, AngleUnits angleUnits, RotationAxisOrder rotationAxisOrder) : this(eulerAnglesArray)
        {
            _angleUnits = angleUnits;
            _rotationAxisOrder = rotationAxisOrder;
        }

        /// <summary>
        /// Поместить углы Эйлера в массив
        /// </summary>
        /// <returns>Массив углов Эйлера</returns>
        public double[] ToArray()
        {
            return new double[] { _alpha, _betta, _gamma };
        }

        /// <summary>
        /// Преобразовать матрицу поворота в углы Эйлера
        /// </summary>
        /// <param name="rotationMatrix">Матрица поворота 3x3</param>
        /// <param name="rotationAxisOrder">Порядок осей поворота. Определяет тип углов Эйлера</param>
        /// <exception cref="ArgumentException">Если матрица поворота имеет неверные размеры</exception>
        /// <returns>Углы Эйлера в радианах</returns>
        public static EulerAngles GetEulersFromRotation(Matrix rotationMatrix, RotationAxisOrder rotationAxisOrder)
        {
            if (rotationMatrix.RowCount != 3 && rotationMatrix.ColumnCount != 3)
            {
                var ex = new ArgumentException("Matrix isn`t rotation, because dimencions are wrong!");
                ex.Data.Add("Matrix dimensions", string.Format("({0},{1})",
                    rotationMatrix.RowCount, rotationMatrix.ColumnCount));
                throw ex;
            }
            switch (rotationAxisOrder)
            {
                case RotationAxisOrder.XYZ: return GetEulersXYZ(rotationMatrix);
                case RotationAxisOrder.ZXZ: return GetEulersZXZ(rotationMatrix);
                case RotationAxisOrder.ZYZ: return GetEulersZYZ(rotationMatrix);
                default: return GetEulersXYZ(rotationMatrix);
            }
        }
        private static EulerAngles GetEulersZXZ(Matrix rotationMatrix)
        {
            if (rotationMatrix[2,2] == 1.0)
            {
                double betta = 0;
                double gamma = 0;
                double alpha = Math.Atan2(rotationMatrix[1, 0], rotationMatrix[0, 0]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians, RotationAxisOrder.ZXZ);
            }
            else if(rotationMatrix[2, 2] == -1.0)
            {
                double betta = Math.PI;
                double gamma = 0;
                double alpha = Math.Atan2(rotationMatrix[1, 0], rotationMatrix[0, 0]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians, RotationAxisOrder.ZXZ);
            }
            else
            {
                double betta = Math.Atan2(Math.Sqrt(1.0 - Math.Pow(rotationMatrix[2, 2], 2.0)), rotationMatrix[2, 2]);
                double gamma = Math.Atan2(rotationMatrix[2, 0], rotationMatrix[2, 1]);
                double alpha = Math.Atan2(rotationMatrix[0, 2], -rotationMatrix[1, 2]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians, RotationAxisOrder.ZXZ);
            }
        }
        private static EulerAngles GetEulersXYZ(Matrix rotationMatrix)
        {
            if (rotationMatrix[0, 2] == 1.0)
            {
                double betta = Math.PI / 2.0;
                double gamma = 0;
                double alpha = Math.Atan2(rotationMatrix[1, 0], rotationMatrix[1, 1]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians, RotationAxisOrder.XYZ);
            }
            else if (rotationMatrix[0, 2] == -1.0)
            {
                double betta = -Math.PI / 2.0;
                double gamma = 0;
                double alpha = Math.Atan2(-rotationMatrix[1, 0], rotationMatrix[1, 1]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians, RotationAxisOrder.XYZ);
            }
            else
            {
                double betta = Math.Atan2(rotationMatrix[0, 2], Math.Sqrt(1.0 - Math.Pow(rotationMatrix[0, 2], 2.0)));
                double alpha = Math.Atan2(-rotationMatrix[1, 2], rotationMatrix[2, 2]);
                double gamma = Math.Atan2(-rotationMatrix[0, 1], rotationMatrix[0, 0]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians, RotationAxisOrder.XYZ);
            }
        }
        private static EulerAngles GetEulersZYZ(Matrix rotationMatrix)
        {
            if (rotationMatrix[2, 2] == 1.0)
            {
                double betta = 0;
                double gamma = 0;
                double alpha = Math.Atan2(rotationMatrix[1, 0], rotationMatrix[0, 0]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians, RotationAxisOrder.ZYZ);
            }
            else if (rotationMatrix[2, 2] == -1.0)
            {
                double betta = Math.PI;
                double gamma = 0;
                double alpha = Math.Atan2(-rotationMatrix[0, 1], -rotationMatrix[0, 0]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians, RotationAxisOrder.ZYZ);
            }
            else
            {
                double betta = Math.Atan2(Math.Sqrt(1.0 - Math.Pow(rotationMatrix[2, 2], 2.0)), rotationMatrix[2, 2]);
                double alpha = Math.Atan2(rotationMatrix[1, 2], rotationMatrix[0, 2]);
                double gamma = Math.Atan2(rotationMatrix[2, 1], -rotationMatrix[2, 0]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians, RotationAxisOrder.ZYZ);
            }
        }

        /// <summary>
        /// Преобразовать углы Эйлера к матрице поворота
        /// </summary>
        /// <returns>Матрица поворота 3x3</returns>
        public Matrix GetRotationMatrix()
        {
            EulerAngles eulers = this;
            if (_angleUnits == AngleUnits.Degrees)
            {
                eulers = ConvertToRadians();
            }
            switch (_rotationAxisOrder)
            {
                case RotationAxisOrder.XYZ: return eulers.GetRotationFromXYZ();
                case RotationAxisOrder.ZXZ: return eulers.GetRotationFromZXZ();
                case RotationAxisOrder.ZYZ: return eulers.GetRotationFromZYZ();
                default: return eulers.GetRotationFromXYZ();
            }
        }

        private Matrix GetRotationFromZXZ()
        {
            double r11 = Math.Cos(_alpha) * Math.Sin(_gamma) - Math.Cos(_betta) * Math.Sin(_alpha) * Math.Sin(_gamma);
            double r12 = -Math.Cos(_gamma)*Math.Sin(_alpha) -  Math.Cos(_alpha) * Math.Cos(_betta) * Math.Sin(_gamma);
            double r13 = Math.Sin(_betta) * Math.Sin(_gamma);

            double r21 = Math.Cos(_betta) * Math.Cos(_gamma) * Math.Sin(_alpha) + Math.Cos(_alpha) * Math.Sin(_gamma);
            double r22 = Math.Cos(_alpha) * Math.Cos(_betta) * Math.Cos(_gamma) - Math.Sin(_alpha) * Math.Sin(_gamma);
            double r23 = -Math.Cos(_gamma) * Math.Sin(_betta);

            double r31 = Math.Sin(_alpha) * Math.Sin(_betta);
            double r32 = Math.Cos(_alpha) * Math.Sin(_betta);
            double r33 = Math.Cos(_betta);
            return new Matrix(new[,]
            {
                { r11, r12, r13 },
                { r21, r22, r23 },
                { r31, r32, r33 }
            });
        }
        private Matrix GetRotationFromXYZ()
        {
            double r11 = Math.Cos(_betta) * Math.Cos(_gamma);
            double r12 = -Math.Cos(_betta) * Math.Sin(_gamma);
            double r13 = Math.Sin(_betta);

            double r21 = Math.Cos(_alpha) * Math.Sin(_gamma) + Math.Cos(_gamma) * Math.Sin(_alpha) * Math.Sin(_betta);
            double r22 = Math.Cos(_alpha) * Math.Cos(_gamma) - Math.Sin(_alpha) * Math.Sin(_betta) * Math.Sin(_gamma);
            double r23 = -Math.Cos(_betta) * Math.Sin(_alpha);

            double r31 = Math.Sin(_alpha) * Math.Sin(_gamma) - Math.Cos(_alpha) * Math.Cos(_gamma) * Math.Sin(_betta);
            double r32 = Math.Cos(_gamma) * Math.Sin(_alpha) + Math.Cos(_alpha) * Math.Sin(_betta) * Math.Sin(_gamma);
            double r33 = Math.Cos(_alpha) * Math.Cos(_betta);
            return new Matrix(new[,]
            {
                { r11, r12, r13 },
                { r21, r22, r23 },
                { r31, r32, r33 }
            });
        }
        private Matrix GetRotationFromZYZ()
        {
            double r11 = Math.Cos(_alpha) * Math.Cos(_betta) * Math.Cos(_gamma) - Math.Sin(_alpha) * Math.Sin(_gamma);
            double r12 = -Math.Cos(_gamma) * Math.Sin(_alpha) - Math.Cos(_alpha) * Math.Cos(_betta) * Math.Sin(_gamma);
            double r13 = Math.Cos(_alpha) * Math.Sin(_betta);

            double r21 = Math.Cos(_alpha) * Math.Sin(_gamma) + Math.Cos(_betta) * Math.Cos(_gamma) * Math.Sin(_alpha);
            double r22 = Math.Cos(_alpha) * Math.Cos(_gamma) - Math.Cos(_betta) * Math.Sin(_alpha) * Math.Sin(_gamma);
            double r23 = Math.Sin(_alpha) * Math.Sin(_betta);

            double r31 = -Math.Cos(_gamma) * Math.Sin(_betta);
            double r32 = Math.Sin(_betta) * Math.Sin(_gamma);
            double r33 = Math.Cos(_betta);
            return new Matrix(new[,]
            {
                { r11, r12, r13 },
                { r21, r22, r23 },
                { r31, r32, r33 }
            });
        }

        /// <summary>
        /// Преобразовать значения углов в радианы
        /// </summary>
        /// <returns>Углы Эйлера в радианах</returns>
        public EulerAngles ConvertToRadians()
        {
            if (_angleUnits == AngleUnits.Degrees)
                return new EulerAngles(AdvancedMath.DegreeToRadian(_alpha),
                    AdvancedMath.DegreeToRadian(_betta),
                    AdvancedMath.DegreeToRadian(_gamma),
                    AngleUnits.Radians,
                    _rotationAxisOrder);
            else
                return this;
        }

        /// <summary>
        /// Преобразовать углы Эйлера в градусы
        /// </summary>
        /// <returns>Углы Эйлера в градусах</returns>
        public EulerAngles ConvertToDegrees()
        {
            if (_angleUnits == AngleUnits.Radians)
                return new EulerAngles(AdvancedMath.RadianToDegree(_alpha),
                    AdvancedMath.RadianToDegree(_betta),
                    AdvancedMath.RadianToDegree(_gamma),
                    AngleUnits.Degrees,
                    _rotationAxisOrder);
            else
                return this;
        }

        /// <summary>
        /// Преобразовать объект к строке
        /// </summary>
        /// <returns>Строка углов Эйлера</returns>
        public override string ToString()
        {
            return string.Format("{0}:{1:F2}; {2:F2}; {3:F2}", _rotationAxisOrder, _alpha, _betta, _gamma);
        }

        /// <summary>
        /// Преобразовать объект к строке в заданных единицах измерения
        /// </summary>
        /// <param name="angleUnits">Единицы измерения углов</param>
        /// <returns>Строка углов Эйлера</returns>
        public string ToString(AngleUnits angleUnits)
        {
            if (angleUnits == _angleUnits)
                return ToString();
            else if (angleUnits == AngleUnits.Radians)
                return ConvertToDegrees().ToString();
            else
                return ConvertToRadians().ToString();
        }

        /// <summary>
        /// Сравнение углов Эйлера в упаковке
        /// </summary>
        /// <param name="obj">Сравниваемый объект</param>
        /// <returns>Результат сравнения</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else if (obj is EulerAngles eulers)
                return Equals(eulers);
            else
                return false;
        }

        /// <summary>
        /// Сравнение углов Эйлера
        /// </summary>
        /// <param name="other">Другой объект углов Эйлера</param>
        /// <returns>Результат сравнения</returns>
        public bool Equals(EulerAngles other)
        {
            if (other.RotationAxisOrder == RotationAxisOrder && other.AngleUnits == AngleUnits &&
                other.Alpha == Alpha && other.Betta == Betta && other.Gamma == Gamma)
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
            double result = (int)_rotationAxisOrder * 1.0E+5 + (int)_angleUnits * 1.0E+4 + _alpha * 1.0E+3 +
                _betta * 1.0E+2 + _gamma * 10;
            if (result > -1.0 && result < 1.0)
                result *= 1.0E+9;
            return (int)Math.Round(result);
        }
    }
}
