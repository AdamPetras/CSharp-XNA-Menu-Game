using System;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Character
    {
        private GameClass game;
        public bool Alive { get; private set; }
        public int Energy { get; private set; }
        public Vector2 CharacterPosition { get; set; }
        public float Angle { get; set; }
        public int Hp { get; set; }


        private double speed;
        private double distance;
        private double animationTimer;       //timer pro animace postavy
        private double interval = 150;    //interval na animaci postavy
        private double energyDrainRegenerateTimer;
        private bool regeneration;
        private int currentFrame;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="savedData"></param>
        /// <param name="position"></param>
        /// <param name="angle"></param>
        public Character(GameClass game, SavedData savedData)
        {
            this.game = game;
            Hp = 100;
            Energy = 100;
            speed = 1;
            Alive = true;
            regeneration = false;
            if (!savedData.InTheCar)
            {
                CharacterPosition = savedData.CharacterPosition;
                Angle = savedData.CharacterAngle;
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
                distance += speed;
                CharacterAnimation(gameTime);
                CharacterPosition = game.CalculatePosition(CharacterPosition, Angle, ref distance);
            }
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key) && Alive)
            {
                distance -= speed;
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
            #region Běh
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Shift].Key) && Alive && Energy > 0 && !regeneration)
            {
                speed = 1.5;
                interval = 120;
                energyDrainRegenerateTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (energyDrainRegenerateTimer > 100)
                {
                    Energy -= 1;
                    energyDrainRegenerateTimer = 0;
                }
                if (Energy == 0)
                    speed = 1;
            }
            else if(Energy<100)
            {
                regeneration = true;
                speed = 1;
                interval = 150;
                energyDrainRegenerateTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (energyDrainRegenerateTimer > 200 && Energy < 100)
                {
                    Energy += 1;
                    energyDrainRegenerateTimer = 0;
                }
                if (Energy >= 40)
                    regeneration = false;
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
            animationTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
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
