using System.Linq;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentQuestSystem : DrawableGameComponent
    {
        private GameClass game;
        private QuestMasterService questMasterService;
        private Character character;
        private Camera camera;
        private bool infoOpen;
        private QuestService QuestService;
        public ComponentQuestSystem(GameClass game, Character character, Camera camera)
            : base(game)
        {
            this.game = game;
            this.camera = camera;
            this.character = character;
            questMasterService = new QuestMasterService(game, camera, this);
            QuestService = new QuestService();
            questMasterService.AddQuester("Ahoj", 100, new Vector2(200, 200), game.spritCharacter[0], 0, 1);
            questMasterService.AddQuester("Zdar", 100, new Vector2(300, 100), game.spritCharacter[0], 0, 1);
            QuestService.AddQuest("první", "První úkol dfsjklgfdjgnfdslkgnlfdsgljdsjgfnfdsjgndfsjgljdjlsfgfdgdhsdfhdsfhsdfh", 100,0, questMasterService.QuesterList[0], questMasterService.QuesterList[1]);
            QuestService.AddQuest("třetí", "Třetí úkol dfsjklgfdjgnfdslkgnlfdsgljdsjgfnfdsjgndfsjgljdjlsfgfdgdhsdfhdsfhsdfh", 100,0, questMasterService.QuesterList[1], questMasterService.QuesterList[0]);
            QuestService.AddQuest("Kill", "Zabij 5x Zombie", 200,1, 5, questMasterService.QuesterList[1], questMasterService.QuesterList[0]);

            AddQuestsToQuestMasters();  //nastavení úkolů přísušným quest masterům
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.InGameCar || game.EGameState == EGameState.Reloading)
            {
                questMasterService.SetQuestGiveOver();
                questMasterService.SetActivatedQuest(character);
                //questMasterService.ColisionWithCharacter(character);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                    camera.Transform);
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.InGameCar || game.EGameState == EGameState.Reloading || game.EGameState == EGameState.Pause)
                questMasterService.DrawQuestMaster();
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.InGameCar || game.EGameState == EGameState.Reloading)
            {
                DrawOrder = 5;
                questMasterService.OnEventPickUpQuest(character);
                if (game.SingleClick(game.controlsList[(int)EKeys.I].Key) && infoOpen)
                {
                    infoOpen = false;
                }
                else if (game.SingleClick(game.controlsList[(int)EKeys.I].Key) || infoOpen)
                {
                    questMasterService.DrawQuestTable(character);
                    infoOpen = true;
                }
            }
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void AddQuestsToQuestMasters()
        {
            foreach (Quest quest in QuestService.QuestList)
            {
                if (questMasterService.QuesterList.Any(s => s == quest.Start))
                {
                    questMasterService.QuesterList.Find(s => s == quest.Start).QuestList.Add(quest);
                }
            }
            QuestService.QuestList.Clear();
        }
    }
}
