using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using System;

namespace RememberBirthdays
{
    public class ModEntry : Mod
    {
        ClickableTextureComponent birthdayIcon = null;
        BirthdayHandler hbd;
        bool birthdayToday = false;
        bool gifted = false;
        bool hover = false;
        ModConfig Config;

        public override void Entry(IModHelper helper)
        {
            Config = this.Helper.ReadConfig<ModConfig>();
            if (!Config.Disable)
            {
                helper.Events.GameLoop.DayStarted += this.OnDayStarted;
                helper.Events.Display.RenderingHud += this.OnRenderingHUD;
                helper.Events.Display.WindowResized += this.OnWindowResized;
                helper.Events.Display.Rendered += this.OnRendered;
                helper.Events.Player.InventoryChanged += this.OnInventoryChanged;
                helper.Events.Input.CursorMoved += this.OnCursorMoved;
               
            }

        }

        internal void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            Config = this.Helper.ReadConfig<ModConfig>();
            this.hbd = new BirthdayHandler(this.Monitor, Config);
            this.birthdayToday = hbd.birthdayToday;
            this.birthdayIcon = this.hbd.BirthdayIcon(this.gifted);
            if (this.birthdayToday)
            {
                Monitor.Log($"{this.hbd.birthdayNPC.Name} has a birthday today.", LogLevel.Debug);
            }
        }

        internal void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
        {
            if (this.birthdayToday)
            {
                if (Game1.player.friendshipData.ContainsKey(this.hbd.birthdayNPC.Name))
                {
                    if (Game1.player.friendshipData[this.hbd.birthdayNPC.Name].LastGiftDate == new WorldDate(Game1.Date))
                    {
                        this.gifted = true;
                        this.birthdayIcon = this.hbd.BirthdayIcon(this.gifted);
                    }
                }
            }

        }

        internal void OnRenderingHUD(object sender, RenderingHudEventArgs e)
        {

            if (Context.IsPlayerFree)
            {
                if (this.birthdayIcon != null)
                {
                    this.birthdayIcon.draw(Game1.spriteBatch);
                }
                
            }

        }

        internal void OnRendered(object sender, RenderedEventArgs e)
        {
            if (Context.IsPlayerFree && hover)
            {
                IClickableMenu.drawHoverText(Game1.spriteBatch, this.hbd.birthdayNPC.Name, Game1.smallFont);
            }
        }

        internal void OnCursorMoved(object sender, CursorMovedEventArgs e)
        {
            if (Context.IsPlayerFree && this.birthdayIcon != null)
            {
                if (this.birthdayIcon.containsPoint(Game1.getMouseX(), Game1.getMouseY()))
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
            this.birthdayIcon = this.hbd.BirthdayIcon(this.gifted);
        }


    }
}
