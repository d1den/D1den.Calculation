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
            Matrix r = new[,]
            {
                {-0.6829, -0.0085, 0.7305 },
                {0.4174, -0.8252, 0.3806 },
                {0.5996, 0.5648, 0.5870 }
            };
            var eulers = EulerAngles.GetEulersXYZFromRotation(r).ConvertToDegrees();
        }
    }
}
