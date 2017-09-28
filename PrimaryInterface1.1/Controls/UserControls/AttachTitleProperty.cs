using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrimaryInterface1._1.Controls.UserControls
{
    class AttachTitleProperty : DependencyObject
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached("Title", typeof(string), typeof(AttachTitleProperty));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static void SetTitle(DependencyObject obj, string value)
        {
            obj.SetValue(TitleProperty, value);
        }

        public static string GetTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(TitleProperty);
        }
    }
}
