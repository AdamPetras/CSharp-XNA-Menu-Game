﻿using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.MenuFolder.Classes
{
    public class SavedControls
    {
        public string Text { get; set; }
        public Keys Key { get; set; }

        public SavedControls(string text,Keys key)
        {
            Text = text;
            Key = key;
        }
    }
}