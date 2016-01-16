using System.Linq;
using GrandTheftAuto.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.MenuFolder.Components
{
    public class ComponentSettings : DrawableGameComponent
    {
        private GameClass game;
        private SettingValues values;
        private IMenu settings;
        private int index;      //pomocná promìnná
        private bool IsResolutionChanged;
        private Vector2 position;
        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="componentGameMenu"></param>
        public ComponentSettings(GameClass game)
            : base(game)
        {
            this.game = game;
            position = new Vector2(game.graphics.PreferredBackBufferWidth / 4, game.graphics.PreferredBackBufferHeight / 2);
            IsResolutionChanged = false;
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
        public override void Initialize()
        {
            settings = new MenuItems(game);
            values = new SettingValues(game);
            for (int i = 0; i<values.GetResolutionList().Count;i++)
            {
                if (values.GetResolutionList()[i].Width == game.graphics.PreferredBackBufferWidth &&values.GetResolutionList()[i].Height == game.graphics.PreferredBackBufferHeight)
                {
                    index = i;
                    break;
                }
            }
            position = new Vector2(game.graphics.PreferredBackBufferWidth/4,game.graphics.PreferredBackBufferHeight/2);
            WriteMenu();
            settings.Selected = settings.Items.First();
            settings.SetKeysDown(Keys.Down, Keys.S);
            settings.SetKeysUp(Keys.W, Keys.Up);
            base.Initialize();
        }
        /// <summary>
        /// Updatable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            settings.Moving();
            if (game.SingleClick(Keys.Enter) || (game.SingleClickLeftMouse() && settings.CursorColision()))
            {
                //Dìlej nìco pøi zmáèknutí enter na urèitém místì
                switch (settings.Selected.Text)
                {
                    case "Display mode:":
                        game.graphics.IsFullScreen = !game.graphics.IsFullScreen;
                        game.graphics.ApplyChanges();
                        position = new Vector2(game.graphics.PreferredBackBufferWidth / 4, game.graphics.PreferredBackBufferHeight / 2);
                        WriteMenu();
                        break;
                    case "Resolution:":
                        //index = index < values.GetResolutionList().Count-1 ? index++ : index = 0;
                        if (index < values.GetResolutionList().Count - 1)
                            index++;
                        else index = 0;
                        IsResolutionChanged = true;
                        game.graphics.PreferredBackBufferWidth = values.GetResolutionList()[index].Width;
                        game.graphics.PreferredBackBufferHeight = values.GetResolutionList()[index].Height;
                        game.graphics.ApplyChanges();
                        game.componentGameMenu = new ComponentGameMenu(game);
                        settings.Selected = settings.Items.Find(s => s.Text=="Resolution:");
                        position = new Vector2(game.graphics.PreferredBackBufferWidth / 4, game.graphics.PreferredBackBufferHeight / 2);
                        WriteMenu();
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
            settings.CursorPosition();
            base.Update(gameTime);
        }
        /// <summary>
        /// Drawable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.spritMenuBackground, new Rectangle(0, 0, game.graphics.PreferredBackBufferWidth, game.graphics.PreferredBackBufferHeight), Color.White);  //vykreslení backgroundu pro settings
            settings.Draw();
            game.spriteBatch.Draw(game.cursor, game.mouseState.Position.ToVector2(), Color.White);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void WriteMenu()
        {
            settings.Items.Clear();
            settings.AddItem("Display mode:", position, game.normalFont, value: values.IsFullScreen());
            settings.AddItem("Resolution:", position, game.normalFont, value: values.GetResolution(index));
            settings.AddItem("Back", position, game.normalFont);
        }
    }
}
