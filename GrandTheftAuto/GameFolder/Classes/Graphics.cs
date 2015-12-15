using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Graphics
    {
        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; private set; }
        public Rectangle Rectangle { get; private set; }
        public bool Colision { get; private set; }
        public float Angle { get; private set; }
        public string Description { get; private set; }
        public SpriteFont SpriteFont { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="colision"></param>
        /// <param name="angle"></param>
        public Graphics(Vector2 position,Texture2D texture, bool colision,float angle)
        {
            Position = position;
            Texture = texture;
            Rectangle = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
            Colision = colision;
            Description = "";
            SpriteFont = null;
            Angle = angle;
        }
        public Graphics(Vector2 position, Texture2D texture, bool colision, float angle,string description,SpriteFont spriteFont)
        {
            Position = position;
            Texture = texture;
            Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            Colision = colision;
            Angle = angle;
            Description = description;
            SpriteFont = spriteFont;
        }
    }
}
