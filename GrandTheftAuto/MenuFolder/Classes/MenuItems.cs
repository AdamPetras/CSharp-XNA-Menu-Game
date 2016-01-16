using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.GameFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.MenuFolder.Classes
{
    public class MenuItems : IMenu
    {
        public Items Selected { get; set; }
        public List<Items> Items { get; set; }
        private GameClass Game;
        private Keys[] Up;
        private Keys[] Down;
        private int id;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public MenuItems(GameClass game)
        {
            Game = game;
            Items = new List<Items>();
            id = 0;
        }
        public void AddItem(string text, Vector2 position, SpriteFont font, bool centerText = true, string value = "", float rotation = 0f, int spaceBeforeValue = 0, bool nonClick = false, Camera camera = null)
        {
            Vector2 origin = font.MeasureString(text) / 2;
            Items item = new Items(text, new Vector2(position.X, position.Y + (font.MeasureString(text).Y * Items.Count)), font, rotation, id++, centerText, new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y + (font.MeasureString(text).Y * Items.Count)), (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y), value, nonClick, spaceBeforeValue, camera);
            if (!Items.Exists(s => s.Text == item.Text && s.Value == item.Value))
            {
                Items.Add(item);
                Selected = Items.First();
            }
        }
        /// <summary>
        /// Method to draw items
        /// </summary>
        public void Draw()
        {
            foreach (Items item in Items)
            {
                Color color = item == Selected ? Color.Red : Color.White;
                Game.spriteBatch.DrawString(item.Font, item.Text, new Vector2(item.ActualPosition.X, item.ActualPosition.Y), color, item.Rotation, item.StringOrigin, 1f, SpriteEffects.None, 0.5f);
                Game.spriteBatch.DrawString(item.Font, item.Value, new Vector2(item.ActualPosition.X + item.Font.MeasureString(item.Text).X, item.ActualPosition.Y), color, item.Rotation, new Vector2(0, item.Font.MeasureString(item.Text).Y / 2), 1f, SpriteEffects.None, 0.5f);
            }
        }

        public void SetKeysUp(params Keys[] keys)
        {
            Up = new Keys[keys.Length];
            Up = keys;
        }
        public void SetKeysDown(params Keys[] keys)
        {
            Down = new Keys[keys.Length];
            Down = keys;
        }

        public void Moving()
        {
            if (Up.Any(s => Game.SingleClick(s)))
            {
                Before();
            }
            if (Down.Any(s => Game.SingleClick(s)))
            {
                Next();
            }
        }

        /// <summary>
        /// Method to set selected item + 1
        /// </summary>
        private void Next()
        {
            if (Items.Count != 0)
            {
                int index = Items.IndexOf(Selected);
                Selected = index < Items.Count - 1 ? Items[index + 1] : Items[0];
                Game.menuSound.Play();
            }
        }
        /// <summary>
        /// Method to set selected item - 1
        /// </summary>
        private void Before()
        {
            if (Items.Count != 0)
            {
                int index = Items.IndexOf(Selected);
                Selected = index > 0 ? Items[index - 1] : Items[Items.Count - 1];
                Game.menuSound.Play();
            }
        }
        /// <summary>
        /// Method to update item
        /// </summary>
        /// <param name="text"></param>
        /// <param name="i"></param>
        /// <param name="value"></param>
        public void UpdateItem(string text, int i, Vector2 position, SpriteFont font, bool centerText = true, string value = "", float rotation = 0f, int spaceBeforeValue = 0)
        {
            Items item = new Items(text, new Vector2(position.X, position.Y + font.MeasureString(text).Y * i), font, rotation, i + 1, centerText, new Rectangle((int)position.X, (int)position.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y), value, spaceBeforeValue: spaceBeforeValue);
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
                    if (item != Selected)   //pokud neni vybrám Item
                        Game.menuSound.Play();
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
            return Items.Any(item => item.Rectangle.Contains(Game.mouseState.Position) && !item.NonClick);
        }

        public void PositionIfCameraMoving(Vector2 offset)
        {
            foreach (Items item in Items)
            {
                item.ActualPosition = new Vector2(offset.X + item.DefaultPosition.X - item.StringLength.X / 2, offset.Y + item.DefaultPosition.Y + item.StringLength.Y);
            }
        }
    }
}