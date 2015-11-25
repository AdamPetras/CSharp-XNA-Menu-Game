using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.MenuFolder.Classes
{
    public class DrawItems : IDraw
    {
        public Items Selected { get; set; }
        private List<Items> Items;
        private GameClass Game;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public DrawItems(GameClass game)
        {
            Game = game;
            Items = new List<Items>();
        }
        /// <summary>
        /// Method to add items
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public void AddItem(string text, string value = "",bool nonClick = false)
        {
            Vector2 position = new Vector2(Game.graphics.PreferredBackBufferWidth / 2, Game.graphics.PreferredBackBufferHeight / 2 + Items.Count * Game.normalFont.MeasureString(text).Y); //určení pozice přidané položky
            Items item = new Items(text, position,Rectangle.Empty, value);
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
