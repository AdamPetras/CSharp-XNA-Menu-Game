using System;
using Menu.GameFolder.Interface;
using Menu.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
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
        Stop,
        Colision
    }

    public enum EKeys
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }

    public class Car
    {
        private Game game;
        public Vector2 Position { get; set; }
        public float Angle { get; private set; }
        public Color[] CarData { get; private set; }

        private double velocity;
        private double enginePower;
        private double carWeight;
        private ECar ECar;
        private readonly EKeys EKeys = EKeys.Down;
        private IPhysics physics;

        public Car(Game game, SavedData savedData,double enginePower,double carWeight)
        {
            this.game = game;
            if (savedData.InTheCar||!savedData.InTheCar)
            {
                Angle = savedData.Angle;
                Position = savedData.Position;
            }
            else
            {
                Position = Vector2.Zero;
                Angle = 0f;
            }
            this.enginePower = enginePower;
            this.carWeight = carWeight;
            ECar = ECar.Stop;
            game.EGameState = EGameState.InGameCar;
            physics = new CarPhysics();

            //Načtení dat ke kolizi
            CarData = new Color[game.spritCar.Width*game.spritCar.Height];
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

            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key) && ECar != ECar.InertiaBackward) //Pokud jede vpřed
            {
                if (game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Down].Key) && ECar != ECar.Backward)
                    //pokud neni záčknuto dolu a auto nejede dozadu
                {
                    ECar = ECar.Forward;
                    CurrentPosition(gameTime);
                }
            }

            #endregion

            #region Dozadu

            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key) && ECar != ECar.InertiaForward) //Pokud jede vzad
            {
                if (game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Up].Key) && ECar != ECar.Forward)
                    //pokud neni záčknuto nahoru a auto nejede dopředu
                {
                    ECar = ECar.Backward;
                    CurrentPosition(gameTime);
                }
            }
            #endregion

            Inertia(gameTime); //Setrvačnost
            Braking(gameTime); //Brždění
            physics.Speed(ref velocity, ECar, carWeight, enginePower, gameTime); //Rychlost   
            if (CurrentSpeed() == 0) //Pokud je rychlost menší než nula nebo nula tak se do enumerátoru hodí stop
                ECar = ECar.Stop;
        }

        /// <summary>
        /// Zatáčení auta
        /// </summary>
        /// <returns></returns>
        private float Rotation() //Zatáčení auta
        {
            float rotationAngle = 0;
            rotationAngle += Angle;
            const float circle = MathHelper.Pi*2;
            rotationAngle = rotationAngle%circle;
            return rotationAngle;
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
                physics.Brake(ref velocity,carWeight);
                CurrentPosition(gameTime);
            }
            else if (ECar == ECar.InertiaBackward && game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key))
                //Pokud je auto v setrvacnosti vzad a sipka nahoru je stracena tak se brzdi
            {
                physics.Brake(ref velocity,carWeight);
                CurrentPosition(gameTime);
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
                CurrentPosition(gameTime);
            }
            else if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key) && game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key))
                //Pokud jsou obě tlačítka zmáčknuty tak se brzdí
            {
                physics.Brake(ref velocity, carWeight);
                CurrentPosition(gameTime);
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
                distance += (velocity*gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (ECar == ECar.Backward || ECar == ECar.InertiaBackward) //pokud jede vzad nebo setrvačností vzda
            {
                distance -= (velocity*gameTime.ElapsedGameTime.TotalSeconds);
            }
            return distance;
        }

        /// <summary>
        /// Výpočet pozice
        /// </summary>
        private void CurrentPosition(GameTime gameTime)
        {
            Vector2 position = Position;
            position.X = (float) (Math.Cos(Angle)*Distance(gameTime) + Position.X);
            //X = Cos(uhlu) *ujeta vzdalenost + predchozi pozice
            position.Y = (float) (Math.Sin(Angle)*Distance(gameTime) + Position.Y);
            Position = position;
            //Y = Sin(uhlu) *ujeta vzdalenost + predchozi pozice
        }

        /// <summary>
        /// Vracení aktuální rychlosti
        /// </summary>
        /// <returns></returns>
        public int CurrentSpeed()
        {                       
            return (int)velocity;
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
        /// Normal Rectangle Colision použití kvůli ušetření výkonu
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public bool CarColision(GraphicsList objects)
        {
            for (int i = 0; i < objects.ColisionList().Count; i++)
            {
                if (objects.ColisionList()[i].Intersects(CarRectangle()))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Pixel Colision
        /// </summary>
        /// <param name="rectangleCar"></param>
        /// <param name="dataCar"></param>
        /// <param name="rectangleObject"></param>
        /// <param name="dataObject"></param>
        public void PixelColision(Rectangle rectangleCar, Color[] dataCar,
                                Rectangle rectangleObject, Color[] dataObject)
        {
            // Hlednání mezí kolize čtyřůhelníků
            int top = Math.Max(rectangleCar.Top, rectangleObject.Top);
            int bottom = Math.Min(rectangleCar.Bottom, rectangleObject.Bottom);
            int left = Math.Max(rectangleCar.Left, rectangleObject.Left);
            int right = Math.Min(rectangleCar.Right, rectangleObject.Right);
            // Kontrola každého bodu uvnitř textur
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Získání barvy z obou pixelů v každém bodě
                    Color colorCar = dataCar[(x - rectangleCar.Left) +
                                         (y - rectangleCar.Top) * rectangleCar.Width];
                    Color colorObject = dataObject[(x - rectangleObject.Left) +
                                         (y - rectangleObject.Top) * rectangleObject.Width];

                    // Pokud nejsou oba pixely průhledné nebo transparentní
                    if (colorCar.A != 0 && colorObject.A != 0)
                    {
                        // Pokud dojde ke kolizi
                        ECar = ECar.Colision;
                        velocity = 0;
                    }
                }
            }
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
            for (int i = 0; i <= game.spritGameBackground.Width * 10; i += game.spritGameBackground.Width)
            {
                for (int j = 0; j <= game.spritGameBackground.Height * 10; j += game.spritGameBackground.Height)
                    game.spriteBatch.Draw(game.spritGameBackground, new Vector2(i, j), Color.White);

            }
            game.spriteBatch.Draw(game.spritCar, new Rectangle((int)Position.X, (int)Position.Y, game.spritCar.Width, game.spritCar.Height), null, Color.White, Rotation(), new Vector2(game.spritCar.Width / 2, game.spritCar.Height / 2), SpriteEffects.None, 0f);
        }
    }
}