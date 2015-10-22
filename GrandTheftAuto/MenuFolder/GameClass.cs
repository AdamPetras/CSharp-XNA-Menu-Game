using System;
using System.Collections.Generic;
using System.Diagnostics;
using GrandTheftAuto.GameFolder.Classes.Gun;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Framework;
using GrandTheftAuto.MenuFolder.Components;
namespace GrandTheftAuto.MenuFolder
{
    public enum EGameState
    {
        Menu,
        Pause,
        Load,
        LoadIngame,
        Save,
        InGameCar,
        InGameOut,
        Reloading
    }
    public enum EKeys
    {
        Up = 0,
        Down,
        Left,
        Right,
        Space,
        E,
        R,
        Q
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameClass : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public KeyboardState keyStateBefore;
        public KeyboardState keyState;
        public MouseState mouseState;
        public MouseState mouseStateBefore;
        public ComponentGameMenu componentGameMenu;

        public EGameState EGameState;

        #region Textury
        public Texture2D spritMenuBackground;
        public Texture2D spritAbout;
        public Texture2D spritGameBackground;
        public Texture2D spritCar;
        public Texture2D spritPauseBackground;
        public Texture2D spritTree;
        public Texture2D spritExplosion;
        public Texture2D[] spritCharacter;
        public Texture2D spritRoad;
        public Texture2D spritCrossRoad;            //200x200
        public Texture2D spritRoadCurveLeft;        //200x200
        public Texture2D spritRoadCurveRight;       //200x200
        public Texture2D[] spritHouse;              //300x300 //300x300 //500x250
        public Texture2D[] spritGuns;
        public Texture2D[] spritEnemy;
        public Texture2D spritBullet;
        #endregion
        #region Fonty
        public SpriteFont bigFont;
        public SpriteFont normalFont;
        public SpriteFont smallFont;
        #endregion

        public static int width = 1600, height = 900;

        public List<SavedData> saveList;
        public List<SavedControls> controlsList;

        public GunsOptions gunsOptions;
        /// <summary>
        /// Constructor
        /// </summary>
        public GameClass()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
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
            smallFont = Content.Load<SpriteFont>(@"SmallFont");
            #endregion
            //********************Inicializace komponenty pro menu*************************
            componentGameMenu = new ComponentGameMenu(this);
            //-----------------------------------------------------------------------------
            Components.Add(componentGameMenu);

            saveList = new List<SavedData>();
            controlsList = new List<SavedControls>();
            for (int i = 1; i <= 5; i++)
            {
                saveList.Add(new SavedData(Vector2.Zero, 0));
            }
            controlsList.Add(new SavedControls("throttle", Keys.W));
            controlsList.Add(new SavedControls("brake", Keys.S));
            controlsList.Add(new SavedControls("turn left", Keys.A));
            controlsList.Add(new SavedControls("turn right", Keys.D));
            controlsList.Add(new SavedControls("handbrake", Keys.Space));
            controlsList.Add(new SavedControls("enter/leave car", Keys.E));
            controlsList.Add(new SavedControls("reload gun", Keys.R));
            controlsList.Add(new SavedControls("change gun", Keys.Q));
            spritEnemy = new Texture2D[3];
            spritCharacter = new Texture2D[5];
            spritHouse = new Texture2D[3];
            spritGuns = new Texture2D[1];
            gunsOptions = new GunsOptions(this);
            IsMouseVisible = true;
            base.Initialize();
        }
        /// <summary>
        /// Method to load all contents
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            #region Naètení spritù
            spritMenuBackground = Content.Load<Texture2D>(@"Sprits/backGround");
            spritAbout = Content.Load<Texture2D>(@"Sprits/About");
            spritGameBackground = Content.Load<Texture2D>(@"Sprits/grass");
            spritCar = Content.Load<Texture2D>(@"Sprits/car");
            spritPauseBackground = Content.Load<Texture2D>(@"Sprits/pauseBackground");
            spritTree = Content.Load<Texture2D>(@"Sprits/tree");
            spritExplosion = Content.Load<Texture2D>(@"Sprits/explosion");

            spritRoad = Content.Load<Texture2D>(@"Sprits/Road/road");
            spritCrossRoad = Content.Load<Texture2D>(@"Sprits/Road/crossroad");
            spritRoadCurveLeft = Content.Load<Texture2D>(@"Sprits/Road/roadcurveleft");
            spritRoadCurveRight = Content.Load<Texture2D>(@"Sprits/Road/roadcurveright");

            spritHouse[0] = Content.Load<Texture2D>(@"Sprits/Houses/house1");
            spritHouse[1] = Content.Load<Texture2D>(@"Sprits/Houses/house2");
            spritHouse[2] = Content.Load<Texture2D>(@"Sprits/Houses/house3");
            #endregion
            #region Sprity Character
            spritCharacter[0] = Content.Load<Texture2D>(@"Sprits/Character/characterStop");
            spritCharacter[1] = Content.Load<Texture2D>(@"Sprits/Character/characterGoOne");
            spritCharacter[2] = Content.Load<Texture2D>(@"Sprits/Character/characterGoTwo");
            spritCharacter[3] = Content.Load<Texture2D>(@"Sprits/Character/characterGoThree");
            spritCharacter[4] = Content.Load<Texture2D>(@"Sprits/Character/characterGoFour");
            #endregion

            spritEnemy[0] = Content.Load<Texture2D>(@"Sprits/Enemy/zombie");
            #region Zbranì
            spritGuns[0] = Content.Load<Texture2D>(@"Sprits/Guns/M4A1");
            spritBullet = Content.Load<Texture2D>(@"Sprits/Guns/bullet");
            #endregion

        }

        protected override void UnloadContent()
        {

        }
        /// <summary>
        /// Updatable method
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            keyStateBefore = keyState;
            mouseStateBefore = mouseState;
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            base.Update(gameTime);
        }
        /// <summary>
        /// Drawable method
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        /// <summary>
        /// Method to check flick of key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool SingleClick(Keys key)
        {
            return keyState.IsKeyDown(key) && keyStateBefore.IsKeyUp(key);
        }
        public bool SingleClickMouse()
        {
            return mouseState.LeftButton == ButtonState.Pressed && mouseStateBefore.LeftButton == ButtonState.Released;
        }
        /// <summary>
        /// Method to Enable/Disable components
        /// </summary>
        /// <param name="component"></param>
        /// <param name="active"></param>
        public void ComponentEnable(GameComponent component, bool active = true)
        {
            component.Enabled = active;
            if (component is DrawableGameComponent)
                ((DrawableGameComponent)component).Visible = active;
        }
        public Vector2 CalculatePosition(Vector2 position, float angle,ref double distance)
        {
            position.X = (float)(Math.Cos(angle) * distance + position.X);
            //X = Cos(uhlu) *ujeta vzdalenost + predchozi pozice
            position.Y = (float)(Math.Sin(angle) * distance + position.Y);
            //Y = Sin(uhlu) *ujeta vzdalenost + predchozi pozice
            distance = 0;
            return position;
        }

        public float Rotation(float angle)
        {
            float rotationAngle = 0;
            rotationAngle += angle;
            const float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;
            return rotationAngle;
        }

        public static float DegreeToRadians(float angle)
        {
            return (float)(Math.PI * angle / 180.0);
        }

        public void SplashDisplay(Color color = default(Color))
        {
            graphics.GraphicsDevice.Clear(color);
        }
    }
}