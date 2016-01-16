using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class BonusOption
    {
        public double VitalityBonus { get; set; }
        public double StaminaBonus { get; set; }
        public double SpiritBonus { get; set; }
        public double AgilityBonus { get; set; }
        public double IntelectBonus { get; set; }

        public BonusOption()
        {
            VitalityBonus = 10;
            SpiritBonus = 1;
            SpiritBonus = 1;
            AgilityBonus = 1;
            IntelectBonus = 10;
        }

        public void LevelUp(int vitality, int intelect, int stamina, int spirit, int agility)
        {
            VitalityBonus += vitality;
            IntelectBonus += intelect;
            StaminaBonus += stamina;
            SpiritBonus += spirit;
            AgilityBonus += agility;
        }        

        public void TypeOfBonus(EWhichBonus eSkill, double bonus, Character character)
        {
            if (eSkill == EWhichBonus.Vitality)
            {
                VitalityBonus += bonus;
            }
            else if (eSkill == EWhichBonus.Agility)
            {
                AgilityBonus += bonus;
            }
            else if (eSkill == EWhichBonus.Stamina)
            {
                StaminaBonus += bonus;
            }
            else if (eSkill == EWhichBonus.Spirit)
            {
                SpiritBonus += bonus;
            }
            else if (eSkill == EWhichBonus.Intelect)
            {
                IntelectBonus += bonus;
            }
            UpdateStatistics(character);
        }
        public void TypeOfBonus(EWhichBonus eSkill, double bonus, bool procentual,Character character)
        {
            if (eSkill == EWhichBonus.Vitality)
            {
                VitalityBonus += Bonus(procentual, bonus, character.Vitality);
            }
            else if (eSkill == EWhichBonus.Agility)
            {
                AgilityBonus += Bonus(procentual, bonus, character.Agility);
            }
            else if (eSkill == EWhichBonus.Stamina)
            {
                StaminaBonus += Bonus(procentual, bonus, character.Stamina);
            }
            else if (eSkill == EWhichBonus.Spirit)
            {
                SpiritBonus += Bonus(procentual, bonus, character.Spirit);
            }
            else if (eSkill == EWhichBonus.Intelect)
            {
                IntelectBonus += Bonus(procentual, bonus, character.Intelect);
            }
            UpdateStatistics(character);
        }

        private double Bonus(bool procentual, double bonus, double actualValue)
        {
            if (!procentual)
                return bonus;
            return (int)(actualValue * bonus * 0.01);
        }
        public void UpdateStatistics(Character character)
        {
            character.Vitality = (int)VitalityBonus;
            character.Intelect = (int)IntelectBonus;
            character.Stamina = (int)StaminaBonus;
            character.Spirit = (int)SpiritBonus;
            character.Agility = (int)AgilityBonus;
            character.UpdateStats();
        }
    }
}
