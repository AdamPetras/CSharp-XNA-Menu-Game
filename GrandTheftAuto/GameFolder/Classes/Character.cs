using System.Collections.Generic;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using GrandTheftAuto.MenuFolder.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Character:Statistics
    {
        public bool Regeneration { get; set; }
        public int CurrentFrame { get; set; }
        public float DefaultSpeed { get; private set; }

        public int Vitality { get; set; }       //životy
        public int Intelect { get; set; }       //energie
        public int Spirit { get; set; }     //vyčerpání energie
        public int Stamina { get; set; }      //regenerace životů
        public int Agility { get; set; }     //rychlost
        public int Energy { get; set; }      //energie
        public double EnergyRegen { get; set; }
        public double HpRegen { get; set; }
        public double MaxHp { get; private set; }
        public int MaxEnergy { get; private set; }
        public int Level { get; set; }
        public int ActualExperiences { get; set; }
        public int LevelUpExperience { get; set; }
        public int SkillPoints { get; set; }
        public int ActualSkillLevel { get; set; }
        public Enemy EnemyKilled { get; set; }
        public List<Quest> QuestList { get; set; }
        public int QuestPoints { get; set; }

        private const int VITALITY = 10;
        private const int ENERGY = 5;
        private const int MINENERGYREGEN = 100;
        private const double ENERGYREGENERATION = 5;
        private const int MINHPREGEN = 5000;
        private const int MINSPEED = 1;
        private const double SPEED = 0.02;
        private const double HPREGENERATION = 20;
        public Character(Vector2 position, Texture2D texture,int level = 0, bool alive = true, float angle = 0, int currentFrame = 0, bool regeneration = false)
        {
            Vitality = 9;
            Intelect = 9;
            Position = position;
            Texture = texture;
            Alive = alive;
            Angle = angle;
            CurrentFrame = currentFrame;
            Regeneration = regeneration;
            Level = level;
            SkillPoints = 0;
            ActualSkillLevel = 0;
            ActualExperiences = 0;
            LevelUpExperience = 400;
            EnemyKilled = null;
            QuestList = new List<Quest>();
            QuestPoints = 0;
        }

        public void UpdateRectangle()
        {
            Rectangle = new Rectangle((int)Position.X-Texture.Width/2,(int)Position.Y-Texture.Height/2,Texture.Width,Texture.Height);
        }

        public void LevelUp(int vitality, int intelect, int stamina, int spirit, int agility)
        {
            Vitality += vitality;
            Intelect += intelect;
            Stamina += stamina;
            Spirit += spirit;
            Agility += agility;
        }

        public void UpdateStats()
        {
            if (Hp == MaxHp)
            {
                Hp = Vitality * VITALITY;
                MaxHp = Hp;
            }
            else
                MaxHp = Vitality * VITALITY;
            if (Energy == MaxEnergy)
            {
                Energy = Intelect * ENERGY;
                MaxEnergy = Energy;
            }
            else
                MaxEnergy = Intelect * ENERGY;
            HpRegen = MINHPREGEN - Stamina * HPREGENERATION;
            EnergyRegen = MINENERGYREGEN - Spirit * ENERGYREGENERATION;
            Speed = (float) (MINSPEED + Agility * SPEED);
            DefaultSpeed = Speed;
        }
    }
}
