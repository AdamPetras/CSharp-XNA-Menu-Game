using System;
using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class AboutItems : IDraw
    {
        public Items Selected { get; set; }
        protected List<Items> Items;
        protected Game Game;
        public AboutItems(Game game)
        {
            Game = game;
            Items = new List<Items>();
        }
        public void AddItem(string text, string value = "")
        {
            Vector2 posit = new Vector2(Game.width / 2, Game.height / 2 + Items.Count * Game.normalFont.MeasureString(text).Y); //určení pozice přidané položky
            Items item = new Items(text, posit, value);
            Items.Add(item);
        }

        public void Draw()
        {
            foreach (Items item in Items)
                Game.spriteBatch.DrawString(Game.normalFont, item.Text + "   " + item.Value, item.Position, Color.Red);
        }
    }
}
