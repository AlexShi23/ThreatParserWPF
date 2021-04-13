using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int numberOfRecPerPage;
        static Paging PagedTable = new Paging();
        List<Threat> threats = ExcelParser.ReadExcel(@"C:\Users\Саша\source\repos\Lab2\thrlist.xlsx");

        public MainWindow()
        {
            InitializeComponent();

            PagedTable.PageIndex = 1; 
            int[] RecordsToShow = { 15, 20, 30, 50, 100 }; 

            foreach (int RecordGroup in RecordsToShow)
            {
                NumberOfRecords.Items.Add(RecordGroup); 
            }

            NumberOfRecords.SelectedItem = 15;
            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem);
            DataTable firstTable = PagedTable.SetPaging(threats, numberOfRecPerPage);
            dataGrid.ItemsSource = firstTable.DefaultView;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // проверка есть ли файл и если нет, то вывод message box

            // парсинг из файла

            //threats.Add(new Threat(1, "Угроза автоматического распространения вредоносного кода в грид - системе", "Угроза заключается в возможности внедрения и запуска вредоносного кода от имени доверенного процесса на любом из ресурсных центров грид-системы и его автоматического распространения на все узлы грид-системы.",
            //"Внешний нарушитель со средним потенциалом; Внутренний нарушитель со средним потенциалом", "Ресурсные центры грид - системы", true, true, true));
            dataGrid.ItemsSource = threats;
        }

        private void DataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            /* Threat path = dataGrid.SelectedItem as Threat;
            MessageBox.Show(" ID: " + path.Id + "\n Исполнитель: " + path.Vocalist + "\n Альбом: " + path.Album
                + "\n Год: " + path.Year); */
        }

        public string PageNumberDisplay()
        {
            int PagedNumber = numberOfRecPerPage * (PagedTable.PageIndex + 1);
            if (PagedNumber > threats.Count)
            {
                PagedNumber = threats.Count;
            }
            return "Показано " + PagedNumber + " из " + threats.Count;
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        { 
            dataGrid.ItemsSource = PagedTable.Next(threats, numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void Backwards_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = PagedTable.Previous(threats, numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {                                                                                          
            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem);
            dataGrid.ItemsSource = PagedTable.First(threats, numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void First_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = PagedTable.First(threats, numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = PagedTable.Last(threats, numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
    }
}

