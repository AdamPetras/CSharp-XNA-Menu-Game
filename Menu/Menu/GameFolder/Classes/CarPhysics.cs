﻿using Menu.GameFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class CarPhysics : IPhysics
    {
        public double Velocity { get; set; }
        public CarPhysics()
        {
            Velocity = 0;
        }

        public double Speed(GameTime gameTime, ECar eCar)
        {
            double time = gameTime.ElapsedGameTime.TotalSeconds;
            if (eCar == ECar.Forward && Velocity <= 13)          //Maximální rychlost vpřed
                Velocity += time;
            else if (eCar == ECar.Backward && Velocity <= 3)    //Maximální rychlost vzad
                Velocity += time;
            return Velocity;
        }

        public double Inertia(GameTime gameTime)        //Setrvačnost
        {
            if (Velocity > 0)       //Pokud je rychlost větší jak nula
                Velocity -= gameTime.ElapsedGameTime.TotalSeconds;      //Tak se od rychlostí odečítá čas
            else Velocity = 0;
            return Velocity;
        }

        public double Brake(GameTime gameTime)      //Brzdění
        {
            if (Velocity > 0)       //Pokud je rychlost větší než nula
                Velocity -= gameTime.ElapsedGameTime.TotalSeconds * 4;  //Tak se od rychlostí odečítá čas
            else Velocity = 0;
            return Velocity;
        }
    }
}
