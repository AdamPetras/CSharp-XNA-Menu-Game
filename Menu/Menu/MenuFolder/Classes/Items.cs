using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class Items
    {
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public string Value { get; set; }

        public Items(string text, Vector2 position, string value = "")
        {
            Text = text;
            Position = position;
            Value = value;
        }
    }
}