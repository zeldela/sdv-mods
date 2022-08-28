using Microsoft.Xna.Framework;
using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using System.Collections.Generic;
using System.Collections;

namespace JustLuckMod
{
    internal class LuckHUD      
    {
        private readonly IModHelper helper;

        public LuckHUD(IModHelper helper)
        {
            this.helper = helper;
        }

        internal string GetFortune(Farmer who)
        {
            double dailyLuck = who.DailyLuck;
            string fortuneID;

            if (dailyLuck < -0.07)
            {
                fortuneID = "13192";
            }
            else if (dailyLuck < -0.02)
            {
                fortuneID = "13193";
            }
            else if (dailyLuck == 0)
            {
                fortuneID = "13201";
            }
            else if (dailyLuck <= 0.02)
            {
                fortuneID = "13200";
            }
            else if (dailyLuck <= 0.07)
            {
                fortuneID = "13199";
            }
            else
            {
                fortuneID = "13198";
            }


            Dictionary<string, string> TV = helper.GameContent.Load<Dictionary<string, string>>("Strings/StringsFromCSFiles");
            string fortuneTV = TV[$"TV.cs.{fortuneID}"];
            string[] delimiterChars = { "! ", "!", ". ", "." };
            string[] words = fortuneTV.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
            string fortune = "";

            foreach (var word in words)
            {
                fortune = $"{fortune}{word}.\n";
            }

            double fortuneLuck = Math.Truncate((dailyLuck * 100 * 100)) / 100;

            if (dailyLuck < 0)
            {
                fortune = $"{fortune}Fortune: {fortuneLuck}%.";
            }
            else
            {
                fortune = $"{fortune}Fortune: +{fortuneLuck}%.";
            }

            return fortune;
        }

        internal Color GetFortuneColor(Farmer who)
        {
            double dailyLuck = who.DailyLuck;
            if (dailyLuck < -0.03)
            {
                return new Color(222, 161, 144, 255);
            }
            else if (dailyLuck > 0.03)
            {
                return new Color(130, 190, 145, 255);
            }
            else
            {
                return Color.White;
            }

        }

        internal ClickableTextureComponent GetLuckIcon()
        {
            int zoom = 3;
            int x = Game1.uiViewport.Width - 300;
            int y = 165;
            ClickableTextureComponent luckIcon = new ClickableTextureComponent(
                    new Rectangle(x, y, 10 * zoom, 10 * zoom),
                    Game1.mouseCursors,
                    new Rectangle(381, 361, 10, 10),
                    zoom);
            return luckIcon;
        }

    }
}

