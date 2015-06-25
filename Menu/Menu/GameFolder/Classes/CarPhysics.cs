using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Menu.Components;
using Menu.GameFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class CarPhysics:IPhysics
    {
        public double Velocity { get; set; }
        public CarPhysics()
        {
            Velocity = 0.5;
        }

        public double Speed(GameTime gameTime,ECar eCar)
        {
            double time = gameTime.ElapsedGameTime.TotalSeconds;
            if (eCar == ECar.Forward && Velocity <= 7)
                Velocity += time;
            else if (eCar == ECar.Backward && Velocity <= 3)
                Velocity += time;
                return Velocity;
        }

        public double Inertia(GameTime gameTime, ECar eCar)
        {
            throw new NotImplementedException();
        }

        public double Brake(GameTime gameTime, ECar eCar)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            Velocity = 0.5;
        }
    }
}
