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
            Matrix r = new[,]
            {
                {-0.6823, -0.0085, 0.7305},
                {0.4174, -0.8252, 0.3806 },
                {0.5996, 0.5648, 0.5670 }
            };
            EulerAngles eulers = EulerAngles.FromRotationMatrix(r, RotationAxisOrder.ZXZ);
            Matrix rNew = eulers.GetRotationMatrix();
            EulerAngles eulersNew = EulerAngles.FromRotationMatrix(rNew, RotationAxisOrder.ZXZ);
            Console.WriteLine(eulers.ToString() + " =? " + eulersNew.ToString());
            Console.WriteLine(r.ToString() + " =? " + rNew.ToString());
        }
    }
}
