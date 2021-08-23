using System;
using System.Collections.Generic;
using System.Text;

namespace d1den.MathLibrary
{
    /// <summary>
    /// Матрица желаемой размерности
    /// </summary>
    public readonly struct Matrix
    {
        #region Поля и свойства
        private readonly double[,] _matrixData;
        /// <summary>
        /// Массив значений матрицы
        /// </summary>
        public double[,] MatrixData { get { return _matrixData; } }
        /// <summary>
        /// Количество строк
        /// </summary>
        public int RowCount { get { return _matrixData.GetLength(0); } }
        /// <summary>
        /// Количество столбцов
        /// </summary>
        public int ColumnCount { get { return _matrixData.GetLength(1); } }
        /// <summary>
        /// Получить значение матрицы по строке и столбцу
        /// </summary>
        /// <param name="row">Индекс строки</param>
        /// <param name="column">Индекс столбца</param>
        /// <returns>Значение матрицы</returns>
        public double this[int row, int column]
        { get { return _matrixData[row, column]; } }
        #endregion

        #region Конструкторы и методы создания объектов
        /// <summary>
        /// Объект матрицы
        /// </summary>
        /// <param name="rowCount">Число строк</param>
        /// <param name="columnCount">Число столбцов</param>
        public Matrix(int rowCount, int columnCount)
        {
            _matrixData = new double[rowCount, columnCount];
        }

        /// <summary>
        /// Объект матрицы
        /// </summary>
        /// <param name="matrixData">Двумерный массив данных матрицы</param>
        public Matrix(double[,] matrixData)
        {
            _matrixData = matrixData;
        }

        /// <summary>
        /// Объект квадратной матрицы
        /// </summary>
        /// <param name="dimension">Размерность квадратной матрицы</param>
        /// <param name="diagonalValue">Значение по главной диагонали</param>
        public Matrix(int dimension, double diagonalValue)
        {
            _matrixData = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                _matrixData[i, i] = diagonalValue;
            }
        }

        /// <summary>
        /// Квадратная матрица нулей
        /// </summary>
        /// <param name="dimension">Размерность</param>
        /// <returns>Объект матрицы</returns>
        public static Matrix GetZerosMatrix(int dimension)
        {
            return new Matrix(dimension, dimension);
        }

        /// <summary>
        /// Единичная матрица (E)
        /// </summary>
        /// <param name="dimension">Размерность</param>
        /// <returns>Объект матрицы</returns>
        public static Matrix GetUnitMatrix(int dimension)
        {
            return new Matrix(dimension, 1.0);
        }

        /// <summary>
        /// Квадратная матрица единиц
        /// </summary>
        /// <param name="dimension">Размерность</param>
        /// <returns>Объект матрицы</returns>
        public static Matrix GetOnesMatrix(int dimension)
        {
            return new Matrix(dimension, dimension).SetAllValues(1.0);
        }
        #endregion

        #region Методы матричных операций
        /// <summary>
        /// Домножение матрицы на единицу
        /// </summary>
        public Matrix Negative()
        {
            var matrixArray = _matrixData;
            ProcessFunctionOverData((i, j) => matrixArray[i, j] = -matrixArray[i, j]);
            return new Matrix(matrixArray);
        }

        /// <summary>
        /// Транспонирование матрицы
        /// </summary>
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
            if (RowCount != matrix2.RowCount &&
                ColumnCount != matrix2.ColumnCount)
            {
                var ex = new ArgumentException("Matrices can not be add, because dimensions aren`t same.");
                ex.Data.Add("Matrices dimensions", string.Format("Matrix1: ({0},{1}), Matrix2: ({2},{3})",
                    this.RowCount, this.ColumnCount, matrix2.RowCount, matrix2.ColumnCount));
                throw ex;
            }
            var result = new double[this.RowCount, this.ColumnCount];
            var thisMatrix = _matrixData;
            ProcessFunctionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] + matrix2[i,j]);
            return new Matrix(result);
        }

        /// <summary>
        /// Разность матрицы и числа
        /// </summary>
        public Matrix Subtract(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessFunctionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] - value);
            return new Matrix(result);
        }

        /// <summary>
        /// Разность матриц
        /// </summary>
        public Matrix Subtract(Matrix matrix2)
        {
            if (RowCount != matrix2.RowCount &&
                ColumnCount != matrix2.ColumnCount)
            {
                var ex = new ArgumentException("Matrices cannot be multiplied because the dimensions do not fit.");
                ex.Data.Add("Matrices dimensions", string.Format("Matrix1: ({0},{1}), Matrix2: ({2},{3})",
                    this.RowCount, this.ColumnCount, matrix2.RowCount, matrix2.ColumnCount));
                throw ex;
            }
            var result = new double[this.RowCount, this.ColumnCount];
            var thisMatrix = _matrixData;
            ProcessFunctionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] - matrix2[i, j]);
            return new Matrix(result);
        }

        /// <summary>
        /// Произведение матрицы и числаы
        /// </summary>
        public Matrix Multiply(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessFunctionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] * value);
            return new Matrix(result);
        }

        /// <summary>
        /// Деление матрицы на число
        /// </summary>
        public Matrix Divide(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessFunctionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] / value);
            return new Matrix(result);
        }

        /// <summary>
        /// Произведение матриц
        /// </summary>
        public Matrix Multiply(Matrix matrix2)
        {
            if (this.ColumnCount != matrix2.RowCount)
            {
                var ex = new ArgumentException("Matrixes can not be multiplied, because dimensions aren`t same.");
                ex.Data.Add("Matrixes dimensions", string.Format("Matrix1: ({0},{1}), Matrix2: ({2},{3})",
                    this.RowCount, this.ColumnCount, matrix2.RowCount, matrix2.ColumnCount));
                throw ex;
            }
            var result = new double[this.RowCount, this.ColumnCount];
            var thisMatrix = _matrixData;
            ProcessFunctionOverData((i, j) => {
                for (var k = 0; k < thisMatrix.GetLength(1); k++)
                {
                    result[i, j] += thisMatrix[i, k] * matrix2[k, j];
                }
            });
            return new Matrix(result);
        }

        /// <summary>
        /// Получить определитель матрицы
        /// </summary>
        /// <returns>Определитель</returns>
        public double GetDeterminant()
        {
            if (RowCount != ColumnCount)
            {
                var ex = new ArgumentException("Matrix isn`t square");
                ex.Data.Add("Matrixe size", string.Format("Matrix: ({0},{1})",
                    this.RowCount, this.ColumnCount));
                throw ex;
            }
            else if (_matrixData.Length == 1)
            {
                return _matrixData[0, 0];
            }
            else
            {
                double determinant = 0;
                for (int i = 0; i < RowCount; i++)
                {
                    determinant += Math.Pow(-1.0, i + 0) * _matrixData[i, 0] * this.GetMinorMatrix(i, 0).GetDeterminant();
                }
                return determinant;
            }
        }

        /// <summary>
        /// Вычисление матрицы минора
        /// </summary>
        /// <param name="rowIndex">Индекс строки минора</param>
        /// <param name="columnIndex">Индекс столбца минора</param>
        /// <returns>Матрица минора</returns>
        public Matrix GetMinorMatrix(int rowIndex, int columnIndex)
        {
            if ((RowCount != ColumnCount) || _matrixData.Length == 1)
            {
                var ex = new ArgumentException("This minor does not exist");
                ex.Data.Add("Matrixe size", string.Format("Matrix: ({0},{1})",
                    this.RowCount, this.ColumnCount));
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
        /// Вычисление обратной матрицы
        /// </summary>
        /// <returns>Обратная матрица</returns>
        public Matrix Invert()
        {
            if (RowCount != ColumnCount)
            {
                var ex = new ArgumentException("Matrix isn`t square");
                ex.Data.Add("Matrixe size", string.Format("Matrix: ({0},{1})",
                    this.RowCount, this.ColumnCount));
                throw ex;
            }
            var algebraiсСomplements = new double[RowCount, ColumnCount];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    algebraiсСomplements[i, j] = Math.Pow(-1.0, i + j) * this.GetMinorMatrix(i, j).GetDeterminant();
                }
            }
            return new Matrix(algebraiсСomplements).Transpose().Divide(this.GetDeterminant());
        }

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
        #endregion

        #region Методы
        /// <summary>
        /// Преобразование матрицы к строке
        /// </summary>
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
