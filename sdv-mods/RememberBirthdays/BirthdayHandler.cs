using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Characters;
using System;
using System.Collections.Generic;

namespace RememberBirthdays
{
    internal class BirthdayHandler
    { 
        private readonly IMonitor Monitor;
        private readonly ModConfig Config;
   
        List<NPC> villagers;
        public NPC birthdayNPC;
        public ClickableTextureComponent birthdayIcon;
        public bool birthdayToday = false;
        bool displayNPC = false;

        public BirthdayHandler(IMonitor monitor, ModConfig config)
        {
            Monitor = monitor;
            Config = config;

            if (Config.Icon == "NPC")
            {
                this.displayNPC = true;
            }

            villagers = GetVillagers();
            foreach (var npc in villagers)
            {
                this.birthdayToday = CheckBirthday(npc);
                if (birthdayToday)
                {
                    this.birthdayNPC = npc;
                    break;
                }
            }

        }

        public static bool CheckBirthday(NPC npc)
        {
            string season = Game1.currentSeason;
            int day = Game1.dayOfMonth;
            return npc.isBirthday(season, day);
        }

        internal ClickableTextureComponent BirthdayIcon(bool gifted)
        {
            int zoom = 2;
            int x = Game1.uiViewport.Width - 310;
            int y = 205;
            if (this.displayNPC)
            {
                y = 200;
            }
            Texture2D texture;
            Rectangle sourceRect;
            if (this.birthdayToday && !gifted)
            {
                if (this.displayNPC)
                {
                    texture = Game1.getCharacterFromName(this.birthdayNPC.Name, false).Sprite.Texture;
                    sourceRect = new Rectangle(0, 0, 16, 16);
                }
                else
                {
                    texture = Game1.mouseCursors;
                    sourceRect = new Rectangle(229, 410, 14, 14);
                }

                birthdayIcon = new ClickableTextureComponent(new Rectangle(x, y, 10 * zoom, 10 * zoom), texture, sourceRect, zoom);
                return birthdayIcon;

            }
            else
            {
                birthdayIcon = null;
                return birthdayIcon;
            }

        }

        /// Credit goes to GitHub user bouhm for the GetVillagers method
        /// https://github.com/bouhm/stardew-valley-mods/blob/main/NPCMapLocations/ModEntry.cs#L676
        private List<NPC> GetVillagers()
        {
            var villagers = new List<NPC>();

            foreach (var location in Game1.locations)
            {
                foreach (var npc in location.characters)
                {
                    bool shouldTrack =
                        npc != null
                        && (
                            npc.isVillager()
                            || (npc.Name.Equals("Dwarf") && Game1.player.canUnderstandDwarves)
                            || (npc.CanSocialize && Game1.player.friendshipData.ContainsKey(npc.Name))
                            || npc.isMarried()
                            || npc is Child
                        );

                    if (shouldTrack && !villagers.Contains(npc))
                        villagers.Add(npc);
                }
            }

            return villagers;
        }

    }
}
