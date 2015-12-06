using System.Collections.Generic;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class DialogService
    {
        private GameClass game;
        private Dialog dialog;

        public DialogService(GameClass game)
        {
            this.game = game;
        }

        public void AddBackgroundDialog(Vector2 position, Texture2D background, SpriteFont spriteFont,Color color, string text, int border)
        {
            dialog = new Dialog(position, background, spriteFont, text, border,color);
        }
        public void AddDialog(Vector2 position, SpriteFont spriteFont,Color color, string text, int border)
        {
            dialog = new Dialog(position, spriteFont, text, border,color);
        }

        public void DrawDialog()
        {
            game.spriteBatch.GraphicsDevice.ScissorRectangle = dialog.Rectangle;
            if (dialog.BackGround != null)
                game.spriteBatch.Draw(dialog.BackGround, dialog.Position, Color.White);
            game.spriteBatch.DrawString(dialog.SpriteFont,dialog.Text,dialog.Rectangle.Location.ToVector2(),dialog.TextColor);
        }
    }
}
