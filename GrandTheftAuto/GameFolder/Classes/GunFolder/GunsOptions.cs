using System.Collections.Generic;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes.Gun
{
    public class GunsOptions
    {
        /// <summary>
        /// Enum of guns
        /// </summary>
        private enum EGuns
        {
            M9,
            P90,
            M4A1,
            AK47,
            M98B
        }
        private GameClass game;
        private List<GunFolder.Gun> gunList;
        private EGuns eGuns;
        private float rotation;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GunsOptions(GameClass game)
        {
            this.game = game;
            gunList = new List<GunFolder.Gun>();
            rotation = 0;
        }
        /// <summary>
        /// Method to add gun to the map
        /// </summary>
        /// <param name="position"></param>
        /// <param name="weapon"></param>
        public void AddGun(Vector2 position, int weapon)
        {
            Texture2D texture = null;
            int fireRange = 0;
            int fireRate = 0;
            int damage = 0;
            int ammo = 0;
            int magazine = 0;
            int maxMagazine = 0;
            eGuns = (EGuns)weapon;
            TypeOfWeapon(ref texture, ref fireRange, ref fireRate, ref damage, ref ammo, ref magazine,
                ref maxMagazine);
            GunFolder.Gun gun = new GunFolder.Gun(position, texture,
                new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height), fireRange,
                fireRate, damage, ammo, magazine, maxMagazine, eGuns);
            gunList.Add(gun);
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
        private void TypeOfWeapon(ref Texture2D texture, ref int fireRange, ref int fireRate, ref int damage, ref int ammo, ref int magazine, ref int maxMagazine)
        {
            if (eGuns == EGuns.M9)
            {
                texture = game.spritGuns[0];
                fireRange = 300;
                fireRate = 250;
                damage = 13;
                ammo = 9;
                magazine = 9;
                maxMagazine = 9;
            }
            else if (eGuns == EGuns.P90)
            {
                texture = game.spritGuns[0];
                fireRange = 600;
                fireRate = 100;
                damage = 16;
                ammo = 50;
                magazine = 50;
                maxMagazine = 50;
            }
            else if (eGuns == EGuns.M4A1)
            {
                texture = game.spritGuns[0];
                fireRange = 800;
                fireRate = 150;
                damage = 24;
                ammo = 30;
                magazine = 30;
                maxMagazine = 30;
            }
            else if (eGuns == EGuns.AK47)
            {
                texture = game.spritGuns[0];
                fireRange = 850;
                fireRate = 180;
                damage = 26;
                ammo = 30;
                magazine = 30;
                maxMagazine = 30;
            }
            else if (eGuns == EGuns.M98B)
            {
                texture = game.spritGuns[0];
                fireRange = 1500;
                fireRate = 2000;
                damage = 100;
                ammo = 10;
                magazine = 10;
                maxMagazine = 10;
            }
        }
        /// <summary>
        /// Method to get gunlist
        /// </summary>
        /// <returns></returns>
        public List<GunFolder.Gun> GetGunsList()
        {
            return gunList;
        }
        /// <summary>
        /// Method to draw guns in list
        /// </summary>
        public void DrawGuns()
        {
            foreach (GunFolder.Gun gun in gunList)
            {
                if (game.EGameState != EGameState.Pause)
                    rotation += 0.01f;
                game.spriteBatch.Draw(gun.Texture, new Rectangle((int)gun.Position.X, (int)gun.Position.Y, gun.Texture.Width, gun.Texture.Height), null, Color.White, rotation, new Vector2(gun.Texture.Width / 2, gun.Texture.Height / 2), SpriteEffects.None, 0f);
            }
        }
    }
}
