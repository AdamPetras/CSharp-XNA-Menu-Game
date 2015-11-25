using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class GameGraphics
    {
        public GraphicsList graphicsList { get; private set; }
        private const float Devadesat = 1.57f;
        private const float Stoosmdesat = 3.14f;
        private const float Dvestesedmdesat = 4.71f;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GameGraphics(GameClass game)
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
            graphicsList.AddGraphics(new Vector2(-300, 1200), game.spritRoadCurveRight, Dvestesedmdesat, false);
            graphicsList.AddGraphics(new Vector2(0, 1500), game.spritRoadCurveLeft, Stoosmdesat, false);
            #endregion
            #region ColisionGraphics
            graphicsList.AddGraphics(new Vector2(300, 900), game.spritHouse[0]);
            graphicsList.AddGraphics(new Vector2(300, 600), game.spritHouse[1]);
            #endregion
        }
        /// <summary>
        /// Method to detect simple(rectangle) Colision
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