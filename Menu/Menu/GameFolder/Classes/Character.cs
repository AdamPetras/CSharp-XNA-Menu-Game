using System;
using System.Collections.Generic;
using Menu.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu.GameFolder.Classes
{
    public class Character
    {
        private Game game;
        public Vector2 CharacterPosition { get; set; }
        public float Angle { get; set; }
        private double distance;
        private float timer;
        private const double interval = 150;
        private int currentFrame;
        private Holster holster;
        private Gun selectedGun;
        private Camera camera;
        private List<Bullet> bulletList;
        public Character(Game game, SavedData savedData, Camera camera, Vector2 position, float angle)
        {
            this.game = game;
            this.camera = camera;
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
            holster = new Holster(game.gunsList, this);
            bulletList = new List<Bullet>();
            selectedGun = null;
        }

        public void Move(GameTime gameTime)
        {
            #region Chůze
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key))
            {
                distance++;
                CharacterAnimation(gameTime);
                CharacterPosition = CalculatePosition(CharacterPosition,Angle);
            }
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key))
            {
                distance--;
                CharacterAnimation(gameTime);
                CharacterPosition = CalculatePosition(CharacterPosition,Angle);
            }
            #endregion
            #region Otáčení
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Left].Key))
            {
                Angle -= 0.05f;
            }
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Right].Key))
            {
                Angle += 0.05f;
            }
            #endregion
            #region Zbraně
            holster.PickUpGun();
            SelectGun();
            #region Flying Bullet
            if (selectedGun != null && game.SingleClick(Keys.H) && selectedGun.Ammo != 0)    // střílení
            {
                selectedGun.Ammo--;
                Bullet bullet = new Bullet(new Vector2(CharacterPosition.X-20,CharacterPosition.Y), game.spritBullet,Angle, selectedGun.FireRange);
                bulletList.Add(bullet);
            }
            if (bulletList.Count != 0)      // pokud je vystřelená kulka
            {
                    for (int i = 0; i < bulletList.Count; i++)
                    {
                        if (bulletList[i].BulletRange())    // pokud neni kulka na konci dostřelu tak letí
                        {
                            distance += 2;
                            bulletList[i].FireRange += (int)distance;
                            bulletList[i].Position = CalculatePosition(bulletList[i].Position, bulletList[i].Angle);     
                        }
                        else
                            bulletList.RemoveAt(i);

                    }                          
            }
            #endregion
            #endregion

            //-------------Pokud nechodí nebo je zmáčknuto dopředu i dozadu tak se nastaví obrázek stop-------------
            if (game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Down].Key) &&
                game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Up].Key) || 
                game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key) &&
                game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key))
                currentFrame = 0;

        }

        private Vector2 CalculatePosition(Vector2 position, float angle)
        {
            position.X = (float)(Math.Cos(angle) * distance + position.X);
            //X = Cos(uhlu) *ujeta vzdalenost + predchozi pozice
            position.Y = (float)(Math.Sin(angle) * distance + position.Y);
            //Y = Sin(uhlu) *ujeta vzdalenost + predchozi pozice
            distance = 0;
            return position;
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
            Vector2 position = CharacterPosition;
            position.X = (float)(Math.Cos(Angle + 1.5) * game.spritCharacter[currentFrame].Width + CharacterPosition.X);
            //X = Cos(uhlu) *šířka charakteru + predchozi pozice
            position.Y = (float)(Math.Sin(Angle + 1.5) * game.spritCharacter[currentFrame].Height + CharacterPosition.Y);
            //Y = Sin(uhlu) *šířka charakteru + predchozi pozice
            CharacterPosition = position;
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
            return new Rectangle((int)CharacterPosition.X - game.spritCharacter[currentFrame].Width / 2, (int)CharacterPosition.Y - game.spritCharacter[currentFrame].Height / 2, game.spritCharacter[currentFrame].Width, game.spritCharacter[currentFrame].Height);
        }
        public void SelectGun()
        {
            if (game.mouseState.ScrollWheelValue / 120 < holster.GetHolster().Count &&
                game.mouseState.ScrollWheelValue / 120 > 0)
                selectedGun = holster.GetHolster()[game.mouseState.ScrollWheelValue / 120];
            else
            {
                selectedGun = null;
            }
        }

        public void DrawCharacter()
        {
            foreach (Bullet bullet in bulletList)
            {
                game.spriteBatch.Draw(bullet.Texture, new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, bullet.Texture.Width, bullet.Texture.Height), null, Color.White, bullet.Angle + 1.5f, new Vector2(bullet.Texture.Width / 2, bullet.Texture.Height / 2), SpriteEffects.None, 0f);
            }
            if (selectedGun != null)
            {
                game.spriteBatch.Draw(selectedGun.Texture, new Rectangle((int)CharacterPosition.X, (int)CharacterPosition.Y, selectedGun.Texture.Width, selectedGun.Texture.Height), null, Color.White, Rotation() + 1.5f, new Vector2(-12, selectedGun.Texture.Height / 1.2f), SpriteEffects.None, 0f);
                game.spriteBatch.DrawString(game.normalFont, selectedGun.EGun + "\n" + selectedGun.Ammo, new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
            }
            else
                game.spriteBatch.DrawString(game.normalFont, "No Gun", new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
            game.spriteBatch.Draw(game.spritCharacter[currentFrame], new Rectangle((int)CharacterPosition.X, (int)CharacterPosition.Y, game.spritCharacter[currentFrame].Width, game.spritCharacter[currentFrame].Height), null, Color.White, Rotation()+1.5f, new Vector2(game.spritCharacter[currentFrame].Width / 2, game.spritCharacter[currentFrame].Height / 2), SpriteEffects.None, 0f);

        }
    }
}
