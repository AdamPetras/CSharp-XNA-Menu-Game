using System;
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

    public class Movement
    {
        public Vector2 position;
        private Game game;
        public float angle;
        private ECar eCar;
        public Movement(Game game)
        {
            this.game = game;
            angle = 0.0f;
            position = new Vector2(Game.width / 2, Game.height / 2);
            eCar = ECar.Stop;
        }

        public void Move()
        {
            if (game.keyState.IsKeyDown(Keys.A))
            {
                angle -= 0.02f;
            }
            if (game.keyState.IsKeyDown(Keys.D))
            {
                angle += 0.02f;
            }
            if (game.keyState.IsKeyDown(Keys.W))
            {
                eCar = ECar.Forward;
                Distance();
                Position();
            }
            if (game.keyState.IsKeyDown(Keys.S))
            {
                eCar = ECar.Backward;
                Distance();
                Position();
            }
            else if(eCar != ECar.Stop)
            eCar = ECar.Stop;
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
                distance+=2;
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

        public void DrawPosition()
        {
            game.spriteBatch.Draw(game.spritCar, new Rectangle((int)position.X, (int)position.Y, game.spritCar.Width, game.spritCar.Height), null, Color.White, Rotation(), new Vector2(game.spritCar.Width / 2, game.spritCar.Height / 2), SpriteEffects.None, 0f);
        }
    }
}
