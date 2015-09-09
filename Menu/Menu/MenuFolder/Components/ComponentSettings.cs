using System.Linq;
using System.Runtime.CompilerServices;
using Menu.GameFolder.Classes;
using Menu.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu.MenuFolder.Components
{
    public class ComponentSettings : DrawableGameComponent
    {
        private Game game;
        private SettingValues values;
        private IMenu settings;
        private int index;
        private bool IsResolutionChanged;
        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="componentGameMenu"></param>
        public ComponentSettings(Game game)
            : base(game)
        {
            this.game = game;
            IsResolutionChanged = false;
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
        public override void Initialize()
        {
            settings = new MenuItems(game,new Vector2(100,game.graphics.PreferredBackBufferHeight/2));
            values = new SettingValues(game);
            for (int i = 0; i<values.GetResolutionList().Count;i++)
            {
                if (values.GetResolutionList()[i].Width == game.graphics.PreferredBackBufferWidth &&values.GetResolutionList()[i].Height == game.graphics.PreferredBackBufferHeight)
                {
                    index = i;
                    break;
                }
            }
            settings.AddItem("Display mode:",values.IsFullScreen());
            settings.AddItem("Resolution:",values.GetResolution(index));
            settings.AddItem("Back");
            settings.Next();
            base.Initialize();
        }
        /// <summary>
        /// Updatable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (game.SingleClick(Keys.Up)||game.SingleClick(Keys.W))
            {
                settings.Before();
            }
            if (game.SingleClick(Keys.Down)||game.SingleClick(Keys.S))
            {
                settings.Next();
            }
            if (game.SingleClick(Keys.Enter))
            {
                //Dìlej nìco pøi zmáèknutí enter na urèitém místì
                switch (settings.Selected.Text)
                {
                    case "Display mode:":
                        game.graphics.IsFullScreen = !game.graphics.IsFullScreen;
                        game.graphics.ApplyChanges();
                        settings.UpdateItem("Display mode:",0,values.IsFullScreen());
                        break;
                    case "Resolution:":
                        if (index < values.GetResolutionList().Count - 1)
                            index++;
                        else index = 0;
                        game.graphics.PreferredBackBufferWidth = values.GetResolutionList()[index].Width;
                        game.graphics.PreferredBackBufferHeight = values.GetResolutionList()[index].Height;
                        settings.UpdateItem("Display mode:",0,values.IsFullScreen());
                        settings.UpdateItem("Back", 2);
                        settings.UpdateItem("Resolution:",1,values.GetResolution(index));
                        game.graphics.ApplyChanges();
                        //index = index < values.GetResolutionList().Count ? index++ : index = 0;   Nefunguje...
                        game.componentGameMenu = new ComponentGameMenu(game);
                        IsResolutionChanged = true;
                        break;
                    case "Back":
                        game.ComponentEnable(this,false);
                        if(!IsResolutionChanged)
                        game.ComponentEnable(game.componentGameMenu);
                        else 
                            game.Components.Add(game.componentGameMenu);
                        break;
                }
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// Drawable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.spritMenuBackground, Vector2.Zero, Color.White); //vykreslení backgroundu pro settings
            settings.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
