using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Практическая_работа__13
{
    static class VisualArray
    {
        /// <summary>
        /// Метод для создания таблицы по аказанному размеру матрицы
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }

                res.Rows.Add(row);
            }

            return res;
        }

        /// <summary>
        /// Метод для подсчёта количества столбцов все элементы которых различны
        /// </summary>
        /// <param name="matr">Матрицы</param>
        /// <returns>Возвращаемое значение, количество столбцов все элементы которых различны</returns>
        public static int CalcFunction(int[,] matr)
        {
            int uniqueColumnsCount = 0;
            HashSet<int> columnElements;

            for (int j = 0; j < matr.GetLength(1); j++)
            {
                columnElements = new HashSet<int>();
                bool unique = true;

                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    if (!columnElements.Add(matr[i, j]))
                    {
                        unique = false;
                        break;
                    }
                }
                if (unique == true)
                {
                    uniqueColumnsCount++;
                }
            }

            return uniqueColumnsCount;
        }
    }
}
