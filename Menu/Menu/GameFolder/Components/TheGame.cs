using Menu.Classes;
using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.Components
{
    public class TheGame : DrawableGameComponent
    {
        private Game game;
        private Track track;
        public Movement movement;
        private Camera camera;
        public TheGame(Game game)
            : base(game)
        {
            this.game = game;
            track = new Track(game);
            movement = new Movement(game,track);
            camera = new Camera(GraphicsDevice.Viewport);
        } 

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            track.GeneratingTrack();
            movement.Move();
            camera.Update(this);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,camera.transform);
            track.DrawTrack();
            movement.DrawPosition();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
