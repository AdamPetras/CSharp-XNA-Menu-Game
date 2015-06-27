using Menu.GameFolder.Components;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class Camera
    {
        public Matrix transform;
        public Vector2 centering;
        private Car car;
        public Camera(Car car)
        {
            this.car = car;
        }

        public void Update()
        {
            centering = new Vector2(car.position.X - Game.width / 2, car.position.Y - Game.height / 2);
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                        Matrix.CreateTranslation(new Vector3(-centering.X, -centering.Y, 0));
        }
    }
}