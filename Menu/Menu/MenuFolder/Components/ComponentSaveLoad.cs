using System;
using Menu.GameFolder.Classes;
using Menu.GameFolder.Components;
using Menu.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Menu.MenuFolder.Components
{


    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentSaveLoad : DrawableGameComponent
    {
        private IMenu load;
        private Game game;
        private ComponentPause componentPause;
        private ComponentCar componentCar;
        private ComponentCharacter componentCharacter;
        private Car car;
        private Character character;
        public ComponentSaveLoad(Game game,ComponentPause componentPause = null, Car car = null, ComponentCar componentCar = null,ComponentCharacter componentCharacter = null,Character character = null)
            : base(game)
        {
            this.game = game;
            this.componentPause = componentPause;
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
            load = new MenuItems(game,new Vector2(game.graphics.PreferredBackBufferWidth/2,game.graphics.PreferredBackBufferHeight/2),"middle");
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
            if (game.SingleClick(Keys.Enter))
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
            game.ComponentEnable(this, false);
            if (game.EGameState == EGameState.Load)
                game.ComponentEnable(game.componentGameMenu);
            else if (game.EGameState == EGameState.LoadIngame||game.EGameState==EGameState.Save)
            {
                game.ComponentEnable(componentPause);
            }
        }
        /// <summary>
        /// Method to know if save or load
        /// </summary>
        /// <param name="index"></param>
        private void LoadOrSave(int index)
        {
            if (game.EGameState == EGameState.Load||game.EGameState==EGameState.LoadIngame)     //Pokud je Egamestate load nebo ingameload pro detekci jestli je load nebo save
            {
                if (game.saveList[index].Time != "Empty")   // Pokud není daný save prázdný
                {
                    if (game.EGameState == EGameState.LoadIngame&& componentCar != null)    // Pokud je EGameState Ingameload a componenta car existuje
                        game.ComponentEnable(componentCar, false);
                    else if (game.EGameState == EGameState.LoadIngame && componentCharacter != null)    // Pokud je EGameState Ingameload a componenta character existuje
                        game.ComponentEnable(componentCharacter, false);

                    SavedData savedData = new SavedData(game.saveList[index].Position, game.saveList[index].Angle,game.saveList[index].CharacterPosition,game.saveList[index].CharacterAngle,game.saveList[index].InTheCar);                    
                    if (savedData.InTheCar) // Pokud je save v aute tak
                    {
                        ComponentCar newComponentCar = new ComponentCar(game, savedData);
                        Game.Components.Add(newComponentCar);
                    }
                    else if (!savedData.InTheCar)   //Pokud save neni v autì tak
                    {
                        GameGraphics gameGraphics = new GameGraphics(game);    // naètení grafiky
                        ComponentCharacter newComponentCharacter = new ComponentCharacter(game,new Car(game,savedData,103000,1770), savedData,gameGraphics);
                        Game.Components.Add(newComponentCharacter);
                    }
                    game.ComponentEnable(this, false);
                }
            }
            else if (game.EGameState == EGameState.Save)    //Pokud je Egamestate save pro detekci jestli load nebo save
            {
                if (car != null && character ==null)    //Save pro auto
                {
                    game.saveList.Insert(index, new SavedData(car.Position, car.Angle,Vector2.Zero,0, true, DateTime.Now.ToString()));
                    game.saveList.RemoveAt(index + 1);
                }
                else if (character != null)     //Save pro character
                {
                    game.saveList.Insert(index, new SavedData(car.Position, car.Angle,character.Position,character.Angle, false, DateTime.Now.ToString()));
                    game.saveList.RemoveAt(index + 1);
                }
            }
        }
        /// <summary>
        /// Method to draw statistics of saving
        /// </summary>
        private void DrawStats()
        {
            for (int i = 0; i < game.saveList.Count; i++)
                game.spriteBatch.DrawString(game.normalFont, game.saveList[i].Time, new Vector2(game.graphics.PreferredBackBufferWidth / 2 + 250, game.graphics.PreferredBackBufferHeight / 2 + i * game.bigFont.MeasureString(game.saveList[i].Time).Y - game.bigFont.MeasureString(game.saveList[i].Time).Y * 3), Color.White);
        }
    }
}
