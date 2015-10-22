using System;
using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.GameFolder.Classes.Gun;
using GrandTheftAuto.GameFolder.Classes.GunFolder;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class EnemyService
    {
        public List<Enemy> EnemyList { get; set; }
        private GameClass game;
        private EnemyAi enemyAi;
        private Enemy enemy;
        private float attackTimer;
        private float spawnTimer;
        private int score;
        public EnemyService(GameClass game,List<Rectangle> obstactleList)
        {
            this.game = game;
            EnemyList = new List<Enemy>();
            enemyAi = new EnemyAi(obstactleList);
            score = 0;
        }

        private void AddEnemy(int enemySet, Vector2 position)
        {
            int hp = 0;
            float speed = 0;
            int damage = 0;
            float angle = 0;
            int score = 0;
            Texture2D texture = game.spritCharacter[0];
            EnemyOption enemyOption = new EnemyOption(game);
            enemyOption.GetEnemy(enemySet, ref hp, ref damage, ref speed, ref texture, ref score);
            enemy = new Enemy(hp, damage, speed, texture, position, angle, score);
            EnemyList.Add(enemy);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            EnemyList.Remove(enemy);
        }

        public void Attack(ref int Hp,Rectangle attackedRectangle, GameTime gameTime)
        {
            if (Hp > 0)
                foreach (Enemy enemy in EnemyList.Where(enemy => attackedRectangle.Intersects(enemy.Rectangle)))
                {
                    attackTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (1000 < attackTimer)
                    {
                        attackTimer = 0;
                        Hp -= enemy.Damage;
                    }
                }
            else Hp = 0;
        }

        public void GetDamage(List<Bullet> bulletList)
        {
            for (int i = 0; i <= EnemyList.Count - 1; i++)
            {
                for (int j = 0; j <= bulletList.Count - 1; j++)
                {
                    if (EnemyList[i].Rectangle.Contains(bulletList[j].Position.X, bulletList[j].Position.Y))
                    {
                        EnemyList[i].IsAngry = true;
                        EnemyList[i].Hp -= bulletList[j].Damage;
                        bulletList.Remove(bulletList[j]);
                    }
                }
                if (EnemyList[i].Hp <= 0)
                {
                    EnemyList[i].Alive = false;
                    score += EnemyList[i].Score;
                    EnemyList.Remove(EnemyList[i]);
                }
            }
        }

        public int Score()
        {
            return score;
        }

        public void PathFinding(Vector2 characterPosition, Rectangle characterRectangle = default(Rectangle), Rectangle carRectangle = default(Rectangle))
        {
            foreach (Enemy enemy in EnemyList)
            {
                enemy.UpdateEachRectangle(); //aktualizace rectanglu enemy
                float angle = enemy.Angle;
                enemy.Position = enemyAi.PathFinding(enemy.Position, characterPosition, enemy, characterRectangle, carRectangle);
                enemy.Angle = angle;
            }
        }

        public void GeneratingEnemies(GameTime gameTime, Camera camera)
        {
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (10000 < spawnTimer)
            {
                Random rand = new Random();
                AddEnemy(rand.Next(0, Enum.GetNames(typeof(EEnemies)).Length), new Vector2(rand.Next(0, 1000), rand.Next(0, 1000)));
                spawnTimer = 0;
            }
        }

        public void RotationOfEnemy(Vector2 position, Character character)
        {
            foreach (Enemy enemy in EnemyList.Where(s => s.IsAngry))
            {
                double legOne;
                double legTwo; //leg = přepona
                if (enemy.Position.X > position.X && enemy.Position.Y < position.Y) //první kvadrant
                {
                    legOne = enemy.Position.X - position.X;
                    legTwo = position.Y - enemy.Position.Y;
                    enemy.Angle = (float)Math.Atan2(legOne, legTwo) + GameClass.DegreeToRadians(180);
                }
                else if (enemy.Position.X < position.X && enemy.Position.Y < position.Y) //druhý kvadrant
                {
                    legOne = position.X - enemy.Position.X;
                    legTwo = position.Y - enemy.Position.Y;
                    enemy.Angle = (float)Math.Atan2(legTwo, legOne) + GameClass.DegreeToRadians(90);
                }
                else if (enemy.Position.X < position.X && enemy.Position.Y > position.Y) //třetí kvadrant
                {
                    legOne = position.X - enemy.Position.X;
                    legTwo = enemy.Position.Y - position.Y;
                    enemy.Angle = (float)Math.Atan2(legOne, legTwo);
                }
                else if (enemy.Position.X > position.X && enemy.Position.Y > position.Y) //čtvrtý kvadrant
                {
                    legOne = enemy.Position.X - position.X;
                    legTwo = enemy.Position.Y - position.Y;
                    enemy.Angle = (float)Math.Atan2(legTwo, legOne) - GameClass.DegreeToRadians(90);
                }
                enemy.Angle = enemy.Angle;
            }
        }

        public void DrawEnemy(Camera camera)
        {
            foreach (Enemy enemy in EnemyList)
            {
                game.spriteBatch.Draw(enemy.Texture, new Rectangle((int)(enemy.Position.X - camera.Centering.X), (int)(enemy.Position.Y - camera.Centering.Y), enemy.Texture.Width, enemy.Texture.Height), null, Color.White, enemy.Angle, new Vector2(enemy.Texture.Width / 2, enemy.Texture.Height / 2), SpriteEffects.None, 0f);
            }
        }
    }
}
