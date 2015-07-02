using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Menu.MenuFolder.Classes
{

    public class SettingValues
    {
        private Game game;

        public SettingValues(Game game)
        {
            this.game = game;
        }

        public string IsFullScreen()
        {
            return game.graphics.IsFullScreen ? "FullScreen" : "Windowed";
        }

        public string Resolution()
        {
            return game.graphics.GraphicsDevice.Viewport.Width+"x"+game.graphics.GraphicsDevice.Viewport.Height;
        }

        public void SetResolution()
        {

        }
    }
}
