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
    public class ComponentControls : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private ControlItems controlItems;
        private Game game;
        public ComponentControls(Game game)
            : base(game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            controlItems = new ControlItems(game);
            controlItems.AddItem("***MOVEMENT***");
            controlItems.AddItem("Brake-A");
            controlItems.AddItem("Throttle-D");
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            controlItems.DrawControls();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
