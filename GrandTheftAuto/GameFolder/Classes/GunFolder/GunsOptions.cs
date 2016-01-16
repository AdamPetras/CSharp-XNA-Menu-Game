using System;
using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes.GunFolder
{
    public enum EGuns
    {
        M9 = 0,
        P90,
        M4A1,
        Ak47,
        ScarL,
        ACWR,
        PKP,
        M60
    }
    public class GunsOptions
    {
        /// <summary>
        /// Enum of guns
        /// </summary>
        private GameClass game;
        public List<Gun> GunList { get; set; }
        private EGuns eGuns;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GunsOptions(GameClass game)
        {
            this.game = game;
            GunList = new List<Gun>();
        }
        /// <summary>
        /// Method to add gun to the map
        /// </summary>
        /// <param name="position"></param>
        /// <param name="weapon"></param>
        public void AddGun(Vector2 position, int weapon)
        {
            Texture2D texture = null;
            SoundEffect gunShotEffect = null;
            SoundEffect gunReloadEffect = null;
            int fireRange = 0;
            int fireRate = 0;
            int maxDamage = 0;
            int minDamage = 0;
            int ammo = 0;
            int magazine = 0;
            int maxMagazine = 0;
            eGuns = (EGuns)weapon;
            TypeOfWeapon(ref texture, ref fireRange, ref fireRate, ref maxDamage, ref minDamage, ref ammo, ref magazine,
                ref maxMagazine, ref gunShotEffect, ref gunReloadEffect);
            Gun gun = new Gun(position, texture,
                new Rectangle((int)position.X - texture.Width / 2, (int)position.Y - texture.Height / 2, texture.Width, texture.Height), fireRange,
                fireRate, maxDamage, minDamage, ammo, magazine, maxMagazine, eGuns, gunShotEffect, gunReloadEffect);
            GunList.Add(gun);
        }
        /// <summary>
        /// Method to get type of gun and values
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="fireRange"></param>
        /// <param name="fireRate"></param>
        /// <param name="damage"></param>
        /// <param name="ammo"></param>
        /// <param name="magazine"></param>
        /// <param name="maxMagazine"></param>
        private void TypeOfWeapon(ref Texture2D texture, ref int fireRange, ref int fireRate, ref int maxDamage, ref int minDamage, ref int ammo, ref int magazine, ref int maxMagazine, ref SoundEffect gunShotEffect, ref SoundEffect gunReloadEffect)
        {
            if (eGuns == EGuns.M9)
            {
                texture = game.spritGuns[(int)EGuns.M9];
                fireRange = 400;
                fireRate = 270;
                maxDamage = 25;
                minDamage = 13;
                ammo = 9;
                magazine = 9;
                maxMagazine = 9;
                gunShotEffect = game.gunSoundShots[(int)EGuns.M9];
                gunReloadEffect = game.gunSoundReloads[(int)EGuns.M9];
            }
            else if (eGuns == EGuns.P90)
            {
                texture = game.spritGuns[(int)EGuns.P90];
                fireRange = 700;
                fireRate = 100;
                maxDamage = 20;
                minDamage = 11;
                ammo = 50;
                magazine = 50;
                maxMagazine = 50;
                gunShotEffect = game.gunSoundShots[(int)EGuns.P90];
                gunReloadEffect = game.gunSoundReloads[(int)EGuns.P90];
            }
            else if (eGuns == EGuns.M4A1)
            {
                texture = game.spritGuns[(int)EGuns.M4A1];
                fireRange = 950;
                fireRate = 130;
                maxDamage = 25;
                minDamage = 14;
                ammo = 30;
                magazine = 30;
                maxMagazine = 30;
                gunShotEffect = game.gunSoundShots[(int)EGuns.M4A1];
                gunReloadEffect = game.gunSoundReloads[(int)EGuns.M4A1];
            }
            else if (eGuns == EGuns.Ak47)
            {
                texture = game.spritGuns[(int)EGuns.Ak47];
                fireRange = 950;
                fireRate = 180;
                maxDamage = 25;
                minDamage = 18;
                ammo = 30;
                magazine = 30;
                maxMagazine = 30;
                gunShotEffect = game.gunSoundShots[(int)EGuns.Ak47];
                gunReloadEffect = game.gunSoundReloads[(int)EGuns.Ak47];
            }
            else if (eGuns == EGuns.ScarL)
            {
                texture = game.spritGuns[(int)EGuns.ScarL];
                fireRange = 1000;
                fireRate = 165;
                maxDamage = 25;
                minDamage = 18;
                ammo = 30;
                magazine = 30;
                maxMagazine = 30;
                gunShotEffect = game.gunSoundShots[(int)EGuns.ScarL];
                gunReloadEffect = game.gunSoundReloads[(int)EGuns.ScarL];
            }
            else if (eGuns == EGuns.ACWR)
            {
                texture = game.spritGuns[(int)EGuns.ACWR];
                fireRange = 950;
                fireRate = 140;
                maxDamage = 20;
                minDamage = 17;
                ammo = 30;
                magazine = 30;
                maxMagazine = 30;
                gunShotEffect = game.gunSoundShots[(int)EGuns.ACWR];
                gunReloadEffect = game.gunSoundReloads[(int)EGuns.ACWR];
            }
            else if (eGuns == EGuns.PKP)
            {
                texture = game.spritGuns[(int)EGuns.PKP];
                fireRange = 1200;
                fireRate = 170;
                maxDamage = 34;
                minDamage = 22;
                ammo = 100;
                magazine = 100;
                maxMagazine = 100;
                gunShotEffect = game.gunSoundShots[(int)EGuns.PKP];
                gunReloadEffect = game.gunSoundReloads[(int)EGuns.PKP];
            }
            else if (eGuns == EGuns.M60)
            {
                texture = game.spritGuns[(int)EGuns.M60];
                fireRange = 1300;
                fireRate = 170;
                maxDamage = 34;
                minDamage = 22;
                ammo = 200;
                magazine = 200;
                maxMagazine = 200;
                gunShotEffect = game.gunSoundShots[(int)EGuns.M60];
                gunReloadEffect = game.gunSoundReloads[(int)EGuns.M60];
            }
        }
        /// <summary>
        /// Method to draw guns in list
        /// </summary>
        public void DrawGuns()
        {
            foreach (Gun gun in GunList)
            {
                if (game.EGameState != EGameState.Pause)
                    gun.Rotation += 0.03f;
                game.spriteBatch.Draw(gun.Texture, new Rectangle((int)gun.Position.X, (int)gun.Position.Y, gun.Texture.Width, gun.Texture.Height), null, Color.White, gun.Rotation, new Vector2(gun.Texture.Width / 2, gun.Texture.Height / 2), SpriteEffects.None, 0f);
            }
        }
    }
}
