using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class ControlItems: IMenuItems
    {
        private Game game;
        private List<Items> items;
        private float height;
        public ControlItems(Game game)
        {
            this.game = game;
            height = 16;
            items = new List<Items>();
        }
        public void AddItem(string text)
        {
            Vector2 posit = new Vector2(950, Game.height / 2 + items.Count * height);  //určení pozice přidané položky
            IItems controls = new Items(text,posit);
            items.Add((Items) controls);        //vložení do listu
        }

        public void Draw() //výpis controls cyklem foreach tzn... vypíše všechny položky controls
        {
            foreach (Items controls in items)
            {
                Color color = Color.Red;
                game.spriteBatch.DrawString(game.normalFont, controls.Text, controls.Position, color);
            }
        }
    }
}
