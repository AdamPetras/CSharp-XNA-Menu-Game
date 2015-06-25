using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.GameFolder.Components
{
    public class ComponentCar : DrawableGameComponent
    {
        private Game game;
        private Car car;
        private Camera camera;
        public ComponentCar(Game game)
            : base(game)
        {
            this.game = game;

        }

        public override void Initialize()
        {
            car = new Car(game);
            camera = new Camera(car);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            car.Move(gameTime);
            camera.Update();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            game.spriteBatch.Draw(game.spritGameBackground, Vector2.Zero, Color.White);
            car.DrawCar();
            game.spriteBatch.DrawString(game.font, car.CurrentSpeed() + "\n" + car.ECar, new Vector2(400, 200), Color.White);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
