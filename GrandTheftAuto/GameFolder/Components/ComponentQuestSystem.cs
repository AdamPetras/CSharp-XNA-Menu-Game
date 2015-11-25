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
        public ComponentQuestSystem(GameClass game, Character character, Camera camera)
            : base(game)
        {
            this.game = game;
            this.camera = camera;
            this.character = character;
            questMasterService = new QuestMasterService(game, camera,this);
            questMasterService.AddQuester("Ahoj", 100, new Vector2(200, 200), game.spritCharacter[0], 0, 1);
            questMasterService.AddQuester("Zdar", 100, new Vector2(300, 100), game.spritCharacter[0], 0, 1);
            questMasterService.QuesterList[0].QuestService.AddQuest("první", "První úkol dfsjklgfdjgnfdslkgnlfdsgljdsjgfnfdsjgndfsjgljdjlsfgfdgdhsdfhdsfhsdfh", 100, true, questMasterService.QuesterList[0], questMasterService.QuesterList[1]);
            //questMasterService.QuesterList[0].QuestService.AddQuest("druhý", "Druhý úkol dfsjklgfdjgnfdslkgnlfdsgljdsjgfnfdsjgndfsjgljdjlsfgfdgdhsdfhdsfhsdfh", 100, true, ETypeOfQuest.Delivery, questMasterService.QuesterList[0], questMasterService.QuesterList[0]);
            questMasterService.QuesterList[1].QuestService.AddQuest("třetí", "Třetí úkol dfsjklgfdjgnfdslkgnlfdsgljdsjgfnfdsjgndfsjgljdjlsfgfdgdhsdfhdsfhsdfh", 100, true, questMasterService.QuesterList[1], questMasterService.QuesterList[0]);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            questMasterService.SetQuestGiveOver();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                    camera.Transform);
            DrawOrder = 5;
            questMasterService.DrawQuestMaster();
            questMasterService.OnEventPickUpQuest(character);
            if (game.SingleClick(game.controlsList[(int)EKeys.I].Key) && infoOpen)
            {
                infoOpen = false;
            }
            else if (game.SingleClick(game.controlsList[(int) EKeys.I].Key) || infoOpen)
            {
                questMasterService.DrawQuestTable(character);
                infoOpen = true;
            }
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
