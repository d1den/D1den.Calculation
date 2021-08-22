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
            Matrix matrix = new Matrix( new double[,]
                {
                    {2, 5, 7 },
                    {6,3,4 },
                    {5,-2,-3 }
                }
                );
            Console.WriteLine(matrix.Invert());
        }
    }
}
