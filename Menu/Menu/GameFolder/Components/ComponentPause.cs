using Menu.GameFolder.Classes;
using Menu.MenuFolder.Components;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Menu.GameFolder.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentPause : DrawableGameComponent
    {
        private IMenu pause;
        private Game game;
        private ComponentCar car;
        public ComponentPause(Game game, ComponentCar car)
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
            pause.AddItem("Menu");
            pause.AddItem("Exit");
            pause.Next();
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
           if (game.SingleClick(Keys.Up))
                pause.Before();
            if (game.SingleClick(Keys.Down))
                pause.Next();
            if (game.SingleClick(Keys.Enter))
            {
                switch (pause.Selected.Text)
                {
                    case "Back":
                        game.ComponentEnable(this,false);
                        car.Enabled = true;
                        break;
                    case "Menu":
                        game.ComponentEnable(this,false);
                        game.ComponentEnable(car,false);;
                        game.ComponentEnable(game.menu,true);
                        break;
                    case "Exit":
                        Game.Exit();
                        break;
                }
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
