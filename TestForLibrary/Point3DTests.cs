using NUnit.Framework;
using D1den.EngineeringMath;
using System;

namespace TestForLibrary
{
    public class Point3DTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreatePointFromParams()
        {
            int x = 3;
            double y = -23.4;
            double z = 45.12;
            Point3D point3D = new Point3D(x, y, z);
            if (point3D.X == x && point3D.Y == y && point3D.Z == z)
                Assert.Pass();
        }
        [Test]
        public void CreatePointFromArray()
        {
            double[] pointArray = { 123, 0.23, -234 };
            Point3D point3D = new Point3D(pointArray);
            if (point3D.X == pointArray[0] && point3D.Y == pointArray[1] && point3D.Z == pointArray[2])
                Assert.Pass();
        }

        [Test]
        public void CreatePointFromWrongArray()
        {
            double[] pointArray = { 123, 0.23, -234, 543 };
            TestDelegate testCode = delegate ()
            {
                 new Point3D(pointArray);
            };
            Assert.Throws<ArgumentException>(testCode);
        }

        [Test]
        public void GetArrayFromPoint()
        {
            double[] pointArray = { 123, 0.23, -234 };
            Point3D point3D = new Point3D(pointArray);
            double[] newArray = point3D.ToArray();
            if (newArray[0] == pointArray[0] && newArray[1] == pointArray[1] && newArray[2] == pointArray[2])
                Assert.Pass();
        }

        [Test]
        public void ConvertArrayToPointAndBack()
        {
            double[] pointArray = { 123, 0.23, -234 };
            Point3D point3D = pointArray;
            double[] newArray = (double[])point3D;
            if (newArray[0] == pointArray[0] && newArray[1] == pointArray[1] && newArray[2] == pointArray[2])
                Assert.Pass();
        }

        [Test]
        public void GetZeroDistance()
        {
            double[] pointArray = { 123, 0.23, -234 };
            Point3D point1 = pointArray;
            Point3D point2 = point1;
            Assert.AreEqual(point1.GetDistance(point2), 0.0);
        }

        [Test]
        public void GetDistance()
        {
            double[] pointArray = { 2, 4, -3 };
            Point3D point1 = pointArray;
            Point3D point2 = new Point3D(1,-2, 4);
            if (point1.GetDistance(point2) == point2.GetDistance(point1) && point2.GetDistance(point1) > 9.27)
                Assert.Pass();
        }

        [Test]
        public void EqualsTest()
        {
            Point3D point1 = new Point3D(2, -1, 3.14);
            Point3D point2 = point1;
            Point3D point3 = new Point3D(2, -1, 3.14);
            if (point1.Equals(point1) && point1 == point2 && point2 == point3)
                Assert.Pass();
        }

        [Test]
        public void NotEqualsTest()
        {
            Point3D point1 = new Point3D(2, -1, 3.14);
            Point3D point3 = new Point3D(-2, -1, 3.14);
            if (!point1.Equals(0) && point1 != point3)
                Assert.Pass();
        }


        [Test]
        public void HashCodeTest()
        {
            Point3D point1 = new Point3D(2, -1, 3.14);
            Point3D point2 = point1;
            Point3D point3 = new Point3D(-2, -1, 3.14);
            if (point1.GetHashCode() == point1.GetHashCode() && point1.GetHashCode() == point2.GetHashCode()
                && point1.GetHashCode() != point3.GetHashCode())
                Assert.Pass();
        }
    }
}