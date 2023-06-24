using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;

namespace NetProxyController.View
{
    public class EventCommandBehavior : Control
    {
        public static DependencyProperty DoubleclickProperty = DependencyProperty.RegisterAttached("DoubleClick1",
            typeof(ICommand),
            typeof(EventCommandBehavior),
            new PropertyMetadata(new RelayCommand(() => _ = 1), DoubleClickChanged));

        public static ICommand GetDoubleClick1(DependencyObject target)
        {
            return (ICommand)target.GetValue(DoubleclickProperty);
        }
        public static void SetDoubleClick1(DependencyObject target, ICommand value)
        {
            target.SetValue(DoubleclickProperty, value);
        }

        private static void DoubleClickChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if(target is Control element)
            {
                if (element != null)
                {
                    if ((e.NewValue != null) && (e.OldValue == null))
                    {
                        element.MouseDoubleClick += element_MouseDoubleClick;
                    }
                    else if ((e.NewValue == null) && (e.OldValue != null))
                    {
                        element.MouseDoubleClick -= element_MouseDoubleClick;
                    }
                }

            }
            
        }

        static void element_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement element = (UIElement)sender;
            ICommand command = (ICommand)element.GetValue(DoubleclickProperty);
            command.Execute(null);
        }
    }
}
