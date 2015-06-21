using Microsoft.Xna.Framework;

namespace Menu
{
    public class Menu
    {
        public string text;
        public Vector2 position;

        public Menu(string text,Vector2 position)
        {
            this.text = text;
            this.position = position;
        }
    }
}
