using System;
using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class AboutItems : DrawItems
    {
        private new int height;
        public AboutItems(Game game):base(game)
        {
            height = 18;
        }
        public new void AddItem(string text, string value = "")
        {
            Vector2 posit = new Vector2(Game.width / 2, Game.height / 2 + items.Count * height); //určení pozice přidané položky
            Items setting = new Items(text, posit, value);
            items.Add((Items)setting);
        }

        public new void Draw()
        {
            foreach (Items setting in items)
            {
                game.spriteBatch.DrawString(game.normalFont, setting.Text + "   " + setting.Value, setting.Position, Color.Red);
            }
        }
    }
}
