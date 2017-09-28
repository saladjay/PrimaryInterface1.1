using MS.Internal.PresentationFramework;
using PrimaryInterface1._1.Controls.Core;
using PrimaryInterface1._1.Model;
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
using static PrimaryInterface1._1.Model.DeviceModel;

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
    ///     <MyNamespace:DeviceItemList/>
    ///
    /// </summary>
    public class DeviceItemList : Grid
    {
        static DeviceItemList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DeviceItemList), new FrameworkPropertyMetadata(typeof(DeviceItemList)));
        }
        private ItemsGrid CGridItemsSource = new ItemsGrid();
        [CommonDependencyProperty]
        public IEnumerable ItemsSource
        {
            get { return CGridItemsSource.ItemsSource; }
            set { CGridItemsSource.ItemsSource = value; }
        }

        public static readonly DependencyProperty DockDirectionProperty = DependencyProperty.Register("DockDirection", typeof(_Direction), typeof(DeviceItemList), new PropertyMetadata(_Direction.Left));
        public _Direction DockDirection
        {
            get { return (_Direction)GetValue(DockDirectionProperty); }
            set { SetValue(DockDirectionProperty, value); }
        }

        public static readonly DependencyProperty DeviceHeaderProperty = DependencyProperty.Register("DeviceHeader", typeof(string), typeof(DeviceItemList));
        public string DeviceHeader
        {
            get { return (string)GetValue(DeviceHeaderProperty); }
            set { SetValue(DeviceHeaderProperty, value); }
        }

        #region Construction
        public DeviceItemList()
        {
            CGridItemsSource.ExtendedItemsChanged += CGridItemsSource_ExtendedItemsChanged;
        }
        #endregion

        private ViewModel1 _DataSource = null;
        public ViewModel1 DataSource
        {
            get { return _DataSource; }
            set
            {
                _DataSource = value;
                if (DockDirection == _Direction.Left)
                    SideCellState = _DataSource.RowCellState;
                else
                    SideCellState = _DataSource.ColumnCellState;
                ConstructionHelper = _DataSource.ConstructionHelper;
                PositionHelper = _DataSource.PositionHelper;
                ItemsSource = _DataSource.DataCollection;
            }
        }
        private List<CellState> SideCellState { get; set; }
        private List<object> ConstructionHelper { get; set; }
        private List<int> PositionHelper { get; set; }
        public List<DeviceModel> InnerDeviceList = new List<DeviceModel>();
        private void CGridItemsSource_ExtendedItemsChanged(object item, bool AddOrRemove)
        {
            if (!(item is DeviceModel))
                return;
            DeviceModel device = item as DeviceModel;
            if (AddOrRemove)
            {
                InnerDeviceList.Add(device);
                for (int i = 0; i < device.InterfaceCount + 1; i++)
                {
                    if (DockDirection == _Direction.Left)
                        this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20, GridUnitType.Auto) });
                    else
                        this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Auto) });
                }

                int Index = ConstructionHelper.IndexOf(device);
                for (int i = 0; i < device.InterfaceCount + 1; i++)
                {
                    CTreeViewItem tempItem = DeviceItemFactory.CreateItem(ConstructionHelper[Index + i], Index + i, this) as CTreeViewItem;
                    this.Children.Add(tempItem);
                    if (DockDirection == _Direction.Left)
                        Grid.SetRow(tempItem, PositionHelper[Index + i]);
                    else
                        Grid.SetColumn(tempItem, PositionHelper[Index + i]);
                    ItemsList.Add(tempItem);
                }
            }
            else
            {
                int Index = InnerDeviceList.IndexOf(device);
                int RemoveIndex = DataSource.RemoveIndex;//ConstructionHelper.IndexOf(device);
                InnerDeviceList.RemoveAt(Index);
                for (int i = 0; i < device.InterfaceCount + 1; i++)
                {
                    this.Children.RemoveAt(RemoveIndex);
                    ItemsList.RemoveAt(RemoveIndex);
                }
            }
        }
        private List<Control> ItemsList = new List<Control>();

        internal void ItemExpandHandler(bool Open, CTreeViewItem source)
        {
            DeviceModel TempDevice = source.Tag as DeviceModel;
            int Index = ItemsList.IndexOf(source);
            for (int i = 0; i < TempDevice.InterfaceCount + 1; i++)
            {
                SideCellState[Index + i].SingleBool = Open;
            }
        }

        internal void ItemSelectHandler(bool Selected, CTreeViewItem source)
        {
            int Index = ItemsList.IndexOf(source);
            if (DockDirection == _Direction.Left)
                DataSource.SelectRow = Index;
            else
                DataSource.SelectColumn = Index;
        }

        public class DeviceItemFactory
        {
            public static Control CreateItem(object a, int Index, DeviceItemList DeviceList)
            {
                Control Good = null;
                if (a is DeviceModel)
                {
                    DeviceModel A = a as DeviceModel;
                    Good = new CTreeViewItem() { Header = A.DeviceName, Direction = DeviceList.DockDirection, Tag = A };
                    Good.Style = (Style)Good.FindResource("CTreeViewItemStyle2");
                    Good.SetBinding(CTreeViewItem.OpenProperty, new Binding("SingleBool") { Source = DeviceList.SideCellState[Index] });
                    Good.SetBinding(CTreeViewItem.MouseSelectedProperty, new Binding("IsSelect") { Source = DeviceList.SideCellState[Index] });
                    ((CTreeViewItem)Good).ExpandItems += DeviceList.ItemExpandHandler;
                    ((CTreeViewItem)Good).SelectItem += DeviceList.ItemSelectHandler;
                }
                else if (a is DeviceInterface)
                {
                    DeviceInterface A = a as DeviceInterface;
                    if (DeviceList.DockDirection == _Direction.Left)
                    {
                        Good = new CTreeViewItem() { Header = "Input" + A.InterfaceName, Direction = _Direction.Left, HideBtn = true };
                    }
                    else
                    {
                        Good = new CTreeViewItem() { Header = "Output" + A.InterfaceName, Direction = _Direction.Top, HideBtn = true };
                    }
                    Good.Style = (Style)Good.FindResource("CTreeViewItemStyle2");
                    Good.SetBinding(CTreeViewItem.VisibilityProperty, new Binding("SingleBool") { Source = DeviceList.SideCellState[Index], Converter = Converter.CellVisibilityConverter2 });
                    Good.SetBinding(CTreeViewItem.MouseSelectedProperty, new Binding("IsSelect") { Source = DeviceList.SideCellState[Index] });
                    ((CTreeViewItem)Good).SelectItem += DeviceList.ItemSelectHandler;
                }
                return Good;
            }
        }
    }

    public class CTreeViewItem : TreeViewItem
    {
        static CTreeViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CTreeViewItem), new FrameworkPropertyMetadata(typeof(CTreeViewItem)));
        }
        #region style switch
        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register("Direction", typeof(_Direction), typeof(CTreeViewItem));

        public _Direction Direction
        {
            get { return (_Direction)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        public static readonly DependencyProperty HideBtnProperty = DependencyProperty.Register("HideBtn", typeof(bool), typeof(CTreeViewItem), new PropertyMetadata(false));
        public bool HideBtn
        {
            get { return (bool)GetValue(HideBtnProperty); }
            set { SetValue(HideBtnProperty, value); }
        }
        #endregion

        #region expand and collapse
        public static readonly DependencyProperty OpenProperty = DependencyProperty.Register("Open", typeof(bool), typeof(CTreeViewItem), new PropertyMetadata(false, OnOpenChanged));

        private static void OnOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CTreeViewItem tempItem = d as CTreeViewItem;
            if (tempItem == null)
            {
                return;
            }
            tempItem.ExpandItems?.Invoke((bool)e.NewValue, tempItem);
        }

        public bool Open
        {
            get { return (bool)GetValue(OpenProperty); }
            set { SetValue(OpenProperty, value); }
        }
        #endregion

        #region select and unselect
        public static readonly DependencyProperty MouseSelectedProperty = DependencyProperty.Register("MouseSelected", typeof(bool), typeof(CTreeViewItem),
            new PropertyMetadata(false));
        public bool MouseSelected
        {
            get { return (bool)GetValue(MouseSelectedProperty); }
            set { SetValue(MouseSelectedProperty, value); }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            SelectItem?.Invoke(true, this);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
        }
        #endregion

        public delegate void ExpandItemsHandler(bool IsOpen, CTreeViewItem Source);
        public event ExpandItemsHandler ExpandItems;
        public delegate void SelectItemHandler(bool IsSelect, CTreeViewItem Source);
        public event SelectItemHandler SelectItem;
    }

    public enum _Direction
    {
        Left,
        Top
    }

}
