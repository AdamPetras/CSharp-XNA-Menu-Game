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
        private Vector2 position;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public ComponentAbout(GameClass game)
            : base(game)
        {
            this.game = game;
            position = new Vector2(game.graphics.PreferredBackBufferWidth / 4, game.graphics.PreferredBackBufferHeight / 2);
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
        public override void Initialize()
        {
            about = new DrawItems(game);
            about.AddItem("***GAME DESIGNERS***", position, game.bigFont,rotation:-0.3f);
            about.AddItem("", position, game.smallFont);
            about.AddItem("Main programmer: ", position, game.smallFont);
            about.AddItem("Adam Petráš", position, game.smallFont);
            about.AddItem("", position, game.smallFont);
            about.AddItem("Main graphics: ", position, game.smallFont);
            about.AddItem("Adam Petráš", position, game.smallFont);
            about.AddItem("Jiří Mrhálek", position, game.smallFont);
            about.AddItem("THANK YOU!!!", position, game.smallFont);
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

