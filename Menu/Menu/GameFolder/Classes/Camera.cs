using Menu.Components;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class Camera
    {
        public Matrix transform;
        private Vector2 centering;
        private Movement movement;
        public Camera(Movement movement)
        {
            this.movement = movement;
        }

        public void Update(TheGame theGame)
        {
            centering = new Vector2(movement.position.X + movement.position.Y / 2 - Game.width / 2, movement.position.Y);
            transform = Matrix.CreateScale(new Vector3(1, 1, 0))*
                        Matrix.CreateTranslation(new Vector3(-centering.X, -centering.Y, 0));
        }
    }
}