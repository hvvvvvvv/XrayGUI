using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XrayGUI.Modle
{
    internal class HotkeySettingObject
    {
        public bool Enable { get; set; } = true;
        public Hotkey Hotkey { get; set; } = new(KeyModifier.Alt, Key.F);
    }
}
