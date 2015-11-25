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
        Damage
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
            Skill skill = new Skill(name, description, level, SkillPosition(texture), texture, bonus, (EWhichBonus)eSkill, procentual);
            ListOfSkills.Add(skill);
        }

        public Skill GetDescriptionOfSkill()
        {
            foreach (Skill skill in ListOfSkills.Where(skill => skill.Rectangle.Contains(game.mouseState.Position)))
            {
                return skill;
            }
            return null;
        }

        public void ActivateSkill()
        {
            foreach (Skill skill in ListOfSkills)
            {
                if (skill.Rectangle.Contains(game.mouseState.Position) && game.mouseState.LeftButton == ButtonState.Pressed && character.SkillPoints > 0 && skill.ActivePossible && !skill.Activated)
                {
                    character.SkillPoints -= 1;
                    character.ActualSkillLevel += 1;
                    skill.Activated = true;
                    TypeOfBonus(skill.ESkill, skill.Bonus, skill.Procentual);
                }
            }
        }

        public void SkillSetActivePossible()
        {
            foreach (Skill skill in ListOfSkills)
            {
                if (skill.Level <= character.ActualSkillLevel)
                    skill.ActivePossible = true;
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

        public Vector2 SkillPosition(Texture2D texture)
        {
            return new Vector2((game.graphics.PreferredBackBufferWidth / 2 - texture.Width / 2), game.spritArrow.Height * ListOfSkills.Count + texture.Height * ListOfSkills.Count);
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
