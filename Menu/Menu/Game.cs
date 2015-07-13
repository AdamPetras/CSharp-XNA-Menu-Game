using Menu.MenuFolder.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public KeyboardState keyState;
        public KeyboardState keyStateBefore;
        public ComponentGameMenu componentGameMenu;

        #region Textury
        public Texture2D spritMenuBackground;
        public Texture2D spritAbout;
        public Texture2D spritGameBackground;
        public Texture2D spritCar;
        public Texture2D spritPauseBackground;
        #endregion
        #region Fonty
        public SpriteFont bigFont;
        public SpriteFont normalFont;
        #endregion

        public static int width = 1600, height = 900;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            #region DefiniceOkna
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
            #endregion
            #region NaèteníFontù
            bigFont = Content.Load<SpriteFont>(@"Font");
            normalFont = Content.Load<SpriteFont>(@"NormalFont");
            #endregion
            //********************Inicializace komponenty pro menu*************************
            componentGameMenu = new ComponentGameMenu(this);
            //-----------------------------------------------------------------------------
            Components.Add(componentGameMenu);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            #region Naètení spritù
            spritMenuBackground = Content.Load<Texture2D>(@"Sprits/backGround");
            spritAbout = Content.Load<Texture2D>(@"Sprits/About");
            spritGameBackground = Content.Load<Texture2D>(@"Sprits/grass");
            spritCar = Content.Load<Texture2D>(@"Sprits/car");
            spritPauseBackground = Content.Load<Texture2D>(@"Sprits/pauseBackground");
            #endregion
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            keyStateBefore = keyState;
            keyState = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public bool SingleClick(Keys key)
        {
            return keyState.IsKeyDown(key) && keyStateBefore.IsKeyUp(key);
        }
        public void ComponentEnable(GameComponent component,bool active)
        {
            component.Enabled = active;
            if (component is DrawableGameComponent)
                ((DrawableGameComponent)component).Visible = active;
        }
    }
}