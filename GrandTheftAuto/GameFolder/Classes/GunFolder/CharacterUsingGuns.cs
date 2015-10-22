using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Classes.GunFolder
{
    public class CharacterUsingGuns
    {
        public Holster Holster { get; private set; }
        public List<Bullet> BulletList { get; private set; }
        private GameClass game;
        private int index;
        private GunFolder.Gun selectedGun;
        private float shotTimer;                //timer pro střílení 
        private float reloadTimer;              //timer pro nabíjení
        private const int reloading = 2000;     //konstanta pro určení doby nabíjení
        private Character character;
        private Camera camera;

        public delegate void ChangeGunHandle();
        public event ChangeGunHandle EventChangeGun;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="character"></param>
        /// <param name="camera"></param>
        /// <param name="savedData"></param>
        public CharacterUsingGuns(GameClass game, Character character, Camera camera, SavedData savedData)
        {
            this.game = game;
            this.camera = camera;
            this.character = character;
            EventChangeGun = ChangeGun;     //Event na změnu zbraně
            Holster = new Holster(game.gunsOptions, character, savedData);
            BulletList = new List<Bullet>();
            selectedGun = null;
            if (Holster.GetHolster().Count != 0)
                selectedGun = Holster.GetHolster().First();
            shotTimer = 0;
            reloadTimer = 0;
            index = 0;
        }

        public void PickUpGun()
        {
            Holster.PickUpGun(ref selectedGun);
        }

        public void Reloading(GameTime gameTime)
        {
            if (selectedGun != null && character.Alive)
            {
                if (game.SingleClick(game.controlsList[(int)EKeys.R].Key) &&
                    selectedGun.Magazine != selectedGun.MaxMagazine && selectedGun.Ammo > 0)
                {
                    Holster.Reload(selectedGun);
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

        public void Shooting(GameTime gameTime)
        {
            if (selectedGun != null && game.mouseState.LeftButton == ButtonState.Pressed && selectedGun.Magazine != 0 && game.EGameState != EGameState.Reloading && character.Alive)    // střílení
            {
                shotTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (selectedGun.FireRate < shotTimer)
                {
                    selectedGun.Magazine--;
                    double d = 20;
                    Bullet bullet =
                        new Bullet(game.CalculatePosition(character.CharacterPosition, character.Angle + 1f, ref d),
                        game.spritBullet, character.Angle, selectedGun.Damage, selectedGun.FireRange);
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

        public void BulletColision(GraphicsList graphicsList)
        {
            if (BulletList.Count != 0)
            {
                foreach (Rectangle graphics in graphicsList.ColisionList())
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
                game.spriteBatch.Draw(selectedGun.Texture, new Rectangle((int)character.CharacterPosition.X, (int)character.CharacterPosition.Y, selectedGun.Texture.Width, selectedGun.Texture.Height), null, Color.White, character.Angle + 1.5f, new Vector2(-12, selectedGun.Texture.Height / 1.2f), SpriteEffects.None, 0f);
                game.spriteBatch.DrawString(game.normalFont, selectedGun.EGun + "\nReloading", new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
            }
            else if (selectedGun != null)
            {
                game.spriteBatch.Draw(selectedGun.Texture, new Rectangle((int)character.CharacterPosition.X, (int)character.CharacterPosition.Y, selectedGun.Texture.Width, selectedGun.Texture.Height), null, Color.White, character.Angle + 1.5f, new Vector2(-12, selectedGun.Texture.Height / 1.2f), SpriteEffects.None, 0f);
                game.spriteBatch.DrawString(game.normalFont, selectedGun.EGun + "\n" + selectedGun.Magazine + "/" + selectedGun.Ammo, new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
            }
            else
                game.spriteBatch.DrawString(game.normalFont, "No Gun",
                    new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
        }

        private void ChangeGun()
        {
            if (game.SingleClick(game.controlsList[(int)EKeys.Q].Key) && Holster.GetHolster().Count != 0)
            {
                index = Holster.GetHolster().IndexOf(selectedGun);
                selectedGun = index > 0 ? Holster.GetHolster()[index - 1] : Holster.GetHolster()[Holster.GetHolster().Count - 1];
            }
        }

        protected virtual void OnEventChangeGun()
        {
            if (EventChangeGun != null) EventChangeGun();
        }
    }
}
