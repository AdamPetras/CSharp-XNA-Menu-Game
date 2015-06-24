using Menu.Components;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class Camera
    {
        public Matrix transform;
        public Vector2 centering;
        private Movement movement;
        public Camera(Movement movement)
        {
            this.movement = movement;
        }

        public void Update(TheGame theGame)
        {
            centering = new Vector2(movement.position.X-Game.width/2, movement.position.Y-Game.height/2);
            transform = Matrix.CreateScale(new Vector3(1, 1, 0))*
                        Matrix.CreateTranslation(new Vector3(-centering.X, -centering.Y, 0));
        }
    }
}