using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Menu.GameFolder.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentPause : DrawableGameComponent
    {
        private Pause pause;
        private Game game;
        private ComponentCar car;
        public ComponentPause(Game game,ComponentCar car)
            : base(game)
        {
            this.game = game;
            this.car = car;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            pause = new Pause(game);
            pause.AddItem("Back");
            pause.AddItem("Settings");
            pause.AddItem("Menu");
            pause.AddItem("Exit");
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (game.SingleClick(Keys.Escape))
            {
                car.Enabled = true;
                Enabled = false;
                Visible = false;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            pause.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
