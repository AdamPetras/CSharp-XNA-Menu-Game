using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Menu
{
    public class MenuItems
    {
        private string text;
        public Menu menu;
        private List<Menu> items;
        private Game game;
        private float height;

        public MenuItems(Game game)
        {
            text = "";
            height = 72;
            this.game = game;
            items = new List<Menu>();
        }
        //přidání itemu do menu
        public void AddItem(string text)
        {
            Vector2 posit = new Vector2(100,Game.height/2+items.Count*height);  //určení pozice přidané položky
            Menu menu = new Menu(text,posit);
            items.Add(menu);        //vložení do listu
        }

        public void DrawMenu()  //výpis menu cyklem foreach tzn... vypíše všechny položky menu
        {
            foreach (Menu menu in items)
            {
                Color color = Color.White;  //pokud je nějaky menu item aktivni, změní barvu na červenou
                if (menu == this.menu)
                    color = Color.Red;
                game.spriteBatch.DrawString(game.font, menu.text, menu.position, color);
            }
        }
        public void Next()      //postupování v menu dolu
        {
            int index = items.IndexOf(menu);
            if (index < items.Count - 1)
                menu = items[index + 1];
            else
                menu = items[0];
        }
        public void Before()    //postupování v menu nahoru
        {
            int index = items.IndexOf(menu);
            if (index > 0)
                menu = items[index - 1];
            else
                menu = items[items.Count - 1];
        }
    }
}
