using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d1den.MathLibrary;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix1 = new Matrix();
            Matrix matrix2 = new Matrix(new double[,] { { 1, 2, 3.45 }, { 234, 324, -232.23 } });
            Console.WriteLine(matrix2);
            Matrix matrix3 = new Matrix(3, -Math.PI);
            Matrix matrix4 = new Matrix(2, 4);
            Matrix matrix5 = Matrix.GetZerosMatrix(4);
            Matrix matrix6 = Matrix.GetOnesMatrix(2);
            Matrix matrix7 = Matrix.GetUnitMatrix(3);
        }
    }
}
