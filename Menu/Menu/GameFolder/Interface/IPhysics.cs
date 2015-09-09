using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Interface
{
    public interface IPhysics
    {
        double Speed(ref double velocity, ECar eCar, double carWeight, double enginePower, GameTime gameTime);
        double Inertia(ref double velocity);
        double Brake(ref double velocity, double carWeight);
    }
}
