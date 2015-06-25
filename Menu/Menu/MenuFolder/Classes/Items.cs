using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class Items : IItems
    {
        public string Text { get; set; }
        public Vector2 Position { get; set; }

        public Items(string text, Vector2 position)
        {
            Text = text;
            Position = position;
        }
    }
}
