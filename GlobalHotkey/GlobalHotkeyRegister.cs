using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.ComponentModel;
using Vanara.PInvoke;
using System.Text.Json.Serialization;
using System.Text;

namespace GlobalHotkey
{
    public sealed class GlobalHotkeyRegister : IDisposable
    {
        private const int WmHotkey = 0x0312;

        private System.Windows.Application _app;
        private readonly Dictionary<Hotkey, Action> _hotkeyActions;
        public delegate void HotkeyCollback(Hotkey parm);
        public event HotkeyCollback? HotkeyHappenEvent;
        public GlobalHotkeyRegister()
        {
            _hotkeyActions = new Dictionary<Hotkey, Action>();
            var startupTcs = new TaskCompletionSource<object>();
            ComponentDispatcher.ThreadPreprocessMessage += OnThreadPreProcessMessage;

            _app = System.Windows.Application.Current;
            _app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        public void Add(Hotkey hotkey, Action action)
        {
            _hotkeyActions.Add(hotkey, action);

            var keyModifier = (int)hotkey.KeyModifier;
            var key = KeyInterop.VirtualKeyFromKey(hotkey.Key);
            
            _app.Dispatcher.Invoke(() =>
            {
                if (!User32.RegisterHotKey(IntPtr.Zero, hotkey.GetHashCode(), (User32.HotKeyModifiers)keyModifier, (uint)key))
                {
                    _hotkeyActions.Remove(hotkey);
                    throw new Win32Exception(Marshal.GetLastWin32Error());                    
                }
            });
        }

        public void Remove(Hotkey hotkey)
        {
            if (_hotkeyActions.Remove(hotkey))
            {
                _app.Dispatcher.Invoke(() =>
                {
                    if (!User32.UnregisterHotKey(IntPtr.Zero, hotkey.GetHashCode()))
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                });
            }
        }
        public void RemoveAll()
        {
            foreach(var hotkey in _hotkeyActions.Keys)
            {
                Remove(hotkey);
            }
        }

        public bool Contains(Hotkey hotkey)
        {
            return _hotkeyActions.Keys.Contains(hotkey);
        }

        private void OnThreadPreProcessMessage(ref System.Windows.Interop.MSG msg, ref bool handled)
        {
            if (msg.message != WmHotkey)
                return;
            handled = true;
            var key = KeyInterop.KeyFromVirtualKey(((int)msg.lParam >> 16) & 0xFFFF);
            var keyModifier = (KeyModifier)((int)msg.lParam & 0xFFFF);
            var hotKey = new Hotkey(keyModifier, key);

            HotkeyHappenEvent?.Invoke(hotKey);
            if (_hotkeyActions.ContainsKey(hotKey))
            {
                _hotkeyActions[hotKey]();
            }
        }

        public void Dispose()
        {
            _app.Dispatcher.InvokeShutdown();
        }
    }
   [JsonConverter(typeof(HotkeyConverter))]
    public struct Hotkey
    {
        public Hotkey(KeyModifier keyModifier, Key key)
        {
            KeyModifier = keyModifier;
            Key = key;
        }

        public KeyModifier KeyModifier { get; }
        public Key Key { get; }
        public override string ToString()
        {
            if(KeyModifier == KeyModifier.None && Key == Key.None)
            {
                return string.Empty;
            }
            StringBuilder sb = new();
            if((KeyModifier & KeyModifier.Ctrl) == KeyModifier.Ctrl)
            {
                sb.Append("Ctrl+");
            }
            if ((KeyModifier & KeyModifier.Shift) == KeyModifier.Shift)
            {
                sb.Append("Shift+");
            }
            if ((KeyModifier & KeyModifier.Alt) == KeyModifier.Alt)
            {
                sb.Append("Alt+");
            }
            sb.Append(Key == Key.None ? string.Empty : Key);
            return sb.ToString();
        }
    }

    [Flags]
    public enum KeyModifier
    {
        None = 0x0000,
        Alt = 0x0001,
        Ctrl = 0x0002,
        Shift = 0x0004,
        Win = 0x0008,
        NoRepeat = 0x4000
    }
}