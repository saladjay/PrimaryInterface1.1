using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrimaryInterface1._1.Controls.Core
{
    public class ConnectGridBase:ItemsControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(DeviceConnectView));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        //public static readonly DependencyPropertyKey TopItemPropertyKey = DependencyProperty.RegisterReadOnly("TopItem", typeof(object), typeof(DeviceConnectView), new PropertyMetadata(default(object)));
        //public static readonly DependencyProperty TopItemProperty = TopItemPropertyKey.DependencyProperty;
        //public object TopItem
        //{
        //    get { return (object)GetValue(TopItemPropertyKey.DependencyProperty); }
        //}

        //public static readonly DependencyPropertyKey LeftItemPropertyKey = DependencyProperty.RegisterReadOnly("LeftItem", typeof(object), typeof(DeviceConnectView), new PropertyMetadata(default(object)));
        //public static readonly DependencyProperty LeftItemProperty = LeftItemPropertyKey.DependencyProperty;
        //public object LeftItem
        //{
        //    get { return (object)GetValue(LeftItemPropertyKey.DependencyProperty); }
        //}

        //public static readonly DependencyPropertyKey ButtomItemPropertyKey = DependencyProperty.RegisterReadOnly("ButtomItem", typeof(object), typeof(DeviceConnectView), new PropertyMetadata(default(object)));
        //public static readonly DependencyProperty ButtomItemProperty = ButtomItemPropertyKey.DependencyProperty;
        //public object ButtomItem
        //{
        //    get { return (object)GetValue(ButtomItemPropertyKey.DependencyProperty); }
        //}

        public static readonly DependencyProperty TopItemProperty = DependencyProperty.Register("TopItem", typeof(object), typeof(DeviceConnectView), new PropertyMetadata(default(object)));
        public object TopItem
        {
            get { return (object)GetValue(TopItemProperty); }
            set { SetValue(TopItemProperty, value); }
        }

        public static readonly DependencyProperty LeftItemProperty = DependencyProperty.Register("LeftItem", typeof(object), typeof(DeviceConnectView), new PropertyMetadata(default(object)));
        public object LeftItem
        {
            get { return (object)GetValue(LeftItemProperty); }
            set { SetValue(LeftItemProperty, value); }
        }

        public static readonly DependencyProperty ButtomItemProperty = DependencyProperty.Register("ButtomItem", typeof(object), typeof(DeviceConnectView), new PropertyMetadata(default(object)));
        public object ButtomItem
        {
            get { return (object)GetValue(ButtomItemProperty); }
            set { SetValue(ButtomItemProperty, value); }
        }
    }
}
