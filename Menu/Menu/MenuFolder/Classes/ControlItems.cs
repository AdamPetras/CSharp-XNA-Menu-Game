using System;
using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class ControlItems : IDraw
    {
        public Items Selected { get; set; }
        protected List<Items> items;
        protected Game game;
        public ControlItems(Game game)
        {
            this.game = game;          
            items = new List<Items>();
        }

        public void AddItem(string text, string value = "")
        {
            Vector2 posit = new Vector2(Game.width / 2, Game.height / 2 + items.Count * game.normalFont.MeasureString(text).Y); //určení pozice přidané položky
            Items item = new Items(text, posit, value);
            items.Add(item);
        }

        public void Draw()
        {
            foreach (Items item in items)
            {
                game.spriteBatch.DrawString(game.normalFont, item.Text, item.Position, Color.Red);
                game.spriteBatch.DrawString(game.normalFont, item.Value, new Vector2(Game.width / 2 + 100,item.Position.Y), Color.Red);
            }
        }
    }
}

