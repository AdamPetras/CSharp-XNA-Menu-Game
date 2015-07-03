using System.Collections.Generic;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class AboutItems : IMenuItems
    {
        private List<Items> items;
        private Game game;
        private float height;
        public AboutItems(Game game)
        {
            height = 18;
            this.game = game;
            items = new List<Items>();
        }
        //přidání itemu do about
        public void AddItem(string text)
        {
            Vector2 posit = new Vector2(Game.width/2, Game.height / 2 + items.Count * height);  //určení pozice přidané položky
            IItems about = new Items(text, posit);
            items.Add((Items)about);        //vložení do listu
        }

        public void Draw()  //výpis about cyklem foreach tzn... vypíše všechny položky about
        {
            game.spriteBatch.Draw(game.spritAbout, new Vector2(Game.width/2-game.spritAbout.Width/3, Game.height/2-game.spritAbout.Height/3), Color.LightBlue * 0.3f);
            foreach (Items about in items)
            {
                game.spriteBatch.DrawString(game.normalFont, about.Text, about.Position,Color.Red);
            }
        }
    }
}
