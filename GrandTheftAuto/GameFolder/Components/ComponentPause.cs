using System.Linq;
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
        public EGameState eGameStateBefore;
        private GameClass game;
        private ComponentCar componentCar;
        private ComponentCharacter componentCharacter;
        private ComponentEnemy componentEnemy;
        private ComponentGameGraphics componentGameGraphics;
        private ComponentGuns componentGuns;
        private ComponentGUI componentGui;
        private ComponentQuestSystem componentQuestSystem;
        private ComponentInventory componentInventory;
        private ComponentItem componentItem;
        public ComponentPause(GameClass game, ComponentCar componentCar, ComponentCharacter componentCharacter, ComponentEnemy componentEnemy, ComponentGameGraphics componentGameGraphics, ComponentGuns componentGuns, ComponentGUI componentGui, ComponentQuestSystem componentQuestSystem, ComponentInventory componentInventory,ComponentItem componentItem)
            : base(game)
        {
            this.game = game;
            this.componentCar = componentCar;
            this.componentQuestSystem = componentQuestSystem;
            this.componentCharacter = componentCharacter;
            this.componentEnemy = componentEnemy;
            this.componentGameGraphics = componentGameGraphics;
            this.componentGuns = componentGuns;
            this.componentGui = componentGui;
            this.componentInventory = componentInventory;
            this.componentItem = componentItem;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            pause = new MenuItems(game);
            Vector2 position = new Vector2(game.graphics.PreferredBackBufferWidth / 2, game.graphics.PreferredBackBufferHeight / 2);
            pause.AddItem("Back", position, game.bigFont);
            pause.AddItem("Load", position, game.bigFont);
            pause.AddItem("Save", position, game.bigFont);
            pause.AddItem("Menu", position, game.bigFont);
            pause.AddItem("Exit", position, game.bigFont);
            pause.Selected = pause.Items.First();
            pause.SetKeysDown(Keys.Down, Keys.S);
            pause.SetKeysUp(Keys.W, Keys.Up);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (game.EGameState == EGameState.Pause)
            {
                componentGuns.Enabled = false;
                componentEnemy.Enabled = false;
                game.ComponentEnable(componentInventory,false);
                pause.Moving();
                if (game.SingleClick(Keys.Enter) || (game.SingleClickLeftMouse() && pause.CursorColision()))
                {
                    switch (pause.Selected.Text)
                    {
                        case "Back":
                            Exit();
                            Game.IsMouseVisible = false;
                            break;
                        case "Load":
                            game.EGameState = EGameState.LoadIngame;
                            ComponentSaveLoad componentLoad = new ComponentSaveLoad(game, componentCar, componentGameGraphics, componentGuns, componentCharacter, componentEnemy);
                            game.Components.Add(componentLoad);
                            break;
                        case "Save":
                            game.EGameState = EGameState.Save;
                            ComponentSaveLoad componentSave = new ComponentSaveLoad(game, componentCar, componentGameGraphics, componentGuns, componentCharacter, componentEnemy);
                            Game.Components.Add(componentSave);
                            break;
                        case "Menu":
                            game.Components.Remove(componentCar);
                            game.carList.Clear();
                            game.Components.Remove(componentGui);
                            game.Components.Remove(componentEnemy);
                            game.Components.Remove(componentCharacter);
                            game.Components.Remove(componentGuns);
                            game.Components.Remove(componentGameGraphics);
                            game.Components.Remove(componentQuestSystem);
                            game.Components.Remove(componentInventory);
                            game.Components.Remove(this);
                            game.Components.Remove(componentItem);
                            game.ComponentEnable(game.componentGameMenu);
                            game.ComponentEnable(this, false);
                            break;
                        case "Exit":
                            Game.Exit();
                            break;
                    }
                }
                if (game.SingleClick(Keys.Escape))
                {
                    Exit();
                }
                pause.CursorPosition();
                game.SplashDisplay();       //èištìní displeje
            }
            else if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.InGameCar)
                eGameStateBefore = game.EGameState;
            base.Update(gameTime);
        }
        /// <summary>
        /// Drawable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            if (game.EGameState == EGameState.Pause)
            {
                game.spriteBatch.Draw(game.spritPauseBackground, new Rectangle(0, 0, game.graphics.PreferredBackBufferWidth, game.graphics.PreferredBackBufferHeight), Color.White * 0.6f);
                pause.Draw();
                DrawOrder = 9;
                game.spriteBatch.DrawString(game.normalFont, eGameStateBefore.ToString(), new Vector2(0, 0), Color.White);
                game.spriteBatch.Draw(game.cursor, game.mouseState.Position.ToVector2(), Color.White);
            }
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void Exit()
        {
            if (eGameStateBefore == EGameState.InGameCar)
            {
                game.EGameState = EGameState.InGameCar;
                componentCar.Enabled = true;
            }
            else if (eGameStateBefore == EGameState.InGameOut)
            {
                game.EGameState = EGameState.InGameOut;
                componentCharacter.Enabled = true;
            }
            componentEnemy.Enabled = true;
            componentGuns.Enabled = true;
            game.ComponentEnable(componentInventory);
        }
    }
}
