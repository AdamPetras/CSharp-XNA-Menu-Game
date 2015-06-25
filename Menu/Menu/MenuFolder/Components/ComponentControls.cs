using Menu.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Components
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
            controlItems.AddItem("Throttle       -   Up_Arrow");
            controlItems.AddItem("Brake           -   Down_Arrow");
            controlItems.AddItem("Turn left      -   Left_Arrow");
            controlItems.AddItem("Turn right    -   Right_Arrow");
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
