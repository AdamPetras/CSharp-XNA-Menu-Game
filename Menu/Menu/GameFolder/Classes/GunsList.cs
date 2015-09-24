using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.GameFolder.Classes
{
    public class GunsList
    {
        private enum EGuns
        {
            M9,
            P90,
            M4A1,
            AK47,
            M98B
        }

        private Game game;
        private List<Gun> gunList;
        private EGuns eGuns;
        private float rotation;

        public GunsList(Game game)
        {
            this.game = game;
            gunList = new List<Gun>();
            rotation = 0;
        }

        public void AddGun(Vector2 position, int weapon)
        {
            Texture2D texture = null;
            int fireRange = 0; 
            int fireRate = 0;
            int damage = 0;
            int ammo = 0;
            eGuns = (EGuns)weapon;
            TypeOfWeapon(ref texture, ref fireRange, ref fireRate, ref damage, ref ammo);
            Gun gun = new Gun(position, texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height),fireRange, fireRate, damage, ammo, eGuns);
            gunList.Add(gun);
        }

        public void AddAmmo(Gun gun)
        {
            int ammo = gun.Ammo;
            gun.Ammo = gun.Ammo + ammo;
        }

        private void TypeOfWeapon(ref Texture2D texture, ref int fireRange, ref int fireRate, ref int damage, ref int ammo)
        {
            if (eGuns == EGuns.M9)
            {
                texture = game.spritGuns[0];
                
                fireRate = 10;
                damage = 13;
                ammo = 9;
            }
            else if (eGuns == EGuns.P90)
            {
                texture = game.spritGuns[0];
                fireRange = 600;
                fireRate = 50;
                damage = 16;
                ammo = 50;
            }
            else if (eGuns == EGuns.M4A1)
            {
                texture = game.spritGuns[0];
                fireRange = 800;
                fireRate = 35;
                damage = 24;
                ammo = 30;
            }
            else if (eGuns == EGuns.AK47)
            {
                texture = game.spritGuns[0];
                fireRange = 850;
                fireRate = 40;
                damage = 26;
                ammo = 30;
            }
            else if (eGuns == EGuns.M98B)
            {
                texture = game.spritGuns[0];
                fireRange = 1500;
                fireRate = 4;
                damage = 100;
                ammo = 10;
            }
        }
        public List<Gun> GetGunsList()
        {
            return gunList;
        }
        public void DrawGuns()
        {
            foreach (Gun gun in gunList)
            {
                rotation += 0.01f;
                game.spriteBatch.Draw(gun.Texture, new Rectangle((int)gun.Position.X, (int)gun.Position.Y, gun.Texture.Width, gun.Texture.Height), null, Color.White, rotation, new Vector2(gun.Texture.Width / 2, gun.Texture.Height / 2), SpriteEffects.None, 0f);
            }
        }
    }
}
