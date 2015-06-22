using System;
using System.Collections.Generic;
using System.Linq;
using Menu.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Menu.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class TheGame : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game game;
        private Track track;
        public TheGame(Game game)
            : base(game)
        {
            this.game = game;
            track = new Track(game);
        } 

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            track.GeneratingTrack();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
