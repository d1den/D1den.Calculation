using NUnit.Framework;
using D1den.Calculation;
using System;

namespace Tests
{
    public class Point3DTests
    {
        [Test]
        public void CreatePointFromParams()
        {
            int x = 3;
            double y = -23.4;
            double z = 45.12;
            Point3D point3D = new Point3D(x, y, z);
            Assert.AreEqual(point3D.X, x);
            Assert.AreEqual(point3D.Y, y);
            Assert.AreEqual(point3D.Z, z);
        }
        [Test]
        public void CreatePointFromArray()
        {
            double[] pointArray = { 123, 0.23, -234 };
            Point3D point3D = new Point3D(pointArray);
            Assert.AreEqual(point3D.X, pointArray[0]);
            Assert.AreEqual(point3D.Y, pointArray[1]);
            Assert.AreEqual(point3D.Z, pointArray[2]);
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
            Assert.AreEqual(newArray[0], pointArray[0]);
            Assert.AreEqual(newArray[1], pointArray[1]);
            Assert.AreEqual(newArray[2], pointArray[2]);
        }
        [Test]
        public void ConvertArrayToPointAndBack()
        {
            double[] pointArray = { 123, 0.23, -234 };
            Point3D point3D = pointArray;
            double[] newArray = (double[])point3D;
            Assert.AreEqual(newArray[0], pointArray[0]);
            Assert.AreEqual(newArray[1], pointArray[1]);
            Assert.AreEqual(newArray[2], pointArray[2]);
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
            Point3D point2 = new Point3D(1, -2, 4);
            Assert.AreEqual(point1.GetDistance(point2), point2.GetDistance(point1));
            Assert.IsTrue(point2.GetDistance(point1) > 9.27);
        }
        [Test]
        public void EqualsTest()
        {
            Point3D point1 = new Point3D(2, -1, 3.14);
            Point3D point2 = point1;
            Point3D point3 = new Point3D(2, -1, 3.14);
            Assert.IsTrue(point1.Equals(point1));
            Assert.IsTrue(point1 == point2);
            Assert.IsTrue(point2 == point3);
        }
        [Test]
        public void NotEqualsTest()
        {
            Point3D point1 = new Point3D(2, -1, 3.14);
            Point3D point3 = new Point3D(-2, -1, 3.14);
            Assert.IsTrue(!point1.Equals(0));
            Assert.IsTrue(point1 != point3);
        }
        [Test]
        public void HashCodeTest()
        {
            Point3D point1 = new Point3D(2, -1, 3.14);
            Point3D point2 = point1;
            Point3D point3 = new Point3D(-2, -1, 3.14);
            Assert.AreEqual(point1.GetHashCode(), point1.GetHashCode());
            Assert.AreEqual(point1.GetHashCode(), point2.GetHashCode());
            Assert.AreNotEqual(point1.GetHashCode(), point3.GetHashCode());
        }
    }
}