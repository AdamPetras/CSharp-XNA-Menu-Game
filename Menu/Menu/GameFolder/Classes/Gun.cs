using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.GameFolder.Classes
{
    public class Gun
    {
        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; private set; }
        public Rectangle Rectangle { get; private set; }
        public int FireRate { get; private set; }
        public int FireRange { get; private set; }
        public int Damage { get; private set; }
        public int Ammo { get; set; }
        public Enum EGun { get; private set; }


        public Gun(Vector2 position,Texture2D texture,Rectangle rectangle,int fireRange, int fireRate, int damage, int ammo, Enum eGun)
        {
            Position = position;
            Texture = texture;
            Rectangle = rectangle;
            FireRange = fireRange;
            FireRate = fireRate;
            Damage = damage;
            Ammo = ammo;
            EGun = eGun;
        }
    }
}
