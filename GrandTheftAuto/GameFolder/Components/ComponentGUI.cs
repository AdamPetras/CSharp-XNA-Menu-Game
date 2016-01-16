using System;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.CarFolder;
using GrandTheftAuto.GameFolder.Classes.GunFolder;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentGUI : DrawableGameComponent
    {
        private GameClass game;
        private readonly Camera camera;
        private readonly Character character;
        private readonly GunService characterUsingGuns;
        private readonly EnemyService enemyService;
        public ComponentGUI(GameClass game, Camera camera, Character character, GunService characterUsingGuns, EnemyService enemyService)
            : base(game)
        {
            this.game = game;
            this.camera = camera;
            this.character = character;
            this.characterUsingGuns = characterUsingGuns;
            this.enemyService = enemyService;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
               camera.Transform);
            DrawOrder = 5;
            DrawCharacterGui();
            DrawCarGui();
            enemyService.DrawEnemyHp();
            DrawGameOver();
            DrawEnemyTarget();
            DrawExperience();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawCharacterGui()
        {
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.Reloading)
            {
                //vykreslení životů graficky
                Vector2 HpPosition = new Vector2(camera.Centering.X + game.graphics.PreferredBackBufferWidth - game.spritHealthAndEnergyBar.Width - 20, camera.Centering.Y + 10);
                game.spriteBatch.Draw(game.spritCharacterHealth, new Rectangle((int)HpPosition.X+2, (int)HpPosition.Y+4, (int)((double)character.Hp / (double)character.MaxHp * game.spritHealthAndEnergyBar.Width-4), 10), Color.White);
                game.spriteBatch.Draw(game.spritHealthAndEnergyBar, HpPosition, Color.White * 0.9f);                
                //vykreslení energie graficky
                Vector2 EnergyPosition =
                    new Vector2(
                        camera.Centering.X + game.graphics.PreferredBackBufferWidth - game.spritHealthAndEnergyBar.Width -
                        20, camera.Centering.Y + 3*game.spritHealthAndEnergyBar.Height);
                game.spriteBatch.Draw(game.spritCharacterEnergy, new Rectangle((int)EnergyPosition.X + 2, (int)EnergyPosition.Y + 4, (int)((double)character.Energy / (double)character.MaxEnergy * game.spritHealthAndEnergyBar.Width - 4), 10), Color.White);
                game.spriteBatch.Draw(game.spritHealthAndEnergyBar,EnergyPosition, Color.White * 0.9f);
                //vykreslení infa o životech
                game.spriteBatch.DrawString(game.smallestFont, "Health:" + character.Hp + "/" + character.MaxHp,
                    new Vector2(camera.Centering.X + game.graphics.PreferredBackBufferWidth - game.spritHealthAndEnergyBar.Width, camera.Centering.Y + 5), Color.White);
                //vykreslení infa o energii
                game.spriteBatch.DrawString(game.smallestFont, "Energy:" + character.Energy + "/" + character.MaxEnergy,
    new Vector2(camera.Centering.X + game.graphics.PreferredBackBufferWidth - game.spritHealthAndEnergyBar.Width, camera.Centering.Y + 3 * game.spritHealthAndEnergyBar.Height - 5), Color.White);
                if (game.EGameState == EGameState.Reloading)    //pokud nabíjím
                {
                    game.spriteBatch.DrawString(game.normalFont, characterUsingGuns.SelectedGun.EGun + "\nReloading", new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
                }
                else if (characterUsingGuns.SelectedGun != null)
                {
                    //vykreslení infa o zbrani
                    game.spriteBatch.DrawString(game.normalFont, characterUsingGuns.SelectedGun.EGun + "\n" + characterUsingGuns.SelectedGun.Magazine + "/" + characterUsingGuns.SelectedGun.Ammo, new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
                    //if pro omezení nábojů graficky                  
                    int Ammo = characterUsingGuns.SelectedGun.Magazine > 30 ? 30 : characterUsingGuns.SelectedGun.Magazine;
                    for (int i = 0; i < Ammo; i++)   //vykreslení nábojů graficky
                        game.spriteBatch.Draw(game.spritAmmo, new Vector2(camera.Centering.X + (game.spritAmmo.Width + 1) * i, camera.Centering.Y + 2 * game.normalFont.MeasureString("A").Y), Color.White);
                }
                else
                    //pokud neni vybraná zbraň
                    game.spriteBatch.DrawString(game.normalFont, "No Gun",
                        new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
                game.spriteBatch.DrawString(game.normalFont, "Score: " + enemyService.Score, new Vector2(game.graphics.PreferredBackBufferWidth - game.normalFont.MeasureString("Score: " + enemyService.Score).X + camera.Centering.X, 2 * game.normalFont.MeasureString("Score: " + enemyService.Score).Y + camera.Centering.Y), Color.White);
            }
        }

        private void DrawCarGui()
        {
            foreach (Car car in game.carList)
            {
                if (game.EGameState == EGameState.InGameCar && car.Selected)
                {
                    //vykreslení rychlosti a stavu auta
                    game.spriteBatch.DrawString(game.normalFont, "Speed: " + car.CurrentSpeed() + "\n" + car.GetCarState(),
                        new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
                    //vykreslení životů auta
                    game.spriteBatch.DrawString(game.normalFont, "Car damage: " + (car.Hp / 10 - 100) * (-1) + "%",
                        new Vector2(
                            camera.Centering.X + game.graphics.PreferredBackBufferWidth -
                            game.normalFont.MeasureString("Car damage: " + (car.Hp / 10 - 100) * (-1) + "%").X,
                            camera.Centering.Y), Color.White);
                    game.spriteBatch.DrawString(game.normalFont, "Score: " + enemyService.Score, new Vector2(game.graphics.PreferredBackBufferWidth - game.normalFont.MeasureString("Score: " + enemyService.Score).X + camera.Centering.X, game.normalFont.MeasureString("Score: " + enemyService.Score).Y + camera.Centering.Y), Color.White);
                }
            }
        }

        private void DrawEnemyTarget()
        {
            if (enemyService.EnemyTarget != null && enemyService.EnemyTarget.Alive && game.EGameState != EGameState.GameOver)
            {
                string text = enemyService.EnemyTarget.Name + "\nHp: " +
                              enemyService.EnemyTarget.Hp + "/" +
                              enemyService.EnemyTarget.MaxHp + "\nScore: " +
                              enemyService.EnemyTarget.Score;
                //vykreslení informací o targetu
                game.spriteBatch.DrawString(game.smallFont, text,
                    new Vector2(
                        camera.Centering.X + game.graphics.PreferredBackBufferWidth -
                        game.smallFont.MeasureString(text).X,
                        camera.Centering.Y + game.graphics.PreferredBackBufferHeight -
                        game.smallFont.MeasureString(text).Y), Color.White);
            }
        }

        private void DrawExperience()
        {
            game.spriteBatch.Draw(game.spritExperienceCharge, new Rectangle((int)(camera.Centering.X + ((game.graphics.PreferredBackBufferWidth - game.spritExperienceBar.Width) / 2) + 4),
    (int)(camera.Centering.Y + game.graphics.PreferredBackBufferHeight - game.spritExperienceBar.Height + 2), (int)((double)character.ActualExperiences / (double)character.LevelUpExperience * 712), 14), Color.White);
            game.spriteBatch.Draw(game.spritExperienceBar, new Vector2(camera.Centering.X + ((game.graphics.PreferredBackBufferWidth - game.spritExperienceBar.Width) / 2),
                camera.Centering.Y + game.graphics.PreferredBackBufferHeight - game.spritExperienceBar.Height), Color.White);
            string text = "Level: " + character.Level + " Experiences: " + character.ActualExperiences + "/" + character.LevelUpExperience;
            game.spriteBatch.DrawString(game.smallestFont, text,
                new Vector2(camera.Centering.X + game.graphics.PreferredBackBufferWidth / 2 - game.smallestFont.MeasureString(text).X / 2, camera.Centering.Y + game.graphics.PreferredBackBufferHeight - game.smallestFont.MeasureString(text).Y - game.spritExperienceBar.Height), Color.White);
        }

        private void DrawGameOver()
        {
            if (!character.Alive || game.EGameState == EGameState.GameOver)
                game.spriteBatch.Draw(game.spritGameOver, new Vector2(game.graphics.PreferredBackBufferWidth / 2 - game.spritGameOver.Width / 2 + camera.Centering.X, game.graphics.PreferredBackBufferHeight / 2 - game.spritGameOver.Height / 2 + camera.Centering.Y), Color.White);
        }
    }
}
