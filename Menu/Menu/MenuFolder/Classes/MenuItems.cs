using System.Collections.Generic;
using System.IO;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.MenuFolder.Classes
{
    public class MenuItems : IMenu
    {
        public Items Selected { get; set; }
        protected List<Items> Items;
        protected Game Game;
        private Vector2 position;
        private string posit;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public MenuItems(Game game, Vector2 position,string posit="left")
        {
            Game = game;
            this.position = position;
            this.posit = posit;
            Items = new List<Items>();
        }

        public void Position(string text)
        {
            if(posit == "left")
            position = new Vector2(position.X, position.Y + Game.bigFont.MeasureString(text).Y);
            else
                position = new Vector2(Game.graphics.PreferredBackBufferWidth / 2 - Game.bigFont.MeasureString(text).X / 2, Game.graphics.PreferredBackBufferHeight / 2 + Items.Count * Game.bigFont.MeasureString(text).Y);
        }

        /// <summary>
        /// Method to add items
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public void AddItem(string text, string value = "")
        {
            Position(text);
            Items item = new Items(text,position, value);
            Items.Add(item);
        }
        /// <summary>
        /// Method to draw items
        /// </summary>
        public void Draw()
        {
            foreach (Items item in Items)
            {
                Color color = item == Selected ? Color.Red : Color.White;
                Game.spriteBatch.DrawString(Game.bigFont, item.Text, new Vector2(item.Position.X, item.Position.Y - Items.Count * Game.bigFont.MeasureString(item.Text).Y / 2), color);
                Game.spriteBatch.DrawString(Game.bigFont, item.Value, new Vector2(item.Position.X + 400, item.Position.Y - Items.Count * Game.bigFont.MeasureString(item.Text).Y / 2), Color.White);
            }
        }
        /// <summary>
        /// Method to set selected item + 1
        /// </summary>
        public void Next()
        {
            int index = Items.IndexOf(Selected);
            Selected = index < Items.Count - 1 ? Items[index + 1] : Items[0];
        }
        /// <summary>
        /// Method to set selected item - 1
        /// </summary>
        public void Before()
        {
            int index = Items.IndexOf(Selected);
            Selected = index > 0 ? Items[index - 1] : Items[Items.Count - 1];
        }

        public void UpdateItem(string text, int i, string value = "")
        {
            Items item = new Items(text, new Vector2(position.X, Game.graphics.PreferredBackBufferHeight / 2 + Game.bigFont.MeasureString(text).Y * (i + 1)), value);
            Items.Insert(i, item);
            Items.RemoveAt(i + 1);
            Selected = item;
        }
    }
}

