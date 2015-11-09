using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public enum EEnemies
    {
        Speed = 0,
        Tank,
        Damage,
        EliteTank,
        EliteDamage,
        BossTank,
        BossDamage

    }

    public class EnemyOption
    {
        private GameClass game;
        private EEnemies eEnemies;
        public EnemyOption(GameClass game)
        {
            this.game = game;
        }

        public void GetEnemy(ref string name,int enemy, ref int hp, ref int damage, ref float speed,ref Texture2D texture,ref int score,ref double chanceToMiss)
        {
            eEnemies = (EEnemies) enemy;
            TypeOfEnemy(ref name,ref hp,ref damage,ref speed, ref texture, ref score,ref chanceToMiss);
        }

        private void TypeOfEnemy(ref string name, ref int hp,ref int damage,ref float speed, ref Texture2D texture,ref int score,ref double chanceToMiss)
        {
            name = eEnemies.ToString();
            if (eEnemies == EEnemies.Speed)
            {
                hp = 80;
                damage = 20;
                chanceToMiss = 12;
                speed = 1.2f;
                texture = game.spritEnemy[0];
                score = 15;
            }
            else if (eEnemies == EEnemies.Tank)
            {
                hp = 200;
                damage = 25;
                chanceToMiss = 10;
                speed = 0.5f;
                texture = game.spritEnemy[0];
                score = 15;
            }
            else if(eEnemies == EEnemies.Damage)
            {
                hp = 100;
                damage = 35;
                chanceToMiss = 15;
                speed = 0.7f;
                texture = game.spritEnemy[0];
                score = 15;
            }
            else if (eEnemies == EEnemies.EliteTank)
            {
                hp = 350;
                damage = 30;
                chanceToMiss = 8;
                speed = 0.6f;
                texture = game.spritEnemy[0];
                score = 30;
            }
            else if (eEnemies == EEnemies.EliteDamage)
            {
                hp = 200;
                damage = 60;
                chanceToMiss = 10;
                speed = 0.8f;
                texture = game.spritEnemy[0];
                score = 30;
            }
            else if (eEnemies == EEnemies.BossTank)
            {
                hp = 700;
                damage = 40;
                chanceToMiss = 12;
                speed = 0.6f;
                texture = game.spritEnemy[0];
                score = 100;
            }
            else if (eEnemies == EEnemies.BossDamage)
            {
                hp = 450;
                damage = 90;
                chanceToMiss = 12;
                speed = 0.7f;
                texture = game.spritEnemy[0];
                score = 100;
            }
        }
    }
}
