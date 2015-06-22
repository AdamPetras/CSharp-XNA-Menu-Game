using Microsoft.Xna.Framework;

namespace Menu
{
    public class About
    {
        public string text;
        public Vector2 position;

        public About(string text,Vector2 position)
        {
            this.text = text;
            this.position = position;
        }
    }
}
