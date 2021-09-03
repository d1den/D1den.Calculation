using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using D1den.EngineeringMath;

namespace TestForLibrary
{
    class AdvancedMathTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ClampMax()
        {
            if (MathA.Clamp(5, 0, 4) == 4 && MathA.Clamp(5.54, -0.23, 3.23) == 3.23)
                Assert.Pass();
        }
        [Test]
        public void ClampMin()
        {
            if (MathA.Clamp(-2, 0, 4) == 0 && MathA.Clamp(-5.54, -0.23, 3.23) == -0.23)
                Assert.Pass();
        }
        [Test]
        public void ClampNormal()
        {
            if (MathA.Clamp(2, 0, 4) == 2 && MathA.Clamp(2.28, -0.23, 3.23) == 2.28)
                Assert.Pass();
        }

        [Test]
        public void DegreeToRadianAndBack()
        {
            double degree = 123.45;
            double radian = MathA.DegreeToRadian(degree);
            double degree2 = MathA.RadianToDegree(radian);
            if (degree == degree2)
                Assert.Pass();
        }
    }
}
