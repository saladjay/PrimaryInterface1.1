using MS.Internal.PresentationFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrimaryInterface1._1.Controls.Core
{
    class ItemsSourceHelper
    {
    }

    public class ItemsGrid : ItemsControl
    {
        public ItemsGrid()
        {

        }

        public delegate void ItemsChangedHandler(object item, bool AddOrRemove);
        public event ItemsChangedHandler ExtendedItemsChanged;

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    ExtendedItemsChanged?.Invoke(item, true);
                }
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    ExtendedItemsChanged?.Invoke(item, false);
                }
            }
            base.OnItemsChanged(e);
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
        }
    }

    public class ExtendedItemsSourceComponent : DependencyObject
    {
        [CommonDependencyProperty]
        public static readonly DependencyProperty ExtendedItemsSourceProperty = DependencyProperty.Register("ExtendedItemsSource",
            typeof(IEnumerable), typeof(ExtendedItemsSourceComponent),
            new PropertyMetadata(((IEnumerable)null), new PropertyChangedCallback(OnExtendedItemsSourceChanged)));

        private static void OnExtendedItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExtendedItemsSourceComponent tempComponent = d as ExtendedItemsSourceComponent;
            IEnumerable oldValue = (IEnumerable)e.OldValue;
            IEnumerable newValue = (IEnumerable)e.NewValue;
            tempComponent.OnExtendedItemsSourceChanged(oldValue, newValue);
            tempComponent.ExtendedItemsSourceEvent?.Invoke(oldValue, newValue);
        }

        public delegate void ExtendedItemsSourceEventHandler(IEnumerable oldValue, IEnumerable newValue);
        public event ExtendedItemsSourceEventHandler ExtendedItemsSourceEvent;

        protected virtual void OnExtendedItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {

        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ExtendedItemsSourceProperty); }
            set
            {
                if (value == null)
                {
                    ClearValue(ExtendedItemsSourceProperty);
                }
                else
                {
                    SetValue(ExtendedItemsSourceProperty, value);
                }
            }
        }

        public ExtendedItemsSourceComponent()
        {

        }
    }

    public class AttachedItemsSourceComponent
    {
        public delegate void AttachedItemsSourceChangedEventHandler(IEnumerable oldValue, IEnumerable newValue);
        public event AttachedItemsSourceChangedEventHandler AttachedItemsSourceChanged;
        public delegate void AttachedItemsChangedHandler(object item, bool IsAdd);
        public event AttachedItemsChangedHandler AttachedItemsChanged;

        private ObservableCollection<object> _attachedItemsSource;
        public ObservableCollection<object> AttachedItemsSource
        {
            get { return _attachedItemsSource; }
            set
            {
                AttachedItemsSourceChanged?.Invoke(_attachedItemsSource, value);
                _attachedItemsSource = value;
            }
        }

        public AttachedItemsSourceComponent()
        {
            _attachedItemsSource.CollectionChanged += _attachedItemsSource_CollectionChanged;
        }

        private void _attachedItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems)
            {
                AttachedItemsChanged?.Invoke(item, true);
            }
            foreach (var item in e.OldItems)
            {
                AttachedItemsChanged?.Invoke(item, false);
            }
        }
    }

}
