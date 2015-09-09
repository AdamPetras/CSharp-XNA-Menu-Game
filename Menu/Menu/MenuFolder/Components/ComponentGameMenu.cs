using Menu.GameFolder.Classes;
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
        private ComponentSettings componentSettings;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public ComponentGameMenu(Game game)
            : base(game)
        {
            this.game = game;
            menuItems = new MenuItems(game,new Vector2(100,game.graphics.PreferredBackBufferHeight/2));
            //pøidávání položek do menu
            menuItems.AddItem("Start");
            menuItems.AddItem("Load");
            menuItems.AddItem("Settings");
            menuItems.AddItem("Controls");
            menuItems.AddItem("About");
            menuItems.AddItem("Exit");
            menuItems.Next();
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
        public override void Initialize()
        {
            game.EGameState = EGameState.Menu;
            componentAbout = new ComponentAbout(game);
            base.Initialize();
        }
        /// <summary>
        /// Updatable method
        /// </summary>
        /// <param name="gameTime"></param>
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
                        SavedData savedData = new SavedData(Vector2.Zero, 0);
                        ComponentCar car = new ComponentCar(game, savedData);
                        Game.Components.Add(car);
                        game.ComponentEnable(this, false);
                        break;
                   case "Load":
                        game.EGameState = EGameState.Load;
                        ComponentSaveLoad componentLoad= new ComponentSaveLoad(game);
                        Game.Components.Add(componentLoad);
                        game.ComponentEnable(this,false);
                        break;
                    case "Settings":
                        componentSettings = new ComponentSettings(game);
                        Game.Components.Add(componentSettings);
                        game.ComponentEnable(this, false);
                        break;
                    case "Controls":
                        componentControls = new ComponentControls(game);
                        Game.Components.Add(componentControls);
                        game.ComponentEnable(this,false);
                        break;
                    case "About":
                        if (!componentAbout.Visible)        // ošetøení pro vícenásobné spuštìní
                        {
                            componentAbout = new ComponentAbout(game);
                            game.Components.Add(componentAbout);
                        }
                        break;
                    case "Exit":
                        game.Exit();
                        break;
                }
            }
            if (game.SingleClick(Keys.Down) || game.SingleClick(Keys.Up) || game.SingleClick(Keys.W)||game.SingleClick(Keys.S))
            {
                game.ComponentEnable(componentAbout, false);
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
            game.spriteBatch.Draw(game.spritMenuBackground, Vector2.Zero, Color.White);
            menuItems.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

