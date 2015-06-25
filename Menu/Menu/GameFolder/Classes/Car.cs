using System;
using System.Linq;
using Menu.GameFolder;
using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu.Components
{
    public enum ECar
    {
        Forward,
        Backward,
        Stop
    }

    public class Car
    {
        public Vector2 position;
        private Game game;
        private float angle;
        private ECar eCar;
        public CarPhysics physics;
        public Car(Game game)
        {
            this.game = game;
            angle = 0.0f;
            position = new Vector2(Game.width / 2, Game.height / 2);
            eCar = ECar.Stop;
            physics = new CarPhysics();
        }

        public void Move(GameTime gameTime)
        {

            if (game.keyState.IsKeyDown(Keys.Left))
            {
                if(eCar == ECar.Forward)
                angle -= 0.02f;
                else if(eCar == ECar.Backward)
                    angle += 0.02f;
            }
            if (game.keyState.IsKeyDown(Keys.Right))
            {
                if (eCar == ECar.Forward)
                angle += 0.02f;
                else if (eCar == ECar.Backward)
                    angle -= 0.02f;
            }
            if (game.keyState.IsKeyDown(Keys.Up))
            {
                eCar = ECar.Forward;
                Distance();
                Position();
            }
            if (game.keyState.IsKeyDown(Keys.Down))
            {
                eCar = ECar.Backward;
                Distance();
                Position();
            }
            if (game.keyState.IsKeyUp(Keys.Up) && game.keyState.IsKeyUp(Keys.Down))
            {
                eCar = ECar.Stop;
                physics.Reset();
            }
            physics.Speed(gameTime,eCar);
        }

        private float Rotation()
        {
            float rotationAngle = 0;
            rotationAngle += angle;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;
            return rotationAngle;
        }

        private double Distance()
        {
            double distance = 0;
            if (eCar == ECar.Forward)
            {
                distance+=physics.Velocity;
            }
            else if(eCar==ECar.Backward)
            {
                distance -= 2;
            }
            return distance;
        }

        private void Position()
        {
            position.X = (float)(Math.Cos(angle) * Distance() + position.X);
            position.Y = (float)(Math.Sin(angle) * Distance() + position.Y);  
        }

        public void DrawCar()
        {
            game.spriteBatch.Draw(game.spritCar, new Rectangle((int)position.X, (int)position.Y, game.spritCar.Width, game.spritCar.Height), null, Color.White, Rotation(), new Vector2(game.spritCar.Width / 2, game.spritCar.Height / 2), SpriteEffects.None, 0f);
        }
    }
}
