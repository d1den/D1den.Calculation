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
            Point3D point1 = new Point3D();
            Point3D point2 = new Point3D(2, 3.45, 2.14);
            Point3D point3 = new Point3D(new double[] { 1.234, 124, -0.45 });
            Console.WriteLine(point3.GetDistance(point2));
            Console.WriteLine(Point3D.GetDistance(point1, point1));
        }
    }
}
