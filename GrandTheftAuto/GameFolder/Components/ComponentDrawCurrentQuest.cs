using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentDrawCurrentQuest : DrawableGameComponent
    {
        private GameClass game;
        private Camera camera;
        private Quest currentQuest;
        private QuestMaster currentQuestMaster;
        private Character character;
        private ComponentQuestSystem componentQuestSystem;
        private bool questComplete;
        private double timerIfComplete;
        public ComponentDrawCurrentQuest(GameClass game, Character character, Camera camera, Quest currentQuest, QuestMaster currentQuestMaster, ComponentQuestSystem componentQuestSystem)
            : base(game)
        {
            this.game = game;
            this.camera = camera;
            this.character = character;
            this.currentQuestMaster = currentQuestMaster;
            this.currentQuest = currentQuest;
            this.componentQuestSystem = componentQuestSystem;
            questComplete = false;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.InGameCar ||
                game.EGameState == EGameState.Reloading)
            {

                CheckQuest();
                if (currentQuest.EQuestState == EQuestState.Inactive && character.Rectangle.Intersects(currentQuestMaster.TalkRectangle))
                {
                    if (game.SingleClickRightMouse())
                    {
                        ExitThisComponent();
                    }
                    if (game.SingleClick(Keys.Enter))
                    {
                        AcceptQuest();
                    }
                }
                if (!character.Rectangle.Intersects(currentQuestMaster.TalkRectangle))
                {
                    ExitThisComponent();
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.InGameCar ||
                game.EGameState == EGameState.Reloading)
            {
                game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                    camera.Transform);
                DrawOrder = 6;
                if (currentQuest.EQuestState == EQuestState.Inactive &&
                    character.Rectangle.Intersects(currentQuestMaster.TalkRectangle) && !questComplete)
                {
                    game.spriteBatch.Draw(game.spritPergamen[1], //vykreslení pozadí
                        new Vector2(
                            (camera.Centering.X + game.graphics.PreferredBackBufferWidth / 2 -
                             game.spritPergamen[1].Width / 2),
                            (camera.Centering.Y + game.graphics.PreferredBackBufferHeight / 2 -
                             game.spritPergamen[1].Height / 2)),
                        Color.White);
                    Vector2 stringOrigin = game.smallFont.MeasureString(currentQuest.Description) / 2;
                    game.spriteBatch.DrawString(game.smallFont, currentQuest.Description, //vykreslení textu
                        new Vector2(WhereToWrite().Center.X, WhereToWrite().Center.Y), Color.Red, 0, stringOrigin, 1.0f,
                        SpriteEffects.None, 0.5f);
                }
                else if (questComplete)
                {
                    timerIfComplete += gameTime.ElapsedGameTime.Milliseconds;
                    if (timerIfComplete < 1000)
                    {
                        Vector2 originVector = game.spritQuestComplete.Bounds.Center.ToVector2();
                        game.spriteBatch.Draw(game.spritQuestComplete, new Vector2(
                            (camera.Centering.X + game.graphics.PreferredBackBufferWidth / 2 -
                             originVector.X),
                            (camera.Centering.Y + game.graphics.PreferredBackBufferHeight / 2 -
                             originVector.Y)), Color.White);
                    }
                    else
                    {
                        timerIfComplete = 0;
                        questComplete = false;
                        ExitThisComponent();
                    }                
                }
                game.spriteBatch.End();

            }
            base.Draw(gameTime);
        }

        private void CheckQuest()
        {
            if (currentQuest.ETypeOfQuest == ETypeOfQuest.Delivery)
            {
                if (currentQuest.End == currentQuestMaster && currentQuest.EQuestState == EQuestState.Complete)     //odevzdání úkolu
                {
                    questComplete = true;
                    currentQuest.EQuestState = EQuestState.Done;
                    character.ActualExperiences += currentQuest.Reward;
                    currentQuestMaster.QuestList.Remove(currentQuest);
                    character.QuestList.Remove(currentQuest);
                    character.QuestPoints++;
                }
            }
            else if (currentQuest.ETypeOfQuest == ETypeOfQuest.SearchAndDestroy)
            {
                if (currentQuest.ValueToSuccess == currentQuest.ActualValue && currentQuest.End == currentQuestMaster && currentQuest.EQuestState != EQuestState.Done) //odevzdání úkolu
                {
                    questComplete = true;
                    currentQuest.EQuestState = EQuestState.Done;
                    character.ActualExperiences += currentQuest.Reward;
                    currentQuestMaster.QuestList.Remove(currentQuest);
                    character.QuestList.Remove(currentQuest);
                    character.QuestPoints++;
                }
                else if (currentQuest.EQuestState == EQuestState.Active) ExitThisComponent();
            }
            else if (currentQuest.ETypeOfQuest == ETypeOfQuest.Notice)
            {
                if (currentQuest.SpeakWith.Count == currentQuest.SpeakedWith.Count && currentQuest.End == currentQuestMaster && currentQuest.EQuestState != EQuestState.Done) //odevzdání úkolu
                {
                    questComplete = true;
                    currentQuest.EQuestState = EQuestState.Done;
                    character.ActualExperiences += currentQuest.Reward;
                    currentQuestMaster.QuestList.Remove(currentQuest);
                    character.QuestList.Remove(currentQuest);
                    character.QuestPoints++;
                }
                else if (currentQuest.EQuestState == EQuestState.Active) ExitThisComponent();
            }
        }

        private void ExitThisComponent()
        {
            game.ComponentEnable(this, false);
            game.ComponentEnable(componentQuestSystem);
        }

        private void AcceptQuest()
        {
            currentQuest.EQuestState = currentQuest.ETypeOfQuest == ETypeOfQuest.Delivery ? EQuestState.Complete : EQuestState.Active;
            currentQuest.Start.QuestList.Remove(currentQuest);
            currentQuest.End.QuestList.Add(currentQuest);
            character.QuestList.Add(currentQuest);
            game.ComponentEnable(this, false);
            game.ComponentEnable(componentQuestSystem);
        }

        private Rectangle WhereToWrite()
        {
            return new Rectangle((int)(camera.Centering.X + game.graphics.PreferredBackBufferWidth / 2 - game.spritPergamen[1].Width / 2),
                        (int)(camera.Centering.Y + game.graphics.PreferredBackBufferHeight / 2 - game.spritPergamen[1].Height / 2), game.spritPergamen[1].Width, game.spritPergamen[1].Height);
        }
    }
}
