using System;
using System.Collections.Generic;
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

namespace PrimaryInterface1._1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty OpenCommandProperty =
            DependencyProperty.Register("OpenCommand", typeof(RoutedCommand), typeof(MainWindow), new PropertyMetadata(null));
        public RoutedCommand OpenCommand
        {
            get { return (RoutedCommand)GetValue(OpenCommandProperty); }
            set { SetValue(OpenCommandProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();
            //bind command
            this.OpenCommand = new RoutedCommand();
            var bin = new CommandBinding(OpenCommand);
            bin.Executed += Bin_Executed;
            this.CommandBindings.Add(bin);
        }

        private void Bin_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var btn = e.Source as Button;
            this.PageContext.Source = new Uri(btn.Tag.ToString(), UriKind.Relative);
        }
    }
}
