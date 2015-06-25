using System.Security.AccessControl;
using Menu.Classes;
using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.Components
{
    public class TheGame : DrawableGameComponent
    {
        private Game game;
        private Car car;
        private Camera camera;
        public TheGame(Game game)
            : base(game)
        {
            this.game = game;          
        }

        public override void Initialize()
        {
            base.Initialize();
            car = new Car(game);
            camera = new Camera(car);
        }

        public override void Update(GameTime gameTime)
        {
            camera.Update(this);
            car.Move();
            base.Update(gameTime);        
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            game.spriteBatch.Draw(game.spritGameBackground,Vector2.Zero,Color.White);
            car.DrawCar();
            game.spriteBatch.DrawString(game.font, car.eCar+"\n"+car.position.X+"   "+car.position.Y, new Vector2(400, 200), Color.White);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}