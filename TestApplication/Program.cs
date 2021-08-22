using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d1den.MathLibrary;

namespace TestApplication
{
    class Program
    {
        static double GetDeterminant(double[,] matrix)
        {
            if(matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new ArgumentException("Matrix isn`t squred");
            }
            else if(matrix.Length == 1)
            {
                return matrix[0, 0];
            }
            else
            {
                double determinant = 0; 
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    determinant += Math.Pow(-1.0, i + 0) * matrix[i, 0] * GetDeterminant(GetMinor(matrix, i, 0));
                }
                return determinant;
            }
        }
        static double[,] GetMinor(double[,] matrix, int row, int column)
        {
            if ((matrix.GetLength(0) != matrix.GetLength(1)) || matrix.Length == 1)
            {
                throw new ArgumentException("Matrix isn`t squred");
            }
            double[,] newMatrix = new double[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
            for (int i = 0, iNew = 0; i < matrix.GetLength(0); i++)
            {
                if(i == row)
                    continue;
                for (int j = 0, jNew = 0; j<matrix.GetLength(1); j++)
                {
                    if(j == column)
                        continue;
                    newMatrix[iNew, jNew++] = matrix[i, j];
                }
                iNew++;
            }
            return newMatrix;
        }
        static void Main(string[] args)
        {
            Matrix matrix = new Matrix( new double[,]
                {
                    { 4, 5, 7 },
                    { 4, 8, -5 },
                    { 12, 4, 7 }
                }
                );
            Console.WriteLine(matrix.GetDeterminant());
        }
    }
}
