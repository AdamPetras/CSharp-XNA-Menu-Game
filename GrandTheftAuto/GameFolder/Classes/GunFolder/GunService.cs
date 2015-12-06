using System;
using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Classes.GunFolder
{
    public class GunService
    {
        public Holster Holster { get; private set; }
        public List<Bullet> BulletList { get; private set; }
        public Gun SelectedGun { get; set; }
        private GameClass game;
        private int index;
        private float shotTimer;                //timer pro střílení 
        private float reloadTimer;              //timer pro nabíjení
        private double spawnTimer;              //timer pro objevení zbraně
        private const int reloading = 2000;     //konstanta pro určení doby nabíjení
        private Character character;

        public delegate void ChangeGunHandle();
        public event ChangeGunHandle EventChangeGun;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="character"></param>
        /// <param name="camera"></param>
        /// <param name="savedData"></param>
        public GunService(GameClass game, Character character, SavedData savedData)
        {
            this.game = game;
            this.character = character;
            EventChangeGun = ChangeGun;     //Event na změnu zbraně
            Holster = new Holster(game.gunsOptions, character, savedData);
            BulletList = new List<Bullet>();
            SelectedGun = null;
            if (Holster.HolsterList.Count != 0)
                SelectedGun = Holster.HolsterList.First();
            shotTimer = 0;
            reloadTimer = 0;
            index = 0;
        }

        public void PickUpGun()
        {
            Holster.PickUpGun(this);
        }

        public void Reloading(GameTime gameTime)
        {
            if (SelectedGun != null && character.Alive)
            {
                if (game.SingleClick(game.controlsList[(int)EKeys.R].Key) &&
                    SelectedGun.Magazine != SelectedGun.MaxMagazine && SelectedGun.Ammo > 0)
                {
                    Holster.Reload(SelectedGun);
                    game.EGameState = EGameState.Reloading;
                }
                if (game.EGameState == EGameState.Reloading)
                {
                    reloadTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (reloadTimer > reloading)
                    {
                        game.EGameState = EGameState.InGameOut;
                        reloadTimer = 0;
                    }
                }
            }
        }
        public void GeneratingGuns(GameTime gameTime, List<Rectangle> graphicsRectangles)
        {
            spawnTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (20000 < spawnTimer)
            {
                Random rand = new Random();
                game.gunsOptions.AddGun(GeneratePosition(rand, graphicsRectangles), rand.Next(0, Enum.GetNames(typeof(EGuns)).Length));
                spawnTimer = 0;
            }
        }
        private Vector2 GeneratePosition(Random rand, List<Rectangle> graphicsRectangles)
        {
            Vector2 position;
            do
            {
                position = new Vector2(rand.Next(0, 1000), rand.Next(0, 1000));
            } while (graphicsRectangles.Any(s => s.Contains(position)));
            return position;
        }
        public void Shooting(GameTime gameTime)
        {
            if (SelectedGun != null && game.mouseState.LeftButton == ButtonState.Pressed && SelectedGun.Magazine != 0 && game.EGameState != EGameState.Reloading && character.Alive)    // střílení
            {
                shotTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (SelectedGun.FireRate < shotTimer)
                {
                    SelectedGun.Magazine--;
                    double d = 20;
                    Bullet bullet =
                        new Bullet(game.CalculatePosition(character.Position, character.Angle + 1f, ref d),
                        game.spritBullet, character.Angle, SelectedGun.Damage, SelectedGun.FireRange,SelectedGun.DamageRange);
                    BulletList.Add(bullet);
                    shotTimer = 0;
                }
            }
        }

        public void BulletFly()
        {
            if (BulletList.Count != 0)      // pokud je vystřelená kulka
            {
                for (int i = 0; i < BulletList.Count; i++)
                {
                    if (BulletList[i].BulletRange())    // pokud neni kulka na konci dostřelu tak letí
                    {
                        double bulletSpeed = 10;
                        BulletList[i].FireRange += (int)bulletSpeed;
                        BulletList[i].Position = game.CalculatePosition(BulletList[i].Position, BulletList[i].Angle, ref bulletSpeed);
                    }
                    else
                        BulletList.RemoveAt(i);
                }
            }
        }

        public void SelectGun()
        {
            if (game.EGameState != EGameState.Reloading && character.Alive)
            {
                OnEventChangeGun();
            }
        }

        public void BulletColision(GraphicsService graphicsService)
        {
            if (BulletList.Count != 0)
            {
                foreach (Rectangle graphics in graphicsService.ColisionList())
                {
                    for (int i = 0; i < BulletList.Count; i++)
                    {
                        if (graphics.Contains(new Point((int)BulletList[i].Position.X, (int)BulletList[i].Position.Y)))
                        {
                            BulletList.Remove(BulletList[i]);
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            foreach (Bullet bullet in BulletList)
            {
                game.spriteBatch.Draw(bullet.Texture, new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, bullet.Texture.Width, bullet.Texture.Height), null, Color.White, bullet.Angle + 1.5f, new Vector2(bullet.Texture.Width / 2, bullet.Texture.Height / 2), SpriteEffects.None, 0f);
            }
            if (game.EGameState == EGameState.Reloading)
            {
                game.spriteBatch.Draw(SelectedGun.Texture, new Rectangle((int)character.Position.X, (int)character.Position.Y, SelectedGun.Texture.Width, SelectedGun.Texture.Height), null, Color.White, character.Angle + 1.5f, new Vector2(-12, SelectedGun.Texture.Height / 1.2f), SpriteEffects.None, 0f);
            }
            else if (SelectedGun != null)
            {
                game.spriteBatch.Draw(SelectedGun.Texture, new Rectangle((int)character.Position.X, (int)character.Position.Y, SelectedGun.Texture.Width, SelectedGun.Texture.Height), null, Color.White, character.Angle + 1.5f, new Vector2(-12, SelectedGun.Texture.Height / 1.2f), SpriteEffects.None, 0f);
            }
        }

        private void ChangeGun()
        {
            if (game.SingleClick(game.controlsList[(int)EKeys.Q].Key) && Holster.HolsterList.Count != 0)
            {
                index = Holster.HolsterList.IndexOf(SelectedGun);
                SelectedGun = index > 0 ? Holster.HolsterList[index - 1] : Holster.HolsterList[Holster.HolsterList.Count - 1];
            }
        }

        protected virtual void OnEventChangeGun()       
        {
            if (EventChangeGun != null) EventChangeGun();
        }
    }
}
