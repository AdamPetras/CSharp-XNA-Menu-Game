using System;
using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class EnemyAi
    {
        private List<Rectangle> ObstactleList;   // již projíté pole nebo překážky
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obstactleList"></param>
        public EnemyAi(List<Rectangle> obstactleList)
        {
            ObstactleList = obstactleList;
        }

        private Vector2 TryDirection(Vector2 finishPosition, Enemy enemy, Rectangle rectangle)
        {
            Vector2 position = enemy.Position;
            Vector2 defaultPosition = position;
            float distance = (Math.Abs(position.X - finishPosition.X) + (Math.Abs(position.Y - finishPosition.Y)));
            Rectangle obsRectangle = new Rectangle(rectangle.Top + 1, rectangle.Left + 1, rectangle.Width - 2, rectangle.Height - 2);
            Rectangle tryRectangle = new Rectangle((int)(position.X + 1), (int)position.Y, enemy.Rectangle.Width, enemy.Rectangle.Height);
            float a = (Math.Abs(position.X+1 - finishPosition.X) + Math.Abs(position.Y - finishPosition.Y));
            if (!obsRectangle.Intersects(tryRectangle) && a < distance)   //doprava
            {
                enemy.Angle = (float)(Math.PI / 2);
                return new Vector2(1, 0);
            }
            position = defaultPosition;
            tryRectangle = new Rectangle((int)(position.X - 1), (int)position.Y, enemy.Texture.Width, enemy.Texture.Height);
            a = (Math.Abs(position.X-1 - finishPosition.X) + Math.Abs(position.Y - finishPosition.Y));
            if (!obsRectangle.Intersects(tryRectangle) && a < distance)    //doleva
            {
                enemy.Angle = (float)(Math.PI * 1.5);
                return new Vector2(-1, 0);
            }
            position = defaultPosition;
            tryRectangle = new Rectangle((int)position.X, (int)(position.Y - 1), enemy.Texture.Width, enemy.Texture.Height);
            a = (Math.Abs(position.X - finishPosition.X) + Math.Abs(position.Y-1 - finishPosition.Y));
            if (!obsRectangle.Intersects(tryRectangle) && a < distance)    //nahoru
            {
                enemy.Angle = 0f;
                return new Vector2(0, -1);
            }
            position = defaultPosition;
            tryRectangle = new Rectangle((int)position.X, (int)(position.Y + 1), enemy.Texture.Width, enemy.Texture.Height);
            a = (Math.Abs(position.X - finishPosition.X) + Math.Abs(position.Y+1 - finishPosition.Y));
            if (!obsRectangle.Intersects(tryRectangle) && a < distance)    //dolu
            {
                enemy.Angle = (float)(Math.PI);
                return new Vector2(0, 1);
            }
            return Vector2.Zero;
        }

        private Vector2 Position(Vector2 finishPosition, Enemy enemy)
        {
            if (finishPosition != enemy.Position)
            {
                //tg(a) = Xodvesna/Yodvesna nebo tg(a) = Yodvesna/Xodvesna podle kvadrantu
                //výpočet tangensu vždy pro danou odvěsnu rovnu 1
                float legOne;
                float legTwo; //leg = odvěsna
                float angle;
                if (enemy.Position.X > finishPosition.X && enemy.Position.Y < finishPosition.Y) //první kvadrant
                {
                    legOne = enemy.Position.X - finishPosition.X;        //X odvěsna
                    legTwo = finishPosition.Y - enemy.Position.Y;        //Y odvěsna
                    angle = (float)Math.Atan2(legOne, legTwo);              //výpočet úhlu
                    enemy.Angle = angle + GameClass.DegreeToRadians(180);   //nastavení úhlu enemy
                    if (angle <= Math.PI / 4)                               //první polovina kvadrantu
                        return new Vector2(-(float)(Math.Tan(angle)), 1);
                    if (angle >= Math.PI / 4)                               //druhá polovina kvadrantu
                        return new Vector2(-1, (float)((1) / (Math.Tan(angle))));
                }
                if (enemy.Position.X < finishPosition.X && enemy.Position.Y < finishPosition.Y) //druhý kvadrant
                {
                    legOne = finishPosition.X - enemy.Position.X;        //X odvěsna
                    legTwo = finishPosition.Y - enemy.Position.Y;        //Y odvěsna
                    angle = (float)Math.Atan2(legTwo, legOne);              //výpočet úhlu
                    enemy.Angle = angle + GameClass.DegreeToRadians(90);    //nastavení úhlu enemy
                    if (angle <= Math.PI / 4)                               //první polovina kvadrantu
                        return new Vector2(1, (float)Math.Tan(angle));
                    if (angle >= Math.PI / 4)                               //druhá polovina kvadrantu
                        return new Vector2((float)(1 / (Math.Tan(angle))), 1);
                }
                if (enemy.Position.X < finishPosition.X && enemy.Position.Y > finishPosition.Y) //třetí kvadrant
                {
                    legOne = finishPosition.X - enemy.Position.X;        //X odvěsna
                    legTwo = enemy.Position.Y - finishPosition.Y;        //Y odvěsna
                    angle = (float)Math.Atan2(legOne, legTwo);              //výpočet úhlu
                    enemy.Angle = angle;                                    //nastavení úhlu enemy
                    if (angle <= Math.PI / 4)                               //první polovina kvadrantu
                        return new Vector2((float)Math.Tan(angle), -1);
                    if (angle >= Math.PI / 4)                               //druhá polovina kvadrantu
                        return new Vector2(1, -(float)(1 / (Math.Tan(angle))));
                }
                if (enemy.Position.X > finishPosition.X && enemy.Position.Y > finishPosition.Y) //čtvrtý kvadrant
                {
                    legOne = enemy.Position.X - finishPosition.X;        //X odvěsna
                    legTwo = enemy.Position.Y - finishPosition.Y;        //Y odvěsna
                    angle = (float)Math.Atan2(legTwo, legOne);              //výpočet úhlu
                    enemy.Angle = angle - GameClass.DegreeToRadians(90);    //nastavení úhlu enemy
                    if (angle <= Math.PI / 4)                               //první polovina kvadrantu
                        return new Vector2(-1, -(float)Math.Tan(angle));
                    if (angle >= Math.PI / 4)                               //druhá polovina kvadrantu
                        return new Vector2(-(float)(1 / (Math.Tan(angle))), -1);
                }
                enemy.Angle = enemy.Angle;
            }
            return Vector2.Zero;
        }

        /// <summary>
        /// Method to generate position without colision
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="charRectangle"></param>
        /// <param name="enemyTexture"></param>
        /// <returns></returns>
        public Vector2 GeneratePosition(Random rand, Rectangle charRectangle, Texture2D enemyTexture)
        {
            Vector2 position;
            Rectangle enemyRectangle;
            Rectangle characterRectangle = new Rectangle(charRectangle.Left - 500, charRectangle.Top - 500, charRectangle.Right + 500, charRectangle.Bottom + 500); //aby se nespavnovali blízko charakteru
            do
            {
                position = new Vector2(rand.Next(-3000, 3000), rand.Next(-2100, 7500));
                enemyRectangle = new Rectangle((int)position.X, (int)position.Y, enemyTexture.Width, enemyTexture.Height);

            } while (ObstactleList.Any(s => s.Intersects(enemyRectangle)) || characterRectangle.Contains(position));
            return position;
        }


        public Vector2 PathFinding(Vector2 actualPosition, Vector2 finishPosition, Enemy enemy,
            Rectangle characterRectangle, Rectangle carRectangle)
        {
            if (((Math.Abs(actualPosition.X - finishPosition.X) + Math.Abs(actualPosition.Y - finishPosition.Y)) < 300 || enemy.IsAngry) && actualPosition.ToPoint() != finishPosition.ToPoint())
            {
                if (!ObstactleList.Any(s => s.Intersects(enemy.Rectangle)))
                {
                    enemy.Position += Position(finishPosition, enemy);
                    enemy.UpdateEachRectangle();
                }
                else
                {
                    enemy.Position += TryDirection(finishPosition, enemy, ObstactleList.Find(s => s.Intersects(enemy.Rectangle)));
                    enemy.UpdateEachRectangle();
                }
            }
            return enemy.Position;
        }
    }
}
