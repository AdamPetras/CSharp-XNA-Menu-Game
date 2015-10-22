using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Enemy
    {

        public int Damage { get; private set; }
        public float Speed { get; private set; }
        public Texture2D Texture { get; private set; }
        public Rectangle Rectangle { get; private set; }
        public int MaxHp { get; private set; }
        public int Score { get; private set; }
        public int Hp { get; set; }
        public bool IsAngry { get; set; }
        public bool Alive { get; set; }
        public Vector2 Position { get; set; }
        public float Angle { get; set; }


        public Enemy(int hp, int damage, float speed, Texture2D texture, Vector2 position, float angle,int score,bool alive = true, bool isAngry = false)
        {
            Hp = hp;
            MaxHp = hp;
            Damage = damage;
            Speed = speed;
            Texture = texture;         
            Position = position;
            Angle = angle;
            Score = score;
            Alive = alive;
            IsAngry = isAngry;
        }

        public void UpdateEachRectangle()
        {
            Rectangle = new Rectangle((int)Position.X-Texture.Width/2, (int)Position.Y-Texture.Height/2, Texture.Width, Texture.Height);
        }
    }
}
