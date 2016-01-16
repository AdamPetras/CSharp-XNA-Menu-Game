namespace GrandTheftAuto.GameFolder.Classes
{

    public class StatsService
    {
        private Character character;
        private BonusOption bonusOption;
        private int expOverFlow;


        public StatsService(Character character, BonusOption bonusOption)
        {
            this.character = character;
            this.bonusOption = bonusOption;
            expOverFlow = 0;
        }

        public void NewLevel()
        {
            character.Level++;
            bonusOption.LevelUp(1, 1, 1, 1, 1);
            bonusOption.UpdateStatistics(character);
            character.UpdateStats();
        }

        public void CalculateExperience()
        {
            if (character.Level != 1)
            {
                expOverFlow = character.ActualExperiences - character.LevelUpExperience;
                character.ActualExperiences = expOverFlow;
            }
            character.LevelUpExperience = character.Level * character.LevelUpExperience;
        }
    }
}
