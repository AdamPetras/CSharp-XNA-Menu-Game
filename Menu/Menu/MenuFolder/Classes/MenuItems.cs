using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class MenuItems : DrawItems
    {
        private new float height;

        public MenuItems(Game game)
            : base(game)
        {
            height = 72;
        }
        public void UpdateItem(string text, int i, string value = "")
        {
            Vector2 posit = new Vector2(Game.width / 2, Game.height / 2 + i * height); //určení pozice přidané položky
            Items setting = new Items(text, posit, value);
            items.RemoveAt(i);
            items.Insert(i, (Items)setting);
        }
        public new void AddItem(string text, string value = "")
        {
            Vector2 posit = new Vector2(100, Game.height / 2 + items.Count * height); //určení pozice přidané položky
            Items setting = new Items(text, posit, value);
            items.Add((Items)setting);
        }

        public new void Draw()
        {
            foreach (Items setting in items)
            {
                Color color = Color.White; //pokud je nějaky settings item aktivni, změní barvu na červenou
                if (setting == selected)
                    color = Color.Red;
                game.spriteBatch.DrawString(game.font, setting.Text + "   " + setting.Value, setting.Position, color);
            }
        }
    }
}

