using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.GameFolder.Classes
{
    public class GraphicsList
    {
        private List<Graphics> objectList;
        private Game game;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GraphicsList(Game game)
        {
            this.game = game;
            objectList = new List<Graphics>();
        }
        /// <summary>
        /// Universal method to add graphics with colision or without colision
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="colision"></param>
        public void AddGraphics(Vector2 position, Texture2D texture,float angle=0f, bool colision = true)
        {
            Graphics graphics = new Graphics(position, texture, colision,angle);
            objectList.Add(graphics);
        }
        /// <summary>
        /// Method which add graphics with colisions to list of colisions
        /// </summary>
        /// <returns></returns>
        public List<Rectangle> ColisionList()
        {
            List<Rectangle> colision = new List<Rectangle>();
            foreach (Graphics graphics in objectList)
            {
                if (graphics.Colision)
                    colision.Add(new Rectangle((int)graphics.Position.X, (int)graphics.Position.Y, graphics.Texture.Width, graphics.Texture.Height));
            }
            return colision;
        }

        public List<Graphics> ObjectTextureList()
        {   
            List<Graphics> objectTextureList = new List<Graphics>();
            foreach (Graphics graphics in objectList)
            {
                if (graphics.Colision)
                    objectTextureList.Add(graphics);
            }
            return objectTextureList;
        }
        /// <summary>
        /// Method which add data of texture of graphics
        /// </summary>
        /// <returns></returns>
        public List<Color[]> ObjectDataList()
        {
            List<Color[]> objectsData = new List<Color[]>();
            foreach (Graphics graphics in objectList)
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
        public void DrawColiadableGraphics()
        {
            foreach (Graphics graphics in objectList)
            {
                if (graphics.Colision)
                {
                    game.spriteBatch.Draw(graphics.Texture,
                        new Rectangle((int) graphics.Position.X + graphics.Texture.Width/2,
                            (int) graphics.Position.Y + graphics.Texture.Height/2, graphics.Texture.Width,
                            graphics.Texture.Height), null, Color.White, graphics.Angle,
                        new Vector2(graphics.Texture.Width/2, graphics.Texture.Height/2), SpriteEffects.None, 0f);
                    game.spriteBatch.Draw(game.spritPauseBackground, ColisionList()[0], Color.White*0);
                }
            }
        }
        public void DrawNonColiadableGraphics()
        {
            foreach (Graphics graphics in objectList)
            {
                if (!graphics.Colision)
                {
                    game.spriteBatch.Draw(graphics.Texture,
                        new Rectangle((int)graphics.Position.X + graphics.Texture.Width / 2,
                            (int)graphics.Position.Y + graphics.Texture.Height / 2, graphics.Texture.Width,
                            graphics.Texture.Height), null, Color.White, graphics.Angle,
                        new Vector2(graphics.Texture.Width / 2, graphics.Texture.Height / 2), SpriteEffects.None, 0f);
                    game.spriteBatch.Draw(game.spritPauseBackground, ColisionList()[0], Color.White*0);
                }
            }
        }
    }
}
