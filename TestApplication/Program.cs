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
            EulerAngles euler1 = new EulerAngles(123, -45.23, 123);
            double[] angles = (double[])euler1;
        }
    }
}
