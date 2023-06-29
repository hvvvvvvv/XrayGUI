﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Vanara.Extensions.Reflection;

namespace NetProxyController.View
{
    public class AttachProperties
    {
        #region DoubleClick
        public static readonly DependencyProperty DoubleClickProperty = DependencyProperty.RegisterAttached("DoubleClick",
            typeof(ICommand),
            typeof(AttachProperties),
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

        private static void element_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement element = (UIElement)sender;
            ICommand command = (ICommand)element.GetValue(DoubleClickProperty);
            command.Execute(null);
        }
        #endregion

        #region SelectionChangedCommand
        
        public static readonly DependencyProperty SelectionChangedCommandProperty = DependencyProperty.RegisterAttached("SelectionChangedCommand",
        typeof(ICommand), typeof(AttachProperties), new PropertyMetadata(null, SelectionChangedCommandChanged));
        public static ICommand GetSelectionChangedCommand(DependencyObject target)
        {
            return (ICommand)target.GetValue(SelectionChangedCommandProperty);
        }
        public static void SetSelectionChangedCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(DoubleClickProperty, value);
        }
        private static void SelectionChangedCommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target is Selector element)
            {
                if (element != null)
                {
                    if ((e.NewValue != null) && (e.OldValue == null))
                    {
                        element.SelectionChanged += Element_SelectionChanged;
                    }
                    else if ((e.NewValue == null) && (e.OldValue != null))
                    {
                        element.SelectionChanged -= Element_SelectionChanged;
                    }
                }
            }
        }
        private static void Element_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UIElement element = (UIElement)sender;
            ICommand command = (ICommand)element.GetValue(SelectionChangedCommandProperty);
            command.Execute(sender);
        }
        #endregion

        #region PreviewKeyDownCommand
        public static readonly DependencyProperty PreviewKeyDownCommandProperty = DependencyProperty.RegisterAttached("PreviewKeyDownCommand",
            typeof(ICommand), typeof(AttachProperties), new PropertyMetadata(null, PreviewKeyDownCommandChanged));
        public static ICommand GetPreviewKeyDownCommand(DependencyObject target)
        {
            return (ICommand)target.GetValue(PreviewKeyDownCommandProperty);
        }
        public static void SetPreviewKeyDownCommand(DependencyObject target, ICommand command)
        {
            target.SetValue(PreviewKeyDownCommandProperty, command);
        }
        public static void PreviewKeyDownCommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target is UIElement element)
            {
                if (element != null)
                {
                    if ((e.NewValue != null) && (e.OldValue == null))
                    {
                        element.PreviewKeyDown += Element_PreviewKeyDown;
                    }
                    else if ((e.NewValue == null) && (e.OldValue != null))
                    {
                        element.PreviewKeyDown -= Element_PreviewKeyDown;
                    }
                }
            }
        }
        private static void Element_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ICommand command =  (ICommand)((UIElement)sender).GetValue(PreviewKeyDownCommandProperty);
            command.Execute(e);
        }
        #endregion
    }


}
