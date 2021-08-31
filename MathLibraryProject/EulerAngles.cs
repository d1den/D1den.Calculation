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
    /// Углый Эйлера 
    /// </summary>
    [Serializable]
    public readonly struct EulerAngles
    {
        private readonly double _alpha;
        private readonly double _betta;
        private readonly double _gamma;
        private readonly AngleUnits _angleUnits;

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

        public AngleUnits AngleUnits { get { return _angleUnits; } }


        public EulerAngles(double alpha, double betta, double gamma)
        {
            _alpha = alpha;
            _betta = betta;
            _gamma = gamma;
            _angleUnits = AngleUnits.Degrees;
        }


        public EulerAngles(double alpha, double betta, double gamma, AngleUnits angleUnits) : this(alpha, betta, gamma)
        { 
            _angleUnits = angleUnits;
        }


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
        }

        public EulerAngles(double[] eulerAnglesArray, AngleUnits angleUnits) : this(eulerAnglesArray)
        {
            _angleUnits = angleUnits;
        }

        public double[] ToArray()
        {
            return new double[] { _alpha, _betta, _gamma };
        }

        public static EulerAngles GetEulersZXZFromRotation(Matrix rotationMatrix)
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
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians);
            }
            else if(rotationMatrix[2, 2] == -1.0)
            {
                double betta = Math.PI;
                double gamma = 0;
                double alpha = Math.Atan2(rotationMatrix[1, 0], rotationMatrix[0, 0]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians);
            }
            else
            {
                double betta = Math.Atan2(Math.Sqrt(1.0 - Math.Pow(rotationMatrix[2, 2], 2.0)), rotationMatrix[2, 2]);
                double gamma = Math.Atan2(rotationMatrix[2, 0], rotationMatrix[2, 1]);
                double alpha = Math.Atan2(rotationMatrix[0, 2], -rotationMatrix[1, 2]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians);
            }
        }



        public static EulerAngles GetEulersXYZFromRotation(Matrix rotationMatrix)
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
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians);
            }
            else if (rotationMatrix[0, 2] == -1.0)
            {
                double betta = -Math.PI / 2.0;
                double gamma = 0;
                double alpha = Math.Atan2(-rotationMatrix[1, 0], rotationMatrix[1, 1]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians);
            }
            else
            {
                double betta = Math.Atan2(rotationMatrix[0, 2], Math.Sqrt(1.0 - Math.Pow(rotationMatrix[0, 2], 2.0)));
                double alpha = Math.Atan2(-rotationMatrix[1, 2], rotationMatrix[2, 2]);
                double gamma = Math.Atan2(-rotationMatrix[0, 1], rotationMatrix[0, 0]);
                return new EulerAngles(alpha, betta, gamma, AngleUnits.Radians);
            }
        }

        public Matrix GetRotationFromZXZ()
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



        public Matrix GetRotationFromXYZ()
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

        public EulerAngles ConvertToRadians()
        {
            if (_angleUnits == AngleUnits.Degrees)
                return new EulerAngles(AdvancedMath.DegreeToRadian(_alpha),
                    AdvancedMath.DegreeToRadian(_betta),
                    AdvancedMath.DegreeToRadian(_gamma),
                    AngleUnits.Radians);
            else
                return this;
        }


        public EulerAngles ConvertToDegrees()
        {
            if (_angleUnits == AngleUnits.Radians)
                return new EulerAngles(AdvancedMath.RadianToDegree(_alpha),
                    AdvancedMath.RadianToDegree(_betta),
                    AdvancedMath.RadianToDegree(_gamma));
            else
                return this;
        }

        public override string ToString()
        {
            return string.Format("A={0:F2}, B={1:F2}, G={2:F2}", _alpha, _betta, _gamma);
        }

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
