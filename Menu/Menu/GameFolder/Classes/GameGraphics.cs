using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class GameGraphics
    {
        public GraphicsList graphicsList { get; private set; }   
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GameGraphics(Game game)
        {
            graphicsList = new GraphicsList(game);
            graphicsList.AddGraphics(new Vector2(0, 500), game.spritTree);
        }
        /// <summary>
        /// Method to detect simple(rectangle) and pixel cosilision
        /// </summary>
        public bool Colision(Rectangle colisionRectangle)
        {
            for (int i = 0; i < graphicsList.ColisionList().Count; i++)
            {
                if (graphicsList.ColisionList()[i].Intersects(colisionRectangle))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Method to draw objects
        /// </summary>
        public void DrawGraphics()
        {
            graphicsList.DrawGraphics();
        }
    }
}