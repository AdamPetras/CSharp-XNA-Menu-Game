using GrandTheftAuto.GameFolder.Components;
using GrandTheftAuto.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.MenuFolder.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentGameMenu : DrawableGameComponent
    {
        private IMenu menuItems;
        private GameClass game;
        private ComponentAbout componentAbout;
        private ComponentControls componentControls;
        private ComponentSettings componentSettings;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public ComponentGameMenu(GameClass game)
            : base(game)
        {
            this.game = game;
            menuItems = new MenuItems(game, new Vector2(100, game.graphics.PreferredBackBufferHeight / 3), game.bigFont);
            //p�id�v�n� polo�ek do menu
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
            if (game.SingleClick(Keys.Enter) || (game.SingleClickMouse() && menuItems.CursorColision()))
            {
                //D�lej n�co p�i zm��knut� enter na ur�it�m m�st�
                switch (menuItems.Selected.Text)
                {
                    case "Start":
                        SavedData savedData = new SavedData(new Vector2(100, 100), 0f,Vector2.Zero,0f,true,null,null,1000);
                        game.gunsOptions.GetGunsList().Clear();        // vy�i�t�n� listu se zbran�mi
                        game.gunsOptions.AddGun(new Vector2(200, 200), 1);
                        game.gunsOptions.AddGun(new Vector2(200, 100), 1);
                        game.gunsOptions.AddGun(new Vector2(200, 100), 2);
                        game.gunsOptions.AddGun(new Vector2(300, 200), 2);
                        game.gunsOptions.AddGun(new Vector2(100, 200), 2);
                        game.gunsOptions.AddGun(new Vector2(400, 200), 2);
                        game.gunsOptions.AddGun(new Vector2(300, 500), 4);
                        ComponentCar componentCar = new ComponentCar(game, savedData);   // vytvo�en� nov� komponenty auta
                        Game.Components.Add(componentCar);
                        game.ComponentEnable(this, false);
                        Game.IsMouseVisible = false;
                        break;
                    case "Load":
                        game.EGameState = EGameState.Load;
                        ComponentSaveLoad componentLoad = new ComponentSaveLoad(game);
                        Game.Components.Add(componentLoad);
                        game.ComponentEnable(this, false);
                        break;
                    case "Settings":
                        componentSettings = new ComponentSettings(game);
                        Game.Components.Add(componentSettings);
                        game.ComponentEnable(this, false);
                        break;
                    case "Controls":
                        componentControls = new ComponentControls(game);
                        Game.Components.Add(componentControls);
                        game.ComponentEnable(this, false);
                        break;
                    case "About":
                        if (!componentAbout.Visible)        // o�et�en� pro v�cen�sobn� spu�t�n�
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
            if (menuItems.Selected.Text != "About")
            {
                game.ComponentEnable(componentAbout, false);
            }
            menuItems.CursorPosition();
            base.Update(gameTime);
        }
        /// <summary>
        /// Show the scene
        /// </summary>
        public virtual void Show()
        {
            Visible = true;
            Enabled = true;
        }

        /// <summary>
        /// Hide the scene
        /// </summary>
        public virtual void Hide()
        {
            Visible = false;
            Enabled = false;
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

