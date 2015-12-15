using System.Collections.Generic;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class SkillView : TextVisualisation
    {
        private SkillService skillService;
        private GameClass game;
        private Character character;

        public SkillView(GameClass game, Character character)
        {
            this.game = game;
            this.character = character;
            skillService = new SkillService(game, character);
            AddAllSkills();
        }
        public void AddSkillPoint()
        {
            if (character.Level % 2 == 0)  //každý druhé level přičte skillpoint
                character.SkillPoints += 1;
        }

        public void Update()
        {
            skillService.ActivateSkill();
            skillService.SkillSetActivePossible();
            skillService.SkillPositions();
        }

        public void DrawSkillTable()
        {
            foreach (Skill skill in skillService.ListOfSkills)
            {
                game.spriteBatch.Draw(skill.Texture, skill.Position, Color.White);
                if (!skill.Activable && !skill.Activated)
                    game.spriteBatch.Draw(game.spritPauseBackground, skill.Rectangle, Color.White * 0.8f);
                else if (skill.Activable && !skill.Activated)
                    game.spriteBatch.Draw(game.spritPauseBackground, skill.Rectangle, Color.White * 0.4f);
            }
            if (skillService.GetDescriptionOfSkill() != null)
            {
                string text = "";
                if (!skillService.GetDescriptionOfSkill().Activable)
                    text = "\nYou dont have any skill points or this skill is not activable";
                game.spriteBatch.DrawString(game.smallestFont, skillService.GetDescriptionOfSkill().Description + text,
    new Vector2(skillService.GetDescriptionOfSkill().Rectangle.Right,
        skillService.GetDescriptionOfSkill().Position.Y), Color.White);
            }
            game.spriteBatch.DrawString(game.normalFont, "Vitality bonus: " + skillService.BonusOption.VitalityBonus + "\nIntelect bonus: " + skillService.BonusOption.IntelectBonus + "\nStamina bonus: " + skillService.BonusOption.StaminaBonus + "\nSprit bonus: " + skillService.BonusOption.SpiritBonus + "\nAgility bonus: " + skillService.BonusOption.AgilityBonus + "\nActual skill level: " + character.ActualSkillLevel+"\nSkill points: "+character.SkillPoints, new Vector2(100, 100), Color.White);

        }

        public void AddAllSkills()
        {
            skillService.AddSkill("HealthBonus", "Add 10 health", 0, false, (int)EWhichBonus.Vitality, 1, game.spritTalents[3]);
            skillService.AddSkill("EnergyBonus", "Add 10 energy", 1, false, (int)EWhichBonus.Intelect, 2, game.spritTalents[8]);
            skillService.AddSkill("SpeedBonus", "Add 0.1 speed", 1, false, (int)EWhichBonus.Agility, 5, game.spritTalents[11]);
            skillService.AddSkill("HealthRegenerationBonus", "Add health regeneration", 1, false, (int)EWhichBonus.Stamina, 10, game.spritTalents[9]);
            skillService.AddSkill("HealthBonus", "Add 10% of total health", 2, true, (int)EWhichBonus.Vitality, 10, game.spritTalents[5]);
            skillService.AddSkill("SpeedBonus", "Add 15% of total speed", 2, true, (int)EWhichBonus.Agility, 15, game.spritTalents[0]);
            skillService.AddSkill("EnergyRegenerationBonus", "Add energy regeneration", 3, false, (int)EWhichBonus.Spirit, 5, game.spritTalents[12]);
            skillService.AddSkill("EnergyBonus", "Add 20% of total energy", 3, true, (int)EWhichBonus.Intelect, 20, game.spritTalents[10]);
            skillService.AddSkill("HealthBonus", "Add 100 health", 4, false, (int)EWhichBonus.Vitality, 10, game.spritTalents[2]);
            skillService.AddSkill("HealthBonus", "Add 15% of total health", 5, true, (int)EWhichBonus.Vitality, 15, game.spritTalents[6]);

        }
    }
}
