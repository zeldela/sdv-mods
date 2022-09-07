using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI.Utilities;

namespace JustLuckMod
{
    internal class ModConfig
    {
        public bool Disable { get; set; } = false;
        public bool Fortune { get; set; } = true;
        public KeybindList Toggle { get; set; } = KeybindList.Parse("LeftShift + L");
        public bool Monochrome { get; set; } = false;
        public string Location { get; set; } = "HUD";
    }
}
