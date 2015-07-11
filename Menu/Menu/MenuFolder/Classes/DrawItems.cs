using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class DrawItems : IMenu
    {
        public Items selected { get; set; }
        protected List<Items> items;
        protected Game game;
        protected float height;
        public DrawItems(Game game)
        {
            height = 72;
            this.game = game;
            items = new List<Items>();
        }

        protected void AddItem(string text, string value = "")
        {
            Vector2 posit = new Vector2(Game.width / 2, Game.height / 2 + items.Count * height); //určení pozice přidané položky
            Items setting = new Items(text, posit, value);
            items.Add((Items)setting);
        }

        protected void Draw()
        {
            foreach (Items setting in items)
            {
                Color color = Color.White; //pokud je nějaky settings item aktivni, změní barvu na červenou
                if (setting == selected)
                    color = Color.Red;
                game.spriteBatch.DrawString(game.font, setting.Text + "   " + setting.Value, setting.Position, color);
            }
        }

        public void Next()
        {
            int index = items.IndexOf(selected);
            if (index < items.Count - 1)
                selected = items[index + 1];
            else
                selected = items[0];
        }

        public void Before()
        {
            int index = items.IndexOf(selected);
            if (index > 0)
                selected = items[index - 1];
            else
                selected = items[items.Count - 1];
        }
    }
}
