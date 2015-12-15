using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentItem:DrawableGameComponent
    {
        private GameClass game;
        private Camera camera;
        public ComponentItem(GameClass game,Camera camera) : base(game)
        {
            this.game = game;
            this.camera = camera;
        }

        public override void Initialize()
        {
            game.itemList.Add(new Item("Lol", "xD", new Vector2(-100, 0), game.spritCar, game.spritCar));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                camera.Transform);
            foreach (Item item in game.itemList)
            {
                game.spriteBatch.Draw(item.TextureBackground, item.Position, Color.White);
            }
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
