using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu.GameFolder.Components
{
    public class ComponentCar : DrawableGameComponent
    {
        private Game game;
        private Car car;
        private Camera camera;
        private ComponentPause componentPause;
        public ComponentCar(Game game,ComponentPause componentPause)
            : base(game)
        {
            this.game = game;
            this.componentPause = componentPause;
        }
        public override void Initialize()
        {
            car = new Car(game);
            camera = new Camera();
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            camera.Update(car.position);
            car.Move(gameTime);
            if (game.SingleClick(Keys.Escape))
            {
                componentPause = new ComponentPause(game, this);
                Game.Components.Add(componentPause);
                Enabled = false;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            for (int i = 0; i <= game.spritGameBackground.Width * 10; i += game.spritGameBackground.Width)
            {
                for (int j = 0; j <= game.spritGameBackground.Height * 10; j += game.spritGameBackground.Height)
                    game.spriteBatch.Draw(game.spritGameBackground, new Vector2(i, j), Color.White);
            }
            game.spriteBatch.DrawString(game.font, car.CurrentSpeed() + "km/h\n" + car.GetCarState(), new Vector2(camera.centering.X, camera.centering.Y), Color.White);
            car.DrawCar();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
