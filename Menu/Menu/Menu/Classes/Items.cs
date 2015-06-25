using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Menu.Interface;
using Microsoft.Xna.Framework;

namespace Menu.Classes
{
    public class Items : I_Items
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
