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

        public void AddQuest(string name, string description, int reward, bool activable, int valueToSuccess, QuestMaster start, QuestMaster end)
        {
            QuestList.Add(new Quest(name, description, reward, activable, valueToSuccess, start, end));
        }
        public void AddQuest(string name,string description, int reward, bool activable, QuestMaster start, QuestMaster end)
        {           
            QuestList.Add(new Quest(name, description, reward, activable, start, end));
        }
        public List<Quest> ActiveQuestList()
        {
            return QuestList.Where(s => s.EQuestState == EQuestState.Active) as List<Quest>;
        }

        public List<Quest> CompleteQuestList()
        {
            return QuestList.Where(s => s.EQuestState == EQuestState.Complete) as List<Quest>;
        }
    }
}
