using System;
using System.Collections.Generic;
using System.Text;

namespace d1den.MathLibrary
{
    public readonly struct Matrix
    {
        #region Поля и свойства
        private readonly double[,] _matrixData;
        public double[,] MatrixData { get { return _matrixData; } }
        public int RowCount { get { return _matrixData.GetLength(0); } }
        public int ColumnCount { get { return _matrixData.GetLength(1); } }
        public double this[int row, int column]
        { get { return _matrixData[row, column]; } }
        #endregion

        #region Конструкторы и методы создания объектов
        public Matrix(int rowCount, int columnCount)
        {
            _matrixData = new double[rowCount, columnCount];
        }
        public Matrix(double[,] matrixData)
        {
            _matrixData = matrixData;
        }
        public Matrix(int dimension, double diagonalValue)
        {
            _matrixData = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                _matrixData[i, i] = diagonalValue;
            }
        }
        public static Matrix GetZerosMatrix(int dimension)
        {
            return new Matrix(dimension, dimension);
        }
        public static Matrix GetUnitMatrix(int dimension)
        {
            return new Matrix(dimension, 1.0);
        }
        public static Matrix GetOnesMatrix(int dimension)
        {
            return new Matrix(dimension, dimension).SetAllValues(1.0);
        }
        #endregion

        #region Методы матричных операций
        public Matrix Negative()
        {
            var matrixArray = _matrixData;
            ProcessFunctionOverData((i, j) => matrixArray[i, j] = -matrixArray[i, j]);
            return new Matrix(matrixArray);
        }
        public Matrix Transpose()
        {
            var transposeMatrix = new double[ColumnCount, RowCount];
            var thisMatrix = _matrixData;
            ProcessFunctionOverData((i, j) => transposeMatrix[i, j] = thisMatrix[j, i]);
            return new Matrix(transposeMatrix);
        }
        /// <summary>
        /// Сложение матрицы и числа
        /// </summary>
        public Matrix Add(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessFunctionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] + value);
            return new Matrix(result);
        }

        /// <summary>
        /// Сложение двух матриц
        /// </summary>
        public Matrix Add(Matrix matrix2)
        {
            if (this.ColumnCount != matrix2.ColumnCount && this.RowCount != matrix2.RowCount)
            {
                var ex = new ArgumentException("Matrixes can not be add, because dimensions aren`t same.");
                ex.Data.Add("Matrixes dimensions", string.Format("Matrix1: ({0},{1}), Matrix2: ({2},{3})", this.RowCount, this.ColumnCount, matrix2.RowCount, matrix2.ColumnCount);
                throw ex;
            }
            var result = new Matrix(this.RowCount, this.ColumnCount);
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    result[i, j] = this[i, j] + matrix2[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Вычитание числа из матрицы
        /// </summary>
        public Matrix Subtract(double value)
        {
            var result = new Matrix(this.RowCount, this.ColumnCount);
            result.ProcessFunctionOverData((i, j) =>
                result[i, j] = this[i, j] - value);
            return result;
        }

        /// <summary>
        /// Разность матриц
        /// </summary>
        public Matrix Subtract(Matrix matrix2)
        {
            if (this.ColumnCount != matrix2.ColumnCount && this.RowCount != matrix2.RowCount)
            {
                throw new ArgumentException("Matrixes can not be subtract, because dimension aren`t same.");
            }
            var result = new Matrix(this.RowCount, this.ColumnCount);
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    result[i, j] = this[i, j] - matrix2[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Произведение числа и матрицы
        /// </summary>
        public Matrix Multiply(double value)
        {
            var result = new Matrix(this.RowCount, this.ColumnCount);
            result.ProcessFunctionOverData((i, j) =>
                result[i, j] = this[i, j] * value);
            return result;
        }

        /// <summary>
        /// Деление матрицы на число
        /// </summary>
        public Matrix Divide(double value)
        {
            var result = new Matrix(this.RowCount, this.ColumnCount);
            result.ProcessFunctionOverData((i, j) =>
                result[i, j] = this[i, j] / value);
            return result;
        }

        /// <summary>
        /// Произведение матриц
        /// </summary>
        public Matrix Multiply(Matrix matrix2)
        {
            if (this.ColumnCount != matrix2.RowCount)
            {
                throw new ArgumentException("Matrixes can not be multiplied");
            }
            var result = new Matrix(this.RowCount, matrix2.ColumnCount);
            result.ProcessFunctionOverData((i, j) => {
                for (var k = 0; k < this.ColumnCount; k++)
                {
                    result[i, j] += this[i, k] * matrix2[k, j];
                }
            });
            return result;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Вычисление евклидовой нормы
        /// </summary>
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
        public override string ToString()
        {
            string matrixInString = "";
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    matrixInString += string.Format("{0:F2}\t", _matrixData[i, j]);
                }
                matrixInString += "\n";
            }
            return matrixInString;
        }
        /// <summary>
        /// Установить все значения матрицы
        /// </summary>
        public Matrix SetAllValues(double value)
        {
            var matrixArray = _matrixData;
            ProcessFunctionOverData((i, j) => matrixArray[i, j] = value);
            return new Matrix(matrixArray);
        }

        /// <summary>
        /// Применить действие ко всем элементам матрицы
        /// </summary>
        /// <param name="function"></param>
        private void ProcessFunctionOverData(Action<int, int> function)
        {
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    function(i, j);
                }
            }
        }
        #endregion
    }
}
