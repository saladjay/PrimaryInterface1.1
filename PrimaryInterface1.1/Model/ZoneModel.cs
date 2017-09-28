using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryInterface1._1.Model
{
    public class ZoneModel:NotificationObject
    {
        private int _OutputCount;
        public int OutputCount
        {
            get { return _OutputCount; }
            set
            {
                _OutputCount = value;
                this.RaisePropertyChanged("OutputCount");
            }
        }
    }

    public class ZoneCellState:NotificationObject
    {
        private bool _IsActived;
        public bool IsActived
        {
            get { return _IsActived; }
            set
            {
                _IsActived = value;
                this.RaisePropertyChanged("IsActived");
            }
        }
    }
}
