using NUnit.Framework;
using D1den.Calculation;

namespace Tests
{
    class EulerAnglesTests
    {
        [Test]
        public void CreateObjectsInDegreesAndTestEquals()
        {
            EulerAngles eulerAngles1 = EulerAngles.FromDegrees(30, 40, 50, RotationAxisOrder.XYZ);
            EulerAngles eulerAngles2 = EulerAngles.FromDegreesArray(new double[] { 30, 40, 50 }, RotationAxisOrder.XYZ);
            Assert.AreEqual(eulerAngles1, eulerAngles2);
        }
        [Test]
        public void CreateObjectsInRadiansAndTestEquals()
        {
            EulerAngles eulerAngles1 = EulerAngles.FromRadians(2.34, 1.45, -3.04, RotationAxisOrder.XYZ);
            EulerAngles eulerAngles2 = EulerAngles.FromRadiansArray(new double[] { 2.34, 1.45, -3.04 },
                RotationAxisOrder.XYZ);
            Assert.AreEqual(eulerAngles1, eulerAngles2);
        }
        [Test]
        public void ToArray()
        {
            EulerAngles eulerAngles = new EulerAngles(2.45, 1.78, -3.06, RotationAxisOrder.ZXZ);
            double[] arrayAngles = eulerAngles.ToArray();
            double[] arrayAngles2 = (double[])eulerAngles;
            Assert.AreEqual(arrayAngles[0], 2.45);
            Assert.AreEqual(arrayAngles[1], 1.78);
            Assert.AreEqual(arrayAngles[2], -3.06);
            Assert.AreEqual(arrayAngles[0], arrayAngles2[0]);
            Assert.AreEqual(arrayAngles[2], arrayAngles2[2]);
        }
        [Test]
        public void ToStringTests()
        {
            EulerAngles eulerAngles = new EulerAngles(2.45, 1.78, -3.06, RotationAxisOrder.XYZ);
            string expectedString = string.Format("{0}:{1:F2}; {2:F2}; {3:F2}", eulerAngles.RotationAxisOrder,
                eulerAngles.Alpha, eulerAngles.Beta, eulerAngles.Gamma);
            string expectedStringInDegrees = string.Format("{0}:{1:F2}; {2:F2}; {3:F2}", eulerAngles.RotationAxisOrder,
                MathA.RadianToDegree(eulerAngles.Alpha),
                MathA.RadianToDegree(eulerAngles.Beta),
                MathA.RadianToDegree(eulerAngles.Gamma));
            Assert.AreEqual(eulerAngles.ToString(), expectedString);
            Assert.AreEqual(eulerAngles.ToStringInDegrees(), expectedStringInDegrees);
        }
        [Test]
        public void HashCodeTests()
        {
            EulerAngles eulerAngles = EulerAngles.FromRadians(1.23, 1.24, -0.32, RotationAxisOrder.ZXZ);
            EulerAngles eulerAngles1 = eulerAngles;
            EulerAngles eulerAngles2 = EulerAngles.FromRadians(1.23, 1.24, -0.32, RotationAxisOrder.XYZ);
            EulerAngles eulerAngles3 = EulerAngles.FromDegrees(175, 125, -78, RotationAxisOrder.XYZ);
            Assert.AreEqual(eulerAngles.GetHashCode(), eulerAngles.GetHashCode());
            Assert.AreEqual(eulerAngles.GetHashCode(), eulerAngles1.GetHashCode());
            Assert.AreNotEqual(eulerAngles.GetHashCode(), eulerAngles2.GetHashCode());
            Assert.AreNotEqual(eulerAngles.GetHashCode(), eulerAngles3.GetHashCode());
        }
        [Test]
        public void GetEulersXYZGromRotationAndBack()
        {
            Matrix r = new[,]
            {
                {-0.6823, -0.0085, 0.7305},
                {0.4174, -0.8252, 0.3806 },
                {0.5996, 0.5648, 0.5670 }
            };
            EulerAngles eulers = EulerAngles.FromRotationMatrix(r, RotationAxisOrder.XYZ);
            Matrix rNew = eulers.GetRotationMatrix();
            EulerAngles eulersNew = EulerAngles.FromRotationMatrix(rNew, RotationAxisOrder.XYZ);
            Assert.AreEqual(eulers.ToString(), eulersNew.ToString());
            Assert.AreEqual(r.ToString(), rNew.ToString());
        }
        [Test]
        public void GetEulersZXZGromRotationAndBack()
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
            Assert.AreEqual(eulers.ToString(), eulersNew.ToString());
            Assert.AreEqual(r.ToString(), rNew.ToString());
        }
        [Test]
        public void GetEulersZYZGromRotationAndBack()
        {
            Matrix r = new[,]
            {
                {-0.6823, -0.0085, 0.7305},
                {0.4174, -0.8252, 0.3806 },
                {0.5996, 0.5648, 0.5670 }
            };
            EulerAngles eulers = EulerAngles.FromRotationMatrix(r, RotationAxisOrder.ZYZ);
            Matrix rNew = eulers.GetRotationMatrix();
            EulerAngles eulersNew = EulerAngles.FromRotationMatrix(rNew, RotationAxisOrder.ZYZ);
            Assert.AreEqual(eulers.ToString(), eulersNew.ToString());
            Assert.AreEqual(r.ToString(), rNew.ToString());
        }
        [Test]
        public void EqualsWithAccuracy()
        {
            EulerAngles eulerAngles1 = EulerAngles.FromRadians(2.34, 1.45, -3.04, RotationAxisOrder.XYZ);
            EulerAngles eulerAngles2 = EulerAngles.FromRadians(2.3405, 1.4495, -3.0405, RotationAxisOrder.XYZ);
            Assert.IsTrue(eulerAngles1.Equals(eulerAngles2, 0.001));
        }
    }
}
