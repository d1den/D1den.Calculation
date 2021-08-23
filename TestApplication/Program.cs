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
            Matrix matrix1 = new[,] { 
                { 1, 2 }, 
                { 3, 4 } };
            Matrix matrix2 = new[,]
            {
                {1.23, 5.65 },
                {-0.234, 3.45 }
            };
            int[,] matrix1ToIntArray = (int[,])matrix1;
            double[,] matrix1ToDoubleArray = (double[,])matrix1;
            int[,] matrix2ToIntArray = (int[,])matrix2;
            double[,] matrix2ToDoubleArray = (double[,])matrix2;
        }
    }
}
