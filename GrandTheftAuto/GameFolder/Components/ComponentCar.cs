using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.CarFolder;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentCar : DrawableGameComponent
    {
        public Car SelectedCar { get; private set; }
        private GameClass game;
        private Camera camera;
        private float animation;
        private ComponentCharacter componentCharacter;
        private double getOutOfCarTimer;
        private GameGraphics gameGraphics;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="camera"></param>
        /// <param name="componentCharacter"></param>
        public ComponentCar(GameClass game, Camera camera, ComponentCharacter componentCharacter,GameGraphics gameGraphics)
            : base(game)
        {
            this.game = game;
            this.camera = camera;
            this.componentCharacter = componentCharacter;
            this.gameGraphics = gameGraphics;
            animation = 0;
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
        /// <summary>
        /// Updatable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (Car car in game.carList)
            {
                if ((game.EGameState == EGameState.InGameCar || game.EGameState == EGameState.GameOver) && car.Selected)
                {
                    car.Update();
                    if (!car.Colision)
                    {
                        componentCharacter.CharacterService.Character.Position = car.Position;
                        componentCharacter.CharacterService.Character.UpdateRectangle();
                        SelectedCar = car;
                        getOutOfCarTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                        car.Move(gameTime);
                        camera.Update(SelectedCar.Position);
                        if (game.SingleClick(game.controlsList[(int)EKeys.E].Key) && getOutOfCarTimer > 250)
                        //vystoupení z auta
                        {
                            componentCharacter.CharacterService.Character.Position = car.Position;
                            getOutOfCarTimer = 0;
                            car.Selected = false;
                            car.ResetProperty();
                            componentCharacter.CharacterService.Character.Position = car.GetOutOfCar();
                            componentCharacter.CharacterService.Character.Angle = car.Angle;
                            game.EGameState = EGameState.InGameOut;
                            game.ComponentEnable(componentCharacter);
                        }
                    }
                    if (game.SingleClick(Keys.Escape))      //Pauza
                    {
                        game.EGameState = EGameState.Pause;
                        Enabled = false;
                    }
                    game.SplashDisplay(); // èištìní displeje  
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

            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
               camera.Transform);
            foreach (Car car in game.carList)
            {
                DrawOrder = 3;
                car.DrawCar();
                if (car.GetCarState().Equals(ECar.Colision))
                {
                    if (animation < 1)
                        animation += 0.02f;
                    game.spriteBatch.Draw(game.spritExplosion,
                        new Vector2(car.Position.X - game.spritExplosion.Width / 2,
                            car.Position.Y - game.spritExplosion.Height / 2), Color.White * (0.1f + animation));
                }
            }
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
