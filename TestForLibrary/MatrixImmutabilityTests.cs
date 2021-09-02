using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using d1den.MathLibrary;

namespace TestForLibrary
{
    class MatrixImmutabilityTests
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
            if (matrix[0, 0] == 1 && matrix[0, 1] == 1 && matrix[1, 0] == 1 && matrix[1, 1] == 1)
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
            if (matrix[0, 0] == 1 && matrix[0, 1] == 2 && matrix[0, 2] == 3 && matrix2[1, 0] == 4 &&
                matrix2[1, 1] == 5 && matrix2[1, 2] == 6)
                Assert.Pass();
        }
        [Test]
        public void AddMatrixAndValue()
        {
            Matrix matrix = Matrix.GetOnesMatrix(2);
            Matrix matrix2 = matrix.Add(2.34);
            if (matrix[0, 0] == 1 && matrix[0, 1] == 1 &&
                matrix[1, 0] == 1 && matrix[1, 1] == 1)
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
            if (matrix[0, 0] == 1 && matrix[0, 1] == 1 &&
                matrix[1, 0] == 1 && matrix[1, 1] == 1)
                Assert.Pass();
        }
        [Test]
        public void SubtractMatrixAndValue()
        {
            Matrix matrix = Matrix.GetOnesMatrix(2);
            Matrix matrix2 = matrix.Subtract(2.34);
            if (matrix[0, 0] == 1 && matrix[0, 1] == 1
                && matrix[1, 0] == 1 && matrix[1, 1] == 1)
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
            if (matrix[0, 0] == 1 && matrix[0, 1] == 1
                && matrix[1, 0] == 1 && matrix[1, 1] == 1)
                Assert.Pass();
        }
        [Test]
        public void GetDeterminant()
        {
            Matrix matrix = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            if (matrix[0, 0] == 1 && matrix[0, 1] == 2
                && matrix[1, 0] == 3 && matrix[1, 1] == 4)
                Assert.Pass();
        }
        [Test]
        public void GetMinorMatrix()
        {
            Matrix matrix = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            Matrix minor00 = matrix.GetMinorMatrix(0, 0);
            if (matrix[0, 0] == 1 && matrix[0, 1] == 2
                && matrix[1, 0] == 3 && matrix[1, 1] == 4)
                Assert.Pass();
        }
        [Test]
        public void GetEuclideanNorm()
        {
            Matrix matrix = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            matrix.GetEuclideanNorm();
            if (matrix[0, 0] == 1 && matrix[0, 1] == 2
                && matrix[1, 0] == 3 && matrix[1, 1] == 4)
                Assert.Pass();
        }
        [Test]
        public void MultiplyMatrixAndValue()
        {
            Matrix matrix = new Matrix(dimension: 2, diagonalValue: 2);
            Matrix matrix2 = matrix.Multiply(2.5);
            if (matrix[0, 0] == 2 && matrix[0, 1] == 0 &&
                matrix[1, 0] == 0 && matrix[1, 1] == 2)
                Assert.Pass();
        }
        [Test]
        public void DivideyMatrixAndValue()
        {
            Matrix matrix = new Matrix(dimension: 2, diagonalValue: 5);
            Matrix matrix2 = matrix.Divide(2.5);
            if (matrix[0, 0] == 5 && matrix[0, 1] == 0 &&
                matrix[1, 0] == 0 && matrix[1, 1] == 5)
                Assert.Pass();
        }
    }
}
