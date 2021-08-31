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
    [Serializable]
    public readonly struct EulerAngles
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
        /// Порядок осей поворота данных углов
        /// </summary>
        public RotationAxisOrder RotationAxisOrder { get { return _rotationAxisOrder; } }

        /// <summary>
        /// Объект углов Эйлера, измеряемыми в ГРАДУСАХ
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
            switch (rotationAxisOrder)
            {
                case RotationAxisOrder.XYZ: return GetEulersXYZ(rotationMatrix);
                case RotationAxisOrder.ZXZ: return GetEulersZXZ(rotationMatrix);
                default: return GetEulersXYZ(rotationMatrix);
            }
        }
        private static EulerAngles GetEulersZXZ(Matrix rotationMatrix)
        {
            if (rotationMatrix.RowCount != 3 && rotationMatrix.ColumnCount != 3)
            {
                var ex = new ArgumentException("Matrix isn`t rotation, because dimencions are wrong!");
                ex.Data.Add("Matrix dimensions", string.Format("({0},{1})",
                    rotationMatrix.RowCount, rotationMatrix.ColumnCount));
                throw ex;
            }
            else if (rotationMatrix[2,2] == 1.0)
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
            if (rotationMatrix.RowCount != 3 && rotationMatrix.ColumnCount != 3)
            {
                var ex = new ArgumentException("Matrix isn`t rotation, because dimencions are wrong!");
                ex.Data.Add("Matrix dimensions", string.Format("({0},{1})",
                    rotationMatrix.RowCount, rotationMatrix.ColumnCount));
                throw ex;
            }
            else if (rotationMatrix[0, 2] == 1.0)
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


        /// <summary>
        /// Преобразовать углы Эйлера к матрице поворота
        /// </summary>
        /// <returns>Матрица поворота 3x3</returns>
        public Matrix GetRotationMatrix()
        {
            switch (_rotationAxisOrder)
            {
                case RotationAxisOrder.XYZ: return GetRotationFromXYZ();
                case RotationAxisOrder.ZXZ: return GetRotationFromZXZ();
                default: return GetRotationFromXYZ();
            }
        }

        private Matrix GetRotationFromZXZ()
        {
            EulerAngles eulers = this;
            if (_angleUnits == AngleUnits.Degrees)
            { 
                eulers = ConvertToRadians();
            }
            double r11 = Math.Cos(eulers.Alpha) * Math.Sin(eulers.Gamma) - Math.Cos(eulers.Betta) * Math.Sin(eulers.Alpha) * Math.Sin(eulers.Gamma);
            double r12 = -Math.Cos(eulers.Gamma)*Math.Sin(eulers.Alpha) -  Math.Cos(eulers.Alpha) * Math.Cos(eulers.Betta) * Math.Sin(eulers.Gamma);
            double r13 = Math.Sin(eulers.Betta) * Math.Sin(eulers.Gamma);
            double r21 = Math.Cos(eulers.Betta) * Math.Cos(eulers.Gamma) * Math.Sin(eulers.Alpha) + Math.Cos(eulers.Alpha) * Math.Sin(eulers.Gamma);
            double r22 = Math.Cos(eulers.Alpha) * Math.Cos(eulers.Betta) * Math.Cos(eulers.Gamma) - Math.Sin(eulers.Alpha) * Math.Sin(eulers.Gamma);
            double r23 = -Math.Cos(eulers.Gamma) * Math.Sin(eulers.Betta);
            double r31 = Math.Sin(eulers.Alpha) * Math.Sin(eulers.Betta);
            double r32 = Math.Cos(eulers.Alpha) * Math.Sin(eulers.Betta);
            double r33 = Math.Cos(eulers.Betta);
            return new Matrix(new[,]
            {
                { r11, r12, r13 },
                { r21, r22, r23 },
                { r31, r32, r33 }
            });
        }

        private Matrix GetRotationFromXYZ()
        {
            EulerAngles eulers = this;
            if (_angleUnits == AngleUnits.Degrees)
            {
                eulers = ConvertToRadians();
            }
            double r11 = Math.Cos(eulers.Betta) * Math.Cos(eulers.Gamma);
            double r12 = -Math.Cos(eulers.Betta) * Math.Sin(eulers.Gamma);
            double r13 = Math.Sin(eulers.Betta);
            double r21 = Math.Cos(eulers.Alpha) * Math.Sin(eulers.Gamma) + Math.Cos(eulers.Gamma) * Math.Sin(eulers.Alpha) * Math.Sin(eulers.Betta);
            double r22 = Math.Cos(eulers.Alpha) * Math.Cos(eulers.Gamma) - Math.Sin(eulers.Alpha) * Math.Sin(eulers.Betta) * Math.Sin(eulers.Gamma);
            double r23 = -Math.Cos(eulers.Betta) * Math.Sin(eulers.Alpha);
            double r31 = Math.Sin(eulers.Alpha) * Math.Sin(eulers.Gamma) - Math.Cos(eulers.Alpha) * Math.Cos(eulers.Gamma) * Math.Sin(eulers.Betta);
            double r32 = Math.Cos(eulers.Gamma) * Math.Sin(eulers.Alpha) + Math.Cos(eulers.Alpha) * Math.Sin(eulers.Betta) * Math.Sin(eulers.Gamma);
            double r33 = Math.Cos(eulers.Alpha) * Math.Cos(eulers.Betta);
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
    }
}
