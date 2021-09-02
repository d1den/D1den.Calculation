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
            double[,] array = new double[,]
            {
                {1, 2 },
                {3, 4 }
            };
            Matrix matrix1 = new Matrix(array);
            var matrix2 = matrix1.SetAllValues(2.28);
        }
    }
}
