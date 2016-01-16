using System;
using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.CarFolder;
using GrandTheftAuto.GameFolder.Classes.GunFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GrandTheftAuto.MenuFolder.Components;
using Microsoft.Xna.Framework.Audio;

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
        Reloading,
        Inventory,
        GameOver
    }

    public enum EKeys
    {
        Up = 0, //go up
        Down, //go down
        Left, //go left
        Right, //go right
        Space, //handbrake
        E, //enter the car
        R, //reload gun
        Q, //change gun
        Shift, //run
        C, //open character stats
        T, //open talent list
        F, //talk with NPCs
        I //quest info
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

        public SoundEffect[] gunSoundShots;
        public SoundEffect[] gunSoundReloads;
        public SoundEffect menuSound;
        public SoundEffect pickUpItemSound;
        public SoundEffect moveItemSound;
        public SoundEffect questCompleteSound;
        public SoundEffect FXOpenSound;
        public SoundEffect FXCloseSound;

        #region Textury

        public Texture2D spritMenuBackground;
        public Texture2D spritAbout;
        public Texture2D spritCar;
        public Texture2D spritPauseBackground;
        public Texture2D spritTree;
        public Texture2D spritExplosion;
        public Texture2D[] spritCharacter;
        public Texture2D[] spritCharacterWithGun;
        public Texture2D spritRoad;
        public Texture2D spritCrossRoad; //200x200
        public Texture2D spritRoadCurveLeft; //200x200
        public Texture2D spritRoadCurveRight; //200x200
        public Texture2D[] spritHouse; //300x300 //300x300 //500x250
        public Texture2D[] spritGuns;
        public Texture2D[] spritEnemy;
        public Texture2D spritCharacterHealth;
        public Texture2D spritCharacterEnergy;
        public Texture2D spritHealthAndEnergyBar;
        public Texture2D spritEnemyHealthBar;
        public Texture2D spritGameOver;
        public Texture2D spritBlood;
        public Texture2D spritBullet;
        public Texture2D spritAmmo;
        public Texture2D spritExperienceBar;
        public Texture2D spritExperienceCharge;
        public Texture2D[] spritPergamen;
        public Texture2D[] spritTalents;
        public Texture2D spritCompleteQuestion;
        public Texture2D spritActiveQuestion;
        public Texture2D spritInActiveQuestion;
        public Texture2D spritDialogBubble;
        public Texture2D spritGrass;
        public Texture2D spritTCross;
        public Texture2D spritDeadEndRoad;
        public Texture2D[] spritNPCs;
        public Texture2D cursor;
        public Texture2D spritQuestComplete;
        public Texture2D spritSpeakBubble;
        public Texture2D spritOrc;
        public Texture2D[] spritStarterArmour;
        public Texture2D[] spritFighterArmour;
        public Texture2D[] spritBloodStrikerArmour;
        public Texture2D[] spritHeadHunterArmour;
        public Texture2D[] spritAllStarsArmour;
        public Texture2D[] spritDroppedArmour;
        public Texture2D spritInventoryEmpty;
        public Texture2D[] spritWearEmpty;

        #endregion

        #region Fonty

        public SpriteFont bigFont;
        public SpriteFont normalFont;
        public SpriteFont smallFont;
        public SpriteFont smallestFont;

        #endregion

        public static int width = 1600, height = 900;

        public List<SavedData> saveList;
        public List<SavedControls> controlsList;
        public List<Car> carList;
        public List<Item> itemList;

        public GunsOptions gunsOptions;
        private double ClickTimer;

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

            Window.Position = new Point(0, 0);
            Window.Title = "Survive that!";
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();

            #endregion

            #region NaèteníFontù

            bigFont = Content.Load<SpriteFont>(@"Font");
            normalFont = Content.Load<SpriteFont>(@"NormalFont");
            smallFont = Content.Load<SpriteFont>(@"SmallFont");
            smallestFont = Content.Load<SpriteFont>(@"smallestFont");

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
            carList = new List<Car>();
            itemList = new List<Item>();
            controlsList.Add(new SavedControls("throttle", Keys.W));
            controlsList.Add(new SavedControls("brake", Keys.S));
            controlsList.Add(new SavedControls("turn left", Keys.A));
            controlsList.Add(new SavedControls("turn right", Keys.D));
            controlsList.Add(new SavedControls("handbrake", Keys.Space));
            controlsList.Add(new SavedControls("enter/leave car", Keys.E));
            controlsList.Add(new SavedControls("reload gun", Keys.R));
            controlsList.Add(new SavedControls("change gun", Keys.Q));
            controlsList.Add(new SavedControls("run", Keys.LeftShift));
            controlsList.Add(new SavedControls("stats", Keys.C));
            controlsList.Add(new SavedControls("skills", Keys.T));
            controlsList.Add(new SavedControls("talk", Keys.F));
            controlsList.Add(new SavedControls("questinfo", Keys.I));
            spritEnemy = new Texture2D[3];
            spritCharacter = new Texture2D[5];
            spritCharacterWithGun = new Texture2D[5];
            spritHouse = new Texture2D[3];
            spritPergamen = new Texture2D[2];
            spritTalents = new Texture2D[13];
            spritNPCs = new Texture2D[8];
            gunsOptions = new GunsOptions(this);
            base.Initialize();
        }

        /// <summary>
        /// Method to load all contents
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gunSoundShots = LoadSoundEffects(@"Sound/Shot/", "M9","P90","M4A1","Ak47","M4A1","Ak47","PKP","M60");
            gunSoundReloads = LoadSoundEffects(@"Sound/Reload/", "M9Reload", "P90Reload", "M4A1Reload", "Ak47Reload", "M4A1Reload", "Ak47Reload", "PKPReload", "M60Reload");
            menuSound = Content.Load<SoundEffect>(@"Sound/FXSound/Menu");
            pickUpItemSound = Content.Load<SoundEffect>(@"Sound/FXSound/PickUpItem");
            moveItemSound = Content.Load<SoundEffect>(@"Sound/FXSound/MoveItem");
            FXCloseSound = Content.Load<SoundEffect>(@"Sound/FXSound/FXClose");
            FXOpenSound = Content.Load<SoundEffect>(@"Sound/FXSound/FXOpen");
            questCompleteSound = Content.Load<SoundEffect>(@"Sound/FXSound/QuestComplete");

            #region Naètení spritù

            spritGrass = Content.Load<Texture2D>(@"Sprits/grass");
            spritMenuBackground = Content.Load<Texture2D>(@"Sprits/backGround");
            spritAbout = Content.Load<Texture2D>(@"Sprits/About");
            spritCar = Content.Load<Texture2D>(@"Sprits/car");
            spritPauseBackground = Content.Load<Texture2D>(@"Sprits/pauseBackground");
            spritTree = Content.Load<Texture2D>(@"Sprits/tree");
            spritExplosion = Content.Load<Texture2D>(@"Sprits/explosion");
            spritGameOver = Content.Load<Texture2D>(@"Sprits/gameover");

            spritRoad = Content.Load<Texture2D>(@"Sprits/Road/road");
            spritCrossRoad = Content.Load<Texture2D>(@"Sprits/Road/crossroad");
            spritRoadCurveLeft = Content.Load<Texture2D>(@"Sprits/Road/roadcurveleft");
            spritRoadCurveRight = Content.Load<Texture2D>(@"Sprits/Road/roadcurveright");
            spritTCross = Content.Load<Texture2D>(@"Sprits/Road/Tcross");
            spritDeadEndRoad = Content.Load<Texture2D>(@"Sprits/Road/deadEnd");
            spritInventoryEmpty = Content.Load<Texture2D>(@"Sprits/InventoryWear/InventoryEmpty");

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
            spritCharacterWithGun[0] = Content.Load<Texture2D>(@"Sprits/Character/CharacterWithGun/characterStop");
            spritCharacterWithGun[1] = Content.Load<Texture2D>(@"Sprits/Character/CharacterWithGun/characterGoOne");
            spritCharacterWithGun[2] = Content.Load<Texture2D>(@"Sprits/Character/CharacterWithGun/characterGoTwo");
            spritCharacterWithGun[3] = Content.Load<Texture2D>(@"Sprits/Character/CharacterWithGun/characterGoThree");
            spritCharacterWithGun[4] = Content.Load<Texture2D>(@"Sprits/Character/CharacterWithGun/characterGoFour");
            spritExperienceBar = Content.Load<Texture2D>(@"Sprits/Character/ExperienceBar");
            spritExperienceCharge = Content.Load<Texture2D>(@"Sprits/Character/ExperienceCharge");

            #endregion

            spritEnemy[0] = Content.Load<Texture2D>(@"Sprits/Enemy/zombie");
            spritBlood = Content.Load<Texture2D>(@"Sprits/Enemy/Blood");
            spritEnemyHealthBar = Content.Load<Texture2D>(@"Sprits/Enemy/enemyHealthBar");

            #region Zbranì

            spritGuns = LoadTextures(@"Sprits/Guns/", "M9", "P90", "M4A1", "AK47", "SCAR-L", "ACW-R", "PKP", "M60");
            spritBullet = Content.Load<Texture2D>(@"Sprits/Guns/bullet");
            spritAmmo = Content.Load<Texture2D>(@"Sprits/Guns/Ammo");

            #endregion

            spritPergamen[0] = Content.Load<Texture2D>(@"Sprits/TalentsAndStats/pergamen");
            spritPergamen[1] = Content.Load<Texture2D>(@"Sprits/Quest/pergamen2");
            spritHealthAndEnergyBar = Content.Load<Texture2D>(@"Sprits/HealthAndEnergyBar/Bar");
            spritCharacterHealth = Content.Load<Texture2D>(@"Sprits/HealthAndEnergyBar/Health");
            spritCharacterEnergy = Content.Load<Texture2D>(@"Sprits/HealthAndEnergyBar/Energy");
            spritCompleteQuestion = Content.Load<Texture2D>(@"Sprits/Quest/completeQuest");
            spritInActiveQuestion = Content.Load<Texture2D>(@"Sprits/Quest/inActiveQuest");
            spritActiveQuestion = Content.Load<Texture2D>(@"Sprits/Quest/ActiveQuest");
            spritDialogBubble = Content.Load<Texture2D>(@"Sprits/Dialog/dialogBubble");
            spritSpeakBubble = Content.Load<Texture2D>(@"Sprits/Quest/speakbubble");
            spritQuestComplete = Content.Load<Texture2D>(@"Sprits/Quest/questcomplete");
            LoadTextures(spritTalents, @"Sprits/TalentsAndStats/talent", 13);
            LoadTextures(spritNPCs, @"Sprits/Character/NPCs/Npc", 8);
            cursor = Content.Load<Texture2D>(@"Sprits/cursor");
            spritOrc = Content.Load<Texture2D>(@"Sprits/InventoryWear/orc");
            spritStarterArmour = LoadTextures(@"Sprits/Item/Starter/", "Helm", "Chest", "Legs", "Boots", "Gloves",
                "Shoulders", "Neck", "Ring");
            spritDroppedArmour = LoadTextures(@"Sprits/Item/Dropped/", "Helm", "Chest", "Legs", "Boots", "Gloves",
                "Shoulders", "Neck", "Ring");
            spritFighterArmour = LoadTextures(@"Sprits/Item/Fighter/", "Helm", "Chest", "Legs", "Boots", "Gloves",
                "Shoulders", "Neck", "Ring");
            spritBloodStrikerArmour = LoadTextures(@"Sprits/Item/Bloodstriker/", "Helm", "Chest", "Legs", "Boots",
                "Gloves", "Shoulders", "Neck", "Ring");
            spritHeadHunterArmour = LoadTextures(@"Sprits/Item/HeadHunter/", "Helm", "Chest", "Legs", "Boots", "Gloves",
                "Shoulders", "Neck", "Ring");
            spritAllStarsArmour = LoadTextures(@"Sprits/Item/AllStars/", "Helm", "Chest", "Legs", "Boots", "Gloves",
                "Shoulders", "Neck", "Ring");
            spritWearEmpty = LoadTextures(@"Sprits/InventoryWear/", "HelmEmpty", "ChestEmpty", "LegsEmpty", "BootsEmpty",
                "GlovesEmpty", "ShouldersEmpty", "NeckEmpty", "RingEmpty");
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
            ClickTimer += gameTime.ElapsedGameTime.Milliseconds;
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
            spriteBatch.Begin();

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Method to check flick of key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flickering"></param>
        /// <returns></returns>
        public bool SingleClick(Keys key, bool flickering = true)
        {
            if (keyState.IsKeyDown(key) && keyStateBefore.IsKeyUp(key))
            {
                if (ClickTimer > 0 && flickering)
                {
                    ClickTimer = 0;
                    return true;
                }
                if (!flickering)
                {
                    return true;
                }
            }
            return false;
        }

        public bool SingleClickLeftMouse()
        {
            if (mouseState.LeftButton == ButtonState.Pressed && mouseStateBefore.LeftButton == ButtonState.Released &&
                ClickTimer > 0)
            {
                ClickTimer = 0;
                return true;
            }
            return false;
        }

        public bool SingleClickRightMouse()
        {
            if (mouseState.RightButton == ButtonState.Pressed && mouseStateBefore.RightButton == ButtonState.Released &&
                ClickTimer > 100)
            {
                ClickTimer = 0;
                return true;
            }
            return false;
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

        public Vector2 CalculatePosition(Vector2 position, float angle, ref double distance)
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

        public void LoadTextures(Texture2D[] texture, string trace, int howManyTextures)
        {
            for (int i = 0; i <= howManyTextures - 1; i++)
                texture[i] = Content.Load<Texture2D>(trace + i);
        }

        public Texture2D[] LoadTextures(string trace, params string[] names)
        {
            Texture2D[] texture = new Texture2D[names.Count()];
            for (int i = 0; i <= names.Count() - 1; i++)
                texture[i] = Content.Load<Texture2D>(trace + names[i]);
            return texture;
        }
        public SoundEffect[] LoadSoundEffects(string trace, params string[] names)
        {
            SoundEffect[] soundEffects = new SoundEffect[names.Count()];
            for (int i = 0; i <= names.Count() - 1; i++)
                soundEffects[i] = Content.Load<SoundEffect>(trace + names[i]);
            return soundEffects;
        }
    }
}