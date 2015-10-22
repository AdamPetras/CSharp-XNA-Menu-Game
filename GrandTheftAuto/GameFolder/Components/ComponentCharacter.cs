using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.CarFolder;
using GrandTheftAuto.GameFolder.Classes.Gun;
using GrandTheftAuto.GameFolder.Classes.GunFolder;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentCharacter : DrawableGameComponent
    {
        private Character character;
        private GameClass game;
        private Camera camera;
        private Car car;
        private GameGraphics gameGraphics;
        private Vector2 before;
        private CharacterUsingGuns characterUsingGuns;
        private ComponentEnemy componentEnemy;
        public ComponentCharacter(GameClass game, Car car, SavedData savedData, GameGraphics gameGraphics,ComponentEnemy componentEnemy = null)
            : base(game)
        {
            this.game = game;
            this.car = car;
            this.gameGraphics = gameGraphics;
            camera = new Camera(game);
            character = new Character(game, savedData, car.Position, car.Angle);
            characterUsingGuns = new CharacterUsingGuns(game,character,camera,savedData);
            this.componentEnemy = componentEnemy;
            componentEnemy.camera = camera;
            componentEnemy.character = character;
            componentEnemy.characterUsingGuns = characterUsingGuns;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            camera.Update(character.CharacterPosition);
            character.Move(gameTime);
            character.Live();
            if (game.SingleClick(Keys.Escape))      //Pauza
            {
                ComponentPause componentPause = new ComponentPause(game,car,null,this,componentEnemy,character,characterUsingGuns.Holster);
                Game.Components.Add(componentPause);
                Enabled = false;
                Game.IsMouseVisible = true;
                componentEnemy.Enabled = false;
            }
            if (game.SingleClick(game.controlsList[(int)EKeys.E].Key))      //Nastup do auta
            {
                if (car.CarRectangle().Intersects(character.CharacterRectangle()))
                {
                    ComponentCar componentCar = new ComponentCar(game,
                        new SavedData(car.Position, car.Angle,Vector2.Zero,0f,true,characterUsingGuns.Holster.GetHolster(),null,car.Hp,componentEnemy.enemyService.EnemyList));
                    game.ComponentEnable(this, false);
                    game.Components.Add(componentCar);
                    game.ComponentEnable(componentEnemy,false);
                }
            }
            if (gameGraphics.Colision(character.CharacterRectangle()))
            {
                character.CharacterPosition = before;
            }
            if (!gameGraphics.Colision(character.CharacterRectangle()))
            {
                before = character.CharacterPosition;
            }
            #region Zbraně
            characterUsingGuns.PickUpGun();
            characterUsingGuns.Reloading(gameTime);
            characterUsingGuns.BulletColision(gameGraphics.graphicsList);
            characterUsingGuns.SelectGun();
            characterUsingGuns.Shooting(gameTime);
            characterUsingGuns.BulletFly();
            characterUsingGuns.SelectGun();
            #endregion
            game.SplashDisplay();       // čištění displeje

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            gameGraphics.DrawNonColiadableGraphics();
            game.gunsOptions.DrawGuns();
            car.DrawCar();
            characterUsingGuns.Draw();
            character.DrawCharacter();                
            gameGraphics.DrawColiadableGraphics();
            game.spriteBatch.DrawString(game.normalFont, character.Hp+" HP", new Vector2(camera.Centering.X+game.graphics.PreferredBackBufferWidth-game.normalFont.MeasureString(character.Hp+" HP").X,camera.Centering.Y), Color.White);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
