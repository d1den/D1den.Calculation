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
            Matrix matrix1 = new int[] { 1, 2, 3};
            Matrix matrix2 = new double[] { 1, 2, 3 };
            Console.WriteLine("Matrices aren`t equal? - {0}", matrix1 != matrix2);
        }
    }
}
