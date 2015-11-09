﻿using System;
using Microsoft.Xna.Framework;
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
        public int Damage { get; private set; }
        public int MaxMagazine { get; private set; }
        public Enum EGun { get; private set; }
        public int DamageRange { get; private set; }

        public int Ammo { get; set; }
        public int Magazine { get; set; }

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
        public Gun(Vector2 position,Texture2D texture,Rectangle rectangle,int fireRange, int fireRate, int damage, int ammo,int magazine,int maxMagazine,int damageRange, Enum eGun)
        {
            Position = position;
            Texture = texture;
            Rectangle = rectangle;
            FireRange = fireRange;
            FireRate = fireRate;
            Damage = damage;
            Ammo = ammo;
            Magazine = magazine;
            MaxMagazine = maxMagazine;
            DamageRange = damageRange;
            EGun = eGun;
        }
    }
}