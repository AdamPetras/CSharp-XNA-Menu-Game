using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Menu.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.GameFolder.Classes
{
    public class SurfaceMove
    {
        private Vector2 position;
        private static Game game;
        private Movement movement;
        private Viewport viewport = game.graphics.GraphicsDevice.Viewport;
        public SurfaceMove(Movement movement)
        {
            position = new Vector2(Game.width/2,Game.height/2);
            this.movement = movement;
        }

        public void ScreenMove()
        {
            Vector2 screenCenter = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Vector2 imageCenter = new Vector2(game.spritBackground.Width / 2f, game.spritBackground.Height / 2f);
            game.spriteBatch.Draw(game.spritBackground, screenCenter, null, Color.White, 0f, imageCenter, 1f, SpriteEffects.None, 0f);
        }
    }
}
