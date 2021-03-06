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
using System.Windows.Shapes;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для Differences.xaml
    /// </summary>
    public partial class Differences : Window
    {
        public Differences(DataTable differences, bool status)
        {
            InitializeComponent();
            int countOfUpdated = 0;
            if (status)
            {
                statusOfUpdateLabel.Content = "Успешно";
                statusOfUpdateLabel.Foreground = Brushes.Green;
                countOfUpdated = differences.Rows.Count / 2;
                countOfUpdatedLabel.Content = countOfUpdated;
                dataGrid.ItemsSource = differences.DefaultView;
            }
            else
            {
                statusOfUpdateLabel.Content = "Ошибка";
                statusOfUpdateLabel.Foreground = Brushes.Red;
            }
        }
    }
}
