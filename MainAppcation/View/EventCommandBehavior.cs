using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace NetProxyController.View
{
    public class EventCommandBehavior
    {
        public static readonly DependencyProperty DoubleClickProperty = DependencyProperty.RegisterAttached("DoubleClick",
            typeof(ICommand),
            typeof(EventCommandBehavior),
            new PropertyMetadata(null, DoubleClickChanged));

        public static ICommand GetDoubleClick(DependencyObject target)
        {
            return (ICommand)target.GetValue(DoubleClickProperty);
        }
        public static void SetDoubleClick(DependencyObject target, ICommand value)
        {
            target.SetValue(DoubleClickProperty, value);
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
            ICommand command = (ICommand)element.GetValue(DoubleClickProperty);
            command.Execute(null);
        }
    }
}
