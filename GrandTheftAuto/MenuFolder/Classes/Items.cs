using Microsoft.Xna.Framework;

namespace GrandTheftAuto.MenuFolder.Classes
{
    public class Items
    {
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public string Value { get; set; }
        public Rectangle Rectangle { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="rectangle"></param>
        /// <param name="value"></param>
        public Items(string text, Vector2 position,Rectangle rectangle = default (Rectangle), string value = "" )
        {
            Text = text;
            Position = position;
            Rectangle = rectangle;
            Value = value;
        }
    }
}