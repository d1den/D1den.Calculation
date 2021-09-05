using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using D1den.Calculation;

namespace Tests
{
    class MatrixTests
    {
        [Test]
        public void CreateMatrixByConstructors()
        {
            var matrix1 = new Matrix(2, 2);
            var matrix3 = new Matrix(new int[,] { { 1, 2 }, { 3, 4 } });
            var matrix4 = new Matrix(new double[,] { { 1.1, 2.2 }, { 3.3, 4.4 } });
            Assert.AreEqual(matrix1.RowCount, matrix1.ColumnCount);
            Assert.AreEqual(matrix1.RowCount, 2);
            Assert.AreEqual(matrix1.RowCount, matrix4.ColumnCount);
        }
        [Test]
        public void CreateMatrixByMethods()
        {
            var matrix1 = Matrix.Eye(2);
            var matrix2 = Matrix.Zeros(1);
            var matrix3 = Matrix.Ones(2);
            Assert.AreEqual(matrix1[0, 0], 1.0);
            Assert.AreEqual(matrix1[1, 1], 1.0);
            Assert.AreEqual(matrix1[0, 1], 0.0);
            Assert.AreEqual(matrix1[1, 0], 0.0);

            Assert.AreEqual(matrix2[0, 0], 0);

            Assert.AreEqual(matrix3[0, 0], 1.0);
            Assert.AreEqual(matrix3[1, 1], 1.0);
            Assert.AreEqual(matrix3[0, 1], 1.0);
            Assert.AreEqual(matrix3[1, 0], 1.0);
        }
        [Test]
        public void GetMatrixDataTest()
        {
            var matrix1 = Matrix.Eye(2);
            double[,] array1 = (double[,])matrix1;
            int[,] array2 = matrix1.Int32MatrixData;
            array1[0, 0] = 2;
            array2[0, 0] = 2;
            Assert.AreEqual(matrix1[0, 0], 1);
        }
        [Test]
        public void HashCodeTest()
        {
            var matrix1 = Matrix.Eye(2);
            Matrix matrix2 = matrix1.Clone() as Matrix;
            var matrix3 = new[,]
            {
                {1234, -23214, 23.045 },
                {98.34, 2132, 0.92931 }
            };
            Assert.AreEqual(matrix1.GetHashCode(), matrix1.GetHashCode());
            Assert.AreEqual(matrix1.GetHashCode(), matrix2.GetHashCode());
            Assert.AreNotEqual(matrix1.GetHashCode(), matrix3.GetHashCode());
        }
        [Test]
        public void EqualsTest()
        {
            var matrix1 = Matrix.Eye(2);
            Matrix matrix2 = matrix1.Clone() as Matrix;
            var matrix3 = new[,]
            {
                {1234, -23214, 23.045 },
                {98.34, 2132, 0.92931 }
            };
            Assert.IsTrue(matrix1.Equals(matrix1));
            Assert.IsTrue(matrix1 == matrix2);
            Assert.IsTrue(!matrix1.Equals(matrix3));
            Assert.IsTrue(matrix1 != (matrix3 as object));
        }
        [Test]
        public void EqualsWithAccuracy()
        {
            var matrix1 = Matrix.Eye(2);
            Matrix matrix2 = new[,]
            {
                {1.002, 0.001 },
                {-0.003, 1.0034 }
            };
            Assert.IsTrue(matrix1.Equals(matrix2, 0.01));
        }
        [Test]
        public void CreateMatrixWithZeroColumn()
        {
            TestDelegate testCode = delegate ()
            {
                Matrix matrix = new Matrix(0, 123);
            };
            Assert.Throws<ArgumentOutOfRangeException>(testCode);
        }
    }
}
