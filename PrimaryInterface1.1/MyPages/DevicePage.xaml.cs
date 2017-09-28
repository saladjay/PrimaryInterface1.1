using PrimaryInterface1._1.Model;
using PrimaryInterface1._1.ViewModel;
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
    /// DevicePage.xaml 的互動邏輯
    /// </summary>
    /// 
    public partial class DevicePage : Page
    {
        ViewModel1 SourceData = new ViewModel1();

        public DevicePage()
        {
            InitializeComponent();
            ConnectView.DataSource = SourceData;
            SourceData.DataCollection.Add(new DeviceModel("first", 4));
            SourceData.DataCollection.Add(new DeviceModel("second", 4));
            SourceData.DataCollection.Add(new DeviceModel("third", 4));
            SourceData.DataCollection.Add(new DeviceModel("fourth", 4));
        }
    }
}
