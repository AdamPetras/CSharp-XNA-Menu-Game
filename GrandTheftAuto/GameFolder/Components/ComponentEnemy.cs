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
        private GameClass game;
        public Car car;
        public EnemyService enemyService;
        public Camera camera;
        public Character character;
        public CharacterUsingGuns characterUsingGuns;

        public ComponentEnemy(GameClass game,SavedData savedData,List<Rectangle> obstactleList=null,Camera camera = null, Car car = null,
            CharacterUsingGuns characterUsingGuns = null, Character character = null)
            : base(game)
        {
            this.game = game;
            this.character = character;
            this.characterUsingGuns = characterUsingGuns;
            this.car = car;
            this.camera = camera;
            enemyService = new EnemyService(game,obstactleList);
            if(savedData.EnemyList!=null)
            enemyService.EnemyList = savedData.EnemyList;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            enemyService.GeneratingEnemies(gameTime,camera);
            if (character != null && character.Alive)
            {
                enemyService.PathFinding(character.CharacterPosition, character.CharacterRectangle());
                int hp = character.Hp;
                enemyService.Attack(ref hp,character.CharacterRectangle(), gameTime);
                character.Hp = hp;
                enemyService.RotationOfEnemy(character.CharacterPosition,character);
                camera.Update(character.CharacterPosition);
            }
            else if (car != null && character == null)
            {
                enemyService.PathFinding(car.Position,Rectangle.Empty,car.CarRectangle());
                enemyService.RotationOfEnemy(car.Position,character);
                int damaged = car.Hp;
                enemyService.Attack(ref damaged,car.CarRectangle(),gameTime);
                car.Hp = damaged;
                camera.Update(car.Position);
            }
            if (characterUsingGuns != null)
                enemyService.GetDamage(characterUsingGuns.BulletList);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            DrawOrder = 1;
            enemyService.DrawEnemy(camera);
            game.spriteBatch.DrawString(game.normalFont, "Score: " + enemyService.Score(), new Vector2(game.graphics.PreferredBackBufferWidth - game.normalFont.MeasureString("Score: " + enemyService.Score()).X,game.normalFont.MeasureString("Score: " + enemyService.Score()).Y), Color.White);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
