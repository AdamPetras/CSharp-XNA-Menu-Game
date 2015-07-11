using Menu.GameFolder.Components;
using Menu.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Menu.MenuFolder.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentGameMenu : DrawableGameComponent
    {
        private MenuItems menuItems;
        private Game game;
        private ComponentAbout about;
        private ComponentControls controls;
        private ComponentGame theGame;
        private ComponentSettings settings;
        public ComponentGameMenu(Game game)
            : base(game)
        {
            this.game = game;
            menuItems = new MenuItems(game);
            //pøidávání položek do menu
            menuItems.AddItem("Start");
            menuItems.AddItem("Settings");
            menuItems.AddItem("Controls");
            menuItems.AddItem("About");
            menuItems.AddItem("End");
            menuItems.Next();
        }
        public override void Initialize()
        {
            controls = new ComponentControls(game);
            about = new ComponentAbout(game);

            Game.Components.Add(about);
            Game.Components.Add(controls);

            StartUp(controls);
            StartUp(about);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.SingleClick(Keys.Up))
            {
                menuItems.Before();
            }
            if (game.SingleClick(Keys.Down))
            {
                menuItems.Next();
            }
            if (game.SingleClick(Keys.Enter))
            {
                //Dìlej nìco pøi zmáèknutí enter na urèitém místì
                switch (menuItems.selected.Text)
                {
                    case "Start":
                        theGame = new ComponentGame(game);
                        Game.Components.Add(theGame);
                        SetComponents(this, false);
                        break;
                    case "Settings":
                        settings = new ComponentSettings(game, this);
                        Game.Components.Add(settings);
                        SetComponents(this, false);
                        break;
                    case "Controls":
                        SetComponents(controls,true);
                        break;
                    case "About":
                        SetComponents(about, true);
                        break;
                    case "End":
                        game.Exit();
                        break;
                }
            }
            if (game.SingleClick(Keys.Down) || game.SingleClick(Keys.Up))
            {
                SetComponents(controls,false);
                SetComponents(about, false);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.spritMenuBackground, Vector2.Zero, Color.White); //vykreslení backgroundu pro menu
            menuItems.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void SetComponents(GameComponent component, bool active)
        {
            component.Enabled = active;
            if (component is DrawableGameComponent)
                ((DrawableGameComponent)component).Visible = active;
        }
        private void StartUp(GameComponent component)
        {
            component.Enabled = false;
            if (component is DrawableGameComponent)
                ((DrawableGameComponent)component).Visible = false;
        }
    }
}
