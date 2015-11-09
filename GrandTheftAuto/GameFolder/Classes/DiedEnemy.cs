using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class DiedEnemy
    {
        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; private set; }
        public float Angle { get; private set; }
        public Rectangle Rectangle { get; private set; }

        public DiedEnemy(Vector2 position,Texture2D texture,float angle)
        {
            Position = position;
            Texture = texture;
            Angle = angle;
            Rectangle = new Rectangle((int) Position.X,(int) Position.Y,texture.Width,texture.Height);
        }
    }
}
