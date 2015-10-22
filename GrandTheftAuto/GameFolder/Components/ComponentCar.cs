using System;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.CarFolder;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentCar : DrawableGameComponent
    {
        private GameClass game;
        private Car car;
        private Camera camera;
        private SavedData savedData;
        private ColisionDetection colisionDetection;
        private GameGraphics gameGraphics;
        private float animation;
        private ComponentEnemy componentEnemy;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="savedData"></param>
        public ComponentCar(GameClass game, SavedData savedData)
            : base(game)
        {
            this.game = game;
            this.savedData = savedData;
            car = new Car(game, savedData, 103000, 1770); // vytvoøní auta o výkonu 103kW a hmotnosti 1770kg
            colisionDetection = new ColisionDetection();
            camera = new Camera(game);
            gameGraphics = new GameGraphics(game);
            animation = 0;
            componentEnemy = new ComponentEnemy(game, savedData,gameGraphics.graphicsList.ColisionList(), camera, car);
            game.Components.Add(componentEnemy);
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
            camera.Update(car.Position);
            car.Move(gameTime);
            if (game.SingleClick(Keys.Escape))      //Pauza
            {
                ComponentPause componentPause = new ComponentPause(game, car, this, null, componentEnemy);
                Game.Components.Add(componentPause);
                Enabled = false;
                Game.IsMouseVisible = true;
                componentEnemy.Enabled = false;
            }
            if (game.SingleClick(game.controlsList[(int)EKeys.E].Key) && car.CurrentSpeed() < 10 && car.Hp > 0)     //Vystup z auta
            {
                ComponentCharacter componentCharacter = new ComponentCharacter(game, car, savedData, gameGraphics, componentEnemy);
                game.Components.Add(componentCharacter);
                game.ComponentEnable(this, false);
            }
            if (colisionDetection.RectangleColision(gameGraphics.graphicsList, car.CarRectangle()))
            {
                for (int i = 0; i < gameGraphics.graphicsList.ColisionList().Count; i++) //Kolize øešena pøes pixely
                {
                    Matrix ObjectTransform =
                        Matrix.CreateTranslation(new Vector3(
                            gameGraphics.graphicsList.ObjectTextureList()[i].Position.X,
                            gameGraphics.graphicsList.ObjectTextureList()[i].Position.Y, 0.0f));
                    if (
                        colisionDetection.PixelColision(
                            ObjectTransform,
                            gameGraphics.graphicsList.ObjectTextureList()[i].Texture.Width,
                            gameGraphics.graphicsList.ObjectTextureList()[i].Texture.Height,
                            gameGraphics.graphicsList.ObjectDataList()[i],
                            colisionDetection.ObjectAMatrix(car.OriginVector, car.Position, car.Angle),
                            game.spritCar.Width, game.spritCar.Height, car.CarData))
                    {
                        car.Colision = true;
                        car.Hp = 0;
                    }
                }
            }
            game.SplashDisplay();       // èištìní displeje
            base.Update(gameTime);
        }
        /// <summary>
        /// Drawable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            gameGraphics.DrawNonColiadableGraphics();
            game.gunsOptions.DrawGuns();
            car.DrawCar();
            gameGraphics.DrawColiadableGraphics();

            if (car.GetCarState().Equals(ECar.Colision))
            {
                if (animation < 1)
                    animation += 0.02f;
                game.spriteBatch.Draw(game.spritExplosion, new Vector2(car.Position.X - game.spritExplosion.Width / 2, car.Position.Y - game.spritExplosion.Height / 2), Color.White * (0.1f + animation));
            }
            game.spriteBatch.DrawString(game.normalFont, "Speed: " + car.CurrentSpeed() + "\n" + car.GetCarState(), new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
            game.spriteBatch.DrawString(game.normalFont, "Car damage: " + (car.Hp / 10 - 100) * (-1) + "%", new Vector2(camera.Centering.X + game.graphics.PreferredBackBufferWidth - game.normalFont.MeasureString("Car damage: " + (car.Hp / 10 - 100) * (-1) + "%").X, camera.Centering.Y), Color.White);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
