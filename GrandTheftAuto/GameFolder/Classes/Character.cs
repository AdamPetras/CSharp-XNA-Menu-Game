using System;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Character
    {
        private GameClass game;
        public Vector2 CharacterPosition { get; set; }
        public float Angle { get; set; }
        public int Hp { get; set; }
        public bool Alive { get; private set; }

        private double distance;
        private float animationTimer;       //timer pro animace postavy
        private const double interval = 150;    //interval na animaci postavy
        private int currentFrame;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="savedData"></param>
        /// <param name="position"></param>
        /// <param name="angle"></param>
        public Character(GameClass game, SavedData savedData, Vector2 position, float angle)
        {
            this.game = game;
            Hp = 100;
            Alive = true;
            if (!savedData.InTheCar)
            {
                CharacterPosition = savedData.CharacterPosition;
                Angle = savedData.CharacterAngle;
            }
            else
            {
                CharacterPosition = position;
                Angle = angle;
                GetOutOfCar();
            }
            currentFrame = 0;
        }
        /// <summary>
        /// Updatable method to detect keypress and then call the other method
        /// </summary>
        /// <param name="gameTime"></param>
        public void Move(GameTime gameTime)
        {
            #region Chůze
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key) && Alive)
            {
                distance++;
                CharacterAnimation(gameTime);
                CharacterPosition = game.CalculatePosition(CharacterPosition, Angle, ref distance);
            }
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key) && Alive)
            {
                distance--;
                CharacterAnimation(gameTime);
                CharacterPosition = game.CalculatePosition(CharacterPosition, Angle, ref distance);
            }
            #endregion
            #region Otáčení
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Left].Key) && Alive)
            {
                Angle -= 0.05f;
            }
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Right].Key) && Alive)
            {
                Angle += 0.05f;
            }
            #endregion

            //-------------Pokud nechodí nebo je zmáčknuto dopředu i dozadu tak se nastaví obrázek stop-------------
            if (game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Down].Key) &&
                game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Up].Key) ||
                game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key) &&
                game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key))
                currentFrame = 0;

        }

        public void Live()
        {
                Alive = Hp > 0;
        }

        /// <summary>
        /// Method to change frame of character
        /// </summary>
        /// <param name="gameTime"></param>
        private void CharacterAnimation(GameTime gameTime)
        {
            animationTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (animationTimer > interval)
            {
                // Show the next frame
                currentFrame++;
                // Reset the timer
                animationTimer = 0f;
            }
            if (currentFrame == 5)
            {
                currentFrame = 0;
            }
        }
        /// <summary>
        /// Method to get position if i wanna get out of car
        /// </summary>
        private void GetOutOfCar()
        {
            Vector2 position = CharacterPosition;
            position.X = (float)(Math.Cos(Angle + 1.5) * game.spritCharacter[currentFrame].Width + CharacterPosition.X);
            //X = Cos(uhlu) *šířka charakteru + predchozi pozice
            position.Y = (float)(Math.Sin(Angle + 1.5) * game.spritCharacter[currentFrame].Height + CharacterPosition.Y);
            //Y = Sin(uhlu) *šířka charakteru + predchozi pozice
            CharacterPosition = position;
        }
        /// <summary>
        /// Method to get rectangle of character
        /// </summary>
        /// <returns></returns>
        public Rectangle CharacterRectangle()
        {
            return new Rectangle((int)CharacterPosition.X - game.spritCharacter[currentFrame].Width / 2, (int)CharacterPosition.Y - game.spritCharacter[currentFrame].Height / 2, game.spritCharacter[currentFrame].Width, game.spritCharacter[currentFrame].Height);
        }
        /// <summary>
        /// Method to draw character
        /// </summary>
        public void DrawCharacter()
        {
            game.spriteBatch.Draw(game.spritCharacter[currentFrame], new Rectangle((int)CharacterPosition.X, (int)CharacterPosition.Y, game.spritCharacter[currentFrame].Width, game.spritCharacter[currentFrame].Height), null, Color.White, game.Rotation(Angle) + 1.5f, new Vector2(game.spritCharacter[currentFrame].Width / 2, game.spritCharacter[currentFrame].Height / 2), SpriteEffects.None, 0f);
        }
    }
}
