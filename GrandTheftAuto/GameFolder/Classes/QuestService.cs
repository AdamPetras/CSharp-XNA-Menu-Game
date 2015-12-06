using System.Collections.Generic;
using System.Linq;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class QuestService
    {
        public List<Quest> QuestList { get; private set; }
        public QuestService()
        {
            QuestList = new List<Quest>();
        }

        public void AddQuest(string name, string description, int reward,int levelToActive, int valueToSuccess, QuestMaster start, QuestMaster end)
        {
            QuestList.Add(new Quest(name, description, reward, valueToSuccess, start, end,levelToActive));
        }
        public void AddQuest(string name, string description, int reward, int levelToActive, QuestMaster start, QuestMaster end)
        {
            QuestList.Add(new Quest(name, description, reward, start, end,levelToActive));
        }
    }
}
