using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public enum EEnemies
    {
        None = 0,
        Speed,
        Tank,
        Damage,
        EliteTank,
        EliteDamage,
        BossTank,
        BossDamage,
    }

    public class EnemyOption
    {
        private GameClass game;
        private EEnemies eEnemies;
        public int HowManyZombies; 
        public EnemyOption(GameClass game)
        {
            this.game = game;
            HowManyZombies = 0;
        }

        public void GetEnemy(ref string name,int generate, ref int hp, ref int damage, ref float speed, ref Texture2D texture, ref int score, ref double chanceToMiss, ref int exp,int i=0)
        {
            TypeOfEnemy(ref name,generate, ref hp, ref damage, ref speed, ref texture, ref score, ref chanceToMiss, ref exp, i);
        }

        private void TypeOfEnemy(ref string name,int generate, ref int hp, ref int damage, ref float speed, ref Texture2D texture, ref int score, ref double chanceToMiss, ref int exp,int i)
        {
            if (generate<= 100 &&generate>80)
            {
                HowManyZombies++;
                eEnemies = EEnemies.Speed;
                hp = 80;
                damage = 20;
                chanceToMiss = 12;
                speed = 1.2f;
                texture = game.spritEnemy[0];
                score = 15;
                exp = 90;
            }
            else if (generate <= 80 && generate > 60)
            {
                HowManyZombies++;
                eEnemies = EEnemies.Tank;
                hp = 200;
                damage = 25;
                chanceToMiss = 10;
                speed = 0.5f;
                texture = game.spritEnemy[0];
                score = 15;
                exp = 90;
            }
            else if (generate <= 60 && generate > 40)
            {
                HowManyZombies++;
                eEnemies = EEnemies.Damage;
                hp = 100;
                damage = 35;
                chanceToMiss = 15;
                speed = 0.7f;
                texture = game.spritEnemy[0];
                score = 15;
                exp = 90;
            }
            else if (generate <= 40 && generate > 30)
            {
                HowManyZombies++;
                eEnemies = EEnemies.EliteTank;
                hp = 350;
                damage = 30;
                chanceToMiss = 8;
                speed = 0.6f;
                texture = game.spritEnemy[0];
                score = 30;
                exp = 140;
            }
            else if (generate <= 30 && generate > 20)
            {
                HowManyZombies++;
                eEnemies = EEnemies.EliteDamage;
                hp = 200;
                damage = 60;
                chanceToMiss = 10;
                speed = 0.8f;
                texture = game.spritEnemy[0];
                score = 30;
                exp = 140;
            }
            else if (generate <= 20 && generate > 15)
            {
                HowManyZombies++;
                eEnemies = EEnemies.BossTank;
                hp = 2600;
                damage = 60;
                chanceToMiss = 12;
                speed = 0.6f;
                texture = game.spritEnemy[0];
                score = 100;
                exp = 400;
            }
            else if (generate <= 15 && generate > 10)
            {
                HowManyZombies++;
                eEnemies = EEnemies.BossDamage;
                hp = 1200;
                damage = 150;
                chanceToMiss = 12;
                speed = 0.7f;
                texture = game.spritEnemy[0];
                score = 250;
                exp = 400;
            }
            name = eEnemies.ToString();
        }
    }
}
