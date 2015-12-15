using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
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
        private bool talking;
        private ComponentQuestSystem componentQuestSystem;
        private Rectangle backgroundRectangle;
        public delegate void PickUpQuest(Character character);

        public event PickUpQuest EventPickUpQuest;

        public QuestMasterService(GameClass game, Camera camera, ComponentQuestSystem componentQuestSystem)
        {
            Xp = 0;
            this.game = game;
            this.camera = camera;
            this.componentQuestSystem = componentQuestSystem;
            QuesterList = new List<QuestMaster>();
            orientationMenu = new MenuItems(game);
            orientationMenu.SetKeysDown(Keys.Down);
            orientationMenu.SetKeysUp(Keys.Up);
            EventPickUpQuest += DrawTalkBackground;
            EventPickUpQuest += QuestMenu;
            EventPickUpQuest += DrawQuestMasterInfo;
        }

        public void AddQuester(string name, int hp, Vector2 position, Texture2D texture, float angle, float speed)
        {
            QuesterList.Add(new QuestMaster(name, hp, position, texture, angle, speed, camera));
        }

        private void QuestMenu(Character character)
        {
            Speaking(character);
            //pokud není již zapsáno a aktuální zadavatel má nějaký úkol
            if (CurrentQuestMaster(character).QuestList.Any(s => s.Activable))
                foreach (Quest quest in CurrentQuestMaster(character).QuestList.Where(s => s.Activable && s.EQuestState != EQuestState.Done)) //naplnění menu úkoly
                {
                    orientationMenu.AddItem(quest.Name, new Vector2(0, 0), game.smallFont, true,
                        quest.EQuestState.ToString(), 0, (int)(game.smallFont.MeasureString(quest.Name).X + 20));
                }
            else if (orientationMenu.Items.Count == 0)   //pokud nejsou žádné úkoly u zadavatele tak
                game.spriteBatch.DrawString(game.smallFont, "Hello none quest to do", new Vector2(camera.Centering.X + game.graphics.PreferredBackBufferWidth / 2 - game.smallFont.MeasureString("Hello none quest to do").X / 2,
                    (camera.Centering.Y + game.graphics.PreferredBackBufferHeight / 2 - game.spritPergamen[1].Height / 2) + 20), Color.White);
            //centrování pozice okna s úkoly
            orientationMenu.PositionIfCameraMoving(new Vector2(character.Rectangle.Center.X, character.Rectangle.Center.Y - backgroundRectangle.Height / 2));
            orientationMenu.Draw(); //vykreslení úkolů
            orientationMenu.Moving(); //pohyb po úkolech
            if (game.SingleClick(Keys.Enter) &&
                orientationMenu.Items.Count != 0)
            {
                Quest currentQuest =
CurrentQuestMaster(character)
.QuestList.Find(s => s.Name == orientationMenu.Selected.Text);
                if (currentQuest != null && orientationMenu.Selected.Text == currentQuest.Name)
                {
                    //inicializace aktuálně vybraného úkolu
                    ComponentDrawCurrentQuest componentDrawCurrentQuest = new ComponentDrawCurrentQuest(game, character,
                        camera, currentQuest, CurrentQuestMaster(character), componentQuestSystem);
                    game.Components.Add(componentDrawCurrentQuest);
                    orientationMenu.Items.Clear(); //vyčištění menu
                    game.ComponentEnable(componentQuestSystem, false);
                }
                else if(orientationMenu.Selected.Text == "Speak")
                {
                    Quest quest = character.QuestList.First(s => s.ETypeOfQuest == ETypeOfQuest.Notice);
                    orientationMenu.Items.Clear(); //vyčištění menu
                    quest.SpeakedWith.Add(CurrentQuestMaster(character));
                    CurrentQuestMaster(character).Speak = false;
                    if (quest.SpeakWith.Count == quest.SpeakedWith.Count)
                        quest.EQuestState = EQuestState.Complete;
                }
            }

            else if (game.SingleClickRightMouse()) //konec dialogu
            {
                //reset výpisu a zrušení dialogu
                talking = false;
                orientationMenu.Items.Clear(); //vyčištění menu
            }
        }

        public void DrawQuestMaster()
        {
            if (QuesterList.Count != 0)
            {
                foreach (QuestMaster quester in QuesterList)
                {
                    Vector2 origin = new Vector2(quester.Texture.Width/2, quester.Texture.Height/2);
                    game.spriteBatch.Draw(quester.Texture, quester.Rectangle, null, Color.White, quester.Angle, origin, SpriteEffects.None,0f);
                    //pokud si má promluvit s NPC
                    if (quester.Speak)
                    {
                        game.spriteBatch.Draw(game.spritSpeakBubble, //bublina dialogu
                            new Vector2(quester.Rectangle.Center.X - game.spritSpeakBubble.Width / 2,
                                quester.Position.Y)-origin,
                            Color.White);
                    }
                    //pokud má npc nějaké questy, pokud je nějaký aktivovatelný a je hotov tak...
                    else if (quester.QuestList.Count > 0 &&
                        quester.QuestList.Any(s => s.Activable && s.EQuestState == EQuestState.Complete))
                    {
                        game.spriteBatch.Draw(game.spritCompleteQuestion, //modrý otazník
                            new Vector2(quester.Rectangle.Center.X - game.spritCompleteQuestion.Width / 2,
                                quester.Position.Y)-origin,
                            Color.White);
                    }
                    //pokud má npc nějaké questy, pokud je nějaký aktivovatelný a není aktivní tak...
                    else if (quester.QuestList.Count > 0 &&
                             quester.QuestList.Any(
                                 s => s.Activable && s.EQuestState == EQuestState.Inactive))
                    {
                        game.spriteBatch.Draw(game.spritInActiveQuestion, //žlutý vykřičník
                            new Vector2(quester.Rectangle.Center.X - game.spritInActiveQuestion.Width / 2,
                                quester.Position.Y)-origin,
                            Color.White);
                    }
                    //pokud má npc nějaké questy, pokud je nějaký aktivovatelný a je aktivní tak...
                    else if (quester.QuestList.Count > 0 &&
                             quester.QuestList.Any(s => s.Activable && s.EQuestState == EQuestState.Active))
                    {
                        game.spriteBatch.Draw(game.spritActiveQuestion, //šedý otazník
                            new Vector2(quester.Rectangle.Center.X - game.spritActiveQuestion.Width / 2,
                                quester.Position.Y)-origin,
                            Color.White);
                    }
                }
            }
        }

        public virtual void OnEventPickUpQuest(Character character)
        {
            //pokud je zmáčknuto nastavené tlačítko nebo již probíhá rozhovor
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.F].Key) || talking)
            {
                //pokud má event a questmaster referenci tak se zavolá event
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
            //zjištění aktuálního zadavatele úkolů
            return QuesterList.Find(s => s.TalkRectangle.Intersects(character.Rectangle));
        }

        private void DrawTalkBackground(Character character)
        {
            //vykreslení pozadí pod textem úkolu
            backgroundRectangle = new Rectangle(character.Rectangle.Center.X - game.spritPergamen[1].Width / 2,
                    character.Rectangle.Center.Y - game.spritPergamen[1].Height / 2, game.spritPergamen[1].Width, game.spritPergamen[1].Height);
            game.spriteBatch.Draw(game.spritPergamen[1],
                backgroundRectangle.Location.ToVector2(), Color.White);
        }

        public void DrawQuestTable(Character character)
        {
            //vykreslení pozadí pod infem o úkolech
            Vector2 stringOrigin = game.smallFont.MeasureString("Name QuestState Done") / 2;
            DrawTalkBackground(character);
            game.spriteBatch.DrawString(game.smallFont, "Name QuestState Done", new Vector2(character.Rectangle.Center.X, character.Rectangle.Center.Y - game.spritPergamen[1].Height / 2), Color.White, 0, stringOrigin, 1, SpriteEffects.None, 0.5f);
            if (character.QuestList.Count > 0)
            {
                int i = 0;
                foreach (Quest quest in character.QuestList) //vykreslení infa o všech aktivních úkolech
                {
                    i++;
                    if (quest.ETypeOfQuest == ETypeOfQuest.SearchAndDestroy)
                    {
                        game.spriteBatch.DrawString(game.smallFont,
                            quest.Name + "  " + quest.EQuestState + "  " + quest.ActualValue + " / " +
                            quest.ValueToSuccess,
                            new Vector2(character.Rectangle.Center.X,
                                character.Rectangle.Center.Y - game.spritPergamen[1].Height / 2 +
                                (i * game.smallFont.MeasureString(quest.Name + " " + quest.EQuestState + " " +
                                                             quest.ActualValue + "/" + quest.ValueToSuccess).Y)),
                            Color.White, 0, stringOrigin, 1f, SpriteEffects.None, 0.5f);
                    }
                    else if (quest.ETypeOfQuest == ETypeOfQuest.Delivery)
                    {
                        game.spriteBatch.DrawString(game.smallFont,
                            quest.Name + "  " + quest.EQuestState,
                            new Vector2(character.Rectangle.Center.X,
                                character.Rectangle.Center.Y - game.spritPergamen[1].Height / 2 + (
                                i * game.smallFont.MeasureString(quest.Name + " " + quest.EQuestState + " ").Y)),
                            Color.White, 0, stringOrigin, 1f, SpriteEffects.None, 0.5f);
                    }
                    else if (quest.ETypeOfQuest == ETypeOfQuest.Notice)
                    {
                        game.spriteBatch.DrawString(game.smallFont,
                            quest.Name + "  " + quest.EQuestState+" "+quest.SpeakWith.Count+"/"+quest.SpeakedWith.Count,
                            new Vector2(character.Rectangle.Center.X,
                                character.Rectangle.Center.Y - game.spritPergamen[1].Height / 2 + (
                                i * game.smallFont.MeasureString(quest.Name + "  " + quest.EQuestState + " " + quest.SpeakWith.Count + "/" + quest.SpeakedWith.Count).Y)),
                            Color.White, 0, stringOrigin, 1f, SpriteEffects.None, 0.5f);
                    }
                }
            }
        }

        public void SetActivatedQuest(Character character)
        {
            foreach (QuestMaster questMaster in QuesterList)
            {
                foreach (Quest quest in questMaster.QuestList)
                {
                    SetTellSystemWithNpc(quest);
                    if (quest.Id == character.QuestPoints)
                    {
                        quest.Activable = true;
                    }
                }
            }
        }

        public void SetTellSystemWithNpc(Quest quest)
        {
            if (quest.ETypeOfQuest == ETypeOfQuest.Notice && quest.EQuestState == EQuestState.Active)
            {
                foreach (QuestMaster questMaster in quest.SpeakWith)
                {
                    if(!quest.SpeakedWith.Exists(s=>s == questMaster))
                    questMaster.Speak = true;
                }
            }
        }

        public void Speaking(Character character)
        {
            if (CurrentQuestMaster(character).Speak)
            {
                orientationMenu.AddItem("Speak", new Vector2(0, 0), game.smallFont, true,
                        "Click for speak", 0, (int)(game.smallFont.MeasureString("Speak").X + 20));
            }
        }

        public void DrawQuestMasterInfo(Character character)
        {
            if (CurrentQuestMaster(character) != null && game.EGameState != EGameState.GameOver)
            {
                string text = "Name: "+CurrentQuestMaster(character).Name + "\nHp: " + CurrentQuestMaster(character).Hp;
                //vykreslení informací o targetu
                game.spriteBatch.DrawString(game.smallFont, text,
                    new Vector2(
                        camera.Centering.X + game.graphics.PreferredBackBufferWidth -
                        game.smallFont.MeasureString(text).X,
                        camera.Centering.Y + game.graphics.PreferredBackBufferHeight -
                        game.smallFont.MeasureString(text).Y), Color.White);
            }
        }

        /*
        public void ColisionWithCharacter(Character character)
        {
            if(!QuesterList.Any(s => s.Rectangle.Intersects(character.Rectangle)))
                characterBefore = character.Position;
            if (QuesterList.Any(s => s.Rectangle.Intersects(character.Rectangle)))
            {
                character.Position = characterBefore;
            }
        }*/
    }
}
