using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using D1den.Calculation;

namespace Tests
{
    class MatrixOperationsTest
    {
        [Test]
        public void Negative()
        {
            var matrix = Matrix.Ones(2);
            Matrix matrix2 = matrix.Negative();
            Assert.AreEqual(matrix2[0, 0], -1);
            Assert.AreEqual(matrix2[0, 1], -1);
            Assert.AreEqual(matrix2[1, 0], -1);
            Assert.AreEqual(matrix2[1, 1], -1);
        }
        [Test]
        public void Transpose()
        {
            Matrix matrix = new[,]
            {
                {1, 2, 3 },
                {4, 5, 6 }
            };
            Matrix matrix2 = matrix.Transpose();
            Assert.AreEqual(matrix2[0, 0], 1);
            Assert.AreEqual(matrix2[0, 1], 4);
            Assert.AreEqual(matrix2[1, 0], 2);
            Assert.AreEqual(matrix2[1, 1], 5);
            Assert.AreEqual(matrix2[2, 0], 3);
            Assert.AreEqual(matrix2[2, 1], 6);
        }
        [Test]
        public void TransposeOneElementMatrix()
        {
            Matrix matrix = Matrix.Ones(1);
            Matrix matrix2 = matrix.Transpose();
            Assert.AreEqual(matrix2[0, 0], 1);
        }
        [Test]
        public void AddMatrixAndValue()
        {
            Matrix matrix = Matrix.Ones(2);
            Matrix matrix2 = matrix.Add(2.34);
            Matrix matrix3 = matrix + 2.34;
            Matrix matrix4 = 2.34 + matrix;
            Assert.AreEqual(matrix2[0, 0], 3.34);
            Assert.AreEqual(matrix2[0, 1], 3.34);
            Assert.AreEqual(matrix2[1, 0], 3.34);
            Assert.AreEqual(matrix2[1, 1], 3.34);
            Assert.IsTrue(matrix2 == matrix3);
            Assert.IsTrue(matrix3.Equals(matrix4));
        }
        [Test]
        public void AddMatrices()
        {
            Matrix matrix = Matrix.Ones(2);
            Matrix matrix2 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            Matrix matrix3 = matrix.Add(matrix2);
            Matrix matrix4 = matrix + matrix2;
            Matrix matrix5 = matrix2 + matrix;
            Assert.AreEqual(matrix3[0, 0], 2);
            Assert.AreEqual(matrix3[0, 1], 3);
            Assert.AreEqual(matrix3[1, 0], 4);
            Assert.AreEqual(matrix3[1, 1], 5);
            Assert.IsTrue(matrix3 == matrix4);
            Assert.IsTrue(matrix3.Equals(matrix5));
        }
        [Test]
        public void AddWrongMatrices()
        {
            Matrix matrix = Matrix.Ones(3);
            Matrix matrix2 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            TestDelegate testCode = delegate ()
            {
                Matrix matrix3 = matrix.Add(matrix2);
            };
            Assert.Throws<ArgumentException>(testCode);
        }
        [Test]
        public void SubtractMatrixAndValue()
        {
            Matrix matrix = Matrix.Ones(2);
            Matrix matrix2 = matrix.Subtract(2.34);
            Matrix matrix3 = matrix - 2.34;
            Assert.IsTrue(MathA.CompareAlmostEqual(matrix3[0, 0], -1.34, 0.0001));
            Assert.IsTrue(MathA.CompareAlmostEqual(matrix3[0, 1], -1.34, 0.0001));
            Assert.IsTrue(MathA.CompareAlmostEqual(matrix3[1, 0], -1.34, 0.0001));
            Assert.IsTrue(MathA.CompareAlmostEqual(matrix3[1, 1], -1.34, 0.0001));
            Assert.IsTrue(matrix3 == matrix2);
        }
        [Test]
        public void SubtractMatrices()
        {
            Matrix matrix = Matrix.Ones(2);
            Matrix matrix2 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            Matrix matrix3 = matrix.Subtract(matrix2);
            Matrix matrix4 = matrix - matrix2;
            Matrix matrix5 = matrix2 - matrix;
            Assert.AreEqual(matrix3[0, 0], 0);
            Assert.AreEqual(matrix3[0, 1], -1);
            Assert.AreEqual(matrix3[1, 0], -2);
            Assert.AreEqual(matrix3[1, 1], -3);
            Assert.IsTrue(matrix3 == matrix4);
            Assert.IsTrue(matrix3.Equals(matrix5.Negative()));
        }
        [Test]
        public void SubtractWrongMatrices()
        {
            Matrix matrix = Matrix.Ones(3);
            Matrix matrix2 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            TestDelegate testCode = delegate ()
            {
                Matrix matrix3 = matrix.Subtract(matrix2);
            };
            Assert.Throws<ArgumentException>(testCode);
        }
        [Test]
        public void GetDeterminant()
        {
            Matrix matrix1 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            Assert.AreEqual(matrix1.GetDeterminant(), -2.0);
        }
        [Test]
        public void GetDeterminantOneElementMatrix()
        {
            Matrix matrix1 = new[,]
            {
                {4 }
            };
            Assert.AreEqual(matrix1.GetDeterminant(), 4);
        }
        [Test]
        public void GetDeterminantNotSquareMatrix()
        {
            Matrix matrix1 = new[,]
            {
                {1, 2 },
                {3, 4 },
                {5, 6 }
            };
            TestDelegate testCode = delegate ()
            {
                var determinant = matrix1.GetDeterminant();
            };
            Assert.Throws<ArithmeticException>(testCode);
        }
        [Test]
        public void GetMinorMatrix()
        {
            Matrix matrix1 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            Matrix minor00 = matrix1.GetMinorMatrix(0, 0);
            Assert.AreEqual(minor00.MatrixData.Length, 1);
            Assert.AreEqual(minor00[0, 0], 4);
        }
        [Test]
        public void GetMinorWrongMatrix()
        {
            Matrix matrix1 = new[,]
            {
                {1, 2 },
                {3, 4 },
                {5, 6 }
            };
            TestDelegate testCode = delegate ()
            {
                Matrix minor00 = matrix1.GetMinorMatrix(0,0);
            };
            Assert.Throws<ArithmeticException>(testCode);
        }
        [Test]
        public void GetMinorOneElementMatrix()
        {
            Matrix matrix1 = new[,]
            {
                {1, 2 },
                {3, 4 },
                {5, 6 }
            };
            TestDelegate testCode = delegate ()
            {
                Matrix minor00 = matrix1.GetMinorMatrix(0, 0);
            };
            Assert.Throws<ArithmeticException>(testCode);
        }
        [Test]
        public void GetEuclideanNorm()
        {
            Matrix matrix1 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            Assert.AreEqual(matrix1.GetEuclideanNorm(), Math.Sqrt(30));
        }
        [Test]
        public void MultiplyMatrixAndValue()
        {
            Matrix matrix = Matrix.Eye(2);
            Matrix matrix2 = matrix.Multiply(2.5);
            Matrix matrix3 = matrix * 2.5;
            Matrix matrix4 = 2.5 * matrix;
            Assert.AreEqual(matrix2[0, 0], 2.5);
            Assert.AreEqual(matrix2[0, 1], 0);
            Assert.AreEqual(matrix2[1, 0], 0);
            Assert.AreEqual(matrix2[1, 1], 2.5);
            Assert.IsTrue(matrix2 == matrix3 && matrix2.Equals(matrix4));
        }
        [Test]
        public void DivideyMatrixAndValue()
        {
            Matrix matrix = Matrix.Eye(2) * 5;
            Matrix matrix2 = matrix.Divide(2.5);
            Matrix matrix3 = matrix / 2.5;
            Assert.AreEqual(matrix2[0, 0], 2);
            Assert.AreEqual(matrix2[0, 1], 0);
            Assert.AreEqual(matrix2[1, 0], 0);
            Assert.AreEqual(matrix2[1, 1], 2);
            Assert.IsTrue(matrix2 == matrix3);
        }
        [Test]
        public void MultiplyMatriсes()
        {
            Matrix matrix = new[,]
            {
                {1,2 },
                {3,4 }
            };
            Matrix matrix2 = Matrix.Ones(2);
            Matrix matrix3 = matrix.Multiply(matrix2);
            Matrix matrix4 = matrix * matrix2;
            Matrix matrix5 = matrix2 * matrix;
            Assert.AreEqual(matrix3[0, 0], 3);
            Assert.AreEqual(matrix3[0, 1], 3);
            Assert.AreEqual(matrix3[1, 0], 7);
            Assert.AreEqual(matrix3[1, 1], 7);

            Assert.AreEqual(matrix5[0, 0], 4);
            Assert.AreEqual(matrix5[0, 1], 6);
            Assert.AreEqual(matrix5[1, 0], 4);
            Assert.AreEqual(matrix5[1, 1], 6);
            Assert.IsTrue(matrix3 == matrix4);
        }
        [Test]
        public void MultiplyOneElementMatriсes()
        {
            Matrix matrix = new[,]
            {
                {5}
            };
            Matrix matrix2 = new[,]
            {
                {6}
            };
            Matrix matrix3 = matrix.Multiply(matrix2);
            Assert.AreEqual(matrix3[0, 0], 30);
        }
        [Test]
        public void MultiplytWrongMatrices()
        {
            Matrix matrix = Matrix.Ones(3);
            Matrix matrix2 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            TestDelegate testCode = delegate ()
            {
                Matrix matrix3 = matrix.Multiply(matrix2);
            };
            Assert.Throws<ArgumentException>(testCode);
        }
        [Test]
        public void Invert()
        {
            Matrix matrix = new[,]
            {
                {2,2 },
                {7,6 }
            };
            Matrix matrixInvert = matrix.Invert();
            Assert.AreEqual(matrixInvert[0, 0], -3);
            Assert.AreEqual(matrixInvert[0, 1], 1);
            Assert.AreEqual(matrixInvert[1, 0], 3.5);
            Assert.AreEqual(matrixInvert[1, 1], -1);
        }
        [Test]
        public void InvertOneElementMatrix()
        {
            Matrix matrix = new[,]
            {
                {5}
            };
            Matrix matrixInvert = matrix.Invert();
            Assert.AreEqual(matrixInvert[0, 0], 1.0 / 5.0);
        }
        [Test]
        public void InvertWrongMatrix()
        {
            Matrix matrix1 = new[,]
            {
                {1, 2 },
                {3, 4 },
                {5, 6 }
            };
            TestDelegate testCode = delegate ()
            {
                Matrix matrixInvert = matrix1.Invert();
            };
            Assert.Throws<ArithmeticException>(testCode);
        }
    }
}
