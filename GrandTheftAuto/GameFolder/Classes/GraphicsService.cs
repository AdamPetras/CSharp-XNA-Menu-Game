using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class GraphicsService
    {
        private List<Graphics> objectList;
        private GameClass game;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GraphicsService(GameClass game)
        {
            this.game = game;
            objectList = new List<Graphics>();
        }

        /// <summary>
        /// Universal method to add graphics with Colision or without Colision
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="angle"></param>
        /// <param name="colision"></param>
        /// <param name="description"></param>
        public void AddGraphics(Vector2 position, Texture2D texture, float angle = 0f, bool colision = false, string description = "")
        {
            objectList.Add(new Graphics(position, texture, colision, angle));
        }
        public void AddGraphics(Vector2 position, Texture2D texture, string description, SpriteFont spriteFont, float angle = 0f, bool colision = false)
        {
            objectList.Add(new Graphics(position, texture, colision, angle, description, spriteFont));
        }
        /// <summary>
        /// Method which add graphics with colisions to list of colisions
        /// </summary>
        /// <returns></returns>
        public List<Rectangle> ColisionList()
        {
            return objectList.Where(graphics => graphics.Colision).Select(graphics => graphics.Rectangle).ToList();
        }

        /// <summary>
        /// Method which add data of texture of graphics
        /// </summary>
        /// <returns></returns>
        public List<Color[]> ObjectDataList()
        {
            List<Color[]> objectsData = new List<Color[]>();
            foreach (Graphics graphics in objectList.Where(graphics => graphics.Colision))
            {
                Color[] objectColors = new Color[graphics.Texture.Width * graphics.Texture.Height];
                graphics.Texture.GetData(objectColors);
                objectsData.Add(objectColors);
            }
            return objectsData;
        }
        /// <summary>
        /// Method to draw Coliadable graphics
        /// </summary>
        public void DrawColiadableGraphics()
        {
            foreach (Graphics graphics in objectList.Where(graphics => graphics.Colision))
            {
                game.spriteBatch.Draw(graphics.Texture, new Vector2(graphics.Position.X + graphics.Texture.Width / 2, graphics.Position.Y + graphics.Texture.Height / 2), null, Color.White, graphics.Angle, new Vector2(graphics.Texture.Width / 2, graphics.Texture.Height / 2), 1f, SpriteEffects.None, 0f);
                if (graphics.Description.Length != 0)
                {
                    DrawDescription(graphics.Description, graphics.SpriteFont, graphics.Rectangle.Center.ToVector2());
                }
            }
        }

        private void DrawDescription(string text, SpriteFont spriteFont, Vector2 position)
        {
            Vector2 origin = spriteFont.MeasureString(text) / 2;
            game.spriteBatch.DrawString(spriteFont, text, position - origin, Color.White);
        }

        /// <summary>
        /// Method to draw non-Coliadable graphics
        /// </summary>
        public void DrawNonColiadableGraphics()
        {
            foreach (Graphics graphics in objectList.Where(graphics => !graphics.Colision))
            {
                game.spriteBatch.Draw(graphics.Texture, new Vector2(graphics.Position.X + graphics.Texture.Width / 2, graphics.Position.Y + graphics.Texture.Height / 2), null, Color.White, graphics.Angle, new Vector2(graphics.Texture.Width / 2, graphics.Texture.Height / 2), 1f, SpriteEffects.None, 0f);
                if (graphics.Description != "")
                {
                    DrawDescription(graphics.Description, graphics.SpriteFont, graphics.Rectangle.Center.ToVector2());
                }
            }
        }
    }
}
