using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrimaryInterface1._1.Controls
{
    /// <summary>
    /// 依照步驟 1a 或 1b 執行，然後執行步驟 2，以便在 XAML 檔中使用此自訂控制項。
    ///
    /// 步驟 1a) 於存在目前專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:PrimaryInterface1._1.Controls.Core"
    ///
    ///
    /// 步驟 1b) 於存在其他專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:PrimaryInterface1._1.Controls.Core;assembly=PrimaryInterface1._1.Controls.Core"
    ///
    /// 您還必須將 XAML 檔所在專案的專案參考加入
    /// 此專案並重建，以免發生編譯錯誤: 
    ///
    ///     在 [方案總管] 中以滑鼠右鍵按一下目標專案，並按一下
    ///     [加入參考]->[專案]->[瀏覽並選取此專案]
    ///
    ///
    /// 步驟 2)
    /// 開始使用 XAML 檔案中的控制項。
    ///
    ///     <MyNamespace:DragCanvas/>
    ///
    /// </summary>
    public class DragCanvas : Canvas
    {
        private bool _isDown;
        private bool _isDragging;
        private UIElement _originalElement;
        private double _originalLeft;
        private double _originalTop;
        private SimpleCircleAdorner _overlayElement;
        private Point _startPoint;
        private ObservableCollection<UIElement> _ItemsSource = null;
        public ObservableCollection<UIElement> ItemsSource
        {
            get { return _ItemsSource; }
        }
        public static readonly DependencyProperty CanvasItemsSourceProperty = DependencyProperty.Register("CanvasItemsSource", typeof(ObservableCollection<UIElement>), typeof(DragCanvas), new PropertyMetadata(null));
        public ObservableCollection<UIElement> CanvasItemsSource
        {
            get { return (ObservableCollection<UIElement>)GetValue(CanvasItemsSourceProperty); }
            set { SetValue(CanvasItemsSourceProperty, value); }
        }

        static DragCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DragCanvas), new FrameworkPropertyMetadata(typeof(DragCanvas)));
        }

        public DragCanvas()
        {
            //_ItemsSource.CollectionChanged += _ItemsSource_CollectionChanged;
        }

        private void _ItemsSource_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (UIElement item in e.NewItems)
            {
                this.Children.Add(item);
            }
            foreach (UIElement item in e.OldItems)
            {
                this.Children.Remove(item);
            }
        }


        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            if (_isDown)
            {
                DragFinished(false);
                e.Handled = true;
            }
        }

        private void DragFinished(bool cancelled)
        {
            Mouse.Capture(null);
            if (_isDragging)
            {
                AdornerLayer.GetAdornerLayer(_overlayElement.AdornedElement).Remove(_overlayElement);

                if (cancelled == false)
                {
                    Canvas.SetTop(_originalElement, _originalTop + _overlayElement.TopOffset);
                    Canvas.SetLeft(_originalElement, _originalLeft + _overlayElement.LeftOffset);
                }
                _overlayElement = null;
            }
            _isDragging = false;
            _isDown = false;
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isDown)
            {
                if ((_isDragging == false) &&
                    ((Math.Abs(e.GetPosition(this).X - _startPoint.X) >
                      SystemParameters.MinimumHorizontalDragDistance) ||
                     (Math.Abs(e.GetPosition(this).Y - _startPoint.Y) >
                      SystemParameters.MinimumVerticalDragDistance)))
                {
                    DragStarted();
                }
                if (_isDragging)
                {
                    DragMoved();
                }
            }
            base.OnMouseMove(e);
        }

        private void DragStarted()
        {
            _isDragging = true;
            _originalLeft = Canvas.GetLeft(_originalElement);
            _originalTop = Canvas.GetTop(_originalElement);

            _overlayElement = new SimpleCircleAdorner(_originalElement);
            var layer = AdornerLayer.GetAdornerLayer(_originalElement);
            layer.Add(_overlayElement);
        }

        private void DragMoved()
        {
            var currentPosition = Mouse.GetPosition(this);

            double _LeftOffset = currentPosition.X - _startPoint.X;
            double _TopOffset = currentPosition.Y - _startPoint.Y;
            //_LeftOffset = _LeftOffset < 0 ? 0 : _LeftOffset;
            //_LeftOffset = _LeftOffset > _myCanvas.ActualWidth ? _myCanvas.ActualWidth : _LeftOffset;
            //_TopOffset = _TopOffset < 0 ? 0 : _TopOffset;
            //_TopOffset = _TopOffset > _myCanvas.ActualHeight ? _myCanvas.ActualHeight : _TopOffset;
            _overlayElement.LeftOffset = _LeftOffset;
            _overlayElement.TopOffset = _TopOffset;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (e.Source == this)
            {
            }
            else
            {
                _isDown = true;
                _startPoint = e.GetPosition(this);
                _originalElement = e.Source as UIElement;
                this.CaptureMouse();
                e.Handled = true;
            }
            base.OnPreviewMouseLeftButtonDown(e);
        }
    }

    public class SimpleCircleAdorner : Adorner
    {
        private readonly Rectangle _child;
        private double _leftOffset;
        private double _topOffset;
        // Be sure to call the base class constructor.
        public SimpleCircleAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            var brush = new VisualBrush(adornedElement);

            _child = new Rectangle
            {
                Width = adornedElement.RenderSize.Width,
                Height = adornedElement.RenderSize.Height
            };


            var animation = new DoubleAnimation(0.3, 1, new Duration(TimeSpan.FromSeconds(1)))
            {
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            brush.BeginAnimation(Brush.OpacityProperty, animation);

            _child.Fill = brush;
        }

        protected override int VisualChildrenCount => 1;

        public double LeftOffset
        {
            get { return _leftOffset; }
            set
            {
                _leftOffset = value;
                UpdatePosition();
            }
        }

        public double TopOffset
        {
            get { return _topOffset; }
            set
            {
                _topOffset = value;
                UpdatePosition();
            }
        }

        // A common way to implement an adorner's rendering behavior is to override the OnRender
        // method, which is called by the layout subsystem as part of a rendering pass.
        protected override void OnRender(DrawingContext drawingContext)
        {
            // Get a rectangle that represents the desired size of the rendered element
            // after the rendering pass.  This will be used to draw at the corners of the 
            // adorned element.
            var adornedElementRect = new Rect(AdornedElement.DesiredSize);

            // Some arbitrary drawing implements.
            var renderBrush = new SolidColorBrush(Colors.Green) { Opacity = 0.2 };
            var renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
            const double renderRadius = 5.0;

            // Just draw a circle at each corner.
            drawingContext.DrawRectangle(renderBrush, renderPen, adornedElementRect);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius,
                renderRadius);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _child.Measure(constraint);
            return _child.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _child.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index) => _child;

        private void UpdatePosition()
        {
            var adornerLayer = Parent as AdornerLayer;
            adornerLayer?.Update(AdornedElement);
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            var result = new GeneralTransformGroup();
            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(_leftOffset, _topOffset));
            return result;
        }
    }

}
