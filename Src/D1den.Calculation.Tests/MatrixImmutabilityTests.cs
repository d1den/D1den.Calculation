using NUnit.Framework;
using D1den.Calculation;

namespace Tests
{
    class MatrixImmutabilityTests
    {
        [Test]
        public void Negative()
        {
            var matrix = Matrix.Ones(2);
            Matrix matrix2 = matrix.Negative();
            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[0, 1], 1);
            Assert.AreEqual(matrix[1, 0], 1);
            Assert.AreEqual(matrix[1, 1], 1);
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
            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[0, 1], 2);
            Assert.AreEqual(matrix[0, 2], 3);
            Assert.AreEqual(matrix[1, 0], 4);
            Assert.AreEqual(matrix[1, 1], 5);
            Assert.AreEqual(matrix[1, 2], 6);
        }
        [Test]
        public void AddMatrixAndValue()
        {
            Matrix matrix = Matrix.Ones(2);
            Matrix matrix2 = matrix.Add(2.34);
            if (matrix[0, 0] == 1 && matrix[0, 1] == 1 &&
                matrix[1, 0] == 1 && matrix[1, 1] == 1)
                Assert.Pass();
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
            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[0, 1], 1);
            Assert.AreEqual(matrix[1, 0], 1);
            Assert.AreEqual(matrix[1, 1], 1);
        }
        [Test]
        public void SubtractMatrixAndValue()
        {
            Matrix matrix = Matrix.Ones(2);
            Matrix matrix2 = matrix.Subtract(2.34);
            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[0, 1], 1);
            Assert.AreEqual(matrix[1, 0], 1);
            Assert.AreEqual(matrix[1, 1], 1);
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
            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[0, 1], 1);
            Assert.AreEqual(matrix[1, 0], 1);
            Assert.AreEqual(matrix[1, 1], 1);
        }
        [Test]
        public void GetDeterminant()
        {
            Matrix matrix = new[,]
            {
                {1, 2 },
                {3, 4 }
            };
            matrix.GetDeterminant();
            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[0, 1], 2);
            Assert.AreEqual(matrix[1, 0], 3);
            Assert.AreEqual(matrix[1, 1], 4);
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
            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[0, 1], 2);
            Assert.AreEqual(matrix[1, 0], 3);
            Assert.AreEqual(matrix[1, 1], 4);
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
            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[0, 1], 2);
            Assert.AreEqual(matrix[1, 0], 3);
            Assert.AreEqual(matrix[1, 1], 4);
        }
        [Test]
        public void MultiplyMatrixAndValue()
        {
            Matrix matrix = Matrix.Eye(2);
            Matrix matrix2 = matrix.Multiply(2.5);
            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[0, 1], 0);
            Assert.AreEqual(matrix[1, 0], 0);
            Assert.AreEqual(matrix[1, 1], 1);
        }
        [Test]
        public void DivideyMatrixAndValue()
        {
            Matrix matrix = Matrix.Eye(2);
            Matrix matrix2 = matrix.Divide(2.5);
            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[0, 1], 0);
            Assert.AreEqual(matrix[1, 0], 0);
            Assert.AreEqual(matrix[1, 1], 1);
        }
        [Test]
        public void MultiplyMatriсes()
        {
            Matrix matrix = new[,]
            {
                {1,2 },
                {3,4 }
            };
            Matrix matrix2 = Matrix.Eye(2);
            Matrix matrix3 = matrix.Multiply(matrix2);

            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[0, 1], 2);
            Assert.AreEqual(matrix[1, 0], 3);
            Assert.AreEqual(matrix[1, 1], 4);
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

            Assert.AreEqual(matrix[0, 0], 2);
            Assert.AreEqual(matrix[0, 1], 2);
            Assert.AreEqual(matrix[1, 0], 7);
            Assert.AreEqual(matrix[1, 1], 6);
        }
        [Test]
        public void CreateMatrixAndChangeBaseArray()
        {
            var array1 = new int[,] { { 1, 2 }, { 3, 4 } };
            var matrix1 = new Matrix(array1);
            array1[0, 0] = -10;
            var array2 = new double[,] { { 1.1, 2.2 }, { 3.3, 4.4 } };
            var matrix2 = new Matrix(array2);
            array2[0, 0] = -10;
            Assert.AreNotEqual(matrix1[0, 0], -10);
            Assert.AreNotEqual(matrix2[0, 0], -10);
        }
        [Test]
        public void ChangeMatrixData()
        {
            var array1 = new int[,] { { 1, 2 }, { 3, 4 } };
            var matrix1 = new Matrix(array1);
            double[,] matrixDataDouble = matrix1.MatrixData;
            matrixDataDouble[0, 0] = -10;
            int[,] matrixDataInt = matrix1.Int32MatrixData;
            matrixDataInt[0, 0] = -10;
            Assert.AreNotEqual(matrix1[0, 0], -10);
        }
        [Test]
        public void SetAllValues()
        {
            var array1 = new int[,] { { 1, 2 }, { 3, 4 } };
            var matrix1 = new Matrix(array1);
            Matrix matrix2 = matrix1.SetAllValues(-10);
            Assert.AreNotEqual(matrix1[0, 0], -10);
        }
        [Test]
        public void CopyAndCloneChange()
        {
            var array1 = new int[,] { { 1, 2 }, { 3, 4 } };
            var matrix1 = new Matrix(array1);
            Matrix matrix2 = matrix1;
            matrix2.MatrixData[0, 0] = -10;
            Matrix matrix3 = matrix1.Clone() as Matrix;
            matrix3.MatrixData[0, 0] = -10;
            Assert.AreNotEqual(matrix1[0, 0], -10);
        }
    }
}
