using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Menu.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class Pause:IMenu
    {
        public Items selected { get; set; }
        private Game game;
        private Vector2 pauseMenuPosition;
        private List<Items> items; 
        public Pause(Game game)
        {
            this.game = game;
            pauseMenuPosition = new Vector2(Game.width / 2 - game.spritPauseMenu.Width / 2, Game.height / 2 - game.spritPauseMenu.Height / 2);
            items = new List<Items>();
        }

        public void AddItem(string text)
        {
            Vector2 posit = new Vector2(Game.width / 2 - game.font.MeasureString(text).X / 2, Game.height / 2 + items.Count * game.font.MeasureString(text).Y);  //určení pozice přidané položky
            Items controls = new Items(text, posit);
            items.Add((Items)controls); 
        }

        public void Draw()
        {
            game.spriteBatch.Draw(game.spritPauseBackground,new Vector2(0,0),Color.White *0.7f );
            game.spriteBatch.Draw(game.spritPauseMenu, pauseMenuPosition, Color.White );
            foreach (Items item in items)
            {
                Color color = Color.Red;
                game.spriteBatch.DrawString(game.font, item.Text, item.Position, color);
            }
        }
        public void Next()
        {
            throw new NotImplementedException();
        }

        public void Before()
        {
            throw new NotImplementedException();
        }
    }
}
