using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI.Utilities;

namespace FastFishPond
{
    internal class ModConfig
    {
        public int spawnTime { get; set; } = 1;
        public bool vanilla { get; set; } = true;
        public float multiplier { get; set; } = 1.5f;
    }
}