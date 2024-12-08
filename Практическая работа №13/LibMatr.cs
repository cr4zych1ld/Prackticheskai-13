using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Практическая_работа__13
{
    class LibMatr
    {
        /// <summary>
        /// Автоматическое создание матрицы со случайным количеством строк, столбцов и диапозоном значений
        /// </summary>
        /// <param name="dataGrid">Таблица</param>
        /// <param name="indexColumn">Количество колонок</param>
        /// <param name="indexRow">Количество строк</param>
        /// <returns>Вывод матрицы со случайным количеством строк, столбцов и диапозоном значений</returns>
        public int[,] CreateTable(DataGrid dataGrid, int indexColumn, int indexRow)
        {
            dataGrid.ItemsSource = null;

            Random rnd = new Random();
            indexRow = rnd.Next(50);
            indexColumn = rnd.Next(50);
            int[,] matr = new int[indexRow, indexColumn];
            for (int i = 0; i < indexRow; i++)
                for (int j = 0; j < indexColumn; j++)
                    matr[i, j] = rnd.Next(1000);
            return matr;
        }

        /// <summary>
        /// Создание пустой матрицы(все значения в таблице "0")
        /// </summary>
        /// <param name="dataGrid">Таблица</param>
        /// <param name="indexColumn">Количество колонок</param>
        /// <param name="indexRow">Количество строк</param>
        /// <returns>Вывод пустой матрицы с указанным количеством колонок и строк</returns>
        public int[,] CreatePolzovatel(DataGrid dataGrid, int indexColumn, int indexRow)
        {
            dataGrid.ItemsSource = null;

            int[,] matr = new int[indexColumn, indexRow];
            return matr;
        }

        /// <summary>
        /// Заполнение матрицы случайными значениями
        /// </summary>
        /// <param name="dataGrid">Таблица</param>
        /// <param name="indexColumn">Количество колонок в матрице</param>
        /// <param name="indexRow">Количество строк в матрице</param>
        /// <param name="randMax">Диапозон чисел для заполнения матрицы</param>
        /// <returns>Вывод матрица со значениями распределёнными в указанном диапозоне</returns>
        public int[,] FillTable(DataGrid dataGrid, int indexColumn, int indexRow, int randMax)
        {
            dataGrid.ItemsSource = null;

            int[,] matr = new int[indexColumn, indexRow];
            Random rnd = new Random();
            for (int i = 0; i < indexColumn; i++)
                for (int j = 0; j < indexRow; j++)
                    matr[i, j] = rnd.Next(randMax);
            return matr;
        }


        /// <summary>
        /// Очистка всех объектов программы
        /// </summary>
        /// <param name="dataGrid">Таблица</param>
        /// <param name="matr">Матрица</param>
        /// <param name="tbNumCol">Текстбок для указания количества колонок</param>
        /// <param name="tbNumRow">Текстбок для указания количества строк</param>
        /// <param name="tbDiapozon">Текстбок для указания диапозона значений</param>
        /// <param name="tbRez">Текстбок для указания количества столбцов с разными элементами</param>
        public void AllClear(DataGrid dataGrid, out int[,] matr, TextBox tbNumCol, TextBox tbNumRow, TextBox tbDiapozon, TextBox tbRez)
        {
            dataGrid.ItemsSource = null;
            matr = null;
            tbNumCol.Clear();
            tbNumRow.Clear();
            tbDiapozon.Clear();
            tbRez.Clear();
        }

        /// <summary>
        /// Сохранение таблицы в текстовый файл
        /// </summary>
        /// <param name="matr">Матрица</param>
        public void SaveDataToFile(int[,] matr)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*) | *.* |Текстовые файлы | *.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";

            if (save.ShowDialog() == true)
            {
                StreamWriter file = new StreamWriter(save.FileName);

                file.WriteLine(matr.GetLength(0));
                file.WriteLine(matr.GetLength(1));
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        file.WriteLine(matr[i, j]);
                    }
                }
                file.Close();
            }
        }

        /// <summary>
        /// Открытие таблицы из текстового файла
        /// </summary>
        /// <param name="matr">Матрица</param>
        public void OpenDataToFile(out int[,] matr, out bool openfile)
        {
            openfile = false;

            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы (*.*) | *.* |Текстовые файлы | *.txt";
            open.FilterIndex = 2;
            open.Title = "Сохранение таблицы";
            matr = null;

            if (open.ShowDialog() == true)
            {
                openfile = true;

                StreamReader file = new StreamReader(open.FileName);
                int row = Convert.ToInt32(file.ReadLine());
                int col = Convert.ToInt32(file.ReadLine());
                matr = new int[row, col];

                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        string s = file.ReadLine();
                        bool f1 = int.TryParse(s, out int value);
                        if (f1)
                        {
                            matr[i, j] = value;
                        }
                    }
                }
                file.Close();
            }
            else openfile = false;
        }
    }
}
