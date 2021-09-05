using System;

namespace D1den.Calculation
{
    /// <summary>
    /// The order of the rotation axes. Specifies the type of Euler angles
    /// </summary>
    /// <remarks>The type of Euler angles directly depends
    /// on the order of the rotation axes, since the resulting rotation matrix
    /// is obtained by sequentially multiplying the rotation matrices around each axis.
    /// <see href="https://en.wikipedia.org/wiki/Euler_angles">Euler angles</see>
    /// </remarks>
    [Serializable]
    public enum RotationAxisOrder
    {
        /// <summary>
        /// XYZ rotation. Tait–Bryan angles
        /// </summary>
        XYZ = 0,
        /// <summary>
        /// ZXZ rotation. Standart Euler angles
        /// </summary>
        ZXZ = 1,
        /// <summary>
        /// ZYZ rotation
        /// </summary>
        ZYZ = 2
    }

    /// <summary>
    /// Various Euler angles and operations with them
    /// </summary>
    /// <remarks>
    /// Defines Euler angles for use, which can be set either manually
    /// or from a rigid body rotation matrix.
    /// Euler angles describe the orientation of a rigid body in space
    /// in the most understandable way for a person,
    /// since they have only three angles. 
    /// <see href="https://en.wikipedia.org/wiki/Euler_angles">Euler angles</see>
    /// </remarks>
    [Serializable]
    public readonly struct EulerAngles : IEquatable<EulerAngles>
    {
        private readonly double _alpha;
        private readonly double _beta;
        private readonly double _gamma;
        private readonly RotationAxisOrder _rotationAxisOrder;

        /// <summary>
        /// Precession angle in radians
        /// </summary>
        public double Alpha => _alpha;
        /// <summary>
        /// Nutation angle in radians
        /// </summary>
        public double Beta => _beta;
        /// <summary>
        /// Intrinsic rotation angle in radians
        /// </summary>
        public double Gamma => _gamma;
        /// <summary>
        /// Rotation axis order
        /// </summary>
        public RotationAxisOrder RotationAxisOrder => _rotationAxisOrder;

        /// <summary>
        /// Create Euler angles from parameters
        /// </summary>
        /// <param name="alpha">Precession angle</param>
        /// <param name="beta">Nutation angle</param>
        /// <param name="gamma">Intrinsic rotation angle</param>
        /// <param name="rotationAxisOrder">Rotation axis order</param>
        public EulerAngles(double alpha, double beta, double gamma, RotationAxisOrder rotationAxisOrder)
        {
            _alpha = alpha;
            _beta = beta;
            _gamma = gamma;
            _rotationAxisOrder = rotationAxisOrder;
        }

        /// <summary>
        /// Check that the length of the array is 3
        /// </summary>
        /// <param name="array">Array of angles</param>
        /// <exception cref="ArgumentException">If <paramref name="array"/>.Length != 3</exception>
        private static void ValidateArrayLength(double[] array)
        {
            if (array.Length != 3)
            {
                var ex = new ArgumentException("Array`s length != 3");
                ex.Data.Add("Array length", array.Length);
                throw ex;
            }
        }

        /// <summary>
        /// Create EulerAngles from degrees
        /// </summary>
        /// <param name="alpha">Precession angle in degrees</param>
        /// <param name="beta">Nutation angle in degrees</param>
        /// <param name="gamma">Intrinsic rotation angle in degrees</param>
        /// <param name="rotationAxisOrder">Rotation axis order</param>
        /// <returns>Euler angles</returns>
        public static EulerAngles FromDegrees(double alpha, double beta, double gamma, RotationAxisOrder rotationAxisOrder)
        {
            return new EulerAngles(MathA.DegreeToRadian(alpha), MathA.DegreeToRadian(beta),
                MathA.DegreeToRadian(gamma), rotationAxisOrder);
        }

        /// <summary>
        /// Create EulerAngles from radians
        /// </summary>
        /// <param name="alpha">Precession angle</param>
        /// <param name="beta">Nutation angle</param>
        /// <param name="gamma">Intrinsic rotation angle</param>
        /// <param name="rotationAxisOrder">Rotation axis order</param>
        /// <returns>Euler angles</returns>
        public static EulerAngles FromRadians(double alpha, double beta, double gamma, RotationAxisOrder rotationAxisOrder)
        {
            return new EulerAngles(alpha, beta, gamma, rotationAxisOrder);
        }

        /// <summary>
        /// Create EulerAngles from degrees array
        /// </summary>
        /// <param name="eulerAnglesArray">Angle array in degrees [alpha, beta, gamma]</param>
        /// <param name="rotationAxisOrder">Rotation axis order</param>
        /// <exception cref="ArgumentException">If <paramref name="eulerAnglesArray"/>.Length != 3</exception>
        /// <returns>Euler angles</returns>
        public static EulerAngles FromDegreesArray(double[] eulerAnglesArray, RotationAxisOrder rotationAxisOrder)
        {
            ValidateArrayLength(eulerAnglesArray);
            return new EulerAngles(MathA.DegreeToRadian(eulerAnglesArray[0]), MathA.DegreeToRadian(eulerAnglesArray[1]),
                MathA.DegreeToRadian(eulerAnglesArray[2]), rotationAxisOrder);
        }

        /// <summary>
        /// Create EulerAngles from radians array
        /// </summary>
        /// <param name="eulerAnglesArray">Angle array in radians [alpha, beta, gamma]</param>
        /// <param name="rotationAxisOrder">Rotation axis order</param>
        /// <exception cref="ArgumentException">If <paramref name="eulerAnglesArray"/>.Length != 3</exception>
        /// <returns>Euler angles</returns>
        public static EulerAngles FromRadiansArray(double[] eulerAnglesArray, RotationAxisOrder rotationAxisOrder)
        {
            ValidateArrayLength(eulerAnglesArray);
            return new EulerAngles(eulerAnglesArray[0], eulerAnglesArray[1], eulerAnglesArray[2], rotationAxisOrder);
        }

        /// <summary>
        /// Get Euler angles from rotation matrix
        /// </summary>
        /// <param name="rotationMatrix">Rotation matrix 3x3</param>
        /// <param name="rotationAxisOrder">The order of the rotation axes. Specifies the type of Euler angles</param>
        /// <exception cref="ArgumentException">If <paramref name="rotationMatrix"/> doesn`t 3x3</exception>
        /// <returns>Euler angles</returns>
        public static EulerAngles FromRotationMatrix(Matrix rotationMatrix, RotationAxisOrder rotationAxisOrder)
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
            if (rotationMatrix[2, 2] == 1.0)
            {
                double beta = 0;
                double gamma = 0;
                double alpha = Math.Atan2(rotationMatrix[1, 0], rotationMatrix[0, 0]);
                return new EulerAngles(alpha, beta, gamma, RotationAxisOrder.ZXZ);
            }
            else if (rotationMatrix[2, 2] == -1.0)
            {
                double beta = Math.PI;
                double gamma = 0;
                double alpha = Math.Atan2(rotationMatrix[1, 0], rotationMatrix[0, 0]);
                return new EulerAngles(alpha, beta, gamma, RotationAxisOrder.ZXZ);
            }
            else
            {
                double beta = Math.Atan2(Math.Sqrt(1.0 - Math.Pow(rotationMatrix[2, 2], 2.0)), rotationMatrix[2, 2]);
                double gamma = Math.Atan2(rotationMatrix[2, 0], rotationMatrix[2, 1]);
                double alpha = Math.Atan2(rotationMatrix[0, 2], -rotationMatrix[1, 2]);
                return new EulerAngles(alpha, beta, gamma, RotationAxisOrder.ZXZ);
            }
        }
        private static EulerAngles GetEulersXYZ(Matrix rotationMatrix)
        {
            if (rotationMatrix[0, 2] == 1.0)
            {
                double beta = Math.PI / 2.0;
                double gamma = 0;
                double alpha = Math.Atan2(rotationMatrix[1, 0], rotationMatrix[1, 1]);
                return new EulerAngles(alpha, beta, gamma, RotationAxisOrder.XYZ);
            }
            else if (rotationMatrix[0, 2] == -1.0)
            {
                double beta = -Math.PI / 2.0;
                double gamma = 0;
                double alpha = Math.Atan2(-rotationMatrix[1, 0], rotationMatrix[1, 1]);
                return new EulerAngles(alpha, beta, gamma, RotationAxisOrder.XYZ);
            }
            else
            {
                double beta = Math.Atan2(rotationMatrix[0, 2], Math.Sqrt(1.0 - Math.Pow(rotationMatrix[0, 2], 2.0)));
                double alpha = Math.Atan2(-rotationMatrix[1, 2], rotationMatrix[2, 2]);
                double gamma = Math.Atan2(-rotationMatrix[0, 1], rotationMatrix[0, 0]);
                return new EulerAngles(alpha, beta, gamma, RotationAxisOrder.XYZ);
            }
        }
        private static EulerAngles GetEulersZYZ(Matrix rotationMatrix)
        {
            if (rotationMatrix[2, 2] == 1.0)
            {
                double beta = 0;
                double gamma = 0;
                double alpha = Math.Atan2(rotationMatrix[1, 0], rotationMatrix[0, 0]);
                return new EulerAngles(alpha, beta, gamma, RotationAxisOrder.ZYZ);
            }
            else if (rotationMatrix[2, 2] == -1.0)
            {
                double beta = Math.PI;
                double gamma = 0;
                double alpha = Math.Atan2(-rotationMatrix[0, 1], -rotationMatrix[0, 0]);
                return new EulerAngles(alpha, beta, gamma, RotationAxisOrder.ZYZ);
            }
            else
            {
                double beta = Math.Atan2(Math.Sqrt(1.0 - Math.Pow(rotationMatrix[2, 2], 2.0)), rotationMatrix[2, 2]);
                double alpha = Math.Atan2(rotationMatrix[1, 2], rotationMatrix[0, 2]);
                double gamma = Math.Atan2(rotationMatrix[2, 1], -rotationMatrix[2, 0]);
                return new EulerAngles(alpha, beta, gamma, RotationAxisOrder.ZYZ);
            }
        }

        /// <summary>
        /// Convert Euler angles to array
        /// </summary>
        /// <returns>Euler angles array</returns>
        public double[] ToArray()
        {
            return new double[] { _alpha, _beta, _gamma };
        }

        /// <summary>
        /// Convert Euler angles to rotation matrix
        /// </summary>
        /// <returns>Rotation matrix 3x3</returns>
        public Matrix GetRotationMatrix()
        {
            switch (_rotationAxisOrder)
            {
                case RotationAxisOrder.XYZ: return GetRotationFromXYZ();
                case RotationAxisOrder.ZXZ: return GetRotationFromZXZ();
                case RotationAxisOrder.ZYZ: return GetRotationFromZYZ();
                default: return GetRotationFromXYZ();
            }
        }

        private Matrix GetRotationFromZXZ()
        {
            double r11 = Math.Cos(_alpha) * Math.Cos(_gamma) - Math.Cos(_beta) * Math.Sin(_alpha) * Math.Sin(_gamma);
            double r12 = -Math.Cos(_alpha)*Math.Sin(_gamma) -  Math.Cos(_beta) * Math.Cos(_gamma) * Math.Sin(_alpha);
            double r13 = Math.Sin(_alpha) * Math.Sin(_beta);

            double r21 = Math.Cos(_gamma) * Math.Sin(_alpha) + Math.Cos(_alpha) * Math.Cos(_beta) * Math.Sin(_gamma);
            double r22 = Math.Cos(_alpha) * Math.Cos(_beta) * Math.Cos(_gamma) - Math.Sin(_alpha) * Math.Sin(_gamma);
            double r23 = -Math.Cos(_alpha) * Math.Sin(_beta);

            double r31 = Math.Sin(_beta) * Math.Sin(_gamma);
            double r32 = Math.Cos(_gamma) * Math.Sin(_beta);
            double r33 = Math.Cos(_beta);
            return new Matrix(new[,]
            {
                { r11, r12, r13 },
                { r21, r22, r23 },
                { r31, r32, r33 }
            });
        }
        private Matrix GetRotationFromXYZ()
        {
            double r11 = Math.Cos(_beta) * Math.Cos(_gamma);
            double r12 = -Math.Cos(_beta) * Math.Sin(_gamma);
            double r13 = Math.Sin(_beta);

            double r21 = Math.Cos(_alpha) * Math.Sin(_gamma) + Math.Cos(_gamma) * Math.Sin(_alpha) * Math.Sin(_beta);
            double r22 = Math.Cos(_alpha) * Math.Cos(_gamma) - Math.Sin(_alpha) * Math.Sin(_beta) * Math.Sin(_gamma);
            double r23 = -Math.Cos(_beta) * Math.Sin(_alpha);

            double r31 = Math.Sin(_alpha) * Math.Sin(_gamma) - Math.Cos(_alpha) * Math.Cos(_gamma) * Math.Sin(_beta);
            double r32 = Math.Cos(_gamma) * Math.Sin(_alpha) + Math.Cos(_alpha) * Math.Sin(_beta) * Math.Sin(_gamma);
            double r33 = Math.Cos(_alpha) * Math.Cos(_beta);
            return new Matrix(new[,]
            {
                { r11, r12, r13 },
                { r21, r22, r23 },
                { r31, r32, r33 }
            });
        }
        private Matrix GetRotationFromZYZ()
        {
            double r11 = Math.Cos(_alpha) * Math.Cos(_beta) * Math.Cos(_gamma) - Math.Sin(_alpha) * Math.Sin(_gamma);
            double r12 = -Math.Cos(_gamma) * Math.Sin(_alpha) - Math.Cos(_alpha) * Math.Cos(_beta) * Math.Sin(_gamma);
            double r13 = Math.Cos(_alpha) * Math.Sin(_beta);

            double r21 = Math.Cos(_alpha) * Math.Sin(_gamma) + Math.Cos(_beta) * Math.Cos(_gamma) * Math.Sin(_alpha);
            double r22 = Math.Cos(_alpha) * Math.Cos(_gamma) - Math.Cos(_beta) * Math.Sin(_alpha) * Math.Sin(_gamma);
            double r23 = Math.Sin(_alpha) * Math.Sin(_beta);

            double r31 = -Math.Cos(_gamma) * Math.Sin(_beta);
            double r32 = Math.Sin(_beta) * Math.Sin(_gamma);
            double r33 = Math.Cos(_beta);
            return new Matrix(new[,]
            {
                { r11, r12, r13 },
                { r21, r22, r23 },
                { r31, r32, r33 }
            });
        }

        /// <summary>
        /// Get Euler angles as string
        /// </summary>
        /// <returns>Euler angles as string</returns>
        public override string ToString()
        {
            return string.Format("{0}:{1:F2}; {2:F2}; {3:F2}", _rotationAxisOrder, _alpha, _beta, _gamma);
        }

        /// <summary>
        /// Get Euler angles as string in degrees
        /// </summary>
        /// <returns>Euler angles as string in degrees</returns>
        public string ToStringInDegrees()
        {
            return string.Format("{0}:{1:F2}; {2:F2}; {3:F2}", _rotationAxisOrder,
                MathA.RadianToDegree(_alpha), 
                MathA.RadianToDegree(_beta),
                MathA.RadianToDegree(_gamma));
        }

        /// <summary>
        /// Checking for equality of Euler angles
        /// </summary>
        /// <param name="obj">Packed Euler angles object</param>
        /// <returns>Result of checking</returns>
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
        /// Checking for equality of Euler angles
        /// </summary>
        /// <param name="other">Euler angles object</param>
        /// <returns>Result of checking</returns>
        public bool Equals(EulerAngles other)
        {
            return Equals(other, 1.0E-6);
        }

        /// <summary>
        /// Checking with some accuracy the equality of the Euler angles
        /// </summary>
        /// <param name="other">Euler angles object</param>
        /// <param name="delta">Accuracy</param>
        /// <returns>Result of checking</returns>
        public bool Equals(EulerAngles other, double delta)
        {
            if (other.RotationAxisOrder == RotationAxisOrder &&
                MathA.CompareAlmostEqual(other.Alpha, Alpha, delta) &&
                MathA.CompareAlmostEqual(other.Beta, Beta, delta) && 
                MathA.CompareAlmostEqual(other.Gamma, Gamma, delta))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Hash code generation algorithm
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            double result = (int)_rotationAxisOrder * 1.0E+4 + _alpha * 1.0E+3 +
                _beta * 1.0E+2 + _gamma * 10;
            if (result > -1.0 && result < 1.0)
                result *= 1.0E+9;
            return (int)Math.Round(result);
        }

        /// <summary>
        /// Convert Euler angles to array
        /// </summary>
        /// <param name="eulerAngles">Euler angles array</param>
        public static explicit operator double[](EulerAngles eulerAngles)
        {
            return eulerAngles.ToArray();
        }

        /// <summary>
        /// Checking for equality of points
        /// </summary>
        /// <param name="eulers1">Euler angles 1</param>
        /// <param name="eulers2">Euler angles 2</param>
        /// <returns>Result of checking</returns>
        public static bool operator ==(EulerAngles eulers1, EulerAngles eulers2)
        {
            return eulers1.Equals(eulers2);
        }

        /// <summary>
        /// Checking for inequality of points
        /// </summary>
        /// <param name="eulers1">Euler angles 1</param>
        /// <param name="eulers2">Euler angles 2</param>
        /// <returns>Result of checking</returns>
        public static bool operator !=(EulerAngles eulers1, EulerAngles eulers2)
        {
            return !eulers1.Equals(eulers2);
        }
    }
}
