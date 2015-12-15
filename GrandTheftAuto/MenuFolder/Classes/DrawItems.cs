using System.Collections.Generic;
using GrandTheftAuto.GameFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.MenuFolder.Classes
{
    public class DrawItems : IDraw
    {
        public Items Selected { get; set; }
        private List<Items> Items;
        private GameClass Game;
        private int id;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public DrawItems(GameClass game)
        {
            Game = game;
            Items = new List<Items>();
            id = 0;
        }

        /// <summary>
        /// Method to add items
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="spaceBeforeValue"></param>
        public void AddItem(string text, Vector2 position, SpriteFont font, bool centerText = true, string value = "", float rotation = 0f, int spaceBeforeValue = 0, bool nonClick = false, Camera camera = null)
        {
            position.Y += Items.Count * Game.normalFont.MeasureString(text).Y; //určení pozice přidané položky
            Items item = new Items(text, position, font, rotation,id++, centerText, Rectangle.Empty, value, false, spaceBeforeValue, camera);
            Items.Add(item);
        }
        /// <summary>
        /// Method to draw items
        /// </summary>
        public void Draw()
        {
            foreach (Items item in Items)
                Game.spriteBatch.DrawString(item.Font, item.Text + "   " + item.Value, item.ActualPosition, Color.Red, item.Rotation, item.StringOrigin, 1f, SpriteEffects.None, 0.5f);
        }
    }
}
