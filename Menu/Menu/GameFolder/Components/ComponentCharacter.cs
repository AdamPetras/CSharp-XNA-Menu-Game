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
            character = new Character(game, savedData,car.Position,car.Angle);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            camera = new Camera(game);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            camera.Update(character.Position);
            character.Move(gameTime);
            if (game.SingleClick(Keys.Escape))
            {
                ComponentPause componentPause = new ComponentPause(game,car,null,this,character);
                Game.Components.Add(componentPause);
                Enabled = false;
            }
            if (game.SingleClick(Keys.E))
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
                character.Position = before;
            }
            if (!gameGraphics.Colision(character.CharacterRectangle()))
            {
                before = character.Position;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            car.DrawCar();
            character.Draw();
            gameGraphics.DrawGraphics();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
