using System;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Enemy : Stats
    {
        public string Name { get; private set; }
        public int Damage { get; private set; }
        public double MaxHp { get; private set; }
        public int Score { get; private set; }
        public double ChanceToMiss { get; private set; }
        public int Exp { get; private set; }
        public bool IsAngry { get; set; }


        public Enemy(string name,double hp, int damage, float speed, Texture2D texture, Vector2 position, float angle,int score,double chanceToMiss,int exp,bool alive = true, bool isAngry = false)
        {
            Name = name;
            Hp = hp;
            MaxHp = Hp;
            Damage = damage;
            Speed = speed;
            Texture = texture;         
            Position = position;
            Angle = angle;
            Score = score;
            ChanceToMiss = chanceToMiss;
            Exp = exp;
            Alive = alive;
            IsAngry = isAngry;
        }

        public void UpdateEachRectangle()
        {
            Rectangle = new Rectangle((int)Position.X-Texture.Width/2, (int)Position.Y-Texture.Height/2, Texture.Width, Texture.Height);
        }
    }
}
