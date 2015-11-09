using GrandTheftAuto.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.MenuFolder.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentAbout : DrawableGameComponent
    {
        private GameClass game;
        private IDraw about;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public ComponentAbout(GameClass game)
            : base(game)
        {
            this.game = game;
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
        public override void Initialize()
        {
            about = new DrawItems(game);
            about.AddItem("***GAME DESIGNERS***");
            about.AddItem("");
            about.AddItem("Main programmer: ");
            about.AddItem("Adam Petráš");
            about.AddItem("");
            about.AddItem("Main graphics: ");
            about.AddItem("Adam Petráš");
            about.AddItem("");
            about.AddItem("Other graphics: ");
            about.AddItem("Jiří Mrhálek");
            base.Initialize();
        }
        /// <summary>
        /// Updatable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        /// <summary>
        /// Drawable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            about.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

