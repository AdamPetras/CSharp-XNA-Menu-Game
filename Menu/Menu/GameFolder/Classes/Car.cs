using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu.GameFolder.Classes
{
    public enum ECar
    {
        Forward,
        Backward,
        InertiaForward,
        InertiaBackward,
        Stop
    }

    public class Car
    {
        public Vector2 position;
        private Game game;
        private float angle;
        public ECar ECar;
        private CarPhysics physics;
        public Car(Game game)
        {
            this.game = game;
            angle = 0.0f;
            position = new Vector2(Game.width / 2, Game.height / 2);
            ECar = ECar.Stop;
            physics = new CarPhysics();
        }

        public void Move(GameTime gameTime)
        {
            if (game.keyState.IsKeyDown(Keys.Left))
            {
                if (ECar == ECar.Forward || ECar == ECar.InertiaForward)            //Podmínka, aby auto zatáčelo jen když jede vpřed
                    angle -= 0.02f;
                else if (ECar == ECar.Backward || ECar == ECar.InertiaBackward)     //Podmínka, aby auto zatáčelo jen když jede vzad
                    angle += 0.02f;
            }
            if (physics.Velocity <= 0)     //Pokud je rychlost menší než nula nebo nula tak se do enumerátoru hodí stop
                ECar = ECar.Stop;
            if (game.keyState.IsKeyDown(Keys.Right))
            {
                if (ECar == ECar.Forward || ECar == ECar.InertiaForward)            //Podmínka, aby auto zatáčelo jen když jede vpřed
                    angle += 0.02f;
                else if (ECar == ECar.Backward || ECar == ECar.InertiaForward)     //Podmínka, aby auto zatáčelo jen když jede vzad
                    angle -= 0.02f;
            }
            if (physics.Velocity <= 0)     //Pokud je rychlost menší než nula nebo nula tak se do enumerátoru hodí stop
                ECar = ECar.Stop;
            if (game.keyState.IsKeyDown(Keys.Up) && ECar != ECar.InertiaBackward) //Pokud jede vpřed
            {
                if (!game.keyState.IsKeyDown(Keys.Down) && ECar != ECar.Backward)    //pokud neni záčknuto nahoru i dolu
                {
                    ECar = ECar.Forward;
                    Position();
                }
            }
            if (game.keyState.IsKeyDown(Keys.Down) && ECar != ECar.InertiaForward)     //Pokud jede vzad
            {
                if (!game.keyState.IsKeyDown(Keys.Up) && ECar != ECar.Forward)      //pokud neni záčknuto nahoru i dolu
                {
                    ECar = ECar.Backward;
                    Position();
                }
            }
            Inertia(gameTime);
            Braking(gameTime);
            physics.Speed(gameTime, ECar);
        }

        private float Rotation()        //Zatáčení auta
        {
            float rotationAngle = 0;
            rotationAngle += angle;
            const float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;
            return rotationAngle;
        }

        private double Distance()       //zjištění ujeté vzdálenosti pro výpočet pozice
        {
            double distance = 0;
            if (ECar == ECar.Forward || ECar == ECar.InertiaForward)            //pokud jede dopřed nebo setrvačností dopřed
            {
                distance += physics.Velocity;
            }
            else if (ECar == ECar.Backward || ECar == ECar.InertiaBackward)     //pokud jede vzad nebo setrvačností vzda
            {
                distance -= physics.Velocity;
            }
            return distance;
        }

        private void Braking(GameTime gameTime)
        {
            if (ECar == ECar.InertiaForward && game.keyState.IsKeyDown(Keys.Down))      //Pokud je auto v setrvacnosti dopred a sipka dolu je stlacena tak se brzdi
            {
                physics.Brake(gameTime);
                Position();
            }
            if (ECar == ECar.InertiaBackward && game.keyState.IsKeyDown(Keys.Up))       //Pokud je auto v setrvacnosti vzad a sipka nahoru je stracena tak se brzdi
            {
                physics.Brake(gameTime);
                Position();
            }
        }

        private void Inertia(GameTime gameTime)
        {
            if ((game.keyState.IsKeyUp(Keys.Up) && game.keyState.IsKeyUp(Keys.Down) && physics.Velocity > 0))     //Pokud se nedrží tlačítko vpřed nebo vzad a auto je rozjeté
            {
                if (ECar == ECar.Forward)           //pokud jelo auto dopřed tak setrvačnost dopředu
                    ECar = ECar.InertiaForward;
                else if (ECar == ECar.Backward)     //pokud jelo auto dozad tak setrvačnost dozadu
                    ECar = ECar.InertiaBackward;
                physics.Inertia(gameTime);
                Position();
            }
            else if (game.keyState.IsKeyDown(Keys.Down) && game.keyState.IsKeyDown(Keys.Up))        //Pokud jsou obě tlačítka zmáčknuty tak se brzdí
            {
                physics.Brake(gameTime);
                Position();
            }
        }
        private void Position()     //Výpočet pozice
        {
            Distance();
            position.X = (float)(Math.Cos(angle) * Distance() + position.X);        //X = Cos(a) *ujeta vzdalenost + predchozi pozice
            position.Y = (float)(Math.Sin(angle) * Distance() + position.Y);        //Y = Sin(a) *ujeta vzdalenost + predchozi pozice
        }

        public double CurrentSpeed()
        {
            double velocity = physics.Velocity * 10;
            int speed = (int)velocity;
            return speed;
        }

        public void DrawCar()
        {
            game.spriteBatch.Draw(game.spritCar, new Rectangle((int)position.X, (int)position.Y, game.spritCar.Width, game.spritCar.Height), null, Color.White, Rotation(), new Vector2(game.spritCar.Width / 2, game.spritCar.Height / 2), SpriteEffects.None, 0f);
        }
    }
}
