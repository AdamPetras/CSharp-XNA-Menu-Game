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
        private Car car;
        private ComponentCar componentCar;
        private ComponentCharacter componentCharacter;
        private Character character;
        public ComponentPause(Game game, Car car, ComponentCar componentCar = null,ComponentCharacter componentCharacter = null,Character character = null)
            : base(game)
        {
            this.game = game;
            this.componentCar = componentCar;
            this.componentCharacter = componentCharacter;
            this.character = character;
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
                        if(componentCar != null)
                        componentCar.Enabled = true;
                        else if (componentCharacter != null)
                            componentCharacter.Enabled = true;
                        break;
                    case "Load":
                        game.EGameState = EGameState.LoadIngame;
                        if (componentCar != null)
                        {
                            ComponentSaveLoad componentLoad = new ComponentSaveLoad(game, this, car, componentCar);
                            game.Components.Add(componentLoad);
                        }else if (componentCharacter != null)
                        {
                            ComponentSaveLoad componentLoad = new ComponentSaveLoad(game, this, car, null,componentCharacter,character);
                            game.Components.Add(componentLoad);
                        }
                        game.ComponentEnable(this,false);
                        break;
                    case "Save":
                        game.EGameState = EGameState.Save;
                        if (componentCar != null)
                        {
                            ComponentSaveLoad componentSave = new ComponentSaveLoad(game, this, car);
                            Game.Components.Add(componentSave);
                        }else if (componentCharacter != null)
                        {
                            ComponentSaveLoad componentSave = new ComponentSaveLoad(game, this, car, null, componentCharacter,character);
                            game.Components.Add(componentSave);
                        }
                        game.ComponentEnable(this, false);
                        break;
                    case "Menu":
                        game.ComponentEnable(this, false);
                        if(componentCar != null)
                        game.ComponentEnable(componentCar, false);
                        else if(componentCharacter != null)
                            game.ComponentEnable(componentCharacter, false);
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
