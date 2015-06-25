using Menu.GameFolder.Classes;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Interface
{
    public interface IPhysics
    {
        double Velocity { get; set; }
        double Speed(GameTime gameTime,ECar eCar);
        double Inertia(GameTime gameTime);
        double Brake(GameTime gameTime);
    }
}
