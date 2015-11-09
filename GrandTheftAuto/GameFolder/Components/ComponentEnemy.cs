using System;
using System.Collections.Generic;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.CarFolder;
using GrandTheftAuto.GameFolder.Classes.Gun;
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
        private readonly Character character;
        private readonly CharacterUsingGuns characterUsingGuns;

        public ComponentEnemy(GameClass game, SavedData savedData, List<Rectangle> obstactleList = null,
            Camera camera = null, ComponentCar componentCar = null,
            CharacterUsingGuns characterUsingGuns = null, Character character = null)
            : base(game)
        {
            this.game = game;
            this.character = character;
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
            if (game.EGameState == EGameState.InGameOut || game.EGameState == EGameState.Reloading && character.Alive)
            {
                enemyService.GeneratingEnemies(gameTime, camera, character.CharacterRectangle());
                enemyService.PathFinding(character.CharacterPosition, character.CharacterRectangle());
                int hp = character.Hp;
                enemyService.Attack(ref hp, character.CharacterRectangle(), gameTime,camera);
                character.Hp = hp;
                enemyService.RotationOfEnemy(character.CharacterPosition, character);
            }
            else if (game.EGameState == EGameState.InGameCar)
            {
                enemyService.GeneratingEnemies(gameTime, camera, componentCar.SelectedCar.CarRectangle());
                enemyService.PathFinding(componentCar.SelectedCar.Position, Rectangle.Empty, componentCar.SelectedCar.CarRectangle());
                enemyService.RotationOfEnemy(componentCar.SelectedCar.Position, character);
                int damaged = componentCar.SelectedCar.Hp;
                enemyService.Attack(ref damaged, componentCar.SelectedCar.CarRectangle(), gameTime,camera);
                componentCar.SelectedCar.Hp = damaged;
            }
            if (characterUsingGuns != null)
                enemyService.GetDamage(characterUsingGuns.BulletList,camera);
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
