using System.Collections.Generic;
using Menu.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class Pause:IMenu
    {
        public Items Selected { get; set; }
        protected List<Items> items;
        protected Game game; 
        public Pause(Game game)
        {
            this.game = game;
            items = new List<Items>();
        }
        public void Draw()
        {
            game.spriteBatch.Draw(game.spritPauseBackground, new Vector2(0, 0), Color.White * 0.7f);
            foreach (Items item in items)
            {
                Color color = item == Selected ? Color.Red : Color.White;
                game.spriteBatch.DrawString(game.bigFont, item.Text, item.Position, color);
            }
        }

        public void AddItem(string text, string value = "")
        {
            Vector2 posit = new Vector2(Game.width / 2 - game.bigFont.MeasureString(text).X / 2, Game.height / 2 + items.Count * game.bigFont.MeasureString(text).Y);  //určení pozice přidané položky
            Items item = new Items(text, posit);
            items.Add(item); 
        }
        public void Next()
        {
            int index = items.IndexOf(Selected);
            Selected = index < items.Count - 1 ? items[index + 1] : items[0];
        }

        public void Before()
        {
            int index = items.IndexOf(Selected);
            Selected = index > 0 ? items[index - 1] : items[items.Count - 1];
        }
    }
}
