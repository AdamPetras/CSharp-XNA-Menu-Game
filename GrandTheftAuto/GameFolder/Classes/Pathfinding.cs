using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Pathfinding
    {
        public Vector2 Start { get; private set; }
        public Vector2 Finish { get; private set; }
        public float F { get; private set; } 

        public Pathfinding(Vector2 start,Vector2 finish)
        {
            Start = start;
            Finish = finish;
            F = (Math.Abs(start.X - finish.X) + Math.Abs(start.Y - finish.Y)) + F;
        }
    }
}
