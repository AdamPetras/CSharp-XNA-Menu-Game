using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes.GunFolder
{
    public class Gun
    {
        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; private set; }
        public Rectangle Rectangle { get; private set; }
        public int FireRate { get; private set; }
        public int FireRange { get; private set; }
        public int Damage { get; set; }
        public int MaxDamage { get; private set; }
        public int MinDamage { get; private set; }
        public int MaxMagazine { get; private set; }
        public Enum EGun { get; private set; }
        public int Ammo { get; set; }
        public int Magazine { get; set; }
        public float Rotation { get; set; }
        public double TimeToReload { get; private set; }
        public SoundEffect GunShotEffect { get; private set; }
        public SoundEffectInstance GunReloadEffect { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="rectangle"></param>
        /// <param name="fireRange"></param>
        /// <param name="fireRate"></param>
        /// <param name="damage"></param>
        /// <param name="ammo"></param>
        /// <param name="magazine"></param>
        /// <param name="maxMagazine"></param>
        /// <param name="eGun"></param>
        public Gun(Vector2 position, Texture2D texture, Rectangle rectangle, int fireRange, int fireRate, int maxDamage, int minDamage, int ammo, int magazine, int maxMagazine, Enum eGun, SoundEffect gunShotEffect, SoundEffect gunReloadEffect)
        {
            Position = position;
            Texture = texture;
            Rectangle = rectangle;
            FireRange = fireRange;
            FireRate = fireRate;
            Damage = 0;
            MaxDamage = maxDamage;
            MinDamage = minDamage;
            Ammo = ammo;
            Magazine = magazine;
            MaxMagazine = maxMagazine;
            EGun = eGun;
            Rotation = 0f;
            GunShotEffect = gunShotEffect;
            TimeToReload = gunReloadEffect.Duration.TotalMilliseconds;
            GunReloadEffect = gunReloadEffect.CreateInstance();
        }
    }
}
