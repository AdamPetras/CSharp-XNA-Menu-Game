using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class EnemyAi
    {
        private List<Pathfinding> closedList;   // již projíté pole vektorů
        private List<Pathfinding> openedList;   // pole, které je ještě potřeba projít
        private List<Rectangle> rectangleList;

        public EnemyAi(List<Rectangle> rectangleList)
        {
            closedList = new List<Pathfinding>();
            openedList = new List<Pathfinding>();
            this.rectangleList = rectangleList;
        }

        //G = součet hodnot po nejkratší cestě po které se lze dostat do místa
        //H = odhadová vzdálenost od aktuálního místa do cíle
        public Vector2 PathFinding(Vector2 actualPosition, Vector2 finishPosition, Enemy enemy, Rectangle characterRectangle, Rectangle carRectangle)
        {
            // F = H + G
            // H = |start.X-cíl.X|+|start.Y-cíl.Y|
            // G = startovní pozice (ze začátku vždy 0)
            // F = |start.X-cíl.X|+|start.Y-cíl.Y| + startovní pozice
            if ((!characterRectangle.IsEmpty && !characterRectangle.Contains(actualPosition)) || (!carRectangle.IsEmpty && !carRectangle.Contains(actualPosition)))
            {
                openedList.Clear();
                Vector2 defaultVector = actualPosition;
                Pathfinding pathfinding = new Pathfinding(actualPosition, finishPosition);   //Aktuální pozice
                closedList.Add(pathfinding);
                #region Čtyřrozměrný pohyb
                #region Vlevo
                actualPosition = defaultVector;
                actualPosition.X -= enemy.Speed; //vlevo
                enemy.Angle = 1.47f;
                pathfinding = new Pathfinding(actualPosition, finishPosition);
                openedList.Add(pathfinding);
                #endregion

                #region Vpravo
                actualPosition = defaultVector;
                actualPosition.X += enemy.Speed; //vpravo
                pathfinding = new Pathfinding(actualPosition, finishPosition);
                openedList.Add(pathfinding);
                #endregion

                #region Dolu
                actualPosition = defaultVector;
                actualPosition.Y += enemy.Speed; //dolu
                pathfinding = new Pathfinding(actualPosition, finishPosition);
                openedList.Add(pathfinding);
                #endregion

                #region Nahoru
                actualPosition = defaultVector;
                actualPosition.Y -= enemy.Speed; //nahoru
                pathfinding = new Pathfinding(actualPosition, finishPosition);
                openedList.Add(pathfinding);
                #endregion
                #endregion
                #region Diagonální pohyb
                #region Diagonální vpravo dolu
                actualPosition = defaultVector;
                actualPosition.Y += enemy.Speed; //dolu
                actualPosition.X += enemy.Speed; //vpravo
                pathfinding = new Pathfinding(actualPosition, finishPosition);
                openedList.Add(pathfinding);
                #endregion

                #region Diagonální vpravo nahoru
                actualPosition = defaultVector;
                actualPosition.Y -= enemy.Speed; //nahoru
                actualPosition.X += enemy.Speed; //vpravo
                pathfinding = new Pathfinding(actualPosition, finishPosition);
                openedList.Add(pathfinding);
                #endregion

                #region Diagonální vlevo dolu
                actualPosition = defaultVector;
                actualPosition.Y += enemy.Speed; //dolu
                actualPosition.X -= enemy.Speed; //vlevo
                pathfinding = new Pathfinding(actualPosition, finishPosition);
                openedList.Add(pathfinding);
                #endregion

                #region Diagonální vlevo nahoru
                actualPosition = defaultVector;
                actualPosition.Y -= enemy.Speed; //nahoru
                actualPosition.X -= enemy.Speed; //vlevo
                pathfinding = new Pathfinding(actualPosition, finishPosition);
                openedList.Add(pathfinding);
                #endregion
                #endregion
                actualPosition = defaultVector;
                openedList = openedList.OrderBy(s => s.F).ToList();     //seřazení od nejmenšího prvku
                if (!closedList.Contains(openedList[0]) && openedList[0].F < 200 || enemy.IsAngry)
                //pokud neni obsažen v closedlistu nebo vzdálenost je menší než 200 nebo neni naštvaný 
                {
                    if (openedList[0].F < 200 ||
                        enemy.Hp != enemy.MaxHp && !rectangleList.Any(s => s.Intersects(enemy.Rectangle)))      //podmínka pokud je enemy angry nebo F je menší než 200
                    {
                        enemy.IsAngry = true;
                        return openedList[0].Start;     //nastavení enemy pozice na nejlepší kombinaci
                    }
                    #region OBCHAZENI PŘEKÁŽEK
                    else if (openedList[1].F < 200 ||
                             enemy.Hp != enemy.MaxHp && rectangleList.Any(s => s.Intersects(enemy.Rectangle)))  //podmínka pokud je enemy angry nebo F je menší než 200
                    {
                        return CrossingObstactle(enemy, actualPosition);
                    }
                    #endregion
                    else enemy.IsAngry = false;
                }
            }
            return actualPosition;
        }

        private Vector2 CrossingObstactle(Enemy enemy,Vector2 actualPosition)
        {
            enemy.Position = openedList[1].Start;   //nastavení enemy pozice na druhou nejlepší kombinaci
                        enemy.UpdateEachRectangle();            //update rectanglu
                        if (openedList[1].F < 200 ||
                            enemy.Hp != enemy.MaxHp && !rectangleList.Any(s => s.Intersects(enemy.Rectangle)))      //podminka pokud ten rectangle nekoliduje a následné vrácení hodnoty
                        {
                            return enemy.Position;
                        }
            if (openedList[2].F < 200 ||
                enemy.Hp != enemy.MaxHp && rectangleList.Any(s => s.Intersects(enemy.Rectangle)))
                //podmínka pokud je enemy angry nebo F je menší než 200
            {
                enemy.Position = openedList[2].Start; //nastavení enemy pozice na třetí nejlepší kombinaci
                enemy.UpdateEachRectangle(); //update rectanglu
                if (openedList[2].F < 200 ||
                    enemy.Hp != enemy.MaxHp && !rectangleList.Any(s => s.Intersects(enemy.Rectangle)))
                    //podminka pokud ten rectangle nekoliduje a následné vrácení hodnoty
                {
                    return enemy.Position;
                }
                if (openedList[3].F < 200 ||
                    enemy.Hp != enemy.MaxHp && rectangleList.Any(s => s.Intersects(enemy.Rectangle)))
                    //podmínka pokud je enemy angry nebo F je menší než 200
                {
                    enemy.Position = openedList[3].Start; //nastavení enemy pozice na čtvrtou nejlepší kombinaci
                    enemy.UpdateEachRectangle(); //update rectanglu
                    if (openedList[3].F < 200 ||
                        enemy.Hp != enemy.MaxHp && !rectangleList.Any(s => s.Intersects(enemy.Rectangle)))
                        //podminka pokud ten rectangle nekoliduje a následné vrácení hodnoty
                    {
                        return enemy.Position;
                    }
                    if (openedList[4].F < 200 ||
                        enemy.Hp != enemy.MaxHp && rectangleList.Any(s => s.Intersects(enemy.Rectangle)))
                        //podmínka pokud je enemy angry nebo F je menší než 200
                    {
                        enemy.Position = openedList[4].Start; //nastavení enemy pozice na pátou nejlepší kombinaci
                        enemy.UpdateEachRectangle(); //update rectanglu
                        if (openedList[4].F < 200 ||
                            enemy.Hp != enemy.MaxHp && !rectangleList.Any(s => s.Intersects(enemy.Rectangle)))
                            //podminka pokud ten rectangle nekoliduje a následné vrácení hodnoty
                        {
                            return enemy.Position;
                        }
                    }
                }
            }
            return actualPosition;
        }

        public bool GetAngry(int hp, int maxHp)
        {
            return maxHp != hp;
        }
    }
}
