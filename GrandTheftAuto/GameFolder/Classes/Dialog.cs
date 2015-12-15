using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Dialog : TextVisualisation
    {
        public Vector2 Position { get; private set; }
        public Texture2D BackGround { get; private set; }
        public Rectangle Rectangle { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public Color TextColor { get; private set; }
        public string Text { get; private set; }
        private int Border { get;set; }

        public Dialog(Vector2 position, Texture2D background, SpriteFont spriteFont, string text, int border, Color textColor = default(Color))
        {
            Position = SetDialogPosition(position, background);
            BackGround = background;
            SpriteFont = spriteFont;
            Border = border;
            TextColor = textColor;
            Rectangle = new Rectangle((int)Position.X + border, (int)Position.Y + border, background.Width - border, background.Height - border);
            Text = WrapText(spriteFont, text, Rectangle.Width);
        }
        public Dialog(Vector2 position, SpriteFont spriteFont, string text, int border, Color textColor = default(Color))
        {
            Position = position;
            SpriteFont = spriteFont;
            Border = border;
            TextColor = textColor;
            Rectangle = new Rectangle((int)Position.X + border, (int)Position.Y + border, (int)SpriteFont.MeasureString(text).X - border, (int)SpriteFont.MeasureString(text).Y - border);
            Text = WrapText(spriteFont, text, Rectangle.Width);
        }

        private Vector2 SetDialogPosition(Vector2 position, Texture2D texture)
        {
            return new Vector2(position.X,position.Y-texture.Height);
        }
    }
}
