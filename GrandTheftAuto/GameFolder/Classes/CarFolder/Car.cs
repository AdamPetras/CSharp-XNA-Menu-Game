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
        public int Hp { get; set; }
        public bool Colision { get; set; }
        private double velocity;
        private double enginePower;
        private double carWeight;
        private ECar ECar;
        private IPhysics physics;

        public Car(GameClass game, SavedData savedData, double enginePower, double carWeight)
        {
            this.game = game;
            if (savedData.InTheCar || !savedData.InTheCar)  //pokud je save
            {
                Angle = savedData.Angle;
                Position = savedData.Position;
                Hp = savedData.CarHp;
            }
            else
            {           //pokud neni save
                Position = Vector2.Zero;
                Angle = 0f;
                Hp = 1000;
            }
            this.enginePower = enginePower;
            this.carWeight = carWeight;
            ECar = ECar.Stop;
            game.EGameState = EGameState.InGameCar;
            physics = new CarPhysics();
            OriginVector = new Vector2(game.spritCar.Width / 2, game.spritCar.Height / 2);
            //Načtení dat ke kolizi
            CarData = new Color[game.spritCar.Width * game.spritCar.Height];
            game.spritCar.GetData(CarData);
        }

        /// <summary>
        /// Pohyb auta
        /// </summary>
        /// <param name="gameTime"></param>
        public void Move(GameTime gameTime)
        {
                #region Doleva

                if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Left].Key))
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

                if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Right].Key))
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

                if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key) && ECar != ECar.InertiaBackward)
                //Pokud jede vpřed
                {
                    if (game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Down].Key) && ECar != ECar.Backward)
                    //pokud neni záčknuto dolu a auto nejede dozadu
                    {
                        ECar = ECar.Forward;
                        Position = CalculatePosition(Position, gameTime);
                    }
                }

                #endregion

                #region Dozadu

                if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key) && ECar != ECar.InertiaForward)
                //Pokud jede vzad
                {
                    if (game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Up].Key) && ECar != ECar.Forward)
                    //pokud neni záčknuto nahoru a auto nejede dopředu
                    {
                        ECar = ECar.Backward;
                        Position = CalculatePosition(Position, gameTime);
                    }
                }

                #endregion

                #region Ruční brzda

                if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Space].Key) && velocity > 0)
                {
                    physics.Brake(ref velocity, carWeight);
                    if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Right].Key) && velocity > 30)
                        Angle += 0.03f;
                    if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Left].Key) && velocity > 30)
                        Angle -= 0.03f;
                }

                #endregion

                Inertia(gameTime); //Setrvačnost
                Braking(gameTime); //Brždění
                physics.Speed(ref velocity, ECar, carWeight, enginePower, gameTime); //Rychlost   
            if (Colision || Hp<=0)
                ECar = ECar.Colision;
            if (CurrentSpeed() == 0 && ECar != ECar.Colision)
                //Pokud je rychlost menší než nula nebo nula tak se do enumerátoru hodí stop
                ECar = ECar.Stop;
        }

        /// <summary>
        /// Brždění
        /// </summary>
        /// <param name="gameTime"></param>
        private void Braking(GameTime gameTime)
        {
            if (ECar == ECar.InertiaForward && game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key))
            //Pokud je auto v setrvacnosti dopred a sipka dolu je stlacena tak se brzdi
            {
                physics.Brake(ref velocity, carWeight);
                Position = CalculatePosition(Position, gameTime);
            }
            else if (ECar == ECar.InertiaBackward && game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key))
            //Pokud je auto v setrvacnosti vzad a sipka nahoru je stracena tak se brzdi
            {
                physics.Brake(ref velocity, carWeight);
                Position = CalculatePosition(Position, gameTime);
            }
        }

        /// <summary>
        /// Setrvačnost
        /// </summary>
        /// <param name="gameTime"></param>
        private void Inertia(GameTime gameTime)
        {
            if ((game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Up].Key) && game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Down].Key) && velocity > 0))
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
            else if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key) && game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key))
            //Pokud jsou obě tlačítka zmáčknuty tak se brzdí
            {
                physics.Brake(ref velocity, carWeight);
                Position = CalculatePosition(Position, gameTime);
            }
        }

        /// <summary>
        /// Zjištění ujeté vzdálenosti pro výpočet pozice
        /// </summary>
        /// <returns></returns>
        private double Distance(GameTime gameTime)
        {
            double distance = 0;
            if (ECar == ECar.Forward || ECar == ECar.InertiaForward) //pokud jede dopřed nebo setrvačností dopřed
            {
                distance += (velocity * gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (ECar == ECar.Backward || ECar == ECar.InertiaBackward) //pokud jede vzad nebo setrvačností vzda
            {
                distance -= (velocity * gameTime.ElapsedGameTime.TotalSeconds);
            }
            return distance;
        }

        /// <summary>
        /// Výpočet pozice
        /// </summary>
        private Vector2 CalculatePosition(Vector2 position, GameTime gameTime)
        {
            position.X = (float)(Math.Cos(Angle) * Distance(gameTime) + Position.X);
            //X = Cos(uhlu) *ujeta vzdalenost + predchozi pozice
            position.Y = (float)(Math.Sin(Angle) * Distance(gameTime) + Position.Y);
            //Y = Sin(uhlu) *ujeta vzdalenost + predchozi pozice
            return position;
        }

        /// <summary>
        /// Vracení aktuální rychlosti
        /// </summary>
        /// <returns></returns>
        public int CurrentSpeed()
        {
            return (int)velocity / 2;
        }

        /// <summary>
        /// Get rectangle position of car
        /// </summary>
        /// <returns></returns>
        public Rectangle CarRectangle()
        {
            return new Rectangle((int)Position.X - game.spritCar.Width / 2, (int)Position.Y - game.spritCar.Height / 2,
                game.spritCar.Width, game.spritCar.Height);
        }

        /// <summary>
        /// Vracení aktuálního stavu auta
        /// </summary>
        /// <returns></returns>
        public ECar GetCarState()
        {
            return ECar;
        }

        /// <summary>
        /// Vykreslení auta
        /// </summary>
        public void DrawCar()
        {
            game.spriteBatch.Draw(game.spritCar, new Rectangle((int)Position.X, (int)Position.Y, game.spritCar.Width, game.spritCar.Height), null, Color.White, game.Rotation(Angle), new Vector2(game.spritCar.Width / 2, game.spritCar.Height / 2), SpriteEffects.None, 0f);
        }
    }
}