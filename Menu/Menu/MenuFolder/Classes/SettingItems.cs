using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class SettingItems : IMenu
    {
        public Items Selected { get; set; }
        protected List<Items> items;
        protected Game game;

        public SettingItems(Game game)
        {
            this.game = game;
            items = new List<Items>();
        }
        public void UpdateItem(string text, int i, string value = "")
        {
            Vector2 posit = new Vector2(Game.width / 2, Game.height / 2 + i * game.bigFont.MeasureString(text).Y); //určení pozice přidané položky
            Items setting = new Items(text, posit, value);
            items.RemoveAt(i);
            items.Insert(i, setting);
        }
        public void AddItem(string text, string value = "")
        {
            Vector2 posit = new Vector2(Game.width / 2, Game.height / 2 + items.Count * game.bigFont.MeasureString(text).Y); //určení pozice přidané položky
            Items item = new Items(text, posit, value);
            items.Add(item);
        }

        public void Draw()
        {
            foreach (Items item in items)
            {
                Color color = item == Selected ? Color.Red : Color.White;
                game.spriteBatch.DrawString(game.bigFont, item.Text + "   " + item.Value, item.Position, color);
            }
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
