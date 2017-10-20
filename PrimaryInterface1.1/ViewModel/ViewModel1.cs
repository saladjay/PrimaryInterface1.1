using PrimaryInterface1._1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrimaryInterface1._1.Model.DeviceModel;
using ExtendedString;
using System.Windows;

namespace PrimaryInterface1._1.ViewModel
{
    public class ViewModel1
    {
        private ObservableCollection<object> _DataCollection = new ObservableCollection<object>();
        public ObservableCollection<object> DataCollection
        {
            get { return _DataCollection; }
            private set { _DataCollection = value; }
        }

        private List<DeviceModel> _InnerDeviceList = new List<DeviceModel>();

        private List<object> _ConstructionHelper = new List<object>();
        public List<object> ConstructionHelper
        {
            get { return _ConstructionHelper; }
        }

        private List<int> _PositionHelper = new List<int>();
        public List<int> PositionHelper
        {
            get { return _PositionHelper; }
        }

        private List<CellState> _ColumnCellState = new List<Model.CellState>();//top
        public List<CellState> ColumnCellState
        {
            get { return _ColumnCellState; }
        }

        private List<CellState> _RowCellState = new List<Model.CellState>();//left
        public List<CellState> RowCellState
        {
            get { return _RowCellState; }
        }

        private SquareList<CellState> _ConnectionState = new SquareList<CellState>();
        public SquareList<CellState> ConnectionState
        {
            get { return _ConnectionState; }
        }
        private int _SelectRow = 0;
        public int SelectRow
        {
            get { return _SelectRow; }
            set
            {
                if (value < 0 && value >= _RowCellState.Count)
                    return;
                if (_SelectRow >= 0 && _SelectRow < _RowCellState.Count)
                    _RowCellState[_SelectRow].IsSelect = false;
                _SelectRow = value;
                _RowCellState[_SelectRow].IsSelect = true;
            }
        }

        private int _SelectColumn = 0;
        public int SelectColumn
        {
            get { return _SelectColumn; }
            set
            {
                if (value < 0 && value >= _ColumnCellState.Count)
                    return;
                if (_SelectColumn >= 0 && _SelectColumn < _ColumnCellState.Count)
                    _ColumnCellState[_SelectColumn].IsSelect = false;
                _SelectColumn = value;
                _ColumnCellState[_SelectColumn].IsSelect = true;
            }
        }

        private Dictionary<int, int> RowDictionary = new Dictionary<int, int>();
        private Dictionary<int, int> ColumnDicitionary = new Dictionary<int, int>();

        private IntPoint _ConnectionCellPoint;
        public IntPoint ConnectionCellPoint
        {
            get { return _ConnectionCellPoint; }
            set
            {
                //if (_ConnectionCellPoint != null&&(_ConnectionCellPoint.X == value.X || _ConnectionCellPoint.Y == value.Y))
                //    _ConnectionState[_ConnectionCellPoint.X, _ConnectionCellPoint.Y].IsConnected = !_ConnectionState[_ConnectionCellPoint.X, _ConnectionCellPoint.Y].IsConnected;
                //_ConnectionCellPoint = value;
                //_ConnectionState[value.X, value.Y].IsConnected = true;
                int TempColumn, TempRow;
                bool A = RowDictionary.TryGetValue(value.X,out TempColumn);
                bool B = ColumnDicitionary.TryGetValue(value.Y, out TempRow);
                if (A && B)
                {
                    if (TempRow == value.X&&TempColumn==value.Y)
                    {
                        RowDictionary.Remove(value.X);
                        ColumnDicitionary.Remove(value.Y);
                        _ConnectionState[value.X, value.Y].IsConnected = false;
                    }
                    else
                    {
                        RowDictionary[value.X] = value.Y;
                        ColumnDicitionary[value.Y] = value.X;
                        RowDictionary.Remove(TempRow);
                        ColumnDicitionary.Remove(TempColumn);
                        _ConnectionState[value.X, TempColumn].IsConnected = false;
                        _ConnectionState[TempRow, value.Y].IsConnected = false;
                        _ConnectionState[value.X, value.Y].IsConnected = true;
                    }
                }
                if (A && !B)
                {
                    RowDictionary[value.X] = value.Y;
                    ColumnDicitionary.Remove(TempColumn);
                    ColumnDicitionary.Add(value.Y, value.X);
                    _ConnectionState[value.X, TempColumn].IsConnected = false;
                    _ConnectionState[value.X, value.Y].IsConnected = true;
                }
                if (!A && B)
                {
                    ColumnDicitionary[value.Y] = value.X;
                    RowDictionary.Remove(TempRow);
                    RowDictionary.Add(value.X, value.Y);
                    _ConnectionState[TempRow, value.Y].IsConnected = false;
                    _ConnectionState[value.X, value.Y].IsConnected = true;
                }
                if(!A&&!B)
                {
                    ColumnDicitionary.Add(value.Y, value.X);
                    RowDictionary.Add(value.X, value.Y);
                    _ConnectionState[value.X, value.Y].IsConnected = true;
                }
            }
        }

        public int RemoveIndex { get; set; }

        public ViewModel1()
        {
            _DataCollection.CollectionChanged += _DataCollection_CollectionChanged;
        }
        private int PositionHelperIndex = 0;

        private void _DataCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (!(item is DeviceModel))
                        continue;
                    DeviceModel device = item as DeviceModel;

                    _InnerDeviceList.Add(device);
                    _ConstructionHelper.Add(device);
                    foreach (DeviceInterface element in device.InterfaceList)
                    {
                        _ConstructionHelper.Add(element);
                    }
                    for (int i = 0; i < device.InterfaceCount + 1; i++)
                    {
                        _ColumnCellState.Add(new Model.CellState() { RowState = false, ColumnState = false });
                        _RowCellState.Add(new Model.CellState() { RowState = false, ColumnState = false });
                        _ConnectionState.Add(new CellState());
                        PositionHelper.Add(PositionHelperIndex++);
                    }
                }
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (!(item is DeviceModel))
                        continue;
                    DeviceModel device = item as DeviceModel;
                    int Index = _InnerDeviceList.IndexOf(device);
                    RemoveIndex = _ConstructionHelper.IndexOf(device);
                    for (int i = 0; i < device.InterfaceCount + 1; i++)
                    {
                        _ColumnCellState.RemoveAt(RemoveIndex);
                        _RowCellState.RemoveAt(RemoveIndex);
                        _ConstructionHelper.RemoveAt(RemoveIndex);
                        _PositionHelper.RemoveAt(RemoveIndex);
                    }
                    _ConnectionState.RemoveAtRange(RemoveIndex, device.InterfaceCount + 1);
                    _InnerDeviceList.RemoveAt(Index);
                }
            }
        }
    }
}

