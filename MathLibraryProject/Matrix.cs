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
        /// Массив приведённых к Int32 значений матрицы
        /// </summary>
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
        /// <param name="matrixData">Двумерный double массив данных матрицы</param>
        public Matrix(double[,] matrixData)
        {
            _matrixData = matrixData;
        }

        /// <summary>
        /// Объект матрицы
        /// </summary>
        /// <param name="matrixData">Двумерный int массив данных матрицы</param>
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
        public static Matrix GetEyeMatrix(int dimension)
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
        /// <returns>Матрица</returns>
        public Matrix Negative()
        {
            var matrixArray = _matrixData;
            ProcessActionOverData((i, j) => matrixArray[i, j] = -matrixArray[i, j]);
            return new Matrix(matrixArray);
        }

        /// <summary>
        /// Транспонирование матрицы
        /// </summary>
        /// <returns>Матрица</returns>
        public Matrix Transpose()
        {
            var transposeMatrix = new double[ColumnCount, RowCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) => transposeMatrix[i, j] = thisMatrix[j, i]);
            return new Matrix(transposeMatrix);
        }

        /// <summary>
        /// Сложение матрицы и числа
        /// </summary>
        /// <param name="value">Число, складываемое с матрицей</param>
        /// <returns>Матрица</returns>
        public Matrix Add(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] + value);
            return new Matrix(result);
        }

        /// <summary>
        /// Сложение двух матриц
        /// </summary>
        /// <param name="matrix2">Матрица, складываемая с матрицей</param>
        /// <exception cref="ArgumentException">При несоответсвии размерностей будет вызвано исключение</exception>
        /// <returns>Матрица</returns>
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
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] + matrix2[i,j]);
            return new Matrix(result);
        }

        /// <summary>
        /// Разность матрицы и числа
        /// </summary>
        /// <param name="value">Число, вычитаемое из матрицы</param>
        /// <returns>Матрица</returns>
        public Matrix Subtract(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] - value);
            return new Matrix(result);
        }

        /// <summary>
        /// Разность матриц
        /// </summary>
        /// <param name="matrix2">Матрица, вычитаемая из матрицы</param>
        /// <exception cref="ArgumentException">При несоответсвии размерностей будет вызвано исключение</exception>
        /// <returns>Матрица</returns>
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
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] - matrix2[i, j]);
            return new Matrix(result);
        }

        /// <summary>
        /// Произведение матрицы и числа
        /// </summary>
        /// <param name="value">Множитель матрицы</param>
        /// <returns>Матрица</returns>
        public Matrix Multiply(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] * value);
            return new Matrix(result);
        }

        /// <summary>
        /// Деление матрицы на число
        /// </summary>
        /// <param name="value">Делитель матрицы</param>
        /// <exception cref="DivideByZeroException">При делителе = 0</exception>
        /// <returns>Матрица</returns>
        public Matrix Divide(double value)
        {
            var result = new double[RowCount, ColumnCount];
            var thisMatrix = _matrixData;
            ProcessActionOverData((i, j) =>
                result[i, j] = thisMatrix[i, j] / value);
            return new Matrix(result);
        }

        /// <summary>
        /// Произведение матриц
        /// </summary>
        /// <param name="matrix2">Матрица, умножаемая на исходную</param>
        /// <exception cref="ArgumentException">При неравнестве стобцов первой матрицы и строк второй</exception>
        /// <returns>Матрица</returns>
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
            ProcessActionOverData((i, j) => {
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
        /// <exception cref="ArgumentException">Если матрица не квадратная</exception>
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
        /// Получить матрицу минора данной матрицы
        /// </summary>
        /// <param name="rowIndex">Индекс строки минора</param>
        /// <param name="columnIndex">Индекс столбца минора</param>
        /// <exception cref="ArgumentException">Если матрица не квадратная, или состоит из 1-го элемента</exception>
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
        /// <exception cref="ArgumentException">Если матрица не квадратная</exception>
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
            //if (_matrixData.Length == 1)
            //    return this;
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
        /// Вычисление Eвклидовой нормы
        /// <returns>Евклидовая норма</returns>
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
        /// <returns>Строка матрицы</returns>
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
        /// <param name="value">Значение</param>
        /// <returns>Матрица</returns>
        public Matrix SetAllValues(double value)
        {
            var matrixArray = _matrixData;
            ProcessActionOverData((i, j) => matrixArray[i, j] = value);
            return new Matrix(matrixArray);
        }

        /// <summary>
        /// Применить действие ко всем элементам матрицы
        /// </summary>
        /// <param name="action"></param>
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
        #endregion

        #region Операторы
        /// <summary>
        /// Преобразовать двумерный массив в матрице
        /// </summary>
        /// <param name="matrixArray">Двумерный массив double</param>
        public static implicit operator Matrix(double[,] matrixArray)
        {
            return new Matrix(matrixArray);
        }

        /// <summary>
        /// Преобразовать двумерный массив в матрице
        /// </summary>
        /// <param name="matrixArray">Двумерный массив int</param>
        public static implicit operator Matrix(int[,] matrixArray)
        {
            return new Matrix(matrixArray);
        }

        /// <summary>
        /// Преобразовать матрицу к двумерному массиву double
        /// </summary>
        /// <param name="matrix">Матрица</param>
        public static explicit operator double[,](Matrix matrix)
        {
            return matrix.MatrixData;
        }

        /// <summary>
        /// Преобразовать матрицу к двумерному массиву int
        /// </summary>
        /// <param name="matrix">Матрица</param>
        public static explicit operator int[,](Matrix matrix)
        {
            return matrix.Int32MatrixData;
        }
        #endregion
    }
}
