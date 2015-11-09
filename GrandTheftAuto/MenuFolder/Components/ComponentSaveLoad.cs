using System;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.CarFolder;
using GrandTheftAuto.GameFolder.Classes.Gun;
using GrandTheftAuto.GameFolder.Classes.GunFolder;
using GrandTheftAuto.GameFolder.Components;
using GrandTheftAuto.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.MenuFolder.Components
{


    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentSaveLoad : DrawableGameComponent
    {
        private IMenu load;
        private GameClass game;
        private ComponentCar componentCar;
        private ComponentCharacter componentCharacter;
        private ComponentEnemy componentEnemy;
        private Car car;
        private ComponentGameGraphics componentGameGraphics;
        private ComponentGuns componentGuns;
        public ComponentSaveLoad(GameClass game, Car car = null, ComponentCar componentCar = null,ComponentGameGraphics componentGameGraphics=null,ComponentGuns componentGuns=null, ComponentCharacter componentCharacter = null, ComponentEnemy componentEnemy = null)
            : base(game)
        {
            this.game = game;
            this.componentCar = componentCar;
            this.componentCharacter = componentCharacter;
            this.car = car;
            this.componentGuns = componentGuns;
            this.componentGameGraphics = componentGameGraphics;
            this.componentEnemy = componentEnemy;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            load = new MenuItems(game, new Vector2(game.graphics.PreferredBackBufferWidth / 2, game.graphics.PreferredBackBufferHeight / 3), game.normalFont, "middle");
            for (int i = 1; i <= game.saveList.Count; i++)
            {
                load.AddItem("Position " + i);
            }
            load.AddItem("Back");
            load.Next();
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (game.SingleClick(Keys.Up) || game.SingleClick(Keys.W))
            {
                load.Before();
            }
            if (game.SingleClick(Keys.Down) || game.SingleClick(Keys.S))
            {
                load.Next();
            }
            if (game.SingleClick(Keys.Enter) || (game.SingleClickMouse() && load.CursorColision()))
            {
                switch (load.Selected.Text)
                {
                    case "Position 1":
                        LoadOrSave(0);
                        break;
                    case "Position 2":
                        LoadOrSave(1);
                        break;
                    case "Position 3":
                        LoadOrSave(2);
                        break;
                    case "Position 4":
                        LoadOrSave(3);
                        break;
                    case "Position 5":
                        LoadOrSave(4);
                        break;
                    case "Back":
                        Back();
                        break;
                }
            }
            load.CursorPosition();
            base.Update(gameTime);
        }
        /// <summary>
        /// Drawable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.spritMenuBackground, Vector2.Zero, Color.White);
            load.Draw();
            DrawStats();
            game.spriteBatch.End();
        }
        /// <summary>
        /// Method to know how to back if to GameMenu or PauseMenu
        /// </summary>
        public void Back()
        {
            if (game.EGameState == EGameState.Load)
                game.ComponentEnable(game.componentGameMenu);
            else if (game.EGameState == EGameState.LoadIngame || game.EGameState == EGameState.Save)
            {
                game.EGameState = EGameState.Pause;
            }
            game.ComponentEnable(this, false);
        }
        /// <summary>
        /// Method to know if save or load
        /// </summary>
        /// <param name="index"></param>
        private void LoadOrSave(int index)
        {
            #region !!!*LOAD*!!!
            if (game.EGameState == EGameState.Load || game.EGameState == EGameState.LoadIngame)     //Pokud je Egamestate load nebo ingameload pro detekci jestli je load nebo save
            {
                if (game.saveList[index].Time != "Empty")   // Pokud není daný save prázdný
                {
                    if (game.EGameState == EGameState.LoadIngame && componentCar != null)    // Pokud je EGameState Ingameload a componenta car existuje
                        game.ComponentEnable(componentCar, false);
                    else if (game.EGameState == EGameState.LoadIngame && componentCharacter != null)    // Pokud je EGameState Ingameload a componenta character existuje
                        game.ComponentEnable(componentCharacter, false);
                    SavedData savedData = new SavedData(game.saveList[index].CharacterPosition, game.saveList[index].CharacterAngle,game.saveList[index].CarList, game.saveList[index].InTheCar, game.saveList[index].Holster, game.saveList[index].GunsList, game.saveList[index].EnemyList, game.saveList[index].Score);
                    if (savedData.InTheCar) // Pokud je save v aute tak
                        game.EGameState =EGameState.InGameCar;
                    else
                        game.EGameState = EGameState.InGameOut;
                    componentCharacter.Character.CharacterPosition = savedData.CharacterPosition;
                    componentCharacter.Character.Angle = savedData.CharacterAngle;
                    game.carList = savedData.CarList;
                    componentGuns.CharacterUsingGuns.Holster.HolsterList = savedData.Holster;
                    game.gunsOptions.GunList = savedData.GunsList;
                    componentEnemy.enemyService.EnemyList.Clear();                  //vyèištìní enemylistu
                    componentEnemy.enemyService.EnemyList = savedData.EnemyList;    //naplnìní uloženám listem
                    componentEnemy.enemyService.Score = savedData.Score;
                    game.ComponentEnable(componentEnemy);
                    game.ComponentEnable(componentCar);
                    game.ComponentEnable(componentCharacter);
                    game.ComponentEnable(componentGameGraphics);
                    game.ComponentEnable(componentGuns);
                    Game.IsMouseVisible = false;
                    game.ComponentEnable(this, false);
                }
            }
            #endregion
            #region !!!*SAVE*!!!
            else if (game.EGameState == EGameState.Save)    //Pokud je Egamestate save pro detekci jestli load nebo save
            {
                    game.saveList.Insert(index, new SavedData(componentCharacter.Character.CharacterPosition, componentCharacter.Character.Angle, game.carList,game.EGameState.Equals(EGameState.InGameCar), componentGuns.CharacterUsingGuns.Holster.HolsterList, game.gunsOptions.GunList, componentEnemy.enemyService.EnemyList, componentEnemy.enemyService.Score, DateTime.Now.ToString()));
                    game.saveList.RemoveAt(index + 1);
            }
            #endregion
        }
        /// <summary>
        /// Method to draw statistics of saving
        /// </summary>
        private void DrawStats()
        {
            DrawOrder = 10;
            for (int i = 0; i < game.saveList.Count; i++)
                game.spriteBatch.DrawString(game.normalFont, game.saveList[i].Time, new Vector2(game.graphics.PreferredBackBufferWidth / 2 + 250, game.graphics.PreferredBackBufferHeight / 3 + i * game.normalFont.MeasureString(game.saveList[i].Time).Y), Color.White);
        }
    }
}
