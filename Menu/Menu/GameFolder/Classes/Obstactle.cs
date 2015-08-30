using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.GameFolder.Classes
{
    public class Obstactle
    {
        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; private set; }
        public bool Colision { get; private set; }

        public Obstactle(Vector2 position,Texture2D texture, bool colision)
        {
            Position = position;
            Texture = texture;
            Colision = colision;
        }
    }
}
