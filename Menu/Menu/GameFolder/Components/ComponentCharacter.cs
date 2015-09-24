using Menu.GameFolder.Classes;
using Menu.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Menu.GameFolder.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentCharacter : DrawableGameComponent
    {
        private Character character;
        private Game game;
        private Camera camera;
        private Car car;
        private GameGraphics gameGraphics;
        private Vector2 before;

        public ComponentCharacter(Game game,Car car,SavedData savedData, GameGraphics gameGraphics)
            : base(game)
        {
            this.game = game;
            this.car = car;
            this.gameGraphics = gameGraphics;
            camera = new Camera(game);
            character = new Character(game, savedData, camera, car.Position, car.Angle);
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
            if (game.SingleClick(Keys.Escape))      //Pauza
            {
                ComponentPause componentPause = new ComponentPause(game,car,null,this,character);
                Game.Components.Add(componentPause);
                Enabled = false;
                Game.IsMouseVisible = true;
            }
            if (game.SingleClick(game.controlsList[(int)EKeys.E].Key))      //Nastup do auta
            {
                if (car.CarRectangle().Intersects(character.CharacterRectangle()))
                {
                    ComponentCar componentCar = new ComponentCar(game,
                        new SavedData(car.Position, car.Angle));
                    game.ComponentEnable(this, false);
                    game.Components.Add(componentCar);
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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            gameGraphics.DrawNonColiadableGraphics();
            game.gunsList.DrawGuns();
            car.DrawCar();
            character.DrawCharacter();                
            gameGraphics.DrawColiadableGraphics();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
