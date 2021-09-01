using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using d1den.MathLibrary;

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
            if (AdvancedMath.Clamp(5, 0, 4) == 4 && AdvancedMath.Clamp(5.54, -0.23, 3.23) == 3.23)
                Assert.Pass();
        }
        [Test]
        public void ClampMin()
        {
            if (AdvancedMath.Clamp(-2, 0, 4) == 0 && AdvancedMath.Clamp(-5.54, -0.23, 3.23) == -0.23)
                Assert.Pass();
        }
        [Test]
        public void ClampNormal()
        {
            if (AdvancedMath.Clamp(2, 0, 4) == 2 && AdvancedMath.Clamp(2.28, -0.23, 3.23) == 2.28)
                Assert.Pass();
        }

        [Test]
        public void DegreeToRadianAndBack()
        {
            double degree = 123.45;
            double radian = AdvancedMath.DegreeToRadian(degree);
            double degree2 = AdvancedMath.RadianToDegree(radian);
            if (degree == degree2)
                Assert.Pass();
        }
    }
}
