using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.CarFolder;
using GrandTheftAuto.GameFolder.Classes.Gun;
using GrandTheftAuto.GameFolder.Classes.GunFolder;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using GrandTheftAuto.MenuFolder.Components;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentPause : DrawableGameComponent
    {
        private IMenu pause;
        private GameClass game;
        private Car car;
        private ComponentCar componentCar;
        private ComponentCharacter componentCharacter;
        private ComponentEnemy componentEnemy;
        private Character character;
        private Holster holster;
        public ComponentPause(GameClass game, Car car, ComponentCar componentCar = null, ComponentCharacter componentCharacter = null,ComponentEnemy componentEnemy = null, Character character = null, Holster holster = null)
            : base(game)
        {
            this.game = game;
            this.componentCar = componentCar;
            this.componentCharacter = componentCharacter;
            this.componentEnemy = componentEnemy;
            this.character = character;
            this.car = car;
            this.holster = holster;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            game.EGameState = EGameState.Pause;
            pause = new MenuItems(game, new Vector2(game.graphics.PreferredBackBufferWidth / 2, game.graphics.PreferredBackBufferHeight / 2),game.bigFont, "middle");
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
            if (game.SingleClick(Keys.Enter) || (game.SingleClickMouse() && pause.CursorColision()))
            {
                switch (pause.Selected.Text)
                {
                    case "Back":
                        if (componentCar != null)
                        {
                            game.EGameState = EGameState.InGameCar;
                            componentCar.Enabled = true;
                            componentEnemy.Enabled = true;
                        }
                        else if (componentCharacter != null)
                        {
                            game.EGameState = EGameState.InGameOut;
                            componentCharacter.Enabled = true;
                            componentEnemy.Enabled = true;
                        }
                        game.ComponentEnable(this, false);
                        Game.IsMouseVisible = false;
                        break;
                    case "Load":
                        game.EGameState = EGameState.LoadIngame;
                        if (componentCar != null)
                        {
                            ComponentSaveLoad componentLoad = new ComponentSaveLoad(game, this, car, componentCar,null,null,null,componentEnemy);
                            game.Components.Add(componentLoad);
                        }else if (componentCharacter != null)
                        {
                            ComponentSaveLoad componentLoad = new ComponentSaveLoad(game, this, car, null,componentCharacter,character,holster,componentEnemy);
                            game.Components.Add(componentLoad);
                        }
                        game.ComponentEnable(this,false);
                        break;
                    case "Save":
                        game.EGameState = EGameState.Save;
                        if (componentCar != null)
                        {
                            ComponentSaveLoad componentSave = new ComponentSaveLoad(game, this, car,null,null,null,null,componentEnemy);
                            Game.Components.Add(componentSave);
                        }else if (componentCharacter != null)
                        {
                            ComponentSaveLoad componentSave = new ComponentSaveLoad(game, this, car, null, componentCharacter,character,holster,componentEnemy);
                            game.Components.Add(componentSave);
                        }
                        game.ComponentEnable(this, false);
                        break;
                    case "Menu":
                        if(componentCar != null)
                        game.ComponentEnable(componentCar, false);
                        else if(componentCharacter != null)
                            game.ComponentEnable(componentCharacter, false);
                        game.ComponentEnable(game.componentGameMenu);
                        game.ComponentEnable(componentEnemy,false);
                        game.ComponentEnable(this, false);
                        break;
                    case "Exit":
                        Game.Exit();
                        break;
                }
            }
            if (game.SingleClick(Keys.Escape))
            {
                if (componentCar != null)
                {
                    game.EGameState = EGameState.InGameCar;
                    componentCar.Enabled = true;
                    componentEnemy.Enabled = true;
                }
                else if (componentCharacter != null)
                {
                    game.EGameState = EGameState.InGameOut;
                    componentCharacter.Enabled = true;
                    componentEnemy.Enabled = true;
                }
                game.ComponentEnable(this, false);
                Game.IsMouseVisible = false;
            }
            pause.CursorPosition();
            game.SplashDisplay();       //�i�t�n� displeje
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
            DrawOrder = 9;
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
