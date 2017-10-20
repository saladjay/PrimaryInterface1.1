using PrimaryInterface1._1.Controls.Core;
using PrimaryInterface1._1.ViewModel;
using System;
using System.Collections;
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

namespace PrimaryInterface1._1.Controls
{
    /// <summary>
    /// 依照步驟 1a 或 1b 執行，然後執行步驟 2，以便在 XAML 檔中使用此自訂控制項。
    ///
    /// 步驟 1a) 於存在目前專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:PrimaryInterface1._1.Controls.Core"
    ///
    ///
    /// 步驟 1b) 於存在其他專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:PrimaryInterface1._1.Controls.Core;assembly=PrimaryInterface1._1.Controls.Core"
    ///
    /// 您還必須將 XAML 檔所在專案的專案參考加入
    /// 此專案並重建，以免發生編譯錯誤: 
    ///
    ///     在 [方案總管] 中以滑鼠右鍵按一下目標專案，並按一下
    ///     [加入參考]->[專案]->[瀏覽並選取此專案]
    ///
    ///
    /// 步驟 2)
    /// 開始使用 XAML 檔案中的控制項。
    ///
    ///     <MyNamespace:DeviceConnectView/>
    ///
    /// </summary>
    public class DeviceConnectView : ItemsControl
    {
        static DeviceConnectView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DeviceConnectView), new FrameworkPropertyMetadata(typeof(DeviceConnectView)));
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(DeviceConnectView));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyPropertyKey TopItemPropertyKey = DependencyProperty.RegisterReadOnly("TopItem", typeof(object), typeof(DeviceConnectView), new PropertyMetadata(default(object)));
        public static readonly DependencyProperty TopItemProperty = TopItemPropertyKey.DependencyProperty;
        public object TopItem
        {
            get { return (object)GetValue(TopItemPropertyKey.DependencyProperty); }
        }

        public static readonly DependencyPropertyKey LeftItemPropertyKey = DependencyProperty.RegisterReadOnly("LeftItem", typeof(object), typeof(DeviceConnectView), new PropertyMetadata(default(object)));
        public static readonly DependencyProperty LeftItemProperty = LeftItemPropertyKey.DependencyProperty;
        public object LeftItem
        {
            get { return (object)GetValue(LeftItemPropertyKey.DependencyProperty); }
        }

        public static readonly DependencyPropertyKey ButtomItemPropertyKey = DependencyProperty.RegisterReadOnly("ButtomItem", typeof(object), typeof(DeviceConnectView), new PropertyMetadata(default(object)));
        public static readonly DependencyProperty ButtomItemProperty = ButtomItemPropertyKey.DependencyProperty;
        public object ButtomItem
        {
            get { return (object)GetValue(ButtomItemPropertyKey.DependencyProperty); }
        }

        public DeviceItemList LeftTreeView { get; set; }
        public DeviceItemList TopTreeView { get; set; }
        public DeviceInterfaceGrid InterfaceState { get; set; }
        private ViewModel1 _DataSource = null;
        public ViewModel1 DataSource
        {
            get { return _DataSource; }
            set
            {
                _DataSource = value;
                this.ItemsSource = _DataSource.DataCollection;
            }
        }
        public DeviceConnectView()
        {
            Style = (Style)FindResource("DeviceConnectViewStyle");
            LeftTreeView = new DeviceItemList() { DockDirection = _Direction.Left };
            TopTreeView = new DeviceItemList() { DockDirection = _Direction.Top };
            InterfaceState = new DeviceInterfaceGrid();
            //TopItem = TopTreeView;
            //LeftItem = LeftTreeView;
            //ButtomItem = InterfaceState;
            this.SetValue(LeftItemPropertyKey, LeftTreeView);
            this.SetValue(TopItemPropertyKey, TopTreeView);
            this.SetValue(ButtomItemPropertyKey, InterfaceState);
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            InterfaceState.DataSource = LeftTreeView.DataSource = TopTreeView.DataSource = DataSource;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }
    }
}
