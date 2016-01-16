using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public enum ETypeOfQuest
    {
        Delivery,
        SearchAndDestroy,
        Notice
    }

    public enum EQuestState
    {
        Complete,
        Active,
        Inactive,
        Done
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
        public List<EEnemies> EEnemies { get; set; }
        public List<QuestMaster> SpeakWith { get; set; }
        public List<QuestMaster> SpeakedWith { get; set; }
        public Rectangle? PlaceToDo { get; private set; }
        public Item ItemReward { get; private set; }
        private SpriteFont SpriteFont { get; set; }

        #region Delivery quest
        public Quest(string name, string description, SpriteFont spriteFont, int reward, QuestMaster start, QuestMaster end, int id,Item itemReward)
        {
            Name = name;
            SpriteFont = spriteFont;
            Description = WrapText(spriteFont, description, 450);
            Description += "\nAccept(Enter)         Decline(RightMouse)";
            Id = id;
            Reward = reward;
            ETypeOfQuest = ETypeOfQuest.Delivery;
            Start = start;
            End = end;
            ActualValue = 0;
            EQuestState = EQuestState.Inactive;
            ItemReward = itemReward;
        }
        #endregion
        #region Kill quest
        public Quest(string name, string description, SpriteFont spriteFont, int reward, int valueToSuccess, QuestMaster start, QuestMaster end, int id, Rectangle? placeToDo, Item itemReward, params EEnemies[] eEnemies)
        {
            EEnemies = new List<EEnemies>();
            Name = name;
            SpriteFont = spriteFont;
            Description = WrapText(spriteFont, description, 450);
            Description += "\nAccept(Enter)         Decline(RightMouse)";
            Id = id;
            Reward = reward;
            ETypeOfQuest = ETypeOfQuest.SearchAndDestroy;
            ValueToSuccess = valueToSuccess;
            Start = start;
            End = end;
            ActualValue = 0;
            PlaceToDo = placeToDo;
            EEnemies = eEnemies.ToList();
            EQuestState = EQuestState.Inactive;
            ItemReward = itemReward;
        }
        #endregion
        #region Speaking quest
        public Quest(string name, string description, SpriteFont spriteFont, int reward, QuestMaster start, QuestMaster end, int id, Item itemReward, params QuestMaster[] speakWith)
        {
            SpeakWith = new List<QuestMaster>();
            SpeakedWith = new List<QuestMaster>();
            Name = name;
            SpriteFont = spriteFont;
            Description = WrapText(spriteFont, description, 450);
            Description += "\nAccept(Enter)         Decline(RightMouse)";
            Id = id;
            Reward = reward;
            ETypeOfQuest = ETypeOfQuest.Notice;
            Start = start;
            End = end;
            ActualValue = 0;
            SpeakWith = speakWith.ToList();
            EQuestState = EQuestState.Inactive;
            ItemReward = itemReward;
        }
        #endregion
    }
}
