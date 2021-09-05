using System;

namespace D1den.Calculation
{
    /// <summary>
    /// Matrix of any size
    /// </summary>
    /// <remarks>
    /// Matrix of any size, supporting various matrix operations.
    /// </remarks>
    [Serializable]
    public class Matrix : IEquatable<Matrix>, ICloneable
    {
        #region Fields and properties
        private readonly double[,] _matrixData;

        /// <summary>
        /// Array of matrix values
        /// </summary>
        public double[,] MatrixData => (double[,])_matrixData.Clone();

        /// <summary>
        /// An array of matrix values converted to Int32
        /// </summary>
        /// <remarks>Used <see cref="System.Math.Round(double)"/> to convert</remarks>
        public int[,] Int32MatrixData
        {
            get
            {
                int[,] matrixArray = new int[RowCount, ColumnCount];
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        matrixArray[i, j] = (int)Math.Round(_matrixData[i, j]);
                    }
                }
                return matrixArray;
            }
        }

        /// <summary>
        /// Matrix row count
        /// </summary>
        public int RowCount => _matrixData.GetLength(0);

        /// <summary>
        /// Matrix column count
        /// </summary>
        public int ColumnCount => _matrixData.GetLength(1);

        /// <summary>
        /// Get matrix value by row and column
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <exception cref="IndexOutOfRangeException">If index or indexes wrong</exception>
        /// <returns>Matrix value</returns>
        public double this[int row, int column] => _matrixData[row, column];
        #endregion

        #region Constructors and create methods
        /// <summary>
        /// Create matrix by size
        /// </summary>
        /// <param name="rowCount">Matrix row count</param>
        /// <param name="columnCount">Matrix column count</param>
        /// <exception cref="ArgumentOutOfRangeException">If matrix dimensions not be greater than 0</exception>
        public Matrix(int rowCount, int columnCount)
        {
            if(rowCount <= 0 || columnCount <= 0)
            {
                var ex = new ArgumentOutOfRangeException("Matrix dimensions must be greater than 0");
                ex.Data.Add("Matrix dimension", string.Format("Matrix: ({0},{1})",
                    rowCount, columnCount));
                throw ex;
            }
            _matrixData = new double[rowCount, columnCount];
        }

        /// <summary>
        /// Create a matrix from the values of a two-dimensional array
        /// </summary>
        /// <param name="matrixData">Array of matrix values</param>
        public Matrix(double[,] matrixData)
        {
            _matrixData = (double[,])matrixData.Clone();
        }

        /// <summary>
        /// Create a matrix from the values of a two-dimensional Int32 array
        /// </summary>
        /// <param name="matrixData">Int32 array of matrix values</param>
        public Matrix(int[,] matrixData)
        {
            _matrixData = new double[matrixData.GetLength(0), matrixData.GetLength(1)];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    _matrixData[i, j] = matrixData[i, j];
                }
            }
        }

        /// <summary>
        /// Create a zeros matrix of any size
        /// </summary>
        /// <param name="dimension">Matrix dimension. Must be greater then 0</param>
        /// <returns>Zeros matrix</returns>
        /// <exception cref="ArgumentOutOfRangeException">If matrix dimensions not be greater than 0</exception>
        public static Matrix Zeros(int dimension)
        {
            return new Matrix(dimension, dimension);
        }

        /// <summary>
        /// Create a identity matrix (E)
        /// </summary>
        /// <param name="dimension">Matrix dimension. Must be greater then 0</param>
        /// <returns>Identity matrix (E)</returns>
        /// <exception cref="ArgumentOutOfRangeException">If matrix dimensions not be greater than 0</exception>
        public static Matrix Eye(int dimension)
        {
            double[,] _matrixData = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                _matrixData[i, i] = 1.0;
            }
            return new Matrix(_matrixData);
        }

        /// <summary>
        /// Create an ones matrix
        /// </summary>
        /// <param name="dimension">Matrix dimension. Must be greater then 0</param>
        /// <returns>Matrix of 1.0</returns>
        /// <exception cref="ArgumentOutOfRangeException">If matrix dimensions not be greater than 0</exception>
        public static Matrix Ones(int dimension)
        {
            return Zeros(dimension).SetAllValues(1.0);
        }
        #endregion

        #region Matrix operations methods
        /// <summary>
        /// Negative matrix
        /// </summary>
        /// <returns>Result matrix</returns>
        public Matrix Negative()
        {
            var matrixArray = MatrixData;
            ProcessActionOverData((i, j) => matrixArray[i, j] = -matrixArray[i, j]);
            return new Matrix(matrixArray);
        }

        /// <summary>
        /// Matrix transpose
        /// </summary>
        /// <returns>Result matrix</returns>
        public Matrix Transpose()
        {
            var transposeMatrix = new double[ColumnCount, RowCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) => transposeMatrix[j, i] = thisMatrix[i, j]);
            return new Matrix(transposeMatrix);
        }

        /// <summary>
        /// Adding a matrix and a number
        /// </summary>
        /// <param name="value">Number</param>
        /// <returns>Result matrix</returns>
        public Matrix Add(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] + value);
            return new Matrix(result);
        }

        /// <summary>
        /// Adding two matrices
        /// </summary>
        /// <param name="matrix2">Second matrix</param>
        /// <exception cref="ArgumentException">If the dimensions of the matrices are not the same.</exception>
        /// <returns>Result matrix</returns>
        public Matrix Add(Matrix matrix2)
        {
            ValidateMatricesSize(matrix2);
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] + matrix2[i,j]);
            return new Matrix(result);
        }

        /// <summary>
        /// Subtracting a number from a matrix
        /// </summary>
        /// <param name="value">Number</param>
        /// <returns>Result matrix</returns>
        public Matrix Subtract(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] - value);
            return new Matrix(result);
        }

        /// <summary>
        /// Subtracting two matrices
        /// </summary>
        /// <param name="matrix2">Second matrix</param>
        /// <exception cref="ArgumentException">If the dimensions of the matrices are not the same.</exception>
        /// <returns>Result matrix</returns>
        public Matrix Subtract(Matrix matrix2)
        {
            ValidateMatricesSize(matrix2);
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] - matrix2[i, j]);
            return new Matrix(result);
        }

        /// <summary>
        /// Multiplying a matrix by a number
        /// </summary>
        /// <param name="value">Number</param>
        /// <returns>Result matrix</returns>
        public Matrix Multiply(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] * value);
            return new Matrix(result);
        }

        /// <summary>
        /// Dividing a matrix by a number
        /// </summary>
        /// <param name="value">Number</param>
        /// <exception cref="DivideByZeroException">If number is zero</exception>
        /// <returns>Result matrix</returns>
        public Matrix Divide(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] / value);
            return new Matrix(result);
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="matrix2">Second matrix</param>
        /// <exception cref="ArgumentException">If matrix1.ColumnCount != matrix2.RowCount</exception>
        /// <returns>Result matrix</returns>
        public Matrix Multiply(Matrix matrix2)
        {
            ValidateMatricesMultiply(matrix2);
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) => {
                for (var k = 0; k < thisMatrix.GetLength(1); k++)
                {
                    result[i, j] += thisMatrix[i, k] * matrix2[k, j];
                }
            });
            return new Matrix(result);
        }

        /// <summary>
        /// Get determinant of matrix
        /// </summary>
        /// <exception cref="ArithmeticException">If matrix isn`t square</exception>
        /// <returns>Determinant</returns>
        public double GetDeterminant()
        {
            ValidateSquareMatrix();
            if (_matrixData.Length == 1)
                return _matrixData[0, 0];
            else
            {
                double determinant = 0;
                for (int i = 0; i < RowCount; i++)
                {
                    determinant += Math.Pow(-1.0, i + 0) * _matrixData[i, 0] * GetMinorMatrix(i, 0).GetDeterminant();
                }
                return determinant;
            }
        }

        /// <summary>
        /// Get minor matrix
        /// </summary>
        /// <param name="rowIndex">Minor row index</param>
        /// <param name="columnIndex">Minor column index</param>
        /// <exception cref="ArithmeticException">If matrix isn`t square or matrix.Length == 1</exception>
        /// <returns>Матрица минора</returns>
        public Matrix GetMinorMatrix(int rowIndex, int columnIndex)
        {
            if ((RowCount != ColumnCount) || _matrixData.Length == 1)
            {
                var ex = new ArithmeticException("This minor does not exist");
                ex.Data.Add("Matrix size", string.Format("Matrix: ({0},{1})",
                    RowCount, ColumnCount));
                throw ex;
            }
            double[,] newMatrix = new double[RowCount - 1, ColumnCount - 1];
            for (int i = 0, iNew = 0; i < RowCount; i++)
            {
                if (i == rowIndex)
                    continue;
                for (int j = 0, jNew = 0; j < ColumnCount; j++)
                {
                    if (j == columnIndex)
                        continue;
                    newMatrix[iNew, jNew++] = _matrixData[i, j];
                }
                iNew++;
            }
            return new Matrix(newMatrix);
        }

        /// <summary>
        /// Get invert matrix
        /// </summary>
        /// <exception cref="ArithmeticException">If matrix isn`t square</exception>
        /// <returns>Result matrix</returns>
        public Matrix Invert()
        {
            ValidateSquareMatrix();
            var algebraiсСomplements = new double[RowCount, ColumnCount];
            if (_matrixData.Length == 1)
                return Ones(1) / GetDeterminant();
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    algebraiсСomplements[i, j] = Math.Pow(-1.0, i + j) * GetMinorMatrix(i, j).GetDeterminant();
                }
            }
            return new Matrix(algebraiсСomplements).Transpose().Divide(GetDeterminant());
        }

        /// <summary>
        /// Get euclidean norm of matrix
        /// </summary>
        /// <returns>Euclidean norm</returns>
        public double GetEuclideanNorm()
        {
            double norm = 0.0;
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    norm += Math.Pow(_matrixData[i, j], 2.0);
                }
            }
            norm = Math.Sqrt(norm);
            return norm;
        }
        #endregion

        #region Methods
        private void ValidateSquareMatrix()
        {
            if (RowCount != ColumnCount)
            {
                var ex = new ArithmeticException("Matrix isn`t square");
                ex.Data.Add("Matrix size", string.Format("Matrix: ({0},{1})",
                    RowCount, ColumnCount));
                throw ex;
            }
        }
        private void ValidateMatricesMultiply(Matrix other)
        {
            if (ColumnCount != other.RowCount)
            {
                var ex = new ArgumentException("Matrixes can not be multiplied, because dimensions aren`t same.");
                ex.Data.Add("Matrixes dimensions", string.Format("Matrix1: ({0},{1}), Matrix2: ({2},{3})",
                    RowCount, ColumnCount, other.RowCount, other.ColumnCount));
                throw ex;
            }
        }
        private void ValidateMatricesSize(Matrix other)
        {
            if (RowCount != other.RowCount &&
                ColumnCount != other.ColumnCount)
            {
                var ex = new ArgumentException("Matrices size aren`t same.");
                ex.Data.Add("Matrices dimensions", string.Format("Matrix1: ({0},{1}), Matrix2: ({2},{3})",
                    RowCount, ColumnCount, other.RowCount, other.ColumnCount));
                throw ex;
            }
        }

        /// <summary>
        /// Get matrix as string
        /// </summary>
        /// <returns>Matrix as string</returns>
        public override string ToString()
        {
            string matrixString = "";
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    matrixString += string.Format("{0:F2}\t", _matrixData[i, j]);
                }
                matrixString += "\n";
            }
            return matrixString;
        }

        /// <summary>
        /// Set all matrix values
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Result matrix</returns>
        public Matrix SetAllValues(double value)
        {
            var matrixArray = MatrixData;
            ProcessActionOverData((i, j) => matrixArray[i, j] = value);
            return new Matrix(matrixArray);
        }

        /// <summary>
        /// Apply action to all elements of the matrix
        /// </summary>
        /// <param name="action">Action for all elements</param>
        private void ProcessActionOverData(Action<int, int> action)
        {
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    action(i, j);
                }
            }
        }

        /// <summary>
        /// Checking for equality of matrix with accuracy 1.0E-6
        /// </summary>
        /// <param name="obj">Matrix like <see cref="object"/></param>
        /// <returns>Result of checking</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else if (obj is Matrix matrix2)
                return Equals(matrix2);
            else
                return false;
        }

        /// <summary>
        /// Checking for equality of matrix with accuracy 1.0E-6
        /// </summary>
        /// <param name="other">Matrix</param>
        /// <returns>Result of checking</returns>
        public bool Equals(Matrix other)
        {
            return Equals(other, 1.0E-6);
        }

        /// <summary>
        /// Checking with some accuracy the equality of matrix
        /// </summary>
        /// <param name="other">Matrix</param>
        /// <param name="delta">Accuracy</param>
        /// <returns>Result of checking</returns>
        public bool Equals(Matrix other, double delta)
        {
            if (RowCount != other.RowCount || ColumnCount != other.ColumnCount)
                return false;
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (!MathA.CompareAlmostEqual(this[i, j], other[i, j], delta))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Hash code generation algorithm
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            int countElements = _matrixData.Length;
            double result = 0;
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    result += _matrixData[i, j] * Math.Pow(10.0, countElements--);
                }
            }
            result /= Math.Pow(10.0, _matrixData.Length);
            if (result > int.MaxValue)
                result /= 1.0E+9;
            else if(result > -1.0 && result < 1.0)
                result *= 1.0E+9;
            return (int)Math.Round(result);
        }

        /// <summary>
        /// Duplicate Matrix
        /// </summary>
        /// <returns>Matrix clone like <see cref="object"/></returns>
        public object Clone()
        {
            return new Matrix(_matrixData);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Convert 2D array to matrix
        /// </summary>
        /// <param name="matrixArray">2D array</param>
        public static implicit operator Matrix(double[,] matrixArray)
        {
            return new Matrix(matrixArray);
        }
        /// <summary>
        /// Convert 2D array to matrix
        /// </summary>
        /// <param name="matrixArray">2D array</param>
        public static implicit operator Matrix(int[,] matrixArray)
        {
            return new Matrix(matrixArray);
        }
        /// <summary>
        /// Convert 1D array to matrix-row
        /// </summary>
        /// <param name="matrixArray">1D array</param>
        public static implicit operator Matrix(double[] matrixArray)
        {
            var doubleMatrixArray = new double[1, matrixArray.Length];
            for (int i = 0; i < matrixArray.Length; i++)
            {
                doubleMatrixArray[0, i] = matrixArray[i];
            }
            return new Matrix(doubleMatrixArray);
        }
        /// <summary>
        /// Convert 1D array to matrix-row
        /// </summary>
        /// <param name="matrixArray">1D array</param>
        public static implicit operator Matrix(int[] matrixArray)
        {
            var intMatrixArray = new double[1, matrixArray.Length];
            for (int i = 0; i < matrixArray.Length; i++)
            {
                intMatrixArray[0, i] = matrixArray[i];
            }
            return new Matrix(intMatrixArray);
        }
        /// <summary>
        /// Convert matrix to 2D array
        /// </summary>
        /// <param name="matrix">Matrix</param>
        public static explicit operator double[,](Matrix matrix)
        {
            return matrix.MatrixData;
        }
        /// <summary>
        /// Convert matrix to 2D array
        /// </summary>
        /// <param name="matrix">Matrix</param>
        public static explicit operator int[,](Matrix matrix)
        {
            return matrix.Int32MatrixData;
        }
        /// <summary>
        /// Adding a matrix and a number
        /// </summary>
        /// <param name="value">Number</param>
        /// <param name="matrix">Matrix</param>
        /// <returns>Result matrix</returns>
        public static Matrix operator +(double value, Matrix matrix)
        {
            return matrix.Add(value);
        }
        /// <summary>
        /// Adding a matrix and a number
        /// </summary>
        /// <param name="value">Number</param>
        /// <param name="matrix">Matrix</param>
        /// <returns>Result matrix</returns>
        public static Matrix operator +(Matrix matrix, double value)
        {
            return matrix.Add(value);
        }
        /// <summary>
        /// Adding two matrices
        /// </summary>
        /// <param name="matrix1">First matrix</param>
        /// <param name="matrix2">Second matrix</param>
        /// <exception cref="ArgumentException">If the dimensions of the matrices are not the same.</exception>
        /// <returns>Result matrix</returns>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Add(matrix2);
        }
        /// <summary>
        /// Subtracting a number from a matrix
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <param name="value">Number</param>
        /// <returns>Result matrix</returns>
        public static Matrix operator -(Matrix matrix, double value)
        {
            return matrix.Subtract(value);
        }
        /// <summary>
        /// Subtracting two matrices
        /// </summary>
        /// <param name="matrix1">First matrix</param>
        /// <param name="matrix2">Second matrix</param>
        /// <exception cref="ArgumentException">If the dimensions of the matrices are not the same.</exception>
        /// <returns>Result matrix</returns>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Subtract(matrix2);
        }
        /// <summary>
        /// Multiplying a matrix by a number
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <param name="value">Number</param>
        /// <returns>Result matrix</returns>
        public static Matrix operator *(Matrix matrix, double value)
        {
            return matrix.Multiply(value);
        }
        /// <summary>
        /// Multiplying a matrix by a number
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <param name="value">Number</param>
        /// <returns>Result matrix</returns>
        public static Matrix operator *(double value, Matrix matrix)
        {
            return matrix.Multiply(value);
        }
        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="matrix1">First matrix</param>
        /// <param name="matrix2">Second matrix</param>
        /// <exception cref="ArgumentException">If matrix1.ColumnCount != matrix2.RowCount</exception>
        /// <returns>Result matrix</returns>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Multiply(matrix2);
        }
        /// <summary>
        /// Dividing a matrix by a number
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <param name="value">Number</param>
        /// <exception cref="DivideByZeroException">If number is zero</exception>
        /// <returns>Result matrix</returns>
        public static Matrix operator /(Matrix matrix, double value)
        {
            return matrix.Divide(value);
        }
        /// <summary>
        /// Checking for equality of matrix
        /// </summary>
        /// <param name="matrix1">First matrix</param>
        /// <param name="matrix2">Second matrix</param>
        /// <returns>Result of checking</returns>
        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Equals(matrix2);
        }
        /// <summary>
        /// Checking for inequality of matrix
        /// </summary>
        /// <param name="matrix1">First matrix</param>
        /// <param name="matrix2">Second matrix</param>
        /// <returns>Result of checking</returns>
        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            return !matrix1.Equals(matrix2);
        }
        /// <summary>
        /// Checking for equality of matrix
        /// </summary>
        /// <param name="matrix1">First matrix</param>
        /// <param name="matrix2">Second matrix</param>
        /// <returns>Result of checking</returns>
        public static bool operator ==(Matrix matrix1, object matrix2)
        {
            return matrix1.Equals(matrix2);
        }
        /// <summary>
        /// Checking for inequality of matrix
        /// </summary>
        /// <param name="matrix1">First matrix</param>
        /// <param name="matrix2">Second matrix</param>
        /// <returns>Result of checking</returns>
        public static bool operator !=(Matrix matrix1, object matrix2)
        {
            return !matrix1.Equals(matrix2);
        }
        /// <summary>
        /// Checking for equality of matrix
        /// </summary>
        /// <param name="matrix1">First matrix</param>
        /// <param name="matrix2">Second matrix</param>
        /// <returns>Result of checking</returns>
        public static bool operator ==(object matrix1, Matrix matrix2)
        {
            return matrix1.Equals(matrix2);
        }
        /// <summary>
        /// Checking for inequality of matrix
        /// </summary>
        /// <param name="matrix1">First matrix</param>
        /// <param name="matrix2">Second matrix</param>
        /// <returns>Result of checking</returns>
        public static bool operator !=(object matrix1, Matrix matrix2)
        {
            return !matrix1.Equals(matrix2);
        }
        #endregion
    }
}
