using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Menu.MenuFolder.Classes
{

    public class SettingValues
    {
        private Game game;
        private List<DisplayMode> resolution;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public SettingValues(Game game)
        {
            this.game = game;
            resolution = new List<DisplayMode>();
            foreach (DisplayMode mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                if(mode.Width>=1024)
                resolution.Add(mode);
            }
        }
        /// <summary>
        /// Method to get if it is fullscreen or windowed
        /// </summary>
        /// <returns></returns>
        public string IsFullScreen()
        {
            return game.graphics.IsFullScreen ? "FullScreen" : "Windowed";
        }

        public string GetResolution(int index)
        {
            return game.graphics.PreferredBackBufferWidth + " x " + game.graphics.PreferredBackBufferHeight;
        }

        public List<DisplayMode> GetResolutionList()
        {
            return resolution;
        }
    }
}
