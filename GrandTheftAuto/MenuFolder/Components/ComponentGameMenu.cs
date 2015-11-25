using System.Linq;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.CarFolder;
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
            //pøidávání položek do menu
            menuItems.AddItem("Start");
            menuItems.AddItem("Load");
            menuItems.AddItem("Settings");
            menuItems.AddItem("Controls");
            menuItems.AddItem("About");
            menuItems.AddItem("Exit");
            menuItems.Selected = menuItems.Items.First();
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
            menuItems.Moving(Keys.W, Keys.S);
            if (game.SingleClick(Keys.Enter) || (game.SingleClickMouse() && menuItems.CursorColision()))
            {
                //Dìlej nìco pøi zmáèknutí enter na urèitém místì
                switch (menuItems.Selected.Text)
                {
                    case "Start":
                        GameGraphics gameGraphics = new GameGraphics(game);
                        Camera camera = new Camera(game);
                        ComponentGameGraphics componentGameGraphics = new ComponentGameGraphics(game,gameGraphics,camera);
                        game.Components.Add(componentGameGraphics);
                        SavedData savedData = new SavedData(new Vector2(100,100), 0f,game.carList,false);
                        game.gunsOptions.GunList.Clear();        // vyèištìní listu se zbranìmi
                        game.gunsOptions.AddGun(new Vector2(200, 200), 1);
                        game.gunsOptions.AddGun(new Vector2(200, 100), 1);
                        game.gunsOptions.AddGun(new Vector2(200, 100), 2);
                        game.gunsOptions.AddGun(new Vector2(300, 200), 2);
                        game.gunsOptions.AddGun(new Vector2(100, 200), 2);
                        game.gunsOptions.AddGun(new Vector2(400, 200), 2);
                        game.gunsOptions.AddGun(new Vector2(300, 500), 4);
                        ComponentCharacter componentCharacter = new ComponentCharacter(game,savedData,gameGraphics,camera);
                        Game.Components.Add(componentCharacter);
                        game.carList.Add(new Car(game, new Vector2(0, 0), 103000, 1770));
                        game.carList.Add(new Car(game, new Vector2(200, 0), 103000, 1770));
                        ComponentCar componentCar = new ComponentCar(game,camera,componentCharacter);
                        game.Components.Add(componentCar);
                        ComponentGuns componentGuns = new ComponentGuns(game,gameGraphics,componentCharacter.CharacterService.Character,savedData,camera);
                        game.Components.Add(componentGuns);
                        ComponentEnemy componentEnemy = new ComponentEnemy(game, savedData, gameGraphics.graphicsList.ColisionList(), camera,componentCar,componentGuns.GunService, componentCharacter.CharacterService);
                        game.Components.Add(componentEnemy);
                        ComponentQuestSystem componentQuestSystem = new ComponentQuestSystem(game,componentCharacter.CharacterService.Character,camera);
                        game.Components.Add(componentQuestSystem);
                        ComponentGUI componentGui = new ComponentGUI(game,camera,componentCharacter.CharacterService.Character,componentGuns.GunService,componentEnemy.enemyService);
                        game.Components.Add(componentGui);
                        ComponentPause componentPause = new ComponentPause(game,componentCar,componentCharacter,componentEnemy,componentGameGraphics,componentGuns,componentGui);
                        game.Components.Add(componentPause);
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
            game.spriteBatch.Draw(game.spritMenuBackground, new Rectangle(0,0,game.graphics.PreferredBackBufferWidth,game.graphics.PreferredBackBufferHeight), Color.White);
            menuItems.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

