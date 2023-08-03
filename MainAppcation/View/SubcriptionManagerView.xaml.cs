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
    /// SubcriptionManagerView.xaml 的交互逻辑
    /// </summary>
    public partial class SubcriptionManagerView : Window
    {
        public SubcriptionManagerView()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender is ListView view)
            {
                if(view.DataContext is SubcriptionManagerViewModle vm)
                {
                    vm.SelectedItems.Clear();
                    foreach(var i in view.SelectedItems)
                    {
                        if(i is SubcriptionItemViewModle item)
                        {
                            vm.SelectedItems.Add(item);
                        }
                    }
                }
            }
        }
    }
}
