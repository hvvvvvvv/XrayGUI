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

namespace XrayGUI.View
{
    /// <summary>
    /// RouteRuleEditView.xaml 的交互逻辑
    /// </summary>
    public partial class RouteRuleEditView : Window
    {
        private static RouteRuleEditView? instance;
        public static RouteRuleEditView Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new();
                }
                return instance;
            }
        }

        public RouteRuleEditView()
        {
            InitializeComponent();
            Closing += (_,_) => instance = null;
        }
    }
}
