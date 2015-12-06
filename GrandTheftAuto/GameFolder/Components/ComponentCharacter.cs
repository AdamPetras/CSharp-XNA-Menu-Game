using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.CarFolder;
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
        public CharacterService CharacterService { get; private set; }
        private readonly GameClass game;
        private readonly Camera camera;
        private readonly GameGraphics gameGraphics;
        private Vector2 before;
        public ComponentCharacter(GameClass game, SavedData savedData, GameGraphics gameGraphics, Camera camera)
            : base(game)
        {
            this.game = game;
            this.gameGraphics = gameGraphics;
            this.camera = camera;
            game.EGameState = EGameState.InGameOut;
            CharacterService = new CharacterService(game, savedData);
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
            if (game.EGameState != EGameState.Reloading)
                game.EGameState = EGameState.InGameOut;
            camera.Update(CharacterService.Character.Position);
            CharacterService.Move(gameTime);
            CharacterService.Update(gameTime);
            if (game.SingleClick(Keys.Escape))      //Pauza
            {
                game.EGameState = EGameState.Pause;
                Enabled = false;
                Game.IsMouseVisible = true;
            }
            if (game.SingleClick(game.controlsList[(int)EKeys.E].Key))      //Nastup do auta
            {
                foreach (Car car in game.carList)
                {
                    if (car.CarRectangle().Intersects(CharacterService.Character.Rectangle))
                    {
                        game.ComponentEnable(this, false);
                        car.Selected = true;
                        game.EGameState = EGameState.InGameCar;
                        break;  //skipnutí když koliduje více aut
                    }
                }
            }
            if (gameGraphics.Colision(CharacterService.Character.Rectangle))
            {
                CharacterService.Character.Position = before;
            }
            if (!gameGraphics.Colision(CharacterService.Character.Rectangle))
            {
                before = CharacterService.Character.Position;
            }
            game.SplashDisplay();       // čištění displeje
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            DrawOrder = 4;
            CharacterService.DrawCharacter();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
