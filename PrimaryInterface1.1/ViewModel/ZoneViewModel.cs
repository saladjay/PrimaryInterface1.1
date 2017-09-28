using PrimaryInterface1._1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryInterface1._1.ViewModel
{
    public class ZoneViewModel
    {
        private ObservableCollection<ZoneCellState> _ZoneCells = new ObservableCollection<ZoneCellState>();
        public ObservableCollection<ZoneCellState> ZoneCells
        {
            get { return _ZoneCells; }
            set
            {
                _ZoneCells = value;
            }
        }

    }
}
