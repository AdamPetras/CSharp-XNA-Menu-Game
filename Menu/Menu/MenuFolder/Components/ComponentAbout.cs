using Menu.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentAbout : DrawableGameComponent
    {
        private Game game;
        private IMenuItems about;
        public ComponentAbout(Game game)
            : base(game)
        {
            this.game = game;
        }
        public override void Initialize()
        {
            about = new AboutItems(game);
            about.AddItem("***GAME DESIGNERS***");
            about.AddItem("");
            about.AddItem("Main programmer: ");
            about.AddItem("Adam Petráš");
            about.AddItem("");
            about.AddItem("Main graphics: ");
            about.AddItem("Adam Petráš");
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            about.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

