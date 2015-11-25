namespace GrandTheftAuto.GameFolder.Classes
{

    public class StatsService
    {
        private Character character;
        private int expOverFlow;


        public StatsService(Character character)
        {
            this.character = character;
            expOverFlow = 0;
        }

        public void NewLevel()
        {
            character.Level++;
            character.LevelUp(1,1,1,1,1);
            character.UpdateStats();
        }

        public void CalculateExperience()
        {
            if (character.Level >=2)
            {
                expOverFlow = character.ActualExperiences - character.LevelUpExperience;
                character.ActualExperiences = expOverFlow;
                character.LevelUpExperience *= 2;
            }
        }
    }
}
