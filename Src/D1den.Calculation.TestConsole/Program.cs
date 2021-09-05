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
            Point3D point1 = new Point3D(2, -1, 3.14);
            Point3D point2 = point1;
            Point3D point3 = new Point3D(2, -1, 3.14);
            Console.WriteLine(point1.Equals(point1));
            Console.WriteLine(point1 == point2);
            Console.WriteLine(point2 == point3);
        }
    }
}
