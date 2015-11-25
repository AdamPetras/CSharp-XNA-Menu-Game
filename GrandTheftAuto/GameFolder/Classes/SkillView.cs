using System.Collections.Generic;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class SkillView
    {
        private SkillService skillService;
        private GameClass game;
        private Character character;

        public SkillView(GameClass game,Character character)
        {
            this.game = game;
            this.character = character;
            skillService = new SkillService(game,character);
            AddAllSkills();
        }
        public void AddSkillPoint()
        {
            character.SkillPoints += 1;
        }

        public void Update()
        {
            skillService.ActivateSkill();
            skillService.SkillSetActivePossible();
        }

        public void DrawSkillTable()
        {
            foreach (Skill skill in skillService.ListOfSkills)
            {
                game.spriteBatch.Draw(skill.Texture, skill.Position, Color.White);
                if(!skill.ActivePossible && !skill.Activated)
                game.spriteBatch.Draw(game.spritPauseBackground, skill.Rectangle, Color.White * 0.8f);
                else if(skill.ActivePossible && !skill.Activated)
                    game.spriteBatch.Draw(game.spritPauseBackground, skill.Rectangle, Color.White * 0.3f);
                game.spriteBatch.Draw(game.spritArrow,new Vector2(skill.Rectangle.Center.X-game.spritArrow.Width/2,skill.Rectangle.Bottom),Color.White);
            }
            if(skillService.GetDescriptionOfSkill()!= null)
            game.spriteBatch.DrawString(game.smallestFont, skillService.GetDescriptionOfSkill().Description,new Vector2(skillService.GetDescriptionOfSkill().Rectangle.Right,skillService.GetDescriptionOfSkill().Position.Y), Color.White);
            game.spriteBatch.DrawString(game.normalFont, "Vitality bonus: " + skillService.BonusOption.VitalityBonus + "\nIntelect bonus: " + skillService.BonusOption.IntelectBonus + "\nStamina bonus: " + skillService.BonusOption.StaminaBonus + "\nSprit bonus: " + skillService.BonusOption.SpiritBonus + "\nAgility bonus: " + skillService.BonusOption.AgilityBonus + "\nActualSkillLevel: "+character.ActualSkillLevel, new Vector2(100, 100), Color.White);

        }

        public void AddAllSkills()
        {
            skillService.AddSkill("HPBonus","Add 10% o your total HP", 0,true, (int)EWhichBonus.Vitality, 10, game.spritTalent);
            skillService.AddSkill("HPBonus","Add 10 Vitality to your stats", 1,false, (int)EWhichBonus.Vitality, 10, game.spritTalent);
            skillService.AddSkill("HPBonus","Add", 2,false, (int)EWhichBonus.Vitality, 10, game.spritTalent);
            skillService.AddSkill("IntelectBonus","Add", 3,false, (int)EWhichBonus.Intelect, 5, game.spritTalent);
        }
    }
}
