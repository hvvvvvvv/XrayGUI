using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Vanara.Extensions.Reflection;
using ProxyNotifyWindow;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Interop;

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
            if(target is System.Windows.Controls.Control element)
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
        private static void Element_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ICommand command =  (ICommand)((UIElement)sender).GetValue(PreviewKeyDownCommandProperty);
            command.Execute(e);
        }
        #endregion

        #region WidthByScreenRatio & HeightByScreenRatio
        #region 窗口宽度比例
        /// <summary>
        /// 窗口宽度比例 单位:小数(0 - 1.0]
        /// <para>窗口实际宽度=使用屏幕可显示区域（屏幕高度-任务栏高度）* 窗口宽度比例</para>
        /// </summary>
        public static readonly DependencyProperty WidthByScreenRatioProperty = DependencyProperty.RegisterAttached(
            "WidthByScreenRatio", typeof(double), typeof(AttachProperties), new PropertyMetadata(1.0, OnWidthByScreenRatioPropertyChanged));

        private static void OnWidthByScreenRatioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && e.NewValue is double widthByScreenRatio)
            {
                if (widthByScreenRatio <= 0 || widthByScreenRatio > 1)
                {
                    throw new ArgumentException($"屏幕比例不支持{widthByScreenRatio}");
                }

                var screenDisplayArea = GetScreenSize(window);
                var screenRatioWidth = screenDisplayArea.Width * widthByScreenRatio;

                if (!double.IsNaN(window.MaxWidth) && screenRatioWidth > window.MaxWidth)
                {
                    window.Width = window.MaxWidth;
                }
                else if (!double.IsNaN(window.MinWidth) && screenRatioWidth < window.MinWidth)
                {
                    window.Width = screenDisplayArea.Width;
                }
                else
                {
                    window.Width = screenRatioWidth;
                }
            }
        }

        public static void SetWidthByScreenRatio(DependencyObject element, double value)
        {
            element.SetValue(WidthByScreenRatioProperty, value);
        }

        public static double GetWidthByScreenRatio(DependencyObject element)
        {
            return (double)element.GetValue(WidthByScreenRatioProperty);
        }
        #endregion

        #region 窗口高度比例
        /// <summary>
        /// 窗口宽度比例 单位:小数(0 - 1.0]
        /// <para>窗口实际宽度=使用屏幕可显示区域（屏幕高度-任务栏高度）* 窗口宽度比例</para>
        /// </summary>
        public static readonly DependencyProperty HeightByScreenRatioProperty = DependencyProperty.RegisterAttached(
            "HeightByScreenRatio", typeof(double), typeof(AttachProperties), new PropertyMetadata(1.0, OnHeightByScreenRatioPropertyChanged));

        private static void OnHeightByScreenRatioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && e.NewValue is double heightByScreenRatio)
            {
                if (heightByScreenRatio <= 0 || heightByScreenRatio > 1)
                {
                    throw new ArgumentException($"屏幕比例不支持{heightByScreenRatio}");
                }

                var screenDisplayArea = GetScreenSize(window);
                var screenRatioHeight = screenDisplayArea.Height * heightByScreenRatio;

                if (!double.IsNaN(window.MaxHeight) && screenRatioHeight > window.MaxHeight)
                {
                    window.Height = window.MaxHeight;
                }
                else if (!double.IsNaN(window.MinHeight) && screenRatioHeight < window.MinHeight)
                {
                    window.Height = screenDisplayArea.Height;
                }
                else
                {
                    window.Height = screenRatioHeight;
                }
            }
        }

        public static void SetHeightByScreenRatio(DependencyObject element, double value)
        {
            element.SetValue(HeightByScreenRatioProperty, value);
        }

        public static double GetHeightByScreenRatio(DependencyObject element)
        {
            return (double)element.GetValue(HeightByScreenRatioProperty);
        }
        #endregion

        public static readonly DependencyProperty TopByScreenRatioProprety = DependencyProperty.RegisterAttached(
            "TopBySreenRatio", typeof(double), typeof(AttachProperties), new PropertyMetadata(0.0, OnTopByScreenRatioPropretyChanged));
        public static void OnTopByScreenRatioPropretyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && e.NewValue is double topByRatio)
            {
                window.Top = GetScreenSize(window).Height * topByRatio;
            }
        }

        public static void SetTopBySreenRatio(DependencyObject element, double value)
        {
            element.SetValue(TopByScreenRatioProprety, value);
        }

        public static double GetTopBySreenRatio(DependencyObject element)
        {
            return (double)element.GetValue(TopByScreenRatioProprety);
        }

        public static readonly DependencyProperty LeftByScreenRatioProprety = DependencyProperty.RegisterAttached(
            "LeftBySreenRatio", typeof(double), typeof(WindowAdaptation), new PropertyMetadata(0.0, OnLeftByScreenRatioPropretyChanged));
        public static void OnLeftByScreenRatioPropretyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && e.NewValue is double LeftByRatio)
            {
                window.Left = GetScreenSize(window).Width * LeftByRatio;
            }
        }

        public static void SetLeftBySreenRatio(DependencyObject element, double value)
        {
            element.SetValue(LeftByScreenRatioProprety, value);
        }

        public static double GetLeftBySreenRatio(DependencyObject element)
        {
            return (double)element.GetValue(LeftByScreenRatioProprety);
        }

        const int DpiPercent = 96;
        private static dynamic GetScreenSize(Window window)
        {
            var intPtr = new WindowInteropHelper(window).Handle;//获取当前窗口的句柄
            var screen = Screen.FromHandle(intPtr);//获取当前屏幕
            using (Graphics currentGraphics = Graphics.FromHwnd(intPtr))
            {
                //分别获取当前屏幕X/Y方向的DPI
                double dpiXRatio = currentGraphics.DpiX / DpiPercent;
                double dpiYRatio = currentGraphics.DpiY / DpiPercent;
                var width = screen.WorkingArea.Width / dpiXRatio;
                var height = screen.WorkingArea.Height / dpiYRatio;

                return new { Width = width, Height = height };
            }
        }
        #endregion
    }


}
