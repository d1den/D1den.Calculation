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
            Console.WriteLine(string.Format("{0} degres = {1} radians", 45, AdvancedMath.DegreeToRadian(45)));
            Console.WriteLine(string.Format("{0} radians = {1} degres", Math.PI / 2.0, AdvancedMath.RadianToDegree(Math.PI / 2.0)));
        }
    }
}
