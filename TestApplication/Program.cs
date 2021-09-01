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
            Matrix matrix1 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            double[,] array = matrix1.MatrixData;
            array[0, 0] = 5;
        }
    }
}
