using System;
using Menu.GameFolder.Components;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class Camera
    {
        public Matrix transform;
        public Vector2 centering;
        public void Update(Vector2 objPosition)
        {
            centering = new Vector2(objPosition.X - Game.width / 2, objPosition.Y - Game.height / 2);
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                        Matrix.CreateTranslation(new Vector3(-centering.X, -centering.Y, 0));
        }
    }
}