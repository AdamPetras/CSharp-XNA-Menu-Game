using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrandTheftAuto.GameFolder.Components;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class QuestMasterService
    {
        public List<QuestMaster> QuesterList { get; private set; }
        public int Xp;
        private GameClass game;
        private Camera camera;
        private IMenu orientationMenu;
        private bool writen;
        private bool talking;
        private ComponentQuestSystem componentQuestSystem;


        public delegate void PickUpQuest(Character character);

        public event PickUpQuest EventPickUpQuest;
        public QuestMasterService(GameClass game, Camera camera, ComponentQuestSystem componentQuestSystem)
        {
            Xp = 0;
            this.game = game;
            this.camera = camera;
            this.componentQuestSystem = componentQuestSystem;
            QuesterList = new List<QuestMaster>();
            orientationMenu = new MenuItems(game, new Vector2(0, 0), game.smallFont);
            EventPickUpQuest += DrawTalkBackground;
            EventPickUpQuest += QuestMenu;
        }

        public void AddQuester(string name, int hp, Vector2 position, Texture2D texture, float angle, float speed)
        {
            QuesterList.Add(new QuestMaster(name, hp, position, texture, angle, speed, camera));
        }

        private void QuestMenu(Character character)
        {
            if (!writen)
                foreach (Quest quest in CurrentQuestMaster(character).QuestService.QuestList)  //naplnění menu úkoly
                {
                    orientationMenu.AddItem(quest.Name);
                    orientationMenu.Selected = orientationMenu.Items.First();
                    writen = true;
                }
            //centrování pozice okna s úkoly
            orientationMenu.PositionIfCameraMoving(camera, new Vector2(game.graphics.PreferredBackBufferWidth / 2, (game.graphics.PreferredBackBufferHeight / 2 - game.spritPergamen[1].Height / 2) + 20));
            orientationMenu.Draw();      //vykreslení úkolů
            orientationMenu.Moving(Keys.Up, Keys.Down);  //pohyb po úkolech
            if (game.SingleClick(Keys.Enter))
            {
                ComponentDrawCurrentQuest componentDrawCurrentQuest = new ComponentDrawCurrentQuest(game, character, camera, CurrentQuestMaster(character).QuestService.QuestList.Find(s => s.Name == orientationMenu.Selected.Text), CurrentQuestMaster(character), componentQuestSystem);
                //reset výpisu
                orientationMenu.Items.Clear();
                writen = false;
                game.Components.Add(componentDrawCurrentQuest);
                game.ComponentEnable(componentQuestSystem, false);
            }
            else if (game.mouseState.RightButton == ButtonState.Pressed) //konec dialogu
            {
                //reset výpisu a zrušení dialogu
                talking = false;
                writen = false;
                orientationMenu.Items.Clear();
            }
        }

        public void DrawQuestMaster()
        {
            foreach (QuestMaster quester in QuesterList)
            {
                game.spriteBatch.Draw(quester.Texture, quester.Position, Color.White);
                //pokud má npc nějaké questy, pokud je nějaký aktivovatelný a není hotov tak...
                if (quester.QuestService.QuestList.Count > 0 && quester.QuestService.QuestList.Any(s => s.Activable && s.EQuestState == EQuestState.Active))
                {
                    game.spriteBatch.Draw(game.spritActiveQuestion, new Vector2(quester.Rectangle.Center.X - game.spritQuestion.Width / 2, quester.Position.Y), Color.White);
                }
                else if (quester.QuestService.QuestList.Count > 0 && quester.QuestService.QuestList.Any(s => s.Activable && s.EQuestState == EQuestState.Inactive))
                {
                    game.spriteBatch.Draw(game.spritQuestion, new Vector2(quester.Rectangle.Center.X - game.spritQuestion.Width / 2, quester.Position.Y), Color.White);
                }

            }
        }
        public virtual void OnEventPickUpQuest(Character character)
        {
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.F].Key) || talking)
            {
                if (EventPickUpQuest != null && CurrentQuestMaster(character) != null)
                {
                    talking = true;
                    EventPickUpQuest(character);
                }
                else talking = false;
            }
        }

        private QuestMaster CurrentQuestMaster(Character character)
        {
            return QuesterList.Find(s => s.TalkRectangle.Intersects(character.Rectangle));
        }

        private void DrawTalkBackground(Character character)
        {
            game.spriteBatch.Draw(game.spritPergamen[1],
new Vector2(character.Rectangle.Center.X - game.spritPergamen[1].Width / 2,
character.Rectangle.Center.Y - game.spritPergamen[1].Height / 2), Color.White);
        }

        public void DrawQuestTable(Character character)
        {
            DrawTalkBackground(character);
            game.spriteBatch.DrawString(game.smallFont, "Quest name       QuestState", new Vector2(
                    camera.Centering.X + character.Rectangle.Center.X -
                    game.smallFont.MeasureString("Quest name       QuestState").X / 2,
                    (camera.Centering.Y + character.Rectangle.Center.Y - game.spritPergamen[1].Height / 2) +
                    (game.smallFont.MeasureString("Quest name       QuestState").Y)), Color.White);
            foreach (Quest quest in character.QuestList)
            {
                Vector2 position =
    new Vector2(
        camera.Centering.X + game.graphics.PreferredBackBufferWidth / 2 -
        game.smallFont.MeasureString(quest.Name + "       " + quest.EQuestState).X / 2,
        (camera.Centering.Y + game.graphics.PreferredBackBufferHeight / 2 - game.spritPergamen[1].Height / 2) +
        (game.smallFont.MeasureString("Quest name       QuestState\n").Y *
         (character.QuestList.FindIndex(s => s.Name == quest.Name) + 1)));
                game.spriteBatch.DrawString(game.smallFont, quest.Name + "       " + quest.EQuestState, position, Color.White);
            }
        }

        public void SetQuestGiveOver()
        {
            foreach (QuestMaster questMaster in QuesterList)
            {
                if(questMaster.QuestService.QuestList.Any(s=>s.EQuestState==EQuestState.Active))    //pokud questmastar má nějaký aktivní quest
                {
                    Quest quest = questMaster.QuestService.QuestList.Find(s => s.EQuestState == EQuestState.Active);    //initializace questu , který je aktivní
                    QuestMaster giveOverQuestMaster = QuesterList.Find(s => s == quest.End);    //initializace questmastera, kterému semá odevzdat quest
                    if (!giveOverQuestMaster.QuestService.QuestList.Contains(quest))    //pokud questmaster ještě nemá ten quest
                    {
                        giveOverQuestMaster.QuestService.QuestList.Add(quest);
                        questMaster.QuestService.QuestList.Remove(quest);
                    }
                }
            }
        }
    }
}
