using System;
using GrandTheftAuto.GameFolder.Interface;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes.CarFolder
{

    public enum ECar
    {
        Forward,
        Backward,
        InertiaForward,
        InertiaBackward,
        Stop,
        Colision
    }

    public class Car
    {
        private GameClass game;
        public Vector2 Position { get; set; }
        public Vector2 OriginVector { get; private set; }
        public float Angle { get; private set; }
        public Color[] CarData { get; private set; }
        public double Hp { get; set; }
        public bool Colision { get; set; }
        public bool Selected { get; set; }
        private double velocity;
        private double enginePower;
        private double carWeight;
        private ECar ECar;
        private IPhysics physics;

        /// <summary>
        /// Constuctor of Car
        /// </summary>
        /// <param name="game"></param>
        /// <param name="position"></param>
        /// <param name="enginePower"></param>
        /// <param name="carWeight"></param>
        public Car(GameClass game, Vector2 position, double enginePower, double carWeight)
        {
            this.game = game;
            Position = position;
            this.enginePower = enginePower;
            this.carWeight = carWeight;
            ECar = ECar.Stop;
            Selected = false;
            Hp = 1000;
            physics = new CarPhysics();
            OriginVector = new Vector2(game.spritCar.Width/2, game.spritCar.Height/2);
            //Načtení dat ke kolizi
            CarData = new Color[game.spritCar.Width*game.spritCar.Height];
            game.spritCar.GetData(CarData);
        }

        /// <summary>
        /// Method which is called in the other method Update and its updatable method of class Game
        /// </summary>
        /// <param name="gameTime"></param>
        public void Move(GameTime gameTime)
        {
            #region Doleva

            if (game.keyState.IsKeyDown(game.controlsList[(int) EKeys.Left].Key))
            {
                if (ECar == ECar.Forward || ECar == ECar.InertiaForward)
                    //Podmínka, aby auto zatáčelo jen když jede vpřed
                    Angle -= 0.02f;
                else if (ECar == ECar.Backward || ECar == ECar.InertiaBackward)
                    //Podmínka, aby auto zatáčelo jen když jede vzad
                    Angle += 0.02f;
            }

            #endregion

            #region Doprava

            if (game.keyState.IsKeyDown(game.controlsList[(int) EKeys.Right].Key))
            {
                if (ECar == ECar.Forward || ECar == ECar.InertiaForward)
                    //Podmínka, aby auto zatáčelo jen když jede vpřed
                    Angle += 0.02f;
                else if (ECar == ECar.Backward || ECar == ECar.InertiaBackward)
                    //Podmínka, aby auto zatáčelo jen když jede vzad
                    Angle -= 0.02f;
            }

            #endregion

            #region Dopředu

            if (game.keyState.IsKeyDown(game.controlsList[(int) EKeys.Up].Key) && ECar != ECar.InertiaBackward)
                //Pokud jede vpřed
            {
                if (game.keyState.IsKeyUp(game.controlsList[(int) EKeys.Down].Key) && ECar != ECar.Backward)
                    //pokud neni záčknuto dolu a auto nejede dozadu
                {
                    ECar = ECar.Forward;
                    Position = CalculatePosition(Position, gameTime);
                }
            }

            #endregion

            #region Dozadu

            if (game.keyState.IsKeyDown(game.controlsList[(int) EKeys.Down].Key) && ECar != ECar.InertiaForward)
                //Pokud jede vzad
            {
                if (game.keyState.IsKeyUp(game.controlsList[(int) EKeys.Up].Key) && ECar != ECar.Forward)
                    //pokud neni záčknuto nahoru a auto nejede dopředu
                {
                    ECar = ECar.Backward;
                    Position = CalculatePosition(Position, gameTime);
                }
            }

            #endregion

            #region Ruční brzda

            if (game.keyState.IsKeyDown(game.controlsList[(int) EKeys.Space].Key) && velocity > 0)
            {
                physics.Brake(ref velocity, carWeight);
                if (game.keyState.IsKeyDown(game.controlsList[(int) EKeys.Right].Key) && velocity > 30)
                    Angle += 0.03f;
                if (game.keyState.IsKeyDown(game.controlsList[(int) EKeys.Left].Key) && velocity > 30)
                    Angle -= 0.03f;
            }

            #endregion

            Inertia(gameTime); //Setrvačnost
            Braking(gameTime); //Brždění
            physics.Speed(ref velocity, ECar, carWeight, enginePower, gameTime); //Rychlost   
            if (Colision || Hp <= 0)
            {
                ECar = ECar.Colision;
                game.EGameState = EGameState.GameOver;
                Colision = true;
            }
            if (CurrentSpeed() == 0 && ECar != ECar.Colision)
                //Pokud je rychlost menší než nula nebo nula tak se do enumerátoru hodí stop
                ECar = ECar.Stop;
        }

        /// <summary>
        /// Method of braking the car
        /// </summary>
        /// <param name="gameTime"></param>
        private void Braking(GameTime gameTime)
        {
            if (ECar == ECar.InertiaForward && game.keyState.IsKeyDown(game.controlsList[(int) EKeys.Down].Key))
                //Pokud je auto v setrvacnosti dopred a sipka dolu je stlacena tak se brzdi
            {
                physics.Brake(ref velocity, carWeight);
                Position = CalculatePosition(Position, gameTime);
            }
            else if (ECar == ECar.InertiaBackward && game.keyState.IsKeyDown(game.controlsList[(int) EKeys.Up].Key))
                //Pokud je auto v setrvacnosti vzad a sipka nahoru je stracena tak se brzdi
            {
                physics.Brake(ref velocity, carWeight);
                Position = CalculatePosition(Position, gameTime);
            }
        }

        /// <summary>
        /// Method to calculate inertia of the car
        /// </summary>
        /// <param name="gameTime"></param>
        private void Inertia(GameTime gameTime)
        {
            if ((game.keyState.IsKeyUp(game.controlsList[(int) EKeys.Up].Key) &&
                 game.keyState.IsKeyUp(game.controlsList[(int) EKeys.Down].Key) && velocity > 0))
                //Pokud se nedrží tlačítko vpřed nebo vzad a auto je rozjeté
            {
                switch (ECar)
                {
                    case ECar.Forward:
                        ECar = ECar.InertiaForward;
                        break;
                    case ECar.Backward:
                        ECar = ECar.InertiaBackward;
                        break;
                }
                physics.Inertia(ref velocity);
                Position = CalculatePosition(Position, gameTime);
            }
            else if (game.keyState.IsKeyDown(game.controlsList[(int) EKeys.Down].Key) &&
                     game.keyState.IsKeyDown(game.controlsList[(int) EKeys.Up].Key))
                //Pokud jsou obě tlačítka zmáčknuty tak se brzdí
            {
                physics.Brake(ref velocity, carWeight);
                Position = CalculatePosition(Position, gameTime);
            }
        }
        /// <summary>
        /// Method if i wanna get out of car its return position me
        /// </summary>
        /// <returns> character position </returns>
        public Vector2 GetOutOfCar()
        {
            Vector2 position = new Vector2
            {
                X = (float) (Math.Cos(Angle + 1.5)*game.spritCharacter[0].Width + Position.X),
                Y = (float) (Math.Sin(Angle + 1.5)*game.spritCharacter[0].Height + Position.Y)
            };
            //X = Cos(uhlu) *šířka charakteru + predchozi pozice
            //Y = Sin(uhlu) *šířka charakteru + predchozi pozice
            return position;
        }

        /// <summary>
        /// Get the distance of way which car has driven
        /// </summary>
        /// <returns></returns>
        private double Distance(GameTime gameTime)
        {
            double distance = 0;
            if (ECar == ECar.Forward || ECar == ECar.InertiaForward) //pokud jede dopřed nebo setrvačností dopřed
            {
                distance += (velocity*gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (ECar == ECar.Backward || ECar == ECar.InertiaBackward) //pokud jede vzad nebo setrvačností vzda
            {
                distance -= (velocity*gameTime.ElapsedGameTime.TotalSeconds);
            }
            return distance;
        }

        /// <summary>
        /// Method to calculate position
        /// </summary>
        private Vector2 CalculatePosition(Vector2 position, GameTime gameTime)
        {
            position.X = (float) (Math.Cos(Angle)*Distance(gameTime) + Position.X);
            //X = Cos(uhlu) *ujeta vzdalenost + predchozi pozice
            position.Y = (float) (Math.Sin(Angle)*Distance(gameTime) + Position.Y);
            //Y = Sin(uhlu) *ujeta vzdalenost + predchozi pozice
            return position;
        }

        /// <summary>
        /// Returns the current speed of the car
        /// </summary>
        /// <returns></returns>
        public int CurrentSpeed()
        {
            return (int) velocity/2;
        }

        /// <summary>
        /// Get rectangle position of car
        /// </summary>
        /// <returns></returns>
        public Rectangle CarRectangle()
        {
            return new Rectangle((int) Position.X - game.spritCar.Width/2, (int) Position.Y - game.spritCar.Height/2,
                game.spritCar.Width, game.spritCar.Height);
        }

        /// <summary>
        /// Returns actual state of car
        /// </summary>
        /// <returns></returns>
        public ECar GetCarState()
        {
            return ECar;
        }

        /// <summary>
        /// Method which reset the property of car to the default values
        /// </summary>
        public void ResetProperty()
        {
            velocity = 0;
            Selected = false;
        }

        /// <summary>
        /// Draw of the car
        /// </summary>
        public void DrawCar()
        {
            game.spriteBatch.Draw(game.spritCar,
                new Rectangle((int)Position.X, (int)Position.Y, game.spritCar.Width, game.spritCar.Height), null,
                Color.White, game.Rotation(Angle), OriginVector,
                SpriteEffects.None, 0f);
        }       
    }
}