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
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (currentQuest.EQuestState == EQuestState.Inactive)
            {
                game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                    camera.Transform);
                DrawOrder = 6;

                game.spriteBatch.Draw(game.spritPergamen[0],
                    new Vector2(
                        (camera.Centering.X + game.graphics.PreferredBackBufferWidth / 2 - game.spritPergamen[0].Width / 2),
                        (camera.Centering.Y + game.graphics.PreferredBackBufferHeight / 2 - game.spritPergamen[0].Height / 2)),
                    Color.White);
                game.spriteBatch.DrawString(game.smallestFont, currentQuest.Description,
                    new Vector2(
                        (camera.Centering.X + game.graphics.PreferredBackBufferWidth / 2 - game.spritPergamen[0].Width / 2) +
                        game.smallestFont.MeasureString(currentQuest.Description).X / 2,
                        (camera.Centering.Y + game.graphics.PreferredBackBufferHeight / 2 - game.spritPergamen[0].Height / 2) +
                        game.smallestFont.MeasureString(currentQuest.Description).Y / 2), Color.Red);

                game.spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private void CheckQuest()
        {
            switch (currentQuest.ETypeOfQuest)
            {
                case ETypeOfQuest.Delivery:
                    if (currentQuest.End == currentQuestMaster && currentQuest.EQuestState != EQuestState.Complete)
                        character.ActualExperiences += currentQuest.Reward;
                    currentQuest.EQuestState = EQuestState.Complete;
                    currentQuestMaster.QuestService.QuestList.Remove(currentQuest);
                    character.QuestList.Remove(currentQuest);
                    ExitThisComponent();
                    break;
                case ETypeOfQuest.SearchAndDestroy:
                    if (currentQuest.ValueToSuccess == currentQuest.ActualValue && currentQuest.End == currentQuestMaster &&
                        currentQuest.EQuestState != EQuestState.Complete)
                    {
                        character.ActualExperiences += currentQuest.Reward;
                        currentQuest.EQuestState = EQuestState.Complete;
                        currentQuestMaster.QuestService.QuestList.Remove(currentQuest);
                        character.QuestList.Remove(currentQuest);
                        ExitThisComponent();
                    }
                    break;
            }
        }

        public void UpdateQuestValue()
        {
            if (character.QuestList.Any(s => s.EQuestState == EQuestState.Active))    //pokud questmastar má nějaký aktivní quest
            {
                character.QuestList.Find(s => s.EQuestState == EQuestState.Active).ActualValue = 0;
            }
        }

        private void ExitThisComponent()
        {
            game.ComponentEnable(this, false);
            game.ComponentEnable(componentQuestSystem);
        }

        private void AcceptQuest()
        {
            currentQuest.EQuestState = EQuestState.Active;
            character.QuestList.Add(currentQuest);
            game.ComponentEnable(this, false);
            game.ComponentEnable(componentQuestSystem);
        }

    }
}
