using System;
using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class DrawItems : IDraw
    {
        public Items Selected { get; set; }
        protected List<Items> Items;
        protected Game Game;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public DrawItems(Game game)
        {
            Game = game;
            Items = new List<Items>();
        }
        /// <summary>
        /// Method to add items
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public void AddItem(string text, string value = "")
        {
            Vector2 position = new Vector2(Game.graphics.PreferredBackBufferWidth / 2, Game.graphics.PreferredBackBufferHeight / 2 + Items.Count * Game.normalFont.MeasureString(text).Y); //určení pozice přidané položky
            Items item = new Items(text, position, value);
            Items.Add(item);
        }
        /// <summary>
        /// Method to draw items
        /// </summary>
        public void Draw()
        {
            foreach (Items item in Items)
                Game.spriteBatch.DrawString(Game.smallFont, item.Text + "   " + item.Value, item.Position, Color.Red);
        }
    }
}
