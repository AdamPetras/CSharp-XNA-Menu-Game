using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public abstract class TextVisualisation
    {
        public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');   //rozdělení textu do pole pomocí splitu, který zjišťuje mezery
            StringBuilder stringBuilder = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = spriteFont.MeasureString(" ").X;
            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);
                if (word.Contains("\n"))    //pokud je manuálně odřádkováno
                    lineWidth = 0;
                if (lineWidth + size.X < maxLineWidth)
                {
                    stringBuilder.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    stringBuilder.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return stringBuilder.ToString();
        }
    }
}
