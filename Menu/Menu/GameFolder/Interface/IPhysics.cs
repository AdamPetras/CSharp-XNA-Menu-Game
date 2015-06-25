using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Menu.Components;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Interface
{
    public interface IPhysics
    {
        double Velocity { get; set; }
        double Speed(GameTime gameTime,ECar eCar);
        double Inertia(GameTime gameTime,ECar eCar);
        double Brake(GameTime gameTime,ECar eCar);
    }
}
