using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentGameGraphics : DrawableGameComponent
    {
        private readonly GameGraphics gameGraphics;
        private readonly GameClass game;
        private readonly Camera camera;

        public ComponentGameGraphics(GameClass game,GameGraphics gameGraphics,Camera camera) : base(game)
        {
            this.game = game;
            this.gameGraphics = gameGraphics;
            this.camera = camera;
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
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
               camera.Transform);
            gameGraphics.DrawNonColiadableGraphics();
            gameGraphics.DrawColiadableGraphics();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
