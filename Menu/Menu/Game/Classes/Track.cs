using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Menu.Classes
{
    public class Track
    {
        private Random rnd;
        private Vector2 position;
        private Game game;

        public Track(Game game)
        {
            this.game = game;
            position = new Vector2(0,800);
            rnd = new Random();
        }

        public void GeneratingTrack()
        {
            position.X++;
            int rand = rnd.Next(1, 3);
            if (rand == 1)
            {
                position.Y++;
            }
            else
            {
                position.Y--;
            }
            game.spriteBatch.DrawString(game.font,"X",new Vector2(position.X,position.Y),Color.White);
        }
    }
}
