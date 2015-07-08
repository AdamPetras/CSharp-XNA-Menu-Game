using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Menu.MenuFolder.Classes
{

    public class SettingValues
    {
        private Game game;
        public SettingValues()
        {

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
