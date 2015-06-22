using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Menu.Classes
{
    public class Controls
    {
        public string text;
        public Vector2 position; 
        public Controls(string text,Vector2 position) 
        {
            this.text = text;
            this.position = position;
        }
    }
}
