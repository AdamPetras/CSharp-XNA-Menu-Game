using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class Camera
    {
        public Matrix Transform;
        public Vector2 Centering;
        public Game game;

        public Camera(Game game)
        {
            this.game = game;
        }

        /// <summary>
        /// Method to update camera and center to some position
        /// </summary>
        /// <param name="objPosition"></param>
        public void Update(Vector2 objPosition)
        {
            Centering = new Vector2(objPosition.X - game.graphics.PreferredBackBufferWidth/2, objPosition.Y - game.graphics.PreferredBackBufferHeight/2);
            Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                        Matrix.CreateTranslation(new Vector3(-Centering.X, -Centering.Y, 0));
        }
    }
}