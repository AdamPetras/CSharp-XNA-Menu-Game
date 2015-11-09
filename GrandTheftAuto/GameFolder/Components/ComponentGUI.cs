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
        private readonly CharacterUsingGuns characterUsingGuns;
        private readonly EnemyService enemyService;
        public ComponentGUI(GameClass game, Camera camera, Character character, CharacterUsingGuns characterUsingGuns, EnemyService enemyService)
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

            game.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawCharacterGui()
        {
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.Reloading)
            {
                game.spriteBatch.DrawString(game.normalFont, character.Hp + " HP\nEnergy: " + character.Energy, new Vector2(camera.Centering.X + game.graphics.PreferredBackBufferWidth - game.normalFont.MeasureString(character.Hp + " HP\nEnergy: " + character.Energy).X, camera.Centering.Y), Color.White);
                if (game.EGameState == EGameState.Reloading)
                {
                    game.spriteBatch.DrawString(game.normalFont, characterUsingGuns.SelectedGun.EGun + "\nReloading", new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
                }
                else if (characterUsingGuns.SelectedGun != null)
                {
                    game.spriteBatch.DrawString(game.normalFont, characterUsingGuns.SelectedGun.EGun + "\n" + characterUsingGuns.SelectedGun.Magazine + "/" + characterUsingGuns.SelectedGun.Ammo, new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
                }
                else
                    game.spriteBatch.DrawString(game.normalFont, "No Gun",
                        new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
                game.spriteBatch.DrawString(game.normalFont, "Score: " + enemyService.Score, new Vector2(game.graphics.PreferredBackBufferWidth - game.normalFont.MeasureString("Score: " + enemyService.Score).X + camera.Centering.X, 2*game.normalFont.MeasureString("Score: " + enemyService.Score).Y + camera.Centering.Y), Color.White);
            }
        }

        private void DrawCarGui()
        {
            foreach (Car car in game.carList)
            {
                if (game.EGameState == EGameState.InGameCar && car.Selected)
                {
                    game.spriteBatch.DrawString(game.normalFont, "Speed: " + car.CurrentSpeed() + "\n" + car.GetCarState(),
                        new Vector2(camera.Centering.X, camera.Centering.Y), Color.White);
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
            if (enemyService.EnemyTarget != null && enemyService.EnemyTarget.Alive &&game.EGameState != EGameState.GameOver)
            {
                string text = enemyService.EnemyTarget.Name + "\nHp: " +
                              enemyService.EnemyTarget.Hp + "/" +
                              enemyService.EnemyTarget.MaxHp + "\nScore: " +
                              enemyService.EnemyTarget.Score;
                game.spriteBatch.DrawString(game.smallFont, text,
                    new Vector2(
                        camera.Centering.X + game.graphics.PreferredBackBufferWidth -
                        game.smallFont.MeasureString(text).X,
                        camera.Centering.Y + game.graphics.PreferredBackBufferHeight -
                        game.smallFont.MeasureString(text).Y), Color.White);
            }
        }

        private void DrawGameOver()
        {
            if (!character.Alive || game.EGameState == EGameState.GameOver)
                game.spriteBatch.Draw(game.spritGameOver, new Vector2(game.graphics.PreferredBackBufferWidth / 2 - game.spritGameOver.Width / 2 + camera.Centering.X, game.graphics.PreferredBackBufferHeight / 2 - game.spritGameOver.Height / 2 + camera.Centering.Y), Color.White);
        }
    }
}
