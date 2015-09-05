using Menu.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Menu.MenuFolder.Components
{
    public class ComponentSettings : DrawableGameComponent
    {
        private Game game;
        private SettingValues values;
        private IMenu settings;
        private ComponentGameMenu componentGameMenu;
        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="componentGameMenu"></param>
        public ComponentSettings(Game game,ComponentGameMenu componentGameMenu)
            : base(game)
        {
            this.game = game;
            this.componentGameMenu = componentGameMenu;
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
        public override void Initialize()
        {
            settings = new MenuItems(game,new Vector2(100,Menu.Game.height/2));
            values = new SettingValues(game);
            settings.AddItem("Display mode:",values.IsFullScreen());
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
                    case "Back":
                        game.ComponentEnable(this,false);
                        game.ComponentEnable(componentGameMenu);
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
