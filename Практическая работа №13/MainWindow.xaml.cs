using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Практическая_работа__13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private DispatcherTimer timer;

        /// <summary>
        /// Модуль запускающий таймер при открытии главного окна программы который будет показывать дату и время
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.IsEnabled = true;
        }

        /// <summary>
        /// Модуль, реализующий таймер для обновления времени и даты в главном окне
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            time.Text = d.ToString("HH:mm:ss");
            data.Text = d.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Метод для закрытия программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Метод для отображения информации о задании для разработки программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfoProga_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дана целочисленная матрица \nразмера M * N. Найти количе-\nmство ее столбцов, " +
                "все элеме-\nнты которых различны.", "О программе: ", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Метод для отображения информации о создателе программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfoCreator_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Жаров Артём Андреевич \nСтудент группы ИСП-31", "О создателе", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        int indexColumn;
        int indexRow;
        int[,] matr;
        int randMax;
        bool openfile;
        HashSet<int> columnElements;

        /// <summary>
        /// Метод для автоматического создания матрицы со случайным количеством строк, столбцов и диапозоном значений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.Visibility == Visibility.Hidden)
            {
                dataGrid.Visibility = Visibility.Visible;
                hideTable.IsEnabled = true;
                showTable.IsEnabled = false;
            }

            tbRez.Clear();

            LibMatr create = new LibMatr();
            matr = create.CreateTable(dataGrid, indexColumn, indexRow);
            dataGrid.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;

            razmerTable.Text = $"{matr.GetLength(0)}*{matr.GetLength(1)}";
            razmerTable.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Метод для создания пустой матрицы(все значения в таблице "0")
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatePolzovatel_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(tbNumCol.Text, out indexColumn) && Int32.TryParse(tbNumRow.Text, out indexRow))
            {
                tbRez.Clear();

                indexColumn = Convert.ToInt32(tbNumCol.Text);
                indexRow = Convert.ToInt32(tbNumRow.Text);

                if (dataGrid.Visibility == Visibility.Hidden)
                {
                    dataGrid.Visibility = Visibility.Visible;
                    hideTable.IsEnabled = true;
                    showTable.IsEnabled = false;
                }

                LibMatr createPolz = new LibMatr();
                matr = createPolz.CreatePolzovatel(dataGrid, indexColumn, indexRow);
                dataGrid.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;

                razmerTable.Text = $"{matr.GetLength(0)}*{matr.GetLength(1)}";
                razmerTable.Visibility = Visibility.Visible;
            }
            else MessageBox.Show("Введены некоррентные значения!", "Ошибка:", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Метод для заполнения матрицы случайными значениями
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            if (matr != null && Int32.TryParse(tbDiapozon.Text, out randMax))
            {
                tbRez.Clear();

                randMax = Convert.ToInt32(tbDiapozon.Text);

                if (dataGrid.Visibility == Visibility.Hidden)
                {
                    dataGrid.Visibility = Visibility.Visible;
                    hideTable.IsEnabled = true;
                    showTable.IsEnabled = false;
                }

                LibMatr fill = new LibMatr();
                matr = fill.FillTable(dataGrid, indexColumn, indexRow, randMax);
                dataGrid.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;

                razmerTable.Text = $"{matr.GetLength(0)}*{matr.GetLength(1)}";
                razmerTable.Visibility = Visibility.Visible;
            }
            else MessageBox.Show("   Вы не создали таблицу,или \nввели некорректные значения!", "Ошибка:", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Метод для проверки корректности введенных данных в таблицу а так же отображения номера выделенной
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int indexRow = e.Row.GetIndex();
            int indexColumn = e.Column.DisplayIndex;
            if (Int32.TryParse(((TextBox)e.EditingElement).Text, out int newValue))
                matr[indexRow, indexColumn] = newValue; tbRez.Clear();

            numberCell.Visibility = Visibility.Visible;
            numberCell.Text = $"{indexRow + 1}*{indexColumn + 1}";
        }

        /// <summary>
        /// Метод для подсчёта количества столбцов все элементы в которых различны
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            tbRez.Clear();

            if (dataGrid.ItemsSource != null)
            {
                int uniqueColumnsCount = VisualArray.CalcFunction(matr);
                tbRez.Text = Convert.ToString(uniqueColumnsCount);
            }
            else MessageBox.Show("Таблицы не существует.\nСоздайте её!", "Ошибка:", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Метод для очистки всех результатов в программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.Visibility == Visibility.Visible)
            {
                dataGrid.Visibility = Visibility.Hidden;
                hideTable.IsEnabled = false;
                showTable.IsEnabled = true;
            }

            columnElements = null;

            LibMatr clear = new LibMatr();
            clear.AllClear(dataGrid, out matr, tbNumCol, tbNumRow, tbDiapozon, tbRez);

            razmerTable.Visibility = Visibility.Hidden;
            numberCell.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Метод для сохранения таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (matr != null)
            {
                LibMatr SaveFileManager = new LibMatr();
                SaveFileManager.SaveDataToFile(matr);
            }
            else MessageBox.Show("Нет данных для сохранения.\nВведите значения!", "Ошибка:", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Метод для открытия уже сохранённой таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            LibMatr OpenFileMenedger = new LibMatr();
            OpenFileMenedger.OpenDataToFile(out matr, out openfile);

            if (openfile == true)
            {
                if (dataGrid.Visibility == Visibility.Hidden)
                {
                    dataGrid.Visibility = Visibility.Visible;
                    hideTable.IsEnabled = true;
                    showTable.IsEnabled = false;
                }

                dataGrid.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;
            }
            else matr = null;
        }

        /// <summary>
        /// Метод для показания таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowTable_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid.Visibility == Visibility.Hidden)
            {
                dataGrid.Visibility = Visibility.Visible;
                hideTable.IsEnabled = true;
                showTable.IsEnabled = false;
            }
        }

        /// <summary>
        /// Метод для скрытия таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHideTable_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid.Visibility == Visibility.Visible)
            {
                dataGrid.Visibility = Visibility.Hidden;
                hideTable.IsEnabled = false;
                showTable.IsEnabled = true;
            }
        }
    }
}