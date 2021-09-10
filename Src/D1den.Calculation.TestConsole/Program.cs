using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D1den.Calculation;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Matrix some examples
            Matrix matrix1 = new[,]
            {
                {1,2 },
                {3,4 }
            };
            Matrix matrix2 = Matrix.Eye(2);
            Matrix matrix3 = matrix1.Multiply(matrix2);
            Console.WriteLine(matrix3); // 1,00    2,00
                                        // 3,00    4,00
            Matrix m4 = new Matrix(new double[,] { { -1.23, 3.45 }, { 0.34, -0.56 } });
            Console.WriteLine(m4.GetDeterminant()); // -0,4842
            Console.WriteLine(m4.GetEuclideanNorm()); // 3,72083324001493
            Console.WriteLine(m4.Invert()); // 1,16    7,13
                                            // 0,70    2,54
            Matrix result1 = matrix1 - m4;
            Console.WriteLine(result1); // 2,23    -1,45
                                        // 2,66    4,56
            Matrix result2 = matrix3 + m4;
            Console.WriteLine(result2); // -0,23   5,45
                                        // 3,34    3,44
            Matrix result3 = (result2 * 2.34) * m4;
            Console.WriteLine(result3); // 5,00    -9,00
                                        // -6,88   22,46
            double[,] matrixData = (double[,])result3;
            #endregion

            #region Point3D some examples
            Point3D p1 = new Point3D(1, 2.34, -2.45);
            Point3D p2 = new[] { 1, -2.34, 5.45 };
            Console.WriteLine(p1); // 1,00; 2,34; -2,45
            Console.WriteLine(p2); // 1,00; -2,34; 5,45
            Console.WriteLine(p2.GetDistance(p1)); // 9,18217839077416
            #endregion

            #region Euler angles some examples
            Matrix rotation = new[,]
            {
                {-0.6823, -0.0085, 0.7305},
                {0.4174, -0.8252, 0.3806 },
                {0.5996, 0.5648, 0.5670 }
            };
            EulerAngles eulers = EulerAngles.FromRotationMatrix(rotation, RotationAxisOrder.XYZ);
            Console.WriteLine(eulers); // XYZ:-0,59; 0,82; 3,13
            Console.WriteLine(eulers.ToStringInDegrees()); // XYZ:-33,87; 46,93; 179,29
            Matrix rNew = eulers.GetRotationMatrix();
            EulerAngles eulersNew = EulerAngles.FromRotationMatrix(rNew, RotationAxisOrder.XYZ);
            Console.WriteLine(eulers == eulersNew); // True
            #endregion

            #region MathA some examples
            Console.WriteLine(MathA.Clamp(5.56, 0.45, 5.51)); // 5,51
            Console.WriteLine(MathA.DegreeToRadian(180)); // 3,14159265358979
            Console.WriteLine(MathA.RadianToDegree(Math.PI)); // 180
            Console.WriteLine(MathA.CompareAlmostEqual(2.456, 2.458, 0.005)); // True
            #endregion
        }
    }
}
