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
            Matrix matrix1 = new[,] { { 1.23, 2.34 }, { 3.45, 2.23 } };
            Matrix matrix2 = new[,] { { 1.23, 2.34 }, { 3.45, 2.23 } };
            int hash1 = matrix1.GetHashCode();
            int hash2 = matrix2.GetHashCode();
            Matrix matrix3 = new[,]
            {
                {2345, 23456, 9845.5 },
                {3423452, 23.343, 0.2321 },
                {-0.2312, -1234, 23 }
            };
            int hash3 = matrix3.GetHashCode();
        }
    }
}
