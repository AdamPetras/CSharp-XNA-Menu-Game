using System.Collections.Generic;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Menu.MenuFolder.Interface
{
    public interface IMenu : IDraw
    {
        Items Selected { get; set; }
        List<Items> Items { get; set; }
        void PositionIfCameraMoving(Camera camera, Vector2 defaultposition);
        void Moving(Keys keyUp, Keys keyDown);
        void UpdateItem(string text, int i, string value = "");
        void CursorPosition();
        bool CursorColision();
    }
}
