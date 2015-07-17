using System;
using System.Collections.Generic;
using System.Linq;
using Menu.GameFolder.Classes;
using Menu.GameFolder.Components;
using Menu.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Menu.MenuFolder.Components
{


    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentLoad : DrawableGameComponent
    {
        private IMenu load;
        private Game game;
        public ComponentLoad(Game game)
            : base(game)
        {
            this.game = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            load = new SaveLoad(game);
            foreach (Items item in game.saveList)
            {
                load.AddItem(item.Text);
            }
            load.AddItem("Back");
            load.Next();
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (game.SingleClick(Keys.Up) || game.SingleClick(Keys.W))
                load.Before();
            if (game.SingleClick(Keys.Down) || game.SingleClick(Keys.S))
                load.Next();
            if(game.SingleClick(Keys.Enter))
            {
                switch (load.Selected.Text)
                {
                    case "Back":
                        game.ComponentEnable(this, false);
                        game.ComponentEnable(game.componentGameMenu,true);
                        break;
                }
            }        
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            load.Draw();
            game.spriteBatch.End();
        }
    }
}
