using System;
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
        private Random rnd;
        private string[] questMasterNames;
        public ComponentQuestSystem(GameClass game, Character character, Camera camera)
            : base(game)
        {
            this.game = game;
            this.camera = camera;
            this.character = character;
            rnd = new Random();
            questMasterNames = new[]
            {
                "Amanitar", "Bonemaw","Beauty","Arion","Commander Tharbek","Daakara","Elegon","Erekem","Anomalus","Erudax","Anzu","Falric","Ferra","Anzu","Freya",
            "Garr","Augh","Broggok","C'Thun","Gluth","Gorefiend","Gruul","Cyanigosa","Corborus","Halazzi","Halycon","Ebonroc","Entropius","Hazzas","Hex Lord Malacrass","Hodir","Firemaw","Felmyst","Ick"
            ,"Immerseus","Immol'thar","Isalien"
            };
            questMasterService = new QuestMasterService(game, camera, this);
            QuestService = new QuestService();
            questMasterService.AddQuester(questMasterNames[0], 100, new Vector2(350, 325), GenerateTexture(), 0f, 1);
            questMasterService.AddQuester(questMasterNames[1], 100, new Vector2(-325, -925), GenerateTexture(), 3.14f, 1);
            questMasterService.AddQuester(questMasterNames[2], 100, new Vector2(700, 325), GenerateTexture(), 3.14f, 1);
            questMasterService.AddQuester(questMasterNames[3], 100, new Vector2(-575, 325), GenerateTexture(), 0, 1);
            questMasterService.AddQuester(questMasterNames[4], 100, new Vector2(0, 800), GenerateTexture(), 0, 1);
            questMasterService.AddQuester(questMasterNames[5], 100, new Vector2(-2725, -1225), GenerateTexture(), 0, 1);
            questMasterService.AddQuester(questMasterNames[6], 100, new Vector2(2550, -875), GenerateTexture(), 3.14f, 1);
            questMasterService.AddQuester(questMasterNames[7], 100, new Vector2(3025, 575), GenerateTexture(), 0, 1);
            questMasterService.AddQuester(questMasterNames[8], 100, new Vector2(1775, 900), GenerateTexture(), 0, 1);
            questMasterService.AddQuester(questMasterNames[9], 100, new Vector2(1175, 1475), GenerateTexture(), 0, 1);
            questMasterService.AddQuester(questMasterNames[10], 100, new Vector2(900, 1475), GenerateTexture(), 0, 1);
            questMasterService.AddQuester(questMasterNames[11], 100, new Vector2(-600, 1175), GenerateTexture(), 0, 1);
            questMasterService.AddQuester(questMasterNames[12], 100, new Vector2(-2725, 600), GenerateTexture(), 0, 1);
            questMasterService.AddQuester(questMasterNames[13], 100, new Vector2(1775, 2125), GenerateTexture(), 0, 1);
            questMasterService.AddQuester(questMasterNames[14], 100, new Vector2(-925, 2125), GenerateTexture(), 0, 1);
            questMasterService.AddQuester(questMasterNames[15], 100, new Vector2(-2725, 1950), GenerateTexture(), 4.71f, 1);
            questMasterService.AddQuester(questMasterNames[16], 100, new Vector2(1350, -1175), GenerateTexture(), 3.14f, 1);
            questMasterService.AddQuester(questMasterNames[17], 100, new Vector2(-1500, -1775), GenerateTexture(), 3.14f, 1);

            QuestService.AddQuest("Welcome!!!", "Your welcome this is your first quest in this game i wish you Good luck and Have a fun." + EndOfQuest(questMasterService.QuesterList[1], 100), game.smallFont, 100, 0, questMasterService.QuesterList[0], questMasterService.QuesterList[1]);
            QuestService.AddQuest("My friend", "Hello guy go to my best friend and tell him about my new love. It is a new girlfriend. She is so beautiful and kind i really love her. If he will want to be friend with her than tell him to come to me." + EndOfQuest(questMasterService.QuesterList[2], 100), game.smallFont, 100, 1, questMasterService.QuesterList[1], questMasterService.QuesterList[2]);
            QuestService.AddQuest("Care of my family", "Hi stranger please kill five zombies because the wanted to kill my family so i need it as soon as possible." + EndOfQuest(questMasterService.QuesterList[0], 200, whatToDo: "kill 5 zombies"), game.smallFont, 200, 2, 5, questMasterService.QuesterList[1], questMasterService.QuesterList[1], null);
            QuestService.AddQuest("Brother", "Hello stranger go and tell my brother about the zombies and about you that you killed any of them. Then tell him that the family is all right. Thank you a lot advanturer." + EndOfQuest(questMasterService.QuesterList[3], 250), game.smallFont, 250, 3, questMasterService.QuesterList[1], questMasterService.QuesterList[3]);
            QuestService.AddQuest("Taking care of family", "Hi advanturer its good that they are all right. Tell them that i can help if it will possible and if they will want some resources i can give them something." + EndOfQuest(questMasterService.QuesterList[1], 250), game.smallFont, 250, 4, questMasterService.QuesterList[3], questMasterService.QuesterList[1]);
            QuestService.AddQuest("Bring the gun", "Hi stranger take this gun and give it to my friend. He wanted this gun to protect his own house against the zombies." + EndOfQuest(questMasterService.QuesterList[4], 300, " and unlock Starter Equip."), game.smallFont, 300, 5, questMasterService.QuesterList[1], questMasterService.QuesterList[4]);  //odemknutí Starter equipu
            QuestService.AddQuest("Yea i have the gun", "Hi friend tell him that i send him thanks for the gun its awesome gun and it has got nice damage." + EndOfQuest(questMasterService.QuesterList[1], 150), game.smallFont, 150, 6, questMasterService.QuesterList[4], questMasterService.QuesterList[1]);
            QuestService.AddQuest("Kill them please", "Please kill five zombies specifics like Damage. Because the scared my family because they wanted to kill all of them." + EndOfQuest(questMasterService.QuesterList[0], 400, whatToDo: "kill 5 specifics zombies (Damage)"), game.smallFont, 400, 6, 5, questMasterService.QuesterList[0], questMasterService.QuesterList[0], null, null, EEnemies.Damage);
            QuestService.AddQuest("The Hunter", "Hello stranger please kill some zombies they are so annoying and they want to kill all of us. Please we need to kill their two bosses with the highest health i wish you good luck." + EndOfQuest(questMasterService.QuesterList[0], 500, whatToDo: "kill 2 specifics zombies (BossTank)"), game.smallFont, 500, 8, 2, questMasterService.QuesterList[0], questMasterService.QuesterList[0], null, null, EEnemies.BossTank);
            QuestService.AddQuest("Good job", "Hi advanturer its done thank you for help i hope that you will help me again later. Go and tell this to my friend thank you a lot." + EndOfQuest(questMasterService.QuesterList[4], 500), game.smallFont, 500, 8, questMasterService.QuesterList[1], questMasterService.QuesterList[4]);
            QuestService.AddQuest("Daakara", "Hello guy go to my friend Daakara he is on the South West of map. He is waiting to you because he need help from you." + EndOfQuest(questMasterService.QuesterList[5], 350), game.smallFont, 350, 10, questMasterService.QuesterList[4], questMasterService.QuesterList[5]);
            QuestService.AddQuest("Bring it him", "I'm really glad to see you guy please bring this present to my friend and tell him that i am really happy that we are friends and that i hope for i will see him early." + EndOfQuest(questMasterService.QuesterList[4], 400), game.smallFont, 400, 11, questMasterService.QuesterList[5], questMasterService.QuesterList[4]);
            QuestService.AddQuest("Annihilation", "Hi stranger i have got a difficult task for you we need to kill some zombies but it have to be specific zombies (Damage,Tank,Speed) Good luck guy you will need it. " + EndOfQuest(questMasterService.QuesterList[2], 1200, whatToDo: "kill 20 specifics zombies(Damage,Tank,Speed)"), game.smallFont, 1200, 12, 20, questMasterService.QuesterList[2], questMasterService.QuesterList[2], null, null, EEnemies.Speed, EEnemies.Tank, EEnemies.Damage);
            QuestService.AddQuest("All my friends", "Hello!!! I have a task for you guy please caun you go to my two friends and tell them about the situation with zombie apocalypse. All of they should know it" + EndOfQuest(questMasterService.QuesterList[2], 600, whatToDo: "speak with the my two friends"), game.smallFont, 600, 13, questMasterService.QuesterList[2], questMasterService.QuesterList[2], null, questMasterService.QuesterList[1], questMasterService.QuesterList[4]);
            QuestService.AddQuest("A lot of items", "Hi guy please here is a lot of industry items for example iron pipes and iron sheet please grab it and bring it to my friend. He really need it to build some weapons. He is on North-west of the map thank you." + EndOfQuest(questMasterService.QuesterList[2], 500), game.smallFont, 500, 14, questMasterService.QuesterList[5], questMasterService.QuesterList[2]);
            QuestService.AddQuest("Defend the Smithy", "Yea WELCOME thank you for items now i need to defend the smithy from zombies and i want to smith the weapons." + EndOfQuest(questMasterService.QuesterList[2], 1000, " and unlock Fighter Equip.", "kill 15 zombies near the smithy"), game.smallFont, 1000, 15, 10, questMasterService.QuesterList[2], questMasterService.QuesterList[2], new Rectangle(400, -150, 900, 400));   //odemknutí Fighter equipu
            QuestService.AddQuest("You did it", "Hello i have to thank you because you did a lot of work here. Now please go and tell my friend about this." + EndOfQuest(questMasterService.QuesterList[5], 600), game.smallFont, 600, 16, questMasterService.QuesterList[2], questMasterService.QuesterList[5]);
            QuestService.AddQuest("Its really good", "Hi stranger its really good to know because we need this weapons go to him and tell him that i send you to deliver these gus 2 my friends." + EndOfQuest(questMasterService.QuesterList[2], 600), game.smallFont, 600, 17, questMasterService.QuesterList[5], questMasterService.QuesterList[2]);
            QuestService.AddQuest("Bring it them", "Hi stranger its really good to know because we need this weapons go to him and tell him that i send you to deliver these gus 2 my friends." + EndOfQuest(questMasterService.QuesterList[2], 900), game.smallFont, 900, 18, questMasterService.QuesterList[2], questMasterService.QuesterList[2], null, questMasterService.QuesterList[6], questMasterService.QuesterList[7], questMasterService.QuesterList[8]);
            QuestService.AddQuest("The zombies", "Hello tell " + questMasterService.QuesterList[9].Name + "something about the zombie apocalipse he really want to know detail of the zombie apocalipse." + EndOfQuest(questMasterService.QuesterList[9], 600), game.smallFont, 600, 19, questMasterService.QuesterList[8], questMasterService.QuesterList[9]);
            QuestService.AddQuest("My friends", "Hi!!! please i have a little request to you i need to tell my friend " + questMasterService.QuesterList[10].Name + " " + questMasterService.QuesterList[11].Name + " " + questMasterService.QuesterList[13].Name + " something about apocalipse." + EndOfQuest(questMasterService.QuesterList[9], 1100), game.smallFont, 1100, 20, questMasterService.QuesterList[9], questMasterService.QuesterList[9], null, questMasterService.QuesterList[10], questMasterService.QuesterList[11], questMasterService.QuesterList[13]);
            QuestService.AddQuest("Need help!!!", "Hi guy please i need help to defend my house againist zombies cause the want to destroy my house" + EndOfQuest(questMasterService.QuesterList[11], 1100, whatToDo: "kill 10 zombies and defend " + questMasterService.QuesterList[11].Name + "s house"), game.smallFont, 1800, 20, 10, questMasterService.QuesterList[11], questMasterService.QuesterList[11], new Rectangle(-1500, 300, 1500, 1500));
            QuestService.AddQuest("Does he have the problem?", "Go to the " + questMasterService.QuesterList[15].Name + " he need help too go to him and ask to him." + EndOfQuest(questMasterService.QuesterList[15], 900), game.smallFont, 900, 22, questMasterService.QuesterList[11], questMasterService.QuesterList[15], null, questMasterService.QuesterList[15]);
            QuestService.AddQuest("Good thinking", "Hi advanturer Yea!! I really need some help because they are faggots all of they dont want to let me to go out of my house please exterminate them." + EndOfQuest(questMasterService.QuesterList[15], 1500, " and unlock BloodStriker Equip."), game.smallFont, 1500, 23, 10, questMasterService.QuesterList[15], questMasterService.QuesterList[15], new Rectangle(-3300, 1200, 1500, 1500)); //odemknutí BloodStriker equipu
            QuestService.AddQuest("Really good you are God", "Thank you for help me defend the house. Now go and tell it to my friend " + questMasterService.QuesterList[10] + " . Thank you a lot." + EndOfQuest(questMasterService.QuesterList[10], 1200), game.smallFont, 1200, 24, questMasterService.QuesterList[15], questMasterService.QuesterList[10]);
            QuestService.AddQuest("Okey it was really good", "Hello stranger i'd love to see you guy you are the savior tell to my friends(" + questMasterService.QuesterList[12] + " " + questMasterService.QuesterList[14] + " " + questMasterService.QuesterList[8] + " " + questMasterService.QuesterList[17] + ") that they are everywhere and its worse and worse than before." + EndOfQuest(questMasterService.QuesterList[10], 1700), game.smallFont, 1700, 25, questMasterService.QuesterList[10], questMasterService.QuesterList[10], null, questMasterService.QuesterList[12], questMasterService.QuesterList[14], questMasterService.QuesterList[8], questMasterService.QuesterList[17]);
            QuestService.AddQuest("The killing Mass", "I have a lot of work for you guy we need to kill a lot of zombies." + EndOfQuest(questMasterService.QuesterList[17], 2500,whatToDo:"kill 40 zombies becareful"), game.smallFont, 2500, 26, 40, questMasterService.QuesterList[10], questMasterService.QuesterList[17], null);
            QuestService.AddQuest("They wanted to see you", "Hello there my friends wanted to you you and i think that they want to tell you something." + EndOfQuest(questMasterService.QuesterList[5], 2000), game.smallFont, 2000, 27, questMasterService.QuesterList[14], questMasterService.QuesterList[5], null, questMasterService.QuesterList[17], questMasterService.QuesterList[2], questMasterService.QuesterList[5]);

            //poslední úkol nastaven QP na 50!
            QuestService.AddQuest("The Last of Us", "It was too hard to you guy thank you for it. It was really awesome from you. Now you are really good guy and you are ready to WAR!" + EndOfQuest(questMasterService.QuesterList[1], 5000,whatToDo:"kill 100 zombies becareful"), game.smallFont, 5000,50, 100, questMasterService.QuesterList[1], questMasterService.QuesterList[1], null);
            AddQuestsToQuestMasters();  //nastavení úkolů přísušným questmasterům
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.InGameCar || game.EGameState == EGameState.Reloading)
            {
                questMasterService.SetActivatedQuest(character);
                //questMasterService.ColisionWithCharacter(character);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                    camera.Transform);
            questMasterService.DrawQuestMaster();
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.InGameCar || game.EGameState == EGameState.Reloading)
            {
                DrawOrder = 5;
                questMasterService.OnEventPickUpQuest(character);
                if (game.SingleClick(game.controlsList[(int)EKeys.I].Key, false) && infoOpen)   //questlist zavřen
                {
                    infoOpen = false;
                    game.FXCloseSound.Play();
                }
                else if (game.SingleClick(game.controlsList[(int)EKeys.I].Key, false) || infoOpen)  //quest list otevřen
                {
                    if (!infoOpen)
                        game.FXOpenSound.Play();
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
                questMasterService.QuesterList.Find(s => s == quest.Start).QuestList.Add(quest);
            }
            QuestService.QuestList.Clear();
        }

        private Texture2D GenerateTexture()
        {
            return game.spritNPCs[rnd.Next(0, game.spritNPCs.Length)];
        }

        private string EndOfQuest(QuestMaster questMaster, int reward, string reward1 = "", string whatToDo = "")
        {
            if (whatToDo != "")
                whatToDo = "\nYou should " + whatToDo + " to complete this quest.";
            return whatToDo + "\nGo to the " + questMaster.Name + " and give this quest over.\nYour reward is: " + reward + " Experiences" + reward1;
        }
    }
}