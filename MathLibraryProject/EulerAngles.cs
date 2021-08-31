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
    }
}
