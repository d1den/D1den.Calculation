using System;
using System.Collections.Generic;
using System.Text;

namespace d1den.MathLibrary
{
    /// <summary>
    /// Матрица желаемой размерности
    /// </summary>
    /// <remarks>
    /// Матрица произвольного размера, над которой можно производить различные математические операции. 
    /// НЕ использовать пустой конструктор.</remarks>
    [Serializable]
    public class Matrix : IEquatable<Matrix>, ICloneable
    {
        #region Поля и свойства
        private readonly double[,] _matrixData;

        /// <summary>
        /// Массив значений матрицы
        /// </summary>
        public double[,] MatrixData { get { return (double[,])_matrixData.Clone(); } }

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
            _matrixData = (double[,])matrixData.Clone();
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
        /// Объект квадратной матрицы со значением по диагонали
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
            var matrixArray = MatrixData;
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
            ProcessActionOverData((i, j) => transposeMatrix[j, i] = thisMatrix[i, j]);
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
                    RowCount, ColumnCount, matrix2.RowCount, matrix2.ColumnCount));
                throw ex;
            }
            var result = new double[RowCount, ColumnCount];
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
                    RowCount, ColumnCount, matrix2.RowCount, matrix2.ColumnCount));
                throw ex;
            }
            var result = new double[RowCount, ColumnCount];
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
                    RowCount, ColumnCount, matrix2.RowCount, matrix2.ColumnCount));
                throw ex;
            }
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
        /// Получить определитель матрицы
        /// </summary>
        /// <exception cref="ArgumentException">Если матрица не квадратная</exception>
        /// <returns>Определитель</returns>
        public double GetDeterminant()
        {
            if (RowCount != ColumnCount)
            {
                var ex = new ArgumentException("Matrix isn`t square");
                ex.Data.Add("Matrix size", string.Format("Matrix: ({0},{1})",
                    RowCount, ColumnCount));
                throw ex;
            }
            else if (_matrixData.Length == 1)
                return _matrixData[0, 0];
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
                ex.Data.Add("Matrix size", string.Format("Matrix: ({0},{1})",
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
                ex.Data.Add("Matrix size", string.Format("Matrix: ({0},{1})",
                    this.RowCount, this.ColumnCount));
                throw ex;
            }
            var algebraiсСomplements = new double[RowCount, ColumnCount];
            if (_matrixData.Length == 1)
                return GetOnesMatrix(1) / GetDeterminant();
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
        /// Установить все значения матрицы
        /// </summary>
        /// <param name="value">Значение</param>
        /// <returns>Матрица</returns>
        public Matrix SetAllValues(double value)
        {
            var matrixArray = MatrixData;
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
        
        /// <summary>
        /// Метод сравнениния матриц по значениям
        /// </summary>
        /// <param name="obj">Упакованная матрица</param>
        /// <returns>Результат сравнения</returns>
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
        /// Метод сравнениния матриц по значениям
        /// </summary>
        /// <param name="other">Матрица</param>
        /// <returns>Результат сравнения</returns>
        public bool Equals(Matrix other)
        {
            if (RowCount != other.RowCount || ColumnCount != other.ColumnCount)
                return false;
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (this[i, j] != other[i, j])
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Алгоритм создания хэш-кода
        /// </summary>
        /// <returns>Хэш-код</returns>
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
        /// Создать копию матрицы
        /// </summary>
        /// <returns>Копия матрицы привдённая к object</returns>
        public object Clone()
        {
            return new Matrix(_matrixData);
        }
        #endregion

        #region Операторы
        /// <summary>
        /// Преобразовать двумерный массив в матрицу
        /// </summary>
        /// <param name="matrixArray">Двумерный массив double</param>
        public static implicit operator Matrix(double[,] matrixArray)
        {
            return new Matrix(matrixArray);
        }

        /// <summary>
        /// Преобразовать двумерный массив в матрицу
        /// </summary>
        /// <param name="matrixArray">Двумерный массив int</param>
        public static implicit operator Matrix(int[,] matrixArray)
        {
            return new Matrix(matrixArray);
        }

        /// <summary>
        /// Преобразовать одномерный массив в матрицу
        /// </summary>
        /// <param name="matrixArray">Одномерный массив double</param>
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
        /// Преобразовать одномерный массив в матрицу
        /// </summary>
        /// <param name="matrixArray">Одномерный массив int</param>
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

        /// <summary>
        /// Сложение матрицы и числа
        /// </summary>
        /// <param name="value">Число, складываемое с матрицей</param>
        /// <param name="matrix">Матрица</param>
        /// <returns>Матрица</returns>
        public static Matrix operator +(double value, Matrix matrix)
        {
            return matrix.Add(value);
        }

        /// <summary>
        /// Сложение матрицы и числа
        /// </summary>
        /// <param name="value">Число, складываемое с матрицей</param>
        /// <param name="matrix">Матрица</param>
        /// <returns>Матрица</returns>
        public static Matrix operator +(Matrix matrix, double value)
        {
            return matrix.Add(value);
        }

        /// <summary>
        /// Сложение матриц
        /// </summary>
        /// <param name="matrix1">Матрица</param>
        /// <param name="matrix2">Матрица</param>
        /// <returns>Матрица</returns>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Add(matrix2);
        }

        /// <summary>
        /// Разность матрицы и числа
        /// </summary>
        /// <param name="value">Число, вычитаемое из матрицы</param>
        /// <param name="matrix">Матрица</param>
        /// <returns>Матрица</returns>
        public static Matrix operator -(Matrix matrix, double value)
        {
            return matrix.Subtract(value);
        }

        /// <summary>
        /// Разность матриц
        /// </summary>
        /// <param name="matrix1">Матрица</param>
        /// <param name="matrix2">Матрица</param>
        /// <returns>Матрица</returns>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Subtract(matrix2);
        }

        /// <summary>
        /// Произведение матрицы и числа
        /// </summary>
        /// <param name="value">Множитель</param>
        /// <param name="matrix">Матрица</param>
        /// <returns>Матрица</returns>
        public static Matrix operator *(Matrix matrix, double value)
        {
            return matrix.Multiply(value);
        }

        /// <summary>
        /// Произведение матрицы и числа
        /// </summary>
        /// <param name="value">Множитель</param>
        /// <param name="matrix">Матрица</param>
        /// <returns>Матрица</returns>
        public static Matrix operator *(double value, Matrix matrix)
        {
            return matrix.Multiply(value);
        }

        /// <summary>
        /// Произведение матриц
        /// </summary>
        /// <param name="matrix1">Матрица</param>
        /// <param name="matrix2">Матрица</param>
        /// <returns>Матрица</returns>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Multiply(matrix2);
        }

        /// <summary>
        /// Деление матрицы на число
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="value">Делитель</param>
        /// <returns>Матрица</returns>
        public static Matrix operator /(Matrix matrix, double value)
        {
            return matrix.Divide(value);
        }

        /// <summary>
        /// Оператор проверки на равенство матриц
        /// </summary>
        /// <param name="matrix1">Матрица 1</param>
        /// <param name="matrix2">Матрица 2</param>
        /// <returns>Результат</returns>
        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Equals(matrix2);
        }

        /// <summary>
        /// Оператор проверки на неравенство матриц
        /// </summary>
        /// <param name="matrix1">Матрица 1</param>
        /// <param name="matrix2">Матрица 2</param>
        /// <returns>Результат</returns>
        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            return !matrix1.Equals(matrix2);
        }

        /// <summary>
        /// Оператор проверки на равенство матриц
        /// </summary>
        /// <param name="matrix1">Матрица 1</param>
        /// <param name="matrix2">Матрица 2</param>
        /// <returns>Результат</returns>
        public static bool operator ==(Matrix matrix1, object matrix2)
        {
            return matrix1.Equals(matrix2);
        }
        /// <summary>
        /// Оператор проверки на неравенство матриц
        /// </summary>
        /// <param name="matrix1">Матрица 1</param>
        /// <param name="matrix2">Матрица 2</param>
        /// <returns>Результат</returns>
        public static bool operator !=(Matrix matrix1, object matrix2)
        {
            return !matrix1.Equals(matrix2);
        }

        /// <summary>
        /// Оператор проверки на равенство матриц
        /// </summary>
        /// <param name="matrix1">Матрица 1</param>
        /// <param name="matrix2">Матрица 2</param>
        /// <returns>Результат</returns>
        public static bool operator ==(object matrix1, Matrix matrix2)
        {
            return matrix1.Equals(matrix2);
        }
        /// <summary>
        /// Оператор проверки на неравенство матриц
        /// </summary>
        /// <param name="matrix1">Матрица 1</param>
        /// <param name="matrix2">Матрица 2</param>
        /// <returns>Результат</returns>
        public static bool operator !=(object matrix1, Matrix matrix2)
        {
            return !matrix1.Equals(matrix2);
        }
        #endregion
    }
}
