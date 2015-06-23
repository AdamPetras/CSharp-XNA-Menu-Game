using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.Classes
{
    public class Track
    {
        private Random rnd;
        private Vector2 position;
        private Game game;
        private List<Vector2> trackList;

        public Track(Game game)
        {
            this.game = game;
            position = new Vector2(0, Game.height / 2);
            trackList = new List<Vector2>();
            rnd = new Random();
        }

        private void GeneratingTrack(GameTime gameTime)
        {
            int offset = rnd.Next(0,10);
            position.X++;
            if (offset < 2)
                position.Y ++;
            else if (offset > 7)
                position.Y --;
            else
                position.Y += 0;
            trackList.Add(new Vector2(position.X, position.Y));
        }

        private void StartUp()
        {
            do
            {
                position.X++;
                trackList.Add(new Vector2(position.X, position.Y));
            } while (trackList.Count < Game.width / 2);
        }

        public void DrawTrack(GameTime gameTime)
        {
            StartUp();
            GeneratingTrack(gameTime);
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
