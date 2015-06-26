using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu.GameFolder.Components
{
    public class ComponentCar : DrawableGameComponent
    {
        private Game game;
        public Car car;
        public ComponentCar(Game game)
            : base(game)
        {
            this.game = game;

        }

        public override void Initialize()
        {
            car = new Car(game);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            car.Move(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Draw(game.spritGameBackground, Vector2.Zero, Color.White);
            car.DrawCar();
            base.Draw(gameTime);
        }
    }
}
