using System;
using System.Collections.Generic;
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

namespace PrimaryInterface1._1.MyPages
{
    /// <summary>
    /// ZonePage.xaml 的互動邏輯
    /// </summary>
    public partial class ZonePage : Page
    {
        public ZonePage()
        {
            InitializeComponent();
        }


        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            CellsDataGrid.activateCells();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            CellsDataGrid.inactivateCells();
        }
    }
}
