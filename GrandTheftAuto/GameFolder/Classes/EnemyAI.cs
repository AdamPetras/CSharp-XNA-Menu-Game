using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class EnemyAi
    {
        private List<Pathfinding> closedList;   // již projíté pole nebo překážky
        private List<Pathfinding> openedList;   // pole, které je ještě potřeba projít
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obstactleList"></param>
        public EnemyAi(List<Rectangle> obstactleList)
        {
            closedList = new List<Pathfinding>();
            openedList = new List<Pathfinding>();
            foreach (Rectangle rectangle in obstactleList)
            {
                closedList.Add(new Pathfinding(rectangle));
            }
        }

        /// <summary>
        /// Method to generate position without colision
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="charRectangle"></param>
        /// <returns></returns>
        public Vector2 GeneratePosition(Random rand, Rectangle charRectangle)
        {
            Vector2 position;
            Rectangle characterRectangle = new Rectangle(charRectangle.Left - 100, charRectangle.Top - 100, charRectangle.Right + 100, charRectangle.Bottom + 100);
            do
            {
                position = new Vector2(rand.Next(0, 500), rand.Next(0, 500));
            } while (closedList.Any(s => s.Obstactle.Contains(position)) && characterRectangle.Contains(position));
            return position;
        }


        public Vector2 PathFinding(Vector2 actualPosition, Vector2 finishPosition, Enemy enemy, Rectangle characterRectangle, Rectangle carRectangle)
        {
            // G = součet hodnot po nejkratší cestě po které se lze dostat do místa
            // H = odhadová vzdálenost od aktuálního místa do cíle
            // F = H + G
            // H = |start.X-cíl.X|+|start.Y-cíl.Y|
            // G = startovní pozice (ze začátku vždy 0)
            // F = |start.X-cíl.X|+|start.Y-cíl.Y| + startovní pozice
            if ((!characterRectangle.IsEmpty && !characterRectangle.Contains(actualPosition)) || (!carRectangle.IsEmpty && !carRectangle.Contains(actualPosition)))
            {
                Pathfinding pathfinding = new Pathfinding(actualPosition, finishPosition); //Aktuální pozice
                closedList.Add(pathfinding);
                if (enemy.IsAngry || pathfinding.F < 200)
                {
                    openedList.Clear();
                    Vector2 defaultVector = actualPosition;

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
                    openedList = openedList.OrderBy(s => s.F).ToList(); //seřazení od nejmenšího prvku
                    foreach (Pathfinding t in openedList)
                    {
                        enemy.Position = t.Start;
                        enemy.UpdateEachRectangle();
                        if (!closedList.Any(s => s.Obstactle.Intersects(enemy.Rectangle)))
                        {
                            if (enemy.MaxHp != enemy.Hp || openedList[0].F < 200)
                                enemy.IsAngry = true;
                            else enemy.IsAngry = false;
                            return t.Start;
                        }
                    }
                }
            }
            return actualPosition;
        }
    }
}
