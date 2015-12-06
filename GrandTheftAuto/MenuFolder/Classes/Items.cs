using GrandTheftAuto.GameFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.MenuFolder.Classes
{
    public class Items
    {
        public string Text { get; set; }
        public Vector2 DefaultPosition { get; private set; }
        public Vector2 ActualPosition { get; set; }
        public Vector2 StringOrigin { get; set; }
        public int SpaceBeforeValue { get; set; }
        public string Value { get; set; }
        public Rectangle Rectangle { get; set; }
        public bool NonClick { get; set; }
        public SpriteFont Font { get; set; }
        public float Rotation { get; set; }
        public Camera Camera { get; set; }
        public Vector2 StringLength { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="actualPosition"></param>
        /// <param name="spaceBeforeValue"></param>
        /// <param name="rectangle"></param>
        /// <param name="value"></param>
        /// <param name="nonClick"></param>
        public Items(string text, Vector2 actualPosition, SpriteFont font, float rotation, bool centerText = true, Rectangle rectangle = default (Rectangle), string value = "", bool nonClick = false, int spaceBeforeValue = 0, Camera camera = null)
        {
            Text = text;
            ActualPosition = actualPosition;
            DefaultPosition = actualPosition;
            Font = font;
            if (centerText)
                StringOrigin = Font.MeasureString(Text) / 2;
            SpaceBeforeValue = spaceBeforeValue;
            Rectangle = rectangle;
            Rotation = rotation;
            Value = value + Space();
            StringLength = font.MeasureString(text+value);
            NonClick = nonClick;
            Camera = camera;
        }

        public string Space()
        {
            string space = "";
            for (int i = Text.Length; i < 15; i++)
            {
                space += " ";
            }
            return space;
        }
    }
}