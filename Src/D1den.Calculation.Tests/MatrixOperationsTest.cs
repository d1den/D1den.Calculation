using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using D1den.Calculation;

namespace Tests
{
    class MatrixOperationsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Negative()
        {
            var matrix = Matrix.GetOnesMatrix(2);
            Matrix matrix2 = matrix.Negative();
            if (matrix2[0, 0] == -1 && matrix2[0, 1] == -1 && matrix2[1, 0] == -1 && matrix2[1, 1] == -1)
                Assert.Pass();
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
            if (matrix2[0, 0] == 1 && matrix2[0, 1] == 4 && matrix2[1, 0] == 2 && matrix2[1, 1] == 5 &&
                matrix2[2,0] == 3 && matrix2[2, 1] == 6)
                Assert.Pass();
        }
        [Test]
        public void TransposeOneElementMatrix()
        {
            Matrix matrix = Matrix.GetOnesMatrix(1);
            Matrix matrix2 = matrix.Transpose();
            if (matrix2[0, 0] == 1)
                Assert.Pass();
        }
        [Test]
        public void AddMatrixAndValue()
        {
            Matrix matrix = Matrix.GetOnesMatrix(2);
            Matrix matrix2 = matrix.Add(2.34);
            Matrix matrix3 = matrix + 2.34;
            Matrix matrix4 = 2.34 + matrix;
            if (matrix2[0, 0] == 3.34 && matrix2[0, 1] == 3.34 && matrix2[1, 0] == 3.34 && matrix2[1, 1] == 3.34 &&
                matrix2 == matrix3 && matrix3.Equals(matrix4))
                Assert.Pass();
        }
        [Test]
        public void AddMatrices()
        {
            Matrix matrix = Matrix.GetOnesMatrix(2);
            Matrix matrix2 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            Matrix matrix3 = matrix.Add(matrix2);
            Matrix matrix4 = matrix + matrix2;
            Matrix matrix5 = matrix2 + matrix;
            if (matrix3[0, 0] == 2 && matrix3[0, 1] == 3 && matrix3[1, 0] == 4 && matrix3[1, 1] == 5 &&
                matrix3 == matrix4 && matrix3.Equals(matrix5))
                Assert.Pass();
        }
        [Test]
        public void AddWrongMatrices()
        {
            Matrix matrix = Matrix.GetOnesMatrix(3);
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
            Matrix matrix = Matrix.GetOnesMatrix(2);
            Matrix matrix2 = matrix.Subtract(2.34);
            Matrix matrix3 = matrix - 2.34;
            if (matrix2[0, 0] == -1.34 && matrix2[0, 1] == -1.34 && matrix2[1, 0] == -1.34 && matrix2[1, 1] == -1.34 &&
                matrix2 == matrix3)
                Assert.Pass();
        }
        [Test]
        public void SubtractMatrices()
        {
            Matrix matrix = Matrix.GetOnesMatrix(2);
            Matrix matrix2 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            Matrix matrix3 = matrix.Subtract(matrix2);
            Matrix matrix4 = matrix - matrix2;
            Matrix matrix5 = matrix2 - matrix;
            if (matrix3[0, 0] == 0 && matrix3[0, 1] == -1 && matrix3[1, 0] == -2 && matrix3[1, 1] == -3 &&
                matrix3 == matrix4 && matrix3.Equals(matrix5.Negative()))
                Assert.Pass();
        }
        [Test]
        public void SubtractWrongMatrices()
        {
            Matrix matrix = Matrix.GetOnesMatrix(3);
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
            if (matrix1.GetDeterminant() == -2.0)
                Assert.Pass();
        }
        [Test]
        public void GetDeterminantOneElementMatrix()
        {
            Matrix matrix1 = new[,]
            {
                {4 }
            };
            if (matrix1.GetDeterminant() == 4)
                Assert.Pass();
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
            Assert.Throws<ArgumentException>(testCode);
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
            if (minor00.MatrixData.Length == 1 && minor00[0, 0] == 4)
                Assert.Pass();
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
            Assert.Throws<ArgumentException>(testCode);
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
            Assert.Throws<ArgumentException>(testCode);
        }
        [Test]
        public void GetEuclideanNorm()
        {
            Matrix matrix1 = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            if (matrix1.GetEuclideanNorm() == Math.Sqrt(30))
                Assert.Pass();
        }
        [Test]
        public void MultiplyMatrixAndValue()
        {
            Matrix matrix = new Matrix(dimension: 2, diagonalValue: 2);
            Matrix matrix2 = matrix.Multiply(2.5);
            Matrix matrix3 = matrix * 2.5;
            Matrix matrix4 = 2.5 * matrix;
            if (matrix2[0, 0] == 5 && matrix2[0, 1] == 0 && matrix2[1, 0] == 0 && matrix2[1, 1] == 5 &&
                matrix2 == matrix3 && matrix2.Equals(matrix4))
                Assert.Pass();
        }
        [Test]
        public void DivideyMatrixAndValue()
        {
            Matrix matrix = new Matrix(dimension: 2, diagonalValue: 5);
            Matrix matrix2 = matrix.Divide(2.5);
            Matrix matrix3 = matrix / 2.5;
            if (matrix2[0, 0] == 2 && matrix2[0, 1] == 0 && matrix2[1, 0] == 0 && matrix2[1, 1] == 2 &&
                matrix2 == matrix3)
                Assert.Pass();
        }
        [Test]
        public void MultiplyMatriсes()
        {
            Matrix matrix = new[,]
            {
                {1,2 },
                {3,4 }
            };
            Matrix matrix2 = Matrix.GetEyeMatrix(2);
            Matrix matrix3 = matrix.Multiply(matrix2);
            Matrix matrix4 = matrix * matrix2;
            Matrix matrix5 = matrix2 * matrix;
            if (matrix3[0, 0] == 3 && matrix3[0, 1] == 3 && matrix3[1, 0] == 7 && matrix3[1, 1] == 7 &&
                matrix5[0, 0] == 4 && matrix5[0, 1] == 6 && matrix5[1, 0] == 4 && matrix5[1, 1] == 6 &&
                matrix3 == matrix4)
                Assert.Pass();
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
            if (matrix3[0, 0] == 30)
                Assert.Pass();
        }
        [Test]
        public void MultiplytWrongMatrices()
        {
            Matrix matrix = Matrix.GetOnesMatrix(3);
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
            if (matrixInvert[0, 0] == -3 && matrixInvert[0, 1] == 1 &&
                matrixInvert[1, 0] == 3.5 && matrixInvert[1, 1] == -1)
                Assert.Pass();
        }
        [Test]
        public void InvertOneElementMatrix()
        {
            Matrix matrix = new[,]
            {
                {5}
            };
            Matrix matrixInvert = matrix.Invert();
            if (matrixInvert[0, 0] == 1.0/5.0)
                Assert.Pass();
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
            Assert.Throws<ArgumentException>(testCode);
        }
    }
}
