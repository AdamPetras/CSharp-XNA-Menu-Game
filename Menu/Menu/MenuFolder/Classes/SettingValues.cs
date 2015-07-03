using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Menu.MenuFolder.Classes
{

    public class SettingValues
    {
        public enum EWidth
        {
            Vga = 640,
            Xga = 1024,
            Hd = 1280,
            HdReady = 1600,
            Fullhd = 1920
        }

        public enum EHeight
        {
            Vga = 480,
            Xga = 768,
            Hd = 720,
            HdReady = 900,
            Fullhd = 1080
        }

        private Game game;
        public EWidth eWidth { get; private set; }
        public EHeight eHeight { get; private set; }
        private int i;
        public int width;
        public int height;
        public SettingValues()
        {
            eWidth = EWidth.HdReady;
            eHeight = EHeight.HdReady;
            width = (int)eWidth;
            height = (int) EHeight.HdReady;
            i = 3;
        }

        public SettingValues(Game game)
        {
            this.game = game;
        }

        public string IsFullScreen()
        {
            return game.graphics.IsFullScreen ? "FullScreen" : "Windowed";
        }
    }
}
