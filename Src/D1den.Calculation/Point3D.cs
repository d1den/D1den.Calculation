using System;

namespace D1den.Calculation
{
    /// <summary>
    /// Point in 3-d space
    /// </summary>
    [Serializable]
    public readonly struct Point3D : IEquatable<Point3D>
    {
        #region Fields and properties
        private readonly double _x, _y, _z;
        /// <summary>
        /// X coordinate
        /// </summary>
        public double X => _x;
        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y => _y;
        /// <summary>
        /// Z coordinate
        /// </summary>
        public double Z => _z;
        #endregion

        /// <summary>
        /// Creating a point by coordinates
        /// </summary>
        public Point3D(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <summary>
        /// Creating a point from an array of its coordinates
        /// </summary>
        /// <param name="pointArray">Array of coordinates [x, y, z]</param>
        /// <exception cref="ArgumentException">If <paramref name="pointArray"/>.Length != 3</exception>
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

        #region Methods
        /// <summary>
        /// Convert point to array of coordinates
        /// </summary>
        /// <returns>Array of coordinatesт</returns>
        public double[] ToArray()
        {
            return new double[] { _x, _y, _z };
        }

        /// <summary>
        /// Get distance to other point
        /// </summary>
        /// <param name="point">Other point</param>
        /// <returns>Distance</returns>
        public double GetDistance(Point3D point)
        {
            return Math.Sqrt(Math.Pow(point.X - X, 2.0) + Math.Pow(point.Y - Y, 2.0) + Math.Pow(point.Z - Z, 2.0));
        }

        /// <summary>
        /// Get point as string
        /// </summary>
        /// <returns>Point as string</returns>
        public override string ToString()
        {
            return string.Format("{0:F2}; {1:F2}; {2:F2}", _x, _y, _z);
        }

        /// <summary>
        /// Checking for equality of points
        /// </summary>
        /// <param name="obj">Packed point object</param>
        /// <returns>Result of checking</returns>
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
        /// Checking for equality of points
        /// </summary>
        /// <param name="other">Point object</param>
        /// <returns>Result of checking</returns>
        public bool Equals(Point3D other)
        {
            if (other.X == X && other.Y == Y && other.Z == Z)
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
            double result = _x * 1000.0 + _y * 100 + _z * 10;
            if (result > int.MaxValue)
                result /= 1.0E+9;
            else if (result > -1.0 && result < 1.0)
                result *= 1.0E+9;
            return (int)Math.Round(result);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Convert an array of coordinates to a point
        /// </summary>
        /// <param name="pointArray">Array of coordinates [x, y, z]</param>
        /// <exception cref="ArgumentException">If <paramref name="pointArray"/>.Length != 3</exception>
        public static implicit operator Point3D(double[] pointArray)
        {
            return new Point3D(pointArray);
        }

        /// <summary>
        /// Convert point to array of coordinates
        /// </summary>
        /// <param name="point">Point</param>
        public static explicit operator double[](Point3D point)
        {
            return point.ToArray();
        }

        /// <summary>
        /// Checking for equality of points
        /// </summary>
        /// <param name="point1">Point 1</param>
        /// <param name="point2">Point 2</param>
        /// <returns>Result of checking</returns>
        public static bool operator ==(Point3D point1, Point3D point2)
        {
            return point1.Equals(point2);
        }

        /// <summary>
        /// Checking for inequality of points
        /// </summary>
        /// <param name="point1">Point 1</param>
        /// <param name="point2">Point 2</param>
        /// <returns>Result of checking</returns>>
        public static bool operator !=(Point3D point1, Point3D point2)
        {
            return !point1.Equals(point2);
        }
        #endregion
    }
}
