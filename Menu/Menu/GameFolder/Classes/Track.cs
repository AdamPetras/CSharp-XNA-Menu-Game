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
        private const int value = 3; //čím větsí hodnota tím větší kopce
        private List<Vector2> trackList;

        public Track(Game game)
        {
            this.game = game;
            position = new Vector2(0,800);
            trackList = new List<Vector2>();
            rnd = new Random();
        }

        public void GeneratingTrack()
        {
            position.X+=3;
            int rand = rnd.Next(1, value);
            if (rand == 1)
            {
                position.Y++;
            }
            else
            {
                position.Y--;
            }
            trackList.Add(new Vector2(position.X,position.Y));
            game.spriteBatch.DrawString(game.font,"X",new Vector2(position.X,position.Y),Color.White);
        }

        public void DrawTrack()
        {
            foreach (Vector2 vec in trackList)
            {
                game.spriteBatch.DrawString(game.normalFont, "-", vec, Color.White);
            }
        }

        public List<Vector2> GetTrackList()
        {
            return trackList;
        }
    }
}
