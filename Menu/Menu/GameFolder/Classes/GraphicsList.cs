using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.GameFolder.Classes
{
    public class GraphicsList
    {
        private List<Graphics> objects;
        private Game game;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GraphicsList(Game game)
        {
            this.game = game;
            objects = new List<Graphics>();
        }
        /// <summary>
        /// Universal method to add graphics with colision or without colision
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="colision"></param>
        public void AddGraphics(Vector2 position, Texture2D texture, bool colision = true)
        {
            Graphics graphics = new Graphics(position, texture, colision);
            objects.Add(graphics);
        }
        /// <summary>
        /// Method which add graphics with colisions to list of colisions
        /// </summary>
        /// <returns></returns>
        public List<Rectangle> ColisionList()
        {
            List<Rectangle> colision = new List<Rectangle>();
            foreach (Graphics graphics in objects)
            {
                if (graphics.Colision)
                    colision.Add(new Rectangle((int)graphics.Position.X, (int)graphics.Position.Y, graphics.Texture.Width, graphics.Texture.Height));
            }
            return colision;
        }
        /// <summary>
        /// Method which add data of texture of graphics
        /// </summary>
        /// <returns></returns>
        public List<Color[]> objectsData()
        {
            List<Color[]> objectsData = new List<Color[]>();
            foreach (Graphics graphics in objects)
            {
                if (graphics.Colision)
                {
                    Color[] objectColors = new Color[graphics.Texture.Width*graphics.Texture.Height];
                    graphics.Texture.GetData(objectColors);
                    objectsData.Add(objectColors);
                }
            }
            return objectsData;
        }
        /// <summary>
        /// Method to draw all graphics
        /// </summary>
        public void DrawGraphics()
        {
            foreach (Graphics graphics in objects)
            {
                game.spriteBatch.Draw(graphics.Texture, graphics.Position, Color.White);
            }
        }
    }
}
