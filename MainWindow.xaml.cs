using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
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
        List<Threat> threats = new List<Threat>();

        public MainWindow()
        {
            InitializeComponent();
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\thrlist.xlsx")) // Если файла с локальной базой данных не существует
            {
                MessageBox.Show("Файла с локальной базой не существует. Будет произведена первичная загрузка данных.");
                try
                {
                    WebClient wc = new WebClient();
                    string url = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
                    string savePath = Directory.GetCurrentDirectory();
                    string name = "thrlist.xlsx";
                    wc.DownloadFile(url, savePath + "\\" + name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            threats = ExcelParser.ReadExcel(Directory.GetCurrentDirectory() + @"\thrlist.xlsx");

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
            dataGrid.ItemsSource = threats;
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

    }
}

