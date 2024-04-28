using System;
using System.Collections.Generic;
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
using MaterialDesignThemes.Wpf;

namespace XrayGUI.View.Controls
{
    /// <summary>
    /// MenuItem.xaml 的交互逻辑
    /// </summary>
    public partial class MenuItem : System.Windows.Controls.MenuItem
    {
        public static readonly DependencyProperty IconKindProperty = DependencyProperty.Register("IconKind", typeof(PackIconKind), typeof(MenuItem), new PropertyMetadata(null));
        public static readonly DependencyProperty IconBrushProperty = DependencyProperty.Register("Text", typeof(string), typeof(MenuItem), new PropertyMetadata(string.Empty));
        public PackIconKind IconKind
        {
            get => (PackIconKind)GetValue(IconKindProperty);
            set => SetValue(IconKindProperty, value);
        }
        public string Text
        {
            get => (string)GetValue(IconBrushProperty);
            set => SetValue(IconBrushProperty, value);
        }
        public MenuItem()
        {
            InitializeComponent();
        }
    }
}
