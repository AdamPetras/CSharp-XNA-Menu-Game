using Menu.Classes;
using Menu.Interface;
using Microsoft.Xna.Framework;

namespace Menu.Components
{
    public class ComponentControls : DrawableGameComponent
    {
        private IMenuItems controlItems;
        private Game game;
        public ComponentControls(Game game)
            : base(game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            controlItems = new ControlItems(game);
            controlItems.AddItem("***MOVEMENT***");
            controlItems.AddItem("Brake-A");
            controlItems.AddItem("Throttle-D");
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            controlItems.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
