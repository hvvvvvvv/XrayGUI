using XrayGUI.Modle;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayGUI.Handler
{
    internal class HotkeyHandler
    {
        private GlobalHotkey.GlobalHotkeyRegister register;
        private HotkeySettingObject _Setting;
        public bool IsPause { get; set; } = false;
        public event Action? HotkeyHappenedCallback;
        public HotkeyHandler()
        {
            register = new();
            _Setting = ConfigObject.Instance.HotkeySetting;
            LoadConfig();
        }
        public void LoadConfig()
        {
            register.RemoveAll();
            if(_Setting.Enable && _Setting.Hotkey.Key != Key.None)
            {
                register.Add(_Setting.Hotkey, RaiseEnvent);
            }
        }

        private void RaiseEnvent()
        {
            if (!IsPause)
            {
                HotkeyHappenedCallback?.Invoke();
            }
        }

        private static HotkeyHandler? _instance;
        public static HotkeyHandler Instance => _instance ??= new HotkeyHandler();

    }
}
