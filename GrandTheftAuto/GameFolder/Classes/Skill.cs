using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Skill
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Texture2D Texture { get; private set; }
        public double Bonus { get; private set; }
        public EWhichBonus ESkill { get; private set; }
        public int Level { get; private set; }
        public Vector2 DefaultPosition { get; private set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle { get; set; }
        public bool Activable { get; set; }
        public bool Activated { get; set; }
        public bool Procentual { get; set; }

        public Skill(string name,string description,int level, Vector2 position, Texture2D texture, double bonus, EWhichBonus eSkill, bool procentual = false,bool activable = false, bool activated = false)
        {
            Name = name;
            Description = description;
            Level = level;
            Position = position;
            DefaultPosition = Position;
            Texture = texture;
            Bonus = bonus;
            ESkill = eSkill;
            Activable = activable;
            Activated = activated;
            Procentual = procentual;
            Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void UpdateRectangle()
        {
            Rectangle = new Rectangle((int) Position.X,(int) Position.Y,Texture.Width,Texture.Height);
        }
    }
}
