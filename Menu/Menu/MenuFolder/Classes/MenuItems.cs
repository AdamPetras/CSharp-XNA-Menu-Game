using System.Collections.Generic;
using System.IO;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class MenuItems : IMenu
    {
        public Items Selected { get; set; }
        protected List<Items> Items;
        protected Game Game; 

        public MenuItems(Game game)
        {
            Game = game;
            Items = new List<Items>();
        }
        public void AddItem(string text, string value = "")
        {
            Vector2 posit = new Vector2(100, Game.height / 2 + Items.Count * Game.bigFont.MeasureString(text).Y); //určení pozice přidané položky
            Items item = new Items(text, posit, value);
            Items.Add(item);
        }
        public void Draw()
        {
            foreach (Items item in Items)
            {
                Color color = item == Selected ? Color.Red : Color.White;
                Game.spriteBatch.DrawString(Game.bigFont, item.Text + "   " + item.Value, item.Position, color);
            }
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
    }
}

