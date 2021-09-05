using NUnit.Framework;
using D1den.Calculation;

namespace Tests
{
    class MathATests
    {
        [Test]
        public void ClampMax()
        {
            Assert.AreEqual(MathA.Clamp(5, 0, 4), 4);
            Assert.AreEqual(MathA.Clamp(5.54, -0.23, 3.23), 3.23);
        }
        [Test]
        public void ClampMin()
        {
            Assert.AreEqual(MathA.Clamp(-2, 0, 4), 0);
            Assert.AreEqual(MathA.Clamp(-5.54, -0.23, 3.23), -0.23);
        }
        [Test]
        public void ClampNormal()
        {
            Assert.AreEqual(MathA.Clamp(2, 0, 4), 2);
            Assert.AreEqual(MathA.Clamp(2.28, -0.23, 3.23), 2.28);
        }
        [Test]
        public void DegreeToRadianAndBack()
        {
            double degree = 123.45;
            double radian = MathA.DegreeToRadian(degree);
            double degree2 = MathA.RadianToDegree(radian);
            Assert.AreEqual(degree, degree2);
        }
    }
}
