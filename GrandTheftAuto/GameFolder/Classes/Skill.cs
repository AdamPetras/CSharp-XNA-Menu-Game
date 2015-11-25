using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Skill
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; private set; }
        public double Bonus { get; private set; }
        public Rectangle Rectangle { get; private set; }
        public EWhichBonus ESkill { get; private set; }
        public int Level { get; private set; }
        public bool ActivePossible { get; set; }
        public bool Activated { get; set; }
        public bool Procentual { get; set; }

        public Skill(string name,string description,int level, Vector2 position, Texture2D texture, double bonus, EWhichBonus eSkill, bool procentual = false,bool activePossible = false, bool activated = false)
        {
            Name = name;
            Description = description;
            Level = level;
            Position = position;
            Texture = texture;
            Bonus = bonus;
            ESkill = eSkill;
            ActivePossible = activePossible;
            Activated = activated;
            Procentual = procentual;
            Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
    }
}
