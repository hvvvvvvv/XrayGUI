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
    /// RouteRulesManager.xaml 的交互逻辑
    /// </summary>
    public partial class RouteRulesManager : Window
    {
        public RouteRulesManager()
        {
            InitializeComponent();
            Closing += (_, _) => IsClosed = true;
        }
        public bool IsClosed = false;
        public new void Show()
        {
            base.Show();
            Activate();
        }
        private static RouteRulesManager _instance = new RouteRulesManager();
        public static RouteRulesManager Instance
        {
            get
            {
                if (_instance.IsClosed)
                {
                    _instance = new RouteRulesManager();
                }
                return _instance;
            }
        }
    }
}
