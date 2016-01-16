using System.Collections.Generic;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu.MenuFolder.Interface
{
    public interface IMenu : IDraw
    {
        Items Selected { get; set; }
        List<Items> Items { get; set; }
        void PositionIfCameraMoving(Vector2 offset);
        void Moving();
        void SetKeysUp(params Keys[] keys);
        void SetKeysDown(params Keys[] keys);
        bool CursorColision();
        void CursorPosition();
        void UpdateItem(string text, int i, Vector2 position, SpriteFont font, bool centerText = true, string value = "", float rotation = 0f, int spaceBeforeValue = 0);
    }
}
