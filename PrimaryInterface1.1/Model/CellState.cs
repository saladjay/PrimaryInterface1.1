using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryInterface1._1.Model
{
    public class CellState:NotificationObject
    {
        private bool rowstate;
        public bool RowState
        {
            get { return rowstate; }
            set
            {
                rowstate = value;
                this.RaisePropertyChanged("RowState");
            }
        }

        private bool columnstate;
        public bool ColumnState
        {
            get { return columnstate; }
            set
            {
                columnstate = value;
                this.RaisePropertyChanged("ColumnState");
            }
        }

        private bool singlebool;
        public bool SingleBool
        {
            get { return singlebool; }
            set
            {
                singlebool = value;
                this.RaisePropertyChanged("SingleBool");
            }
        }

        private bool isseclect;
        public bool IsSelect
        {
            get { return isseclect; }
            set
            {
                isseclect = value;
                this.RaisePropertyChanged("IsSelect");
            }
        }
    }
}
