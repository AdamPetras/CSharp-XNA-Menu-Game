using System.Collections.Generic;
using GrandTheftAuto.GameFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.MenuFolder.Classes
{
    public class MenuItems : IMenu
    {
        public Items Selected { get; set; }
        public List<Items> Items { get; set; }
        private GameClass Game;
        public Vector2 position;
        private SpriteFont font;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public MenuItems(GameClass game, Vector2 position, SpriteFont font)
        {
            Game = game;
            Items = new List<Items>();
            this.position = position;
            this.font = font;
        }

        /// <summary>
        /// Method to math position of item
        /// </summary>
        /// <param name="text"></param>
        /// <summary>
        /// Method to add items
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public void AddItem(string text, string value = "", bool nonClick = false)
        {
            Items item = new Items(text, new Vector2(position.X, position.Y + (font.MeasureString(text).Y * Items.Count)), new Rectangle((int)position.X, (int)(position.Y + (font.MeasureString(text).Y * Items.Count)), (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y), value, nonClick);
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

        public void Moving(Keys keyUp, Keys keyDown)
        {
            if (Game.SingleClick(keyUp))
            {
                Before();
            }
            if (Game.SingleClick(keyDown))
            {
                Next();
            }
        }

        /// <summary>
        /// Method to set selected item + 1
        /// </summary>
        private void Next()
        {
            int index = Items.IndexOf(Selected);
            Selected = index < Items.Count - 1 ? Items[index + 1] : Items[0];
        }
        /// <summary>
        /// Method to set selected item - 1
        /// </summary>
        private void Before()
        {
            int index = Items.IndexOf(Selected);
            Selected = index > 0 ? Items[index - 1] : Items[Items.Count - 1];
        }
        /// <summary>
        /// Method to update item
        /// </summary>
        /// <param name="text"></param>
        /// <param name="i"></param>
        /// <param name="value"></param>
        public void UpdateItem(string text, int i, string value = "")
        {
            Items item = new Items(text, new Vector2(position.X, Game.graphics.PreferredBackBufferHeight / 3 + font.MeasureString(text).Y * (i + 1)), new Rectangle((int)position.X, (int)(Game.graphics.PreferredBackBufferHeight / 3 + font.MeasureString(text).Y * (i + 1)), (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y), value);
            Items.Insert(i, item);
            Items.RemoveAt(i + 1);
            Selected = item;
        }
        /// <summary>
        /// Method to get if cursor has Colision
        /// </summary>
        public void CursorPosition()
        {
            foreach (Items item in Items)
            {
                if (item.Rectangle.Contains(Game.mouseState.Position) && !item.NonClick)
                {
                    CursorColision();
                    Selected = item;
                }
            }
        }
        /// <summary>
        /// Method to get if cursor Colision
        /// </summary>
        /// <returns></returns>
        public bool CursorColision()
        {
            foreach (Items item in Items)
            {
                if (item.Rectangle.Contains(Game.mouseState.Position) && !item.NonClick)
                {
                    return true;
                }
            }
            return false;
        }

        public void PositionIfCameraMoving(Camera camera, Vector2 defaultposition)
        {
            foreach (Items items in Items)
            {
                items.Position = new Vector2((camera.Centering.X + defaultposition.X - font.MeasureString(items.Text).X / 2), camera.Centering.Y + defaultposition.Y + font.MeasureString(items.Text).Y * Items.FindIndex(s => s.Text == items.Text));
            }
        }
    }
}

