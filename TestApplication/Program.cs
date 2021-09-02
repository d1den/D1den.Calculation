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
            Matrix matrix = new[,]
               {
                {1, 2, 3 },
                {4, 5, 6 }
            };
            Matrix matrix2 = matrix.Transpose();
        }
    }
}
