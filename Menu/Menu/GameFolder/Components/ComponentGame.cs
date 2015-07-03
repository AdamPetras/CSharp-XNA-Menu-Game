using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.GameFolder.Components
{
    public class ComponentGame : DrawableGameComponent
    {
        private ComponentCar componentCar;
        private Camera camera;
        private Game game;
        public ComponentGame(Game game)
            : base(game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            componentCar = new ComponentCar(game);
            componentCar.Initialize();
            camera = new Camera(componentCar.car);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            componentCar.Update(gameTime);
            camera.Update();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            for (int i = 0; i <= game.spritGameBackground.Width*10; i += game.spritGameBackground.Width)
            {
                for (int j =0;j<=game.spritGameBackground.Height*10;j+=game.spritGameBackground.Height)
                game.spriteBatch.Draw(game.spritGameBackground, new Vector2(i, j), Color.White);
            }
            componentCar.Draw(gameTime);
            game.spriteBatch.DrawString(game.font, componentCar.car.CurrentSpeed() + "km/h\n" + componentCar.car.ECar, new Vector2(camera.centering.X, camera.centering.Y), Color.White);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}