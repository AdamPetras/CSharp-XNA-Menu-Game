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
            track = new Track(game);
            movement = new Movement(game, track);
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
            track.DrawTrack(gameTime);
            movement.DrawPosition();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
