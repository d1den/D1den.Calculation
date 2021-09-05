using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using D1den.Calculation;

namespace Tests
{
    class EulerAnglesTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateObjectsInDegreesAndTestEquals()
        {
            EulerAngles eulerAngles = new EulerAngles(30, 40, -50);
            EulerAngles eulerAngles1 = new EulerAngles(new double[] { 30, 40, -50 });
            EulerAngles eulerAngles2 = new EulerAngles(30, 40, -50, AngleUnits.Degrees, RotationAxisOrder.XYZ);
            EulerAngles eulerAngles3 = new EulerAngles(new double[] { 30, 40, -50 }, AngleUnits.Degrees, RotationAxisOrder.XYZ);
            if (eulerAngles.Equals(eulerAngles1) && eulerAngles1 == eulerAngles2 && eulerAngles == eulerAngles3)
                Assert.Pass();
        }
        [Test]
        public void CreateObjectsInRadiansAndTestEquals()
        {
            EulerAngles eulerAngles = new EulerAngles(2.45, 1.78, -3.06, AngleUnits.Radians, RotationAxisOrder.ZXZ);
            EulerAngles eulerAngles2 = new EulerAngles(new double[] { 2.45, 1.78, -3.06, },
                AngleUnits.Radians, RotationAxisOrder.ZXZ);
            if (eulerAngles.Equals(eulerAngles2))
                Assert.Pass();
        }
        [Test]
        public void ToArray()
        {
            EulerAngles eulerAngles = new EulerAngles(2.45, 1.78, -3.06, AngleUnits.Radians, RotationAxisOrder.ZXZ);
            double[] arrayAngles = eulerAngles.ToArray();
            double[] arrayAngles2 = (double[])eulerAngles;
            if (arrayAngles[0] == 2.45 && arrayAngles[1] == 1.78 && arrayAngles[2] == -3.06 &&
                arrayAngles[0] == arrayAngles2[0] && arrayAngles[2] == arrayAngles2[2])
                Assert.Pass();
        }
        [Test]
        public void ConvertToRadians()
        {
            EulerAngles eulerAngles = new EulerAngles(30, 40, -50);
            EulerAngles eulersInRadians1 = eulerAngles.ConvertToRadians();
            EulerAngles eulersInRadians2 = eulersInRadians1.ConvertToRadians();
            if (eulersInRadians1.AngleUnits == AngleUnits.Radians &&
                eulersInRadians1.Alpha == MathA.DegreeToRadian(eulerAngles.Alpha) &&
                eulersInRadians1.Beta == MathA.DegreeToRadian(eulerAngles.Beta) &&
                eulersInRadians1.Gamma == MathA.DegreeToRadian(eulerAngles.Gamma) &&
                eulersInRadians1 == eulersInRadians2)
                Assert.Pass();
        }
        [Test]
        public void ConvertToDegrees()
        {
            EulerAngles eulerAngles = new EulerAngles(2.45, 1.78, -3.06, AngleUnits.Radians, RotationAxisOrder.XYZ);
            EulerAngles eulersInDegrees1 = eulerAngles.ConvertToDegrees();
            EulerAngles eulersInDegrees2 = eulersInDegrees1.ConvertToDegrees();
            if (eulersInDegrees1.AngleUnits == AngleUnits.Degrees &&
                eulersInDegrees1.Alpha == MathA.RadianToDegree(eulerAngles.Alpha) &&
                eulersInDegrees1.Beta == MathA.RadianToDegree(eulerAngles.Beta) &&
                eulersInDegrees1.Gamma == MathA.RadianToDegree(eulerAngles.Gamma) &&
                eulersInDegrees1 == eulersInDegrees2)
                Assert.Pass();
        }
        [Test]
        public void ToStringTests()
        {
            EulerAngles eulerAngles = new EulerAngles(2.45, 1.78, -3.06, AngleUnits.Radians, RotationAxisOrder.XYZ);
            string expectedString = string.Format("{0}:{1:F2}; {2:F2}; {3:F2}", eulerAngles.RotationAxisOrder,
                eulerAngles.Alpha, eulerAngles.Beta, eulerAngles.Gamma);
            string expectedStringInDegrees = string.Format("{0}:{1:F2}; {2:F2}; {3:F2}", eulerAngles.RotationAxisOrder,
                MathA.RadianToDegree(eulerAngles.Alpha),
                MathA.RadianToDegree(eulerAngles.Beta),
                MathA.RadianToDegree(eulerAngles.Gamma));
            if (eulerAngles.ToString() == expectedString &&
                eulerAngles.ToString(AngleUnits.Radians) == expectedString &&
                eulerAngles.ToString(AngleUnits.Degrees) == expectedStringInDegrees)
                Assert.Pass();
        }
        [Test]
        public void HashCodeTests()
        {
            EulerAngles eulerAngles = new EulerAngles(2.45, 1.78, -3.06, AngleUnits.Radians, RotationAxisOrder.XYZ);
            EulerAngles eulerAngles1 = eulerAngles;
            EulerAngles eulerAngles2 = new EulerAngles(2.45, 1.78, -3.06, AngleUnits.Radians, RotationAxisOrder.ZXZ);
            EulerAngles eulerAngles3 = new EulerAngles(231, 1.78, -43.4, AngleUnits.Degrees, RotationAxisOrder.ZXZ);
            if (eulerAngles.GetHashCode() == eulerAngles.GetHashCode() &&
                eulerAngles.GetHashCode() == eulerAngles1.GetHashCode() &&
                eulerAngles.GetHashCode() != eulerAngles2.GetHashCode() &&
                eulerAngles.GetHashCode() != eulerAngles3.GetHashCode())
                Assert.Pass();
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
            EulerAngles eulers = EulerAngles.GetEulersFromRotation(r, RotationAxisOrder.XYZ);
            Matrix rNew = eulers.GetRotationMatrix();
            EulerAngles eulersNew = EulerAngles.GetEulersFromRotation(rNew, RotationAxisOrder.XYZ);
            if (eulers.ToString() == eulersNew.ToString() &&
                r.ToString() == rNew.ToString())
                Assert.Pass();
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
            EulerAngles eulers = EulerAngles.GetEulersFromRotation(r, RotationAxisOrder.ZXZ);
            Matrix rNew = eulers.GetRotationMatrix();
            EulerAngles eulersNew = EulerAngles.GetEulersFromRotation(rNew, RotationAxisOrder.ZXZ);
            if (eulers.ToString() == eulersNew.ToString() &&
                r.ToString() == rNew.ToString())
                Assert.Pass();
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
            EulerAngles eulers = EulerAngles.GetEulersFromRotation(r, RotationAxisOrder.ZYZ);
            Matrix rNew = eulers.GetRotationMatrix();
            EulerAngles eulersNew = EulerAngles.GetEulersFromRotation(rNew, RotationAxisOrder.ZYZ);
            if (eulers.ToString() == eulersNew.ToString() &&
                r.ToString() == rNew.ToString())
                Assert.Pass();
        }
    }
}
