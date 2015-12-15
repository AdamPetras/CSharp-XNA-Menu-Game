using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class QuestService
    {
        public List<Quest> QuestList { get; private set; }

        public QuestService()
        {
            QuestList = new List<Quest>();
        }

        public void AddQuest(string name, string description, SpriteFont spriteFont, int reward, int levelToActive,
            QuestMaster start, QuestMaster end)
        {
            QuestList.Add(new Quest(name, description, spriteFont, reward, start, end, levelToActive));
        }
        public void AddQuest(string name, string description, SpriteFont spriteFont, int reward, int levelToActive,
            int valueToSuccess, QuestMaster start, QuestMaster end,Rectangle? placeToDo, params EEnemies[] eEnemies)
        {
            QuestList.Add(new Quest(name, description, spriteFont, reward, valueToSuccess, start, end, levelToActive,placeToDo,eEnemies));
        }

        public void AddQuest(string name, string description, SpriteFont spriteFont, int reward, int levelToActive, QuestMaster start, QuestMaster end, params QuestMaster[] speakWith)
        {
            QuestList.Add(new Quest(name, description, spriteFont, reward, start, end, levelToActive, speakWith));
        }
    }

}
