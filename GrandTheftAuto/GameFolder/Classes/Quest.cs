using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandTheftAuto.GameFolder.Classes
{
    public enum ETypeOfQuest
    {
        Delivery,
        SearchAndDestroy
    }

    public enum EQuestState
    {
        Complete,
        Active,
        Inactive
    }

    public class Quest : TextVisualisation
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ETypeOfQuest ETypeOfQuest { get; private set; }
        public bool Activable { get; set; }
        public QuestMaster Start { get; private set; }
        public QuestMaster End { get; private set; }
        public int ValueToSuccess { get; private set; }
        public int Reward { get; private set; }
        public int ActualValue { get; set; }
        public EQuestState EQuestState { get; set; }
        public int Id { get; private set; }

        public Quest(string name, string description, int reward,int valueToSuccess, QuestMaster start, QuestMaster end,int id)
        {
            Name = name;
            Description = LineFormat(description, 25);
            Description += "\nAccept(K)         Decline(RightMouse)";
            Id = id;
            Reward = reward;
            ETypeOfQuest = ETypeOfQuest.SearchAndDestroy;
            ValueToSuccess = valueToSuccess;
            Start = start;
            End = end;
            ActualValue = 0;
            EQuestState = EQuestState.Inactive;

        }

        public Quest(string name, string description, int reward, QuestMaster start, QuestMaster end,int id)
        {
            Name = name;
            Description = LineFormat(description, 25);
            Description += "\nAccept(K)         Decline(RightMouse)";
            Id = id;
            Reward = reward;
            ETypeOfQuest = ETypeOfQuest.Delivery;
            Start = start;
            End = end;
            EQuestState = EQuestState.Inactive;
        }
    }
}
