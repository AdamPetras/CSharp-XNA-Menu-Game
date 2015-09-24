using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class GameGraphics
    {
        public GraphicsList graphicsList { get; private set; }
        private const float devadesat = 1.57f;
        private const float stoosmdesat = 3.14f;
        private const float dvestesedmdesat = 4.71f;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GameGraphics(Game game)
        {
            graphicsList = new GraphicsList(game);
            //background
            graphicsList.AddGraphics(new Vector2(0, 0), game.spritGameBackground, 0f, false);

            #region Road
            graphicsList.AddGraphics(new Vector2(0, 300), game.spritRoad, 0f, false);
            graphicsList.AddGraphics(new Vector2(0, 600), game.spritRoad, 0f, false);
            graphicsList.AddGraphics(new Vector2(0, 900), game.spritRoad, 0f, false);
            graphicsList.AddGraphics(new Vector2(0, 1200), game.spritCrossRoad, 0f, false);
            graphicsList.AddGraphics(new Vector2(300, 1200), game.spritRoadCurveLeft, 0f, false);
            graphicsList.AddGraphics(new Vector2(-300, 1200), game.spritRoadCurveRight, dvestesedmdesat, false);
            graphicsList.AddGraphics(new Vector2(0, 1500), game.spritRoadCurveLeft, stoosmdesat, false);
            #endregion

            #region ColisionGraphics
            graphicsList.AddGraphics(new Vector2(300, 900), game.spritHouse[0]);
            graphicsList.AddGraphics(new Vector2(300, 600), game.spritHouse[1]);
            #endregion
        }
        /// <summary>
        /// Method to detect simple(rectangle) colision
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
        public void DrawColiadableGraphics()
        {
            graphicsList.DrawColiadableGraphics();
        }

        public void DrawNonColiadableGraphics()
        {
            graphicsList.DrawNonColiadableGraphics();
        }
    }
}