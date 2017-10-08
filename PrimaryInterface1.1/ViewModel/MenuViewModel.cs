using PrimaryInterface1._1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PrimaryInterface1._1.ViewModel
{
    public class MenuViewModel
    {
        private DelegateCommand _CreateNewControl = new DelegateCommand();
        private DelegateCommand _RemoveOldControl = new DelegateCommand();
        private Stack<UIElement> UI_Stack = new Stack<UIElement>();
        private ObservableCollection<UIElement> _ObjectList = new ObservableCollection<UIElement>();
        public ObservableCollection<UIElement> ObjectList
        {
            get { return _ObjectList; }
        }

        private Canvas _MyCanvas = null;
        public Canvas MyCanvas
        {
            get { return _MyCanvas; }
            set
            {
                _MyCanvas = value;
            }
        }

        private Stack<Rectangle> _stack = new Stack<Rectangle>();
        public Rectangle TopControl
        {
            get
            {
                _stack.Push(new Rectangle() { Width = 20, Height = 20, Fill = Brushes.Blue });
                return _stack.Peek();
            }
        }

        public DelegateCommand CreateNewControl
        {
            get { return _CreateNewControl; }
        }

        public DelegateCommand RemoveOldControl
        {
            get { return _RemoveOldControl; }
        }

        public MenuViewModel()
        {
            _CreateNewControl.ExecuteCommand += CreateNewControlInCanvas;
            _RemoveOldControl.ExecuteCommand += RemoveOldControlInCanvas;
            _RemoveOldControl.CanExecuteCommand += CanRemoveOldControlInCanvas;
            _ObjectList.CollectionChanged += _ObjectList_CollectionChanged;
        }

        private void _ObjectList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            _RemoveOldControl.RaiseCanExecuteChanged();
        }

        private void CreateNewControlInCanvas(object MyCanvas)
        {
            Debug.WriteLine(MyCanvas.ToString()+_stack.Count);
            if (!(MyCanvas is UIElement) && _MyCanvas == null&&(Canvas)MyCanvas == null)
                return;

            Button temp = new Button() { Width = 20, Height = 20, Background = Brushes.Blue };
            
            ((Canvas)MyCanvas).Children.Add(temp);
            Canvas.SetLeft(temp, 200);
            Canvas.SetTop(temp, 150);
            UI_Stack.Push(temp);
            ObjectList.Add(temp);
        }

        private void RemoveOldControlInCanvas(object MyCanvas)
        {
            if (!(MyCanvas is UIElement) && _MyCanvas == null && (Canvas)MyCanvas == null)
                return;
            ((Canvas)MyCanvas).Children.Remove(UI_Stack.Peek());
            ObjectList.Remove(UI_Stack.Pop());
        }

        private bool CanRemoveOldControlInCanvas(object MyCanvas)
        {
            if (MyCanvas == null || UI_Stack.Count == 0)
                return false;
            else
                return true;
        }
    }
}
