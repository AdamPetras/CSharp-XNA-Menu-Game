using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrandTheftAuto.GameFolder.Components;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using GrandTheftAuto.MenuFolder.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class CharacterService
    {
        private GameClass game;
        public Character Character { get; set; }
        public StatsService stats;
        public SkillView skillView;
        public bool IsStatsRunning;
        private double distance;
        private double animationTimer; //timer pro animace postavy
        private double interval = 150; //interval na animaci postavy
        private double energyDrainRegenerateTimer;
        private double healthRegenerateTimer;
        public delegate void LevelUp();

        public event LevelUp EventLevelUp;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="savedData"></param>
        /// <param name="position"></param>
        /// <param name="angle"></param>
        public CharacterService(GameClass game, SavedData savedData)
        {
            this.game = game;
            Character = new Character(new Vector2(100, 100), game.spritCharacter[0]);
            stats = new StatsService(Character);
            skillView = new SkillView(game, Character);
            EventLevelUp += stats.NewLevel;
            EventLevelUp += stats.CalculateExperience;
            EventLevelUp += skillView.AddSkillPoint;
            if (!savedData.InTheCar)
            {
                Character.Position = savedData.CharacterPosition;
                Character.Angle = savedData.CharacterAngle;
            }
        }

        /// <summary>
        /// Updatable method to detect keypress and then call the other method
        /// </summary>
        /// <param name="gameTime"></param>
        public void Move(GameTime gameTime)
        {
            #region Chůze

            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key) && Character.Alive)
            {
                distance += Character.Speed;
                CharacterAnimation(gameTime);
                Character.Position = game.CalculatePosition(Character.Position, Character.Angle, ref distance);
            }
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key) && Character.Alive)
            {
                distance -= Character.Speed;
                CharacterAnimation(gameTime);
                Character.Position = game.CalculatePosition(Character.Position, Character.Angle, ref distance);
            }

            #endregion

            #region Otáčení

            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Left].Key) && Character.Alive)
            {
                Character.Angle -= 0.05f;
            }
            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Right].Key) && Character.Alive)
            {
                Character.Angle += 0.05f;
            }

            #endregion

            #region Běh

            if (game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Shift].Key) && Character.Alive && Character.Energy > 0 &&
                !Character.Regeneration)
            {
                Character.Speed = 0.5f + Character.DefaultSpeed;
                interval = 120;
                energyDrainRegenerateTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (energyDrainRegenerateTimer > Character.EnergyRegen)
                {
                    Character.Energy -= 1;
                    energyDrainRegenerateTimer = 0;
                }
                if (Character.Energy == 0)
                    Character.Speed = Character.DefaultSpeed;
            }
            else if (Character.Energy < Character.MaxEnergy)
            {
                Character.Regeneration = true;
                Character.Speed = Character.DefaultSpeed;
                interval = 150;
                energyDrainRegenerateTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (energyDrainRegenerateTimer > 200 && Character.Energy < Character.MaxEnergy)
                {
                    Character.Energy += 1;
                    energyDrainRegenerateTimer = 0;
                }
                if (Character.Energy >= Character.MaxEnergy / 5)
                    Character.Regeneration = false;
            }

            #endregion

            //-------------Pokud nechodí nebo je zmáčknuto dopředu i dozadu tak se nastaví obrázek stop-------------
            if (game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Down].Key) &&
                game.keyState.IsKeyUp(game.controlsList[(int)EKeys.Up].Key) ||
                game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Down].Key) &&
                game.keyState.IsKeyDown(game.controlsList[(int)EKeys.Up].Key))
                Character.CurrentFrame = 0;

        }

        public void Update(GameTime gameTime)
        {
            Live();
            HealthRegenerate(gameTime);
            UpdateQuestValue();
            Character.UpdateRectangle();
            OnEventLevelUp(Character.Energy, Character.Hp);
            if (game.SingleClick(game.controlsList[(int)EKeys.C].Key, false) && !IsStatsRunning && Character.Alive)
            {
                ComponentStats componentStats = new ComponentStats(game, stats, this);
                game.Components.Add(componentStats);
                IsStatsRunning = true;
            }
            else if (game.SingleClick(game.controlsList[(int)EKeys.T].Key, false) && !IsStatsRunning && Character.Alive)
            {
                ComponentStats componentStats = new ComponentStats(game, skillView, stats, this);
                game.Components.Add(componentStats);
                IsStatsRunning = true;
            }
        }

        private void Live()
        {
            Character.Alive = Character.Hp > 0;
        }

        /// <summary>
        /// Method to change frame of character
        /// </summary>
        /// <param name="gameTime"></param>
        private void CharacterAnimation(GameTime gameTime)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (animationTimer > interval)
            {
                // Show the next frame
                Character.CurrentFrame++;
                // Reset the timer
                animationTimer = 0f;
            }
            if (Character.CurrentFrame == 5)
            {
                Character.CurrentFrame = 0;
            }
        }

        /// <summary>
        /// Method to draw character
        /// </summary>
        public void DrawCharacter()
        {
            game.spriteBatch.Draw(game.spritCharacter[Character.CurrentFrame],
                new Rectangle((int)Character.Position.X, (int)Character.Position.Y,
                    game.spritCharacter[Character.CurrentFrame].Width, game.spritCharacter[Character.CurrentFrame].Height), null,
                Color.White, game.Rotation(Character.Angle) + 1.5f,
                new Vector2(game.spritCharacter[Character.CurrentFrame].Width / 2, game.spritCharacter[Character.CurrentFrame].Height / 2),
                SpriteEffects.None, 0f);
        }

        protected virtual void OnEventLevelUp(int maxEnergy, double maxHp)
        {
            if (EventLevelUp != null && (Character.ActualExperiences >= Character.LevelUpExperience || Character.Level < 1))
                EventLevelUp();
        }

        private void HealthRegenerate(GameTime gameTime)
        {
            if ((int)Character.Hp != (int)Character.MaxHp && Character.Alive)
            {
                healthRegenerateTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (healthRegenerateTimer >= Character.HpRegen)
                {
                    healthRegenerateTimer = 0;
                    Character.Hp += 2;
                    if (Character.Hp > Character.MaxHp) //ošetření overflow
                        Character.Hp = Character.MaxHp;
                }
            }
        }

        public void UpdateQuestValue()  //metoda na přičítání zabítí
        {
            //pokud je aktivní úkol na search and destroy a bool proměnná je true
            if (
                Character.EnemyKilled != null &&
                Character.QuestList.Any(s => s.EQuestState == EQuestState.Active && s.ETypeOfQuest == ETypeOfQuest.SearchAndDestroy))
            {
                //přičtení hodnoty k úkolu
                foreach (Quest quest in Character.QuestList.Where(s => s.EQuestState == EQuestState.Active && s.ETypeOfQuest == ETypeOfQuest.SearchAndDestroy))
                {
                    if (quest.PlaceToDo == null)
                    {
                        if (quest.EEnemies.Count == 0)
                            quest.ActualValue += 1;
                        else if (quest.EEnemies.Any(s => s.ToString() == Character.EnemyKilled.Name))
                            quest.ActualValue += 1;
                        if (quest.ActualValue == quest.ValueToSuccess) //pokud je úkol dokončen
                            quest.EQuestState = EQuestState.Complete;
                    }
                    else if (quest.PlaceToDo != null)
                    {
                        if (quest.PlaceToDo.Value.Intersects(Character.Rectangle))
                        {
                            if (quest.EEnemies.Count == 0)
                                quest.ActualValue += 1;
                            else if (quest.EEnemies.Any(s => s.ToString() == Character.EnemyKilled.Name))
                                quest.ActualValue += 1;
                            if (quest.ActualValue == quest.ValueToSuccess) //pokud je úkol dokončen
                                quest.EQuestState = EQuestState.Complete;
                        }
                    }
                    Character.EnemyKilled = null; //defaultnutí proměnné pro přičítání
                }
            }
            else Character.EnemyKilled = null; //jinak nastav bool proměnnou na false
        }
    }
}