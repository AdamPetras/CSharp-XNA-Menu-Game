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
        public ComponentDrawCurrentQuest(GameClass game, Character character, Camera camera, Quest currentQuest, QuestMaster currentQuestMaster, ComponentQuestSystem componentQuestSystem)
            : base(game)
        {
            this.game = game;
            this.camera = camera;
            this.character = character;
            this.currentQuestMaster = currentQuestMaster;
            this.currentQuest = currentQuest;
            this.componentQuestSystem = componentQuestSystem;
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
                if (currentQuest.EQuestState == EQuestState.Inactive)
                {
                    if (game.mouseState.RightButton == ButtonState.Pressed)
                    {
                        ExitThisComponent();
                    }
                    if (game.SingleClick(Keys.K))
                    {
                        AcceptQuest();
                    }
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.InGameCar ||
                game.EGameState == EGameState.Reloading)
            {
                if (currentQuest.EQuestState == EQuestState.Inactive)
                {
                    game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                        camera.Transform);
                    DrawOrder = 6;
                    game.spriteBatch.Draw(game.spritPergamen[1],
                        new Vector2(
                            (camera.Centering.X + game.graphics.PreferredBackBufferWidth / 2 -
                             game.spritPergamen[1].Width / 2),
                            (camera.Centering.Y + game.graphics.PreferredBackBufferHeight / 2 -
                             game.spritPergamen[1].Height / 2)),
                        Color.White);
                    Vector2 stringOrigin = game.smallFont.MeasureString(currentQuest.Description) / 2;
                    game.spriteBatch.DrawString(game.smallFont, currentQuest.Description,
                        new Vector2(WhereToWrite().Center.X, WhereToWrite().Center.Y), Color.Red, 0, stringOrigin, 1.0f,
                        SpriteEffects.None, 0.5f);
                    game.spriteBatch.End();
                }
            }
            base.Draw(gameTime);
        }

        private void CheckQuest()
        {
            if (currentQuest.ETypeOfQuest == ETypeOfQuest.Delivery)
            {
                if (currentQuest.End == currentQuestMaster)     //odevzdání úkolu
                {
                    character.ActualExperiences += currentQuest.Reward;
                    currentQuestMaster.QuestList.Remove(currentQuest);
                    character.QuestPoints++;
                    character.QuestList.Remove(currentQuest);
                    ExitThisComponent();
                }
            }
            else if (currentQuest.ETypeOfQuest == ETypeOfQuest.SearchAndDestroy)
            {
                if (currentQuest.ValueToSuccess == currentQuest.ActualValue && currentQuest.End == currentQuestMaster) //odevzdání úkolu
                {
                    character.ActualExperiences += currentQuest.Reward;
                    currentQuestMaster.QuestList.Remove(currentQuest);
                    character.QuestList.Remove(currentQuest);
                    character.QuestPoints++;
                    ExitThisComponent();
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
            currentQuest.EQuestState = currentQuest.ETypeOfQuest != ETypeOfQuest.Delivery ? EQuestState.Active : EQuestState.Complete;
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
