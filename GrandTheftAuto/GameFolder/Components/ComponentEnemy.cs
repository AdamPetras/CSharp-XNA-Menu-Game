using System.Collections.Generic;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.GunFolder;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentEnemy : DrawableGameComponent
    {
        public EnemyService enemyService;
        private readonly GameClass game;
        private readonly ComponentCar componentCar;
        private readonly Camera camera;
        private readonly CharacterService characterService;
        private readonly GunService characterUsingGuns;

        public ComponentEnemy(GameClass game, SavedData savedData, List<Rectangle> obstactleList = null,
            Camera camera = null, ComponentCar componentCar = null,
            GunService characterUsingGuns = null, CharacterService characterService = null)
            : base(game)
        {
            this.game = game;
            this.characterService = characterService;
            this.characterUsingGuns = characterUsingGuns;
            this.componentCar = componentCar;
            this.camera = camera;
            enemyService = new EnemyService(game, obstactleList);
            if (savedData.EnemyList != null)
            {
                enemyService.EnemyList = savedData.EnemyList;
            }
            enemyService.Score = savedData.Score;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.Reloading && characterService.Character.Alive)
            {
                enemyService.GeneratingEnemies(gameTime, camera, characterService.Character.Rectangle);
                enemyService.PathFinding(characterService.Character.Position, characterService.Character.Rectangle);
                double hp = characterService.Character.Hp;
                enemyService.Attack(ref hp, characterService.Character.Rectangle, gameTime, camera);
                characterService.Character.Hp = hp;
                enemyService.RotationOfEnemy(characterService.Character.Position, characterService.Character);
            }
            else if (game.EGameState == EGameState.InGameCar)
            {
                enemyService.GeneratingEnemies(gameTime, camera, componentCar.SelectedCar.CarRectangle());
                enemyService.PathFinding(componentCar.SelectedCar.Position, Rectangle.Empty, componentCar.SelectedCar.CarRectangle());
                enemyService.RotationOfEnemy(componentCar.SelectedCar.Position, characterService.Character);
                double damaged = componentCar.SelectedCar.Hp;
                enemyService.Attack(ref damaged, componentCar.SelectedCar.CarRectangle(), gameTime,camera);
                componentCar.SelectedCar.Hp = damaged;
            }
            if (characterUsingGuns != null)
                enemyService.GetDamage(characterUsingGuns.BulletList,camera,characterService.Character);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            DrawOrder = 1;
            enemyService.DrawEnemy(camera,gameTime);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
