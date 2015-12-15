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
        public string LineFormat(string text, int lineLength)
        {
            lineLength = 25; //počet písmen na řádku
            for (int i = 1; i < (text.Length / lineLength); i++)
            {
                text = text.Insert(i * lineLength, "\n");
            }
            return text;
        }

        public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = spriteFont.MeasureString(" ").X;
            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);
                if (word.Contains("\n"))    //pokud je manuálně odřádkováno
                    lineWidth = 0;
                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }
    }
}
