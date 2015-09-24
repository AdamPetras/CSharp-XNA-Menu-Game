using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.MenuFolder.Classes
{
    public class MenuItems : IMenu
    {
        public Items Selected { get; set; }
        private List<Items> Items;
        private Game Game;
        private Vector2 position;
        private string posit;
        private SpriteFont font;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public MenuItems(Game game, Vector2 position,SpriteFont font,string posit="left")
        {
            Game = game;
            this.position = position;
            this.posit = posit;
            this.font = font;
            Items = new List<Items>(); 
        }

        public void Position(string text)
        {
            if(posit == "left")
            position = new Vector2(position.X, position.Y + font.MeasureString(text).Y);
            else
                position = new Vector2(Game.graphics.PreferredBackBufferWidth / 2 - font.MeasureString(text).X / 2, Game.graphics.PreferredBackBufferHeight / 3 + Items.Count * font.MeasureString(text).Y);
        }

        /// <summary>
        /// Method to add items
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public void AddItem(string text, string value = "")
        {
            Position(text);
            Items item = new Items(text,position,new Rectangle((int)position.X,(int)(position.Y),(int)font.MeasureString(text).X,(int)font.MeasureString(text).Y), value);            
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
                Game.spriteBatch.DrawString(font, item.Text, new Vector2(item.Position.X, item.Position.Y), color);
                Game.spriteBatch.DrawString(font, item.Value, new Vector2(item.Position.X + 500, item.Position.Y), Color.White);
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
            Items item = new Items(text, new Vector2(position.X, Game.graphics.PreferredBackBufferHeight / 3 + font.MeasureString(text).Y * (i + 1)), new Rectangle((int)position.X, (int)(Game.graphics.PreferredBackBufferHeight / 3 + font.MeasureString(text).Y * (i + 1)), (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y), value);
            Items.Insert(i, item);
            Items.RemoveAt(i + 1);
            Selected = item;
        }
        public void CursorPosition()
        {
            foreach (Items item in Items)
            {
                if (item.Rectangle.Contains(new Point(Game.mouseState.X, Game.mouseState.Y)))
                {
                    CursorColision();
                    Selected = item;
                }
            }
        }
        public bool CursorColision()
        {
            foreach (Items item in Items)
            {
                if (item.Rectangle.Contains(new Point(Game.mouseState.X, Game.mouseState.Y)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

