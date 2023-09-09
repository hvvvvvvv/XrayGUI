using XrayGUI.Modle;
using System;
using System.Windows.Input;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanara.PInvoke;
using System.Windows.Interop;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace XrayGUI.Handler
{
    internal class HotkeyHandler
    {
        private const int WmHotkey = 0x0312;
        public bool IsPause { get; set; } = false;
        private Hotkey _hotKey;
        public event Action? HotkeyHappenedCallback;
        public HotkeyHandler()
        {
            ComponentDispatcher.ThreadPreprocessMessage += OnThreadPreProcessMessage;
            LoadConfig();            
        }
        public void LoadConfig()
        {
            _hotKey = ConfigObject.Instance.HotkeySetting.Hotkey;
            if (_hotKey.Key == Key.None) return;
            try
            {
                UnRegisterHotKey(_hotKey);
            }
            catch(Win32Exception ex) when(ex.NativeErrorCode == 1419) { }
            RegisterHotKey(_hotKey);
        }
        private static void RegisterHotKey(Hotkey hotkey)
        {
            var keyModifier = (int)hotkey.KeyModifier;
            var key = KeyInterop.VirtualKeyFromKey(hotkey.Key);
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (!User32.RegisterHotKey(IntPtr.Zero, hotkey.GetHashCode(), (User32.HotKeyModifiers)keyModifier, (uint)key))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            });
        }
        private static void UnRegisterHotKey(Hotkey hotkey)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (!User32.UnregisterHotKey(IntPtr.Zero, hotkey.GetHashCode()))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            });
        }

        private void OnThreadPreProcessMessage(ref System.Windows.Interop.MSG msg, ref bool handled)
        {
            if (msg.message != WmHotkey || IsPause)
                return;
            handled = true;
            var key = KeyInterop.KeyFromVirtualKey(((int)msg.lParam >> 16) & 0xFFFF);
            var keyModifier = (KeyModifier)((int)msg.lParam & 0xFFFF);
            var hotKey = new Hotkey(keyModifier, key);
            if (hotKey.Equals(_hotKey))
            {
                HotkeyHappenedCallback?.Invoke();
            }
        }

        private static HotkeyHandler? _instance;
        public static HotkeyHandler Instance => _instance ??= new HotkeyHandler();

    }
}
