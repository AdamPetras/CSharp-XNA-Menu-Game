using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    internal class SettingItems
    {
        public Items menu { get; set; }
        private List<Items> items;
        private Game game;
        private float height;

        public SettingItems(Game game)
        {
            height = 72;
            this.game = game;
            items = new List<Items>();
        }

        public void AddItem(string text, string value = "")
        {
            Vector2 posit = new Vector2(Game.width/2, Game.height/2 + items.Count*height); //určení pozice přidané položky
            IItems setting = new Items(text, posit, value);
            items.Add((Items) setting);
        }

        public void Draw()
        {
            foreach (Items setting in items)
            {
                Color color = Color.White; //pokud je nějaky settings item aktivni, změní barvu na červenou
                if (setting == menu)
                    color = Color.Red;
                game.spriteBatch.DrawString(game.font, setting.Text + "   " + setting.Value, setting.Position, color);
            }
        }

        public void UpdateItem(string text, int i, string value = "")
        {
            Vector2 posit = new Vector2(Game.width / 2, Game.height / 2 + i * height); //určení pozice přidané položky
            IItems setting = new Items(text, posit, value);
            items.RemoveAt(i);
            items.Insert(i,(Items)setting);
        }

        public void Next()
        {
            int index = items.IndexOf(menu);
            if (index < items.Count - 1)
                menu = items[index + 1];
            else
                menu = items[0];
        }

        public void Before()
        {
            int index = items.IndexOf(menu);
            if (index > 0)
                menu = items[index - 1];
            else
                menu = items[items.Count - 1];
        }
    }
}
