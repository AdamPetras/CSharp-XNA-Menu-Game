using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes.GunFolder
{
    public class Bullet
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; private set; }
        public float Angle { get; private set; }
        public int Damage { get; private set; }
        public int MaxFireRange { get; private set; }
        public int FireRange { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="angle"></param>
        /// <param name="maxFireRange"></param>
        /// <param name="fireRange"></param>
        public Bullet(Vector2 position,Texture2D texture,float angle, int damage, int maxFireRange,int fireRange = 0)
        {
            Position = position;
            Texture = texture;
            Angle = angle;
            Damage = damage;
            MaxFireRange = maxFireRange;
            FireRange = fireRange;
        }

        /// <summary>
        /// Method to compare maxrange a range of fire
        /// </summary>
        /// <returns></returns>
        public bool BulletRange()
        {
            if (MaxFireRange>FireRange)
                return true;
            return false;
        }
    }
}
