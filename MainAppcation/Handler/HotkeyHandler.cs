using NetProxyController.Modle;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.Handler
{
    internal class HotkeyHandler
    {
        private GlobalHotkey.GlobalHotkeyRegister register;
        private HotkeySettingObject _Setting;
        public bool IsPause { get; set; } = false;
        public event Action? HotkeyHappenedCallback;
        public HotkeyHandler(HotkeySettingObject setting)
        {
            register = new();
            _Setting= setting;
            Load();
        }
        public void Load()
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



    }
}
