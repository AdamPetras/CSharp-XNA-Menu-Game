using Menu.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Menu.Components
{
    public class Movement
    {
        public Vector2 position;
        private Game game;
        private Track track;
        public Movement(Game game, Track track)
        {
            this.game = game;
            this.track = track;
            position = new Vector2(0, 0);
        }

        public void Move()
        {
            if (game.keyState.IsKeyDown(Keys.A))
            {
                position.X--;
            }
            if (game.keyState.IsKeyDown(Keys.D))
                position.X++;
        }
        
        public void DrawPosition()
        {
            game.spriteBatch.DrawString(game.font, "X", new Vector2(position.X, track.GetTrackList()[(int)position.X].Y), Color.Red);
        }
    }
}
