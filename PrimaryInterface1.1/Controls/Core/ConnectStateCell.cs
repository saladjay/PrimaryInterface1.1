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
    ///     <MyNamespace:ConnectStateCell/>
    ///
    /// </summary>
    public class ConnectStateCell : Control
    {
        static ConnectStateCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ConnectStateCell), new FrameworkPropertyMetadata(typeof(ConnectStateCell)));
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(ConnectStateCell));
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsCommonProperty = DependencyProperty.RegisterAttached("IsCommon", typeof(bool), typeof(ConnectStateCell));
        public bool IsCommon
        {
            get { return (bool)GetValue(IsCommonProperty); }
            set { SetValue(IsCommonProperty, value); }
        }

        public static readonly DependencyProperty IsConnectedProperty = DependencyProperty.RegisterAttached("IsConnected", typeof(bool), typeof(ConnectStateCell),new PropertyMetadata(false));
        public bool IsConnected
        {
            get { return (bool)GetValue(IsConnectedProperty); }
            set { SetValue(IsConnectedProperty, value); }
        }

        public delegate void MouseActionHandler(bool IsMouseSelect, ConnectStateCell Source);
        public event MouseActionHandler IsMouseSelectEvent;
        public event MouseActionHandler IsConnectedEvent;
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            IsMouseSelectEvent?.Invoke(true, this);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            IsMouseSelectEvent?.Invoke(false, this);
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            IsConnectedEvent?.Invoke(true, this);
            base.OnMouseDown(e);
        }

        public object ObjectTag1 { get; set; }
        public object OBjectTag2 { get; set; }


        public int PositionRow { get; set; }
        public int PositionColumn { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            Style = (Style)FindResource("ExpandedLabelStyle");
            IsHitTestVisible = true;
            base.OnInitialized(e);
        }
    }
}
