using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

namespace ProxyNotifyWindow
{
    /// <summary>
    /// NotifyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyWindow : Window
    {
        private WindowDataContext Context;
        private readonly double WindowOpacity;
        private List<CancellationTokenSource> tasksToken = new();
        public static readonly ImageSource StatusEnableImage = ChangeBitmapToImageSource(ImageSourceLib.enable);
        public static readonly ImageSource StatusDisableImage = ChangeBitmapToImageSource(ImageSourceLib.disable);
        public NotifyWindow(ImageSource proxyStatus,double windowOpacity = 0.8)
        {
            InitializeComponent();
            WindowOpacity = windowOpacity;
            Context = new(WindowOpacity,proxyStatus);
            DataContext = Context;           
        }
        public void ShowNotify()
        {
            var proxyStatus_ = Context.ProxyStatus == StatusDisableImage ? StatusEnableImage : StatusDisableImage;
            ShowNotify(proxyStatus_);           
            
        }
        public void ShowNotify(ImageSource proxyStatus)
        {
            var tokenSource = new CancellationTokenSource();
            foreach (var ts in tasksToken)
            {
                ts.Cancel();
            }
            tasksToken.Clear();
            tasksToken.Add(tokenSource);
            Task.Run(() => { ShowWind(tokenSource.Token,proxyStatus); }, tokenSource.Token);
        }
        private void ShowWind(CancellationToken token,ImageSource proxyStatus_)
        {
            Context.ProxyStatus = proxyStatus_;
            Context.Opacidy = WindowOpacity;
            try
            {
                Task.Delay(1500).Wait(token);
                while (Context.WindowVisibility == Visibility.Visible && !token.IsCancellationRequested)
                {
                    Context.Opacidy -= 0.05;
                    Task.Delay(100).Wait(token);
                }
            }
            catch(Exception ex)
            {
                if(ex is not OperationCanceledException)
                {
                    throw;
                }
            }
        }

        [DllImport("gdi32.dll", SetLastError = true)]

        private static extern bool DeleteObject(IntPtr hObject);
        /// <summary>
        /// 从bitmap转换成ImageSource
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        private static ImageSource ChangeBitmapToImageSource(Bitmap bitmap)
        {
            //Bitmap bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();
            ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            if (!DeleteObject(hBitmap))
            {
                throw new System.ComponentModel.Win32Exception();
            }
            return wpfBitmap;
        }
    }

    public class WindowDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private double _Opacity;
        private Visibility _WindowVisibility = Visibility.Hidden;
        public double Opacidy
        {
            get
            {
                return _Opacity;
            }
            set
            {
                _Opacity = value;
                OnOpacidyChanged();                
            }
        }
        public Visibility WindowVisibility 
        { 
            get 
            { 
                return _WindowVisibility; 
            } 
            set 
            {
                _WindowVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WindowVisibility"));                
            } 
        }

        private ImageSource ProxyStatus_;
        public ImageSource ProxyStatus
        {
            get { return ProxyStatus_; }
            set
            {
                ProxyStatus_ = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProxyStatus"));
            }
        }

        public WindowDataContext(double _Opacity,ImageSource proxyStatus)
        {
            this._Opacity = _Opacity;
            this.ProxyStatus_ = proxyStatus;
        }
        private void OnOpacidyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Opacidy"));
            if (_Opacity < 0.3)
            {
                WindowVisibility = Visibility.Hidden;
            }
            else
            {
                WindowVisibility = Visibility.Visible;
            }           
        }
    }
}
