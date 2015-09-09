using System;
using Menu.GameFolder.Components;
using Menu.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu.GameFolder.Classes
{
    public class Character
    {
        private Game game;
        public Vector2 Position { get; set; }
        public float Angle { get; set; }
        private double distance;
        private float timer;
        private const double interval = 175;
        private int currentFrame;

        public Character(Game game, SavedData savedData, Vector2 position, float angle)
        {
            this.game = game;
            if (!savedData.InTheCar)
            {
                Position = savedData.CharacterPosition;
                Angle = savedData.CharacterAngle;
            }
            else
            {
                Position = position;
                Angle = angle;
                GetOutOfCar();
            }
            currentFrame = 0;
        }

        public void Move(GameTime gameTime)
        {
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key))
            {
                distance++;
                CharacterAnimation(gameTime);
                CurrentPosition();
            }
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key))
            {
                distance--;
                CharacterAnimation(gameTime);
                CurrentPosition();
            }
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Left].Key))
            {
                Angle -= 0.05f;
            }
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Right].Key))
            {
                Angle += 0.05f;
            }

        }
        private void CurrentPosition()
        {
            Vector2 position = Position;
            position.X = (float)(Math.Cos(Angle) * distance + Position.X);
            //X = Cos(uhlu) *ujeta vzdalenost + predchozi pozice
            position.Y = (float)(Math.Sin(Angle) * distance + Position.Y);
            //Y = Sin(uhlu) *ujeta vzdalenost + predchozi pozice
            distance = 0;
            Position = position;

        }

        private void CharacterAnimation(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                // Show the next frame
                currentFrame++;
                // Reset the timer
                timer = 0f;
            }
            if (currentFrame == 5)
            {
                currentFrame = 0;
            }
        }

        private void GetOutOfCar()
        {
            Vector2 position = Position;
            position.X = (float)(Math.Cos(Angle + 1.5) * game.spritCharacter[currentFrame].Width + Position.X);
            //X = Cos(uhlu) *ujeta vzdalenost + predchozi pozice
            position.Y = (float)(Math.Sin(Angle + 1.5) * game.spritCharacter[currentFrame].Height + Position.Y);
            Position = position;
        }

        private float Rotation() //Zatáčení auta
        {
            float rotationAngle = 0;
            rotationAngle += Angle;
            const float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;
            return rotationAngle;
        }

        public Rectangle CharacterRectangle()
        {
            return new Rectangle((int)Position.X-game.spritCharacter[currentFrame].Width/2, (int)Position.Y-game.spritCharacter[currentFrame].Height/2, game.spritCharacter[currentFrame].Width, game.spritCharacter[currentFrame].Height);
        }

        public void Draw()
        {
            game.spriteBatch.Draw(game.spritCharacter[currentFrame], new Rectangle((int)Position.X, (int)Position.Y, game.spritCharacter[currentFrame].Width, game.spritCharacter[currentFrame].Height), null, Color.White, Rotation() + 1.5f, new Vector2(game.spritCharacter[currentFrame].Width / 2, game.spritCharacter[currentFrame].Height / 2), SpriteEffects.None, 0f);
        }
    }
}
