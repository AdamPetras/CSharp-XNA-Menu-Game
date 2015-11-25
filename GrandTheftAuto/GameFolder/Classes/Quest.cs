using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandTheftAuto.GameFolder.Classes
{
    public enum ETypeOfQuest
    {
        Delivery,
        SearchAndDestroy,
        Explore,
        Talk
    }

    public enum EQuestState
    {
        Complete,
        Active,
        Inactive
    }

    public class Quest
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ETypeOfQuest ETypeOfQuest { get; private set; }
        public bool Activable { get; private set; }
        public QuestMaster Start { get; private set; }
        public QuestMaster End { get; private set; }
        public int ValueToSuccess { get; private set; }
        public int Reward { get; private set; }

        public int ActualValue { get; set; }
        public EQuestState EQuestState { get; set; }

        private int lineLength;

        public Quest(string name, string description, int reward, bool activable, int valueToSuccess, QuestMaster start, QuestMaster end)
        {
            Name = name;
            Description = LineFormat(description);
            Reward = reward;
            Activable = activable;
            ETypeOfQuest = ETypeOfQuest.SearchAndDestroy;
            ValueToSuccess = valueToSuccess;
            Start = start;
            End = end;
            ActualValue = 0;
            EQuestState = EQuestState.Inactive;
        }

        public Quest(string name, string description, int reward, bool activable, QuestMaster start, QuestMaster end)
        {
            Name = name;
            Description = LineFormat(description);
            Reward = reward;
            Activable = activable;
            ETypeOfQuest = ETypeOfQuest.Delivery;
            Start = start;
            End = end;
            EQuestState = EQuestState.Inactive;
        }

        private string LineFormat(string description)
        {
            lineLength = 40; //počet písmen na řádku
            if (description.Length > lineLength)
                for (int i = 0; i < (description.Length / lineLength); i++)
                {
                    description = description.Insert(i * lineLength, "\n");
                }
            description += "\nAccept(K)         Decline(RightMouse)";
            return description;
        }
    }
}
