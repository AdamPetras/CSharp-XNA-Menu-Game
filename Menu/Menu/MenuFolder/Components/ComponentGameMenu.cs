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
        private IMenu menuItems;
        private Game game;
        private ComponentAbout componentAbout;
        private ComponentControls componentControls;
        private ComponentGame componenTheGame;
        private ComponentSettings componentSettings;

        public ComponentGameMenu(Game game)
            : base(game)
        {
            this.game = game;
            menuItems = new MenuItems(game);
            //pøidávání položek do menu
            menuItems.AddItem("Start");
            menuItems.AddItem("Load");
            menuItems.AddItem("Settings");
            menuItems.AddItem("Controls");
            menuItems.AddItem("About");
            menuItems.AddItem("Exit");
            menuItems.Next();
        }

        public override void Initialize()
        {
            componentControls = new ComponentControls(game);
            componentAbout = new ComponentAbout(game);

            Game.Components.Add(componentAbout);
            Game.Components.Add(componentControls);

            game.ComponentEnable(componentControls, false);
            game.ComponentEnable(componentAbout, false);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.SingleClick(Keys.Up) || game.SingleClick(Keys.W))
            {
                menuItems.Before();
            }
            if (game.SingleClick(Keys.Down) || game.SingleClick(Keys.S))
            {
                menuItems.Next();
            }
            if (game.SingleClick(Keys.Enter))
            {
                //Dìlej nìco pøi zmáèknutí enter na urèitém místì
                switch (menuItems.Selected.Text)
                {
                    case "Start":
                        componenTheGame = new ComponentGame(game);
                        Game.Components.Add(componenTheGame);
                        SetComponents(this, false);
                        break;
                    case "Load":
                        ComponentLoad componentLoad= new ComponentLoad(game);
                        Game.Components.Add(componentLoad);
                        Enabled = false;
                        Visible = false;
                        break;
                    case "Settings":
                        componentSettings = new ComponentSettings(game, this);
                        Game.Components.Add(componentSettings);
                        SetComponents(this, false);
                        break;
                    case "Controls":
                        SetComponents(componentControls, true);
                        break;
                    case "About":
                        SetComponents(componentAbout, true);
                        break;
                    case "Exit":
                        game.Exit();
                        break;
                }
            }
            if (game.SingleClick(Keys.Down) || game.SingleClick(Keys.Up) || game.SingleClick(Keys.W)||game.SingleClick(Keys.S))
            {
                SetComponents(componentControls, false);
                SetComponents(componentAbout, false);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.spritMenuBackground, Vector2.Zero, Color.White);
                //vykreslení backgroundu pro menu
            menuItems.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void SetComponents(GameComponent component, bool active)
        {
            component.Enabled = active;
            if (component is DrawableGameComponent)
                ((DrawableGameComponent) component).Visible = active;
        }

    }
}

