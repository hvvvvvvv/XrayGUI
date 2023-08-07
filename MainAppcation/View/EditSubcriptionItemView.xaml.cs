using NetProxyController.Modle;
using NetProxyController.ViewModle;
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
using System.Windows.Shapes;

namespace NetProxyController.View
{
    /// <summary>
    /// EditSubcriptionItemView.xaml 的交互逻辑
    /// </summary>
    internal partial class EditSubcriptionItemView : Window
    {
        public EditSubcriptionItemView(SubscriptionItem subItem)
        {
            DataContext = new EditSubcriptionItemViewMdole(subItem);
            InitializeComponent();
        }
        public EditSubcriptionItemView() : this(new())
        {
           
        }

    }
}
