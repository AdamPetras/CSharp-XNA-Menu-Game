using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class SaveLoad:IMenu
    {
        public Items Selected { get; set; }
        protected List<Items> Items;
        protected Game Game;
       public SaveLoad(Game game)
        {
           Game = game;
            Items = new List<Items>();
        }
        public void UpdateItem(string text, int i, string value = "")
        {
            Vector2 posit = new Vector2(Game.width / 2, Game.height / 2 + i * Game.bigFont.MeasureString(text).Y); //určení pozice upravené položky
            Items item = new Items(text, posit, value);
            Items.RemoveAt(i);
            Items.Insert(i, item);
        }

        public void AddItem(string text, string value = "")
        {
            Vector2 posit = new Vector2(Game.width / 2 - Game.bigFont.MeasureString(text).X / 2, Game.height / 2 + Items.Count * Game.bigFont.MeasureString(text).Y);  //určení pozice přidané položky
            Items item = new Items(text, posit);
            Items.Add(item);
        }

        public void Next()
        {
            int index = Items.IndexOf(Selected);
            Selected = index < Items.Count - 1 ? Items[index + 1] : Items[0];
        }

        public void Before()
        {
            int index = Items.IndexOf(Selected);
            Selected = index > 0 ? Items[index - 1] : Items[Items.Count - 1];
        }

        public void Draw()
        {
            Game.spriteBatch.Draw(Game.spritMenuBackground, new Vector2(0, 0), Color.White);
            foreach (Items item in Items)
            {
                Color color = item == Selected ? Color.Red : Color.White;
                Game.spriteBatch.DrawString(Game.bigFont, item.Text, new Vector2(item.Position.X, item.Position.Y - Items.Count * Game.bigFont.MeasureString(item.Text).Y / 2), color);
            }
        }
    }
}
