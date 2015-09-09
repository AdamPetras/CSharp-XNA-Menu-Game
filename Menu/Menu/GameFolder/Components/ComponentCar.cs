using System;
using Menu.GameFolder.Classes;
using Menu.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu.GameFolder.Components
{
    public class ComponentCar : DrawableGameComponent
    {
        private Game game;
        private Car car;
        private Camera camera;
        private GameGraphics gameGraphics;
        private float animation;
        private SavedData savedData;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="savedData"></param>
        public ComponentCar(Game game,SavedData savedData)
            : base(game)
        {
            this.game = game;
            this.savedData = savedData;
            animation = 0.0f;
            car = new Car(game, savedData,103000,1770); // vytvoøní auta o výkonu 103kW a hmotnosti 1770kg
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
        public override void Initialize()
        {
            gameGraphics = new GameGraphics(game);
            camera = new Camera(game);
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
            if (game.SingleClick(Keys.Escape))
            {
                ComponentPause componentPause = new ComponentPause(game, car, this);
                Game.Components.Add(componentPause);
                Enabled = false;
            }
            if (game.SingleClick(Keys.E))
            {
                ComponentCharacter componentCharacter = new ComponentCharacter(game,car,savedData,gameGraphics);
                game.Components.Add(componentCharacter);
                game.ComponentEnable(this,false);
            }
            if (gameGraphics.Colision(car.CarRectangle()))
            {
                for (int i = 0; i < gameGraphics.graphicsList.ColisionList().Count; i++) //Kolize øešena pøes pixely
                {
                    car.PixelColision(car.CarRectangle(), car.CarData, gameGraphics.graphicsList.ColisionList()[i],
                        gameGraphics.graphicsList.objectsData()[i]);
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
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            car.DrawCar();
            gameGraphics.DrawGraphics();
            if (car.GetCarState().Equals(ECar.Colision))
            {
                if (animation < 1)
                    animation += 0.02f;
                game.spriteBatch.Draw(game.spritExplosion, new Vector2(car.Position.X - game.spritExplosion.Width / 2, car.Position.Y - game.spritExplosion.Height / 2), Color.White*(0.1f+animation));
            }
            game.spriteBatch.DrawString(game.bigFont, "Speed: " + car.CurrentSpeed()/2 + "\n" + car.GetCarState(), new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
