using System.Collections.Generic;
using Menu.Classes;
using Menu.Interface;
using Microsoft.Xna.Framework;

namespace Menu
{
    public class MenuItems:IMenu
    {
        public Items menu { get; set; }
        private List<Items> items;
        private Game game;
        private float height;
        public string text { get; set; }
        public Vector2 position { get; set; }

        public MenuItems(Game game)
        {
            height = 72;
            this.game = game;
            items = new List<Items>();
        }
        //přidání itemu do menu
        public void AddItem(string text)
        {
            Vector2 posit = new Vector2(100,Game.height/2+items.Count*height);  //určení pozice přidané položky
            I_Items menu = new Items(text,posit);
            items.Add((Items)menu);        //vložení do listu
        }

        public void Draw()  //výpis menu cyklem foreach tzn... vypíše všechny položky menu
        {
            foreach (Items menu in items)
            {
                Color color = Color.White;  //pokud je nějaky menu item aktivni, změní barvu na červenou
                if (menu == this.menu)
                    color = Color.Red;
                game.spriteBatch.DrawString(game.font, menu.Text, menu.Position, color);
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
