using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using ExtendedString;
namespace PrimaryInterface1._1.Controls.UserControls
{
    /// <summary>
    /// ZoneDataGrid.xaml 的互動邏輯
    /// </summary>
    public partial class ZoneDataGrid : UserControl
    {
        private List<CLabel> _CellsList = new List<CLabel>();
        public List<CLabel> CellsList
        {
            get { return _CellsList; }
            set
            {
                _CellsList = value;
            }
        }

        private List<CLabel> _HasSelectedCells = new List<CLabel>();
        public List<CLabel> HasSelectedCells
        {
            get { return _HasSelectedCells; }
            set
            {
                _HasSelectedCells = value;
            }
        }

        public ZoneDataGrid()
        {
            InitializeComponent();
            CellsDataGrid.SelectedCellsChanged += CellsDataGrid_SelectedCellsChanged;
            CellsDataGrid.BeginningEdit += CellsDataGrid_BeginningEdit;
            ZoneInfo first = new ZoneInfo(6);
            int[] abc = new int[5] { 0, 1, 2, 3, 4 };//every row must be different to the each row of the rest
            ZoneInfo[] abcd = new ZoneInfo[5]
            {
                new ZoneInfo() {ZoneName="01" },
                new ZoneInfo() {ZoneName="02" },
                new ZoneInfo() {ZoneName="03" },
                new ZoneInfo() {ZoneName="04" },
                new ZoneInfo() {ZoneName="05" }
            };
            InitialDataGrid(first, CellsDataGrid);
            CellsDataGrid.ItemsSource = abcd;
        }

        private void CellsDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
            DataGrid tempDatagrid = sender as DataGrid;
            DataGridRow temprow = e.Row;
            DataGridColumn tempcolumn = e.Column;
            DataGridCell tempcell= tempcolumn.GetCellContent(temprow) as DataGridCell;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(tempcell); i++)
            {
                CLabel tempCLabel = VisualTreeHelper.GetChild(tempcell, i) as CLabel;
                if (tempCLabel == null)
                    continue;
                if (_CellsList.Contains(tempCLabel))
                    continue;
                _CellsList.Add(tempCLabel);
                tempCLabel.IsSelected = true;
                if (!_HasSelectedCells.Contains(tempCLabel))
                    _HasSelectedCells.Add(tempCLabel);
                Debug.WriteLine("-----------------------add id is " + tempCLabel.ID);
            }
        }

        private void CellsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            foreach (var cellinfo in e.AddedCells)
            {
                var cellContent = cellinfo.Column.GetCellContent(cellinfo.Item);
                if (cellContent == null)
                    continue;
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(cellContent); i++)
                {
                    CLabel tempCLabel = VisualTreeHelper.GetChild(cellContent, i) as CLabel;
                    if (tempCLabel == null)
                        continue;
                    if (_CellsList.Contains(tempCLabel))
                        continue;
                    _CellsList.Add(tempCLabel);
                    tempCLabel.IsCommon = true;
                    Debug.WriteLine("-----------------------add id is " + tempCLabel.ID);
                }
            }
            foreach (var cellinfo in e.RemovedCells)
            {
                var cellContent = cellinfo.Column.GetCellContent(cellinfo.Item);
                if (cellContent == null)
                    continue;
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(cellContent); i++)
                {
                    CLabel tempCLabel = VisualTreeHelper.GetChild(cellContent, i) as CLabel;
                    if (tempCLabel == null)
                        continue;
                    if (_CellsList.Contains(tempCLabel))
                        _CellsList.Remove(tempCLabel);
                    tempCLabel.IsCommon = false;
                    Debug.WriteLine("-----------------------remove id is " + tempCLabel.ID);
                }
            }
        }

        private void testdatagrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        public void activateCells()
        {
            foreach (CLabel item in _CellsList)
            {
                item.IsSelected = true;
            }
        }

        public void inactivateCells()
        {
            foreach (CLabel item in _CellsList)
            {
                item.IsSelected = false;
            }
        }

        public void InitialDataGrid(ObservableCollection<Object> First, DataGrid PrimaryDataGrid)
        {
            for (int i = 0; i < First.Count()+1; i++)
            {
                if (PrimaryDataGrid.Columns.Count == 0)
                {
                    DataGridTemplateColumn Column = new DataGridTemplateColumn()
                    {
                        Header = "Name",
                        Width = 200,
                    };
                    DataTemplate TempCellTemplate = new DataTemplate();
                    FrameworkElementFactory CellTextBox = new FrameworkElementFactory(typeof(TextBox));
                    CellTextBox.SetBinding(TextBox.TextProperty, new Binding("ZoneName"));
                    TempCellTemplate.VisualTree = CellTextBox;
                    Column.CellTemplate = TempCellTemplate;
                    PrimaryDataGrid.Columns.Add(Column);
                }
                else
                {
                    DataGridTemplateColumn Column = new DataGridTemplateColumn()
                    {
                        Header = string.Format("OutPut{0}", PrimaryDataGrid.Columns.Count),
                        Width = 40,
                        HeaderStyle = (Style)FindResource("DataGridColumnHeaderStyle1"),
                    };
                    DataTemplate TempCellTemplate = new DataTemplate();
                    FrameworkElementFactory CellCLabel = new FrameworkElementFactory(typeof(CLabel));
                    TempCellTemplate.VisualTree = CellCLabel;
                    Column.CellTemplate = TempCellTemplate;
                    PrimaryDataGrid.Columns.Add(Column);
                }
            }
        }

        public void InitialDataGrid(ZoneInfo First, DataGrid PrimaryDataGrid)
        {
            for (int i = 0; i < First.OutPutState.Count(); i++)
            {
                if (PrimaryDataGrid.Columns.Count == 0)
                {
                    DataGridTemplateColumn Column = new DataGridTemplateColumn()
                    {
                        Header = "Name",
                        Width = 200,
                    };
                    DataTemplate TempCellTemplate = new DataTemplate();
                    FrameworkElementFactory CellTextBox = new FrameworkElementFactory(typeof(TextBox));
                    CellTextBox.SetBinding(TextBox.TextProperty, new Binding("ZoneName"));
                    TempCellTemplate.VisualTree = CellTextBox;
                    Column.CellTemplate = TempCellTemplate;
                    PrimaryDataGrid.Columns.Add(Column);
                }
                else
                {
                    DataGridTemplateColumn Column = new DataGridTemplateColumn()
                    {
                        Header = string.Format("OutPut{0}", PrimaryDataGrid.Columns.Count),
                        Width = 40,
                        HeaderStyle = (Style)FindResource("DataGridColumnHeaderStyle1"),
                    };
                    DataTemplate TempCellTemplate = new DataTemplate();
                    FrameworkElementFactory CellCLabel = new FrameworkElementFactory(typeof(CLabel));
                    TempCellTemplate.VisualTree = CellCLabel;
                    Column.CellTemplate = TempCellTemplate;
                    PrimaryDataGrid.Columns.Add(Column);
                }
            }
        }

    }

    public class CLabel : Control
    {
        private static int id;
        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(int), typeof(CLabel));
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(CLabel), new PropertyMetadata(false));


        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsCommonProperty = DependencyProperty.RegisterAttached("IsCommon", typeof(bool), typeof(CLabel));
        public bool IsCommon
        {
            get { return (bool)GetValue(IsCommonProperty); }
            set { SetValue(IsCommonProperty, value); }
        }

        public int PositionRow { get; set; }
        public int PositionColumn { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            ID = id++;
            if (Parent != null)
                Debug.WriteLine("id is " + ID + " parent" + Parent.GetType().ToString());
            else
                Debug.WriteLine("id is " + ID);
            Style = (Style)FindResource("ExpandedLabelStyle");
            base.OnInitialized(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            //IsSelected = !IsSelected;
            //IsCommon = !IsCommon;
            if (Parent != null)
                Debug.WriteLine("id is " + ID + " parent" + Parent.GetType().ToString());
            else
                Debug.WriteLine("id is " + ID);
            base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Pressed)
            //    IsSelected = true;
            base.OnMouseEnter(e);
        }
    }


    public class ZoneInfo : DependencyObject
    {
        public static readonly DependencyProperty ZoneNameProperty = DependencyProperty.Register("ZoneName", typeof(string), typeof(ZoneInfo));
        public string ZoneName
        {
            get { return (string)GetValue(ZoneNameProperty); }
            set { SetValue(ZoneNameProperty, value); }
        }

        private bool[] _OutPutState = null;
        private static int _Count;
        public bool[] OutPutState
        {
            get
            {
                if (_OutPutState == null)
                    _OutPutState = new bool[_Count];
                return _OutPutState;
            }
        }

        public ZoneInfo()
        {
            if (_Count == 0)
                return;
            _OutPutState = new bool[_Count];
        }

        public ZoneInfo(int Length)
        {
            _Count = Length;
            _OutPutState = new bool[_Count];
        }
    }

}
