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
            Matrix matrix = new[,]
            {
                {1,2 },
                {3,4 }
            };
            Matrix matrix2 = Matrix.Eye(2);
            Matrix matrix3 = matrix.Multiply(matrix2);
        }
    }
}
