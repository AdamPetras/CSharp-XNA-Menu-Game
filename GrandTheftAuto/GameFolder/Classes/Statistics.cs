using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public abstract class Statistics
    {
        public double Hp { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle { get; set; }
        public Texture2D Texture { get; set; }
        public float Speed { get; set; }
        public float Angle { get; set; }
        public bool Alive { get; set; }
    }
}
