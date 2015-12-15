using System;
using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Classes
{
    public enum EWhichBonus
    {
        Vitality,
        Stamina,
        Intelect,
        Spirit,
        Agility,
    }

    public class SkillService
    {
        private GameClass game;
        public BonusOption BonusOption { get; private set; }
        public List<Skill> ListOfSkills { get; private set; }
        private Character character;

        public SkillService(GameClass game, Character character)
        {
            this.game = game;
            this.character = character;
            ListOfSkills = new List<Skill>();
            BonusOption = new BonusOption();
        }

        public void AddSkill(string name, string description, int level, bool procentual, int eSkill, double bonus, Texture2D texture)
        {
            Skill skill = new Skill(name, description, level, SkillPosition(texture, level), texture, bonus, (EWhichBonus)eSkill, procentual);
            ListOfSkills.Add(skill);
        }

        public Skill GetDescriptionOfSkill()
        {
            return ListOfSkills.FirstOrDefault(skill => skill.Rectangle.Contains(game.mouseState.Position));
        }

        public void ActivateSkill()
        {
            foreach (Skill skill in ListOfSkills)
            {
                if (skill.Rectangle.Contains(game.mouseState.Position) && game.mouseState.LeftButton == ButtonState.Pressed && character.SkillPoints > 0 && skill.Activable && !skill.Activated)
                {
                    character.SkillPoints -= 1;
                    character.ActualSkillLevel += 1;
                    skill.Activated = true;
                    TypeOfBonus(skill.ESkill, skill.Bonus, skill.Procentual);
                }
                else if ((skill.Rectangle.Contains(game.mouseState.Position) &&
                          game.mouseState.LeftButton == ButtonState.Pressed) && (character.SkillPoints == 0 ||
                                                                                 !skill.Activable ||
                                                                                 skill.Activated))
                {
                    //pokud neni skill aktivovatelný nebo aktivovaný
                }
            }
        }

        public void SkillSetActivePossible()
        {
            foreach (Skill skill in ListOfSkills.Where(skill => skill.Level <= character.ActualSkillLevel && character.SkillPoints != 0))
            {
                skill.Activable = true;
            }
        }

        public void UpdateStatistics()
        {
            character.Vitality += (int)BonusOption.VitalityBonus;
            character.Intelect += (int)BonusOption.IntelectBonus;
            character.Stamina += (int)BonusOption.StaminaBonus;
            character.Spirit += (int)BonusOption.SpiritBonus;
            character.Agility += (int)BonusOption.AgilityBonus;
            character.UpdateStats();
        }

        public Vector2 SkillPosition(Texture2D texture, int level)
        {
            if (ListOfSkills.Any(s => s.Level != level) || ListOfSkills.Count == 0)
                return new Vector2(game.graphics.PreferredBackBufferWidth / 2 - texture.Width / 2, texture.Height * level + 50 * level);
            return new Vector2(0, 0);
        }

        public void SkillPositions()    //pokud je více skillu v řadě
        {
            foreach (Skill skill in ListOfSkills)
            {
                List<Skill> list = ListOfSkills.FindAll(s => s.Level == skill.Level);
                if (list.Count > 1)    //pokud je více jak jeden skill v tabulce tak...
                    for (int i = 0; i < list.Count; i++)
                    {
                        Vector2 originVector = new Vector2((list[i].Texture.Width + list.Count * list[i].Texture.Width * 2) - list[i].Texture.Width); //vystředění
                        list[i].Position = new Vector2(list[i].DefaultPosition.X + list[i].Texture.Width + i * list[i].Texture.Width * 2 - originVector.X / 2, list[i].DefaultPosition.Y);
                        list[i].UpdateRectangle();  //update rectanglu
                    }
            }
        }

        private void TypeOfBonus(EWhichBonus eSkill, double bonus, bool procentual)
        {
            if (eSkill == EWhichBonus.Vitality)
            {
                BonusOption.VitalityBonus += Bonus(procentual, bonus, character.Vitality);
            }
            else if (eSkill == EWhichBonus.Agility)
            {
                BonusOption.AgilityBonus += Bonus(procentual, bonus, character.Agility);
            }
            else if (eSkill == EWhichBonus.Stamina)
            {
                BonusOption.StaminaBonus += Bonus(procentual, bonus, character.Stamina);
            }
            else if (eSkill == EWhichBonus.Spirit)
            {
                BonusOption.SpiritBonus += Bonus(procentual, bonus, character.Spirit);
            }
            else if (eSkill == EWhichBonus.Intelect)
            {
                BonusOption.IntelectBonus += Bonus(procentual, bonus, character.Intelect);
            }
            UpdateStatistics();
        }

        private double Bonus(bool procentual, double bonus, double actualValue)
        {
            if (!procentual)
                return bonus;
            return (int)(actualValue * bonus * 0.01);
        }
    }
}
