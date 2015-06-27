using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    class SettingItems : IMenu
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

        public void AddItem(string text)
        {
            Vector2 posit = new Vector2(900, Game.height / 2 + items.Count * height);  //určení pozice přidané položky
            IItems setting = new Items(text, posit);
            items.Add((Items)setting); 
        }

        public void Draw()
        {
            foreach (Items setting in items)
            {
                Color color = Color.White;  //pokud je nějaky settings item aktivni, změní barvu na červenou
                if (setting == menu)
                    color = Color.Red;
                game.spriteBatch.DrawString(game.font, setting.Text, setting.Position, color);
            }
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
