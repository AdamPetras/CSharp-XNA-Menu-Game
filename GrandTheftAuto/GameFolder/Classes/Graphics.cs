using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Graphics
    {
        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; private set; }
        public bool Colision { get; private set; }
        public float Angle { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="colision"></param>
        public Graphics(Vector2 position,Texture2D texture, bool colision,float angle)
        {
            Position = position;
            Texture = texture;
            Colision = colision;
            Angle = angle;
        }
    }
}
