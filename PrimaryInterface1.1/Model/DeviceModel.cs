using ExtendedString;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryInterface1._1.Model
{
    public class DeviceModel:NotificationObject
    {
        private string _deviceName;
        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                _deviceName = value;
                this.RaisePropertyChanged("DeviceName");
            }
        }

        private int _interfaceCount;
        public int InterfaceCount
        {
            get { return _interfaceCount; }
            set
            {
                _interfaceCount = value;
                this.RaisePropertyChanged("InterfaceCount");
            }
        }

        public ObservableCollection<DeviceInterface> InterfaceList { get; set; }

        public DeviceModel(string deviceName, int interfaceCount)
        {
            DeviceName = deviceName;
            InterfaceCount = interfaceCount;
            InterfaceList = new ObservableCollection<DeviceInterface>();
            for (int i = 0; i < InterfaceCount; i++)
            {
                InterfaceList?.Add(new DeviceInterface() { InterfaceName = string.Format("{0}", (1 + i)) });
            }
        }

        public class DeviceInterface
        {
            public string InterfaceName { get; set; }
        }
    }

    //public class DeviceList
    //{
    //    public static ObservableCollection<DeviceModel> ConnectDeviceList { get; set; }

    //    static DeviceList()
    //    {
    //        ConnectDeviceList = new ObservableCollection<DeviceModel>();
    //    }

    //    public static DeviceList GetInstance() => SingleTon<DeviceList>.GetInstance();
    //}



}

