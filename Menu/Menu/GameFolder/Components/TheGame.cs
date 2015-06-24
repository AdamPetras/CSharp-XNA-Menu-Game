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
        private Movement movement;
        private Camera camera;
        public TheGame(Game game)
            : base(game)
        {
            this.game = game;          
        }

        public override void Initialize()
        {
            base.Initialize();
            movement = new Movement(game);
            camera = new Camera(movement);
        }

        public override void Update(GameTime gameTime)
        {
            camera.Update(this);
            movement.Move();
            base.Update(gameTime);        
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            game.spriteBatch.Draw(game.spritGameBackground,Vector2.Zero,Color.White);
            movement.DrawPosition();
            game.spriteBatch.DrawString(game.font, movement.angle+"\n"+movement.position.X+"   "+movement.position.Y, new Vector2(400, 200), Color.White);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}