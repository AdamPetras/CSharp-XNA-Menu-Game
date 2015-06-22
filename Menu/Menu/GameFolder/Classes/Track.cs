using System;
using System.Collections.Generic;
using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;

namespace Menu.Classes
{
    public class Track
    {
        private Random rnd;
        private Vector2 position;
        private Game game;
        private List<Vector2> trackList;
        private Camera camera;

        public Track(Game game,Camera camera)
        {
            this.game = game;
            this.camera = camera;
            position = new Vector2(0, Game.height/2);
            trackList = new List<Vector2>();
            rnd = new Random();
        }

        public void GeneratingTrack()
        {
            do
            {
                position.X++;
                double rand = rnd.NextDouble();
                if (rand <= 0.5)
                {
                    position.Y++;
                }
                else if (rand >= 0.5)
                {
                    position.Y--;
                }
                trackList.Add(new Vector2(position.X, position.Y));
            } while (trackList.Count < Game.width);
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
