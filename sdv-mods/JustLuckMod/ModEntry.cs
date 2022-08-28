using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;

namespace JustLuckMod
{
    public class ModEntry : Mod
    {
        string fortuneMessage;
        Color fortuneColor;
        ClickableTextureComponent luckIcon;
        LuckHUD luckHUD;
        bool hover; 
        

        public override void Entry(IModHelper helper)
        {
            luckHUD = new LuckHUD(helper);
            ModConfig Config = this.Helper.ReadConfig<ModConfig>();

            if (!Config.Disable)
            {
                helper.Events.GameLoop.DayStarted += this.OnDayStarted;
                helper.Events.Display.RenderingHud += this.OnRenderingHud;
                helper.Events.Input.CursorMoved += this.OnCursorMoved;
                helper.Events.Display.Rendered += this.OnRendered;
                helper.Events.Display.WindowResized += this.OnWindowResized;

            }

        }

        internal void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            fortuneMessage = luckHUD.GetFortune(Game1.player);
            fortuneColor = luckHUD.GetFortuneColor(Game1.player);
            this.Monitor.Log($"OnDayStarted luck for {Game1.player.Name}:\n{fortuneMessage}", LogLevel.Debug);
            luckIcon = luckHUD.GetLuckIcon();

        }

        internal void OnRenderingHud(object sender, RenderingHudEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            if (Context.IsPlayerFree)
            {
                luckIcon.draw(Game1.spriteBatch, fortuneColor, 1);
            }
        }

        internal void OnRendered(object sender, RenderedEventArgs e)
        {
            if (Context.IsPlayerFree && hover)
            {
                IClickableMenu.drawHoverText(Game1.spriteBatch, fortuneMessage, Game1.smallFont);
            }
        }

        internal void OnCursorMoved(object sender, CursorMovedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            if (Context.IsPlayerFree && luckIcon != null)
            {
                if (luckIcon.containsPoint(Game1.getMouseX(), Game1.getMouseY()))
                {
                    hover = true; 
                }
                else
                {
                    hover = false;
                }
            }
        }

        internal void OnWindowResized(object sender, WindowResizedEventArgs e)
        {
            luckIcon = luckHUD.GetLuckIcon();
        }

    }
}

