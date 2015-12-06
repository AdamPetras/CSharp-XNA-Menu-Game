using GrandTheftAuto.GameFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.MenuFolder.Interface
{
    public interface IDraw
    {
        void Draw();
        void AddItem(string text, Vector2 position, SpriteFont font,bool centerText = true, string value = "",float rotation = 0f, int spaceBeforeValue = 0, bool nonClick = false,Camera camera = null);
    }
}
