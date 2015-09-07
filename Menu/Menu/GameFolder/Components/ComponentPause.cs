using System.ComponentModel;
using Menu.GameFolder.Classes;
using Menu.MenuFolder.Classes;
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
        private ComponentCar componentCar;
        private Car car;
        public ComponentPause(Game game, ComponentCar componentCar, Car car)
            : base(game)
        {
            this.game = game;
            this.componentCar = componentCar;
            this.car = car;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            game.EGameState = EGameState.Pause;
            pause = new MenuItems(game, new Vector2(game.graphics.PreferredBackBufferWidth / 2, game.graphics.PreferredBackBufferHeight / 2), "middle");
            pause.AddItem("Back");
            pause.AddItem("Load");
            pause.AddItem("Save");
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
            if (game.SingleClick(Keys.Up) || game.SingleClick(Keys.W))
                pause.Before();
            if (game.SingleClick(Keys.Down) || game.SingleClick(Keys.S))
                pause.Next();
            if (game.SingleClick(Keys.Enter))
            {
                switch (pause.Selected.Text)
                {
                    case "Back":
                        game.ComponentEnable(this, false);
                        componentCar.Enabled = true;
                        break;
                    case "Load":
                        game.EGameState = EGameState.LoadIngame;
                        ComponentSaveLoad componentLoad = new ComponentSaveLoad(game,this,componentCar,car);
                        game.Components.Add(componentLoad);
                        game.ComponentEnable(this,false);
                        break;
                    case "Save":
                        game.EGameState = EGameState.Save;
                        ComponentSaveLoad componentSave = new ComponentSaveLoad(game, this,null,car);
                        Game.Components.Add(componentSave);
                        game.ComponentEnable(this, false);
                        break;
                    case "Menu":
                        game.ComponentEnable(this, false);
                        game.ComponentEnable(componentCar, false);
                        game.ComponentEnable(game.componentGameMenu);
                        break;
                    case "Exit":
                        Game.Exit();
                        break;
                }
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// Drawable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            pause.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
