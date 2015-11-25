using System.Linq;
using GrandTheftAuto.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.MenuFolder.Components
{
    public class ComponentControls : DrawableGameComponent
    {
        private IMenu controlItems;
        private GameClass game;
        private SavedControls savedControls;

        public delegate void ChangeKeyHandler(string text, int index);

        public event ChangeKeyHandler EventChangeKey;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public ComponentControls(GameClass game)
            : base(game)
        {
            this.game = game;
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
        public override void Initialize()
        {
            controlItems = new MenuItems(game, new Vector2(100, game.graphics.PreferredBackBufferHeight / 3), game.normalFont);
            for (int i = 0; i < game.controlsList.Count; i++)
                controlItems.AddItem(game.controlsList[i].Text, game.controlsList[i].Key.ToString());
            controlItems.AddItem("Back");
            controlItems.Selected = controlItems.Items.First();
            EventChangeKey += ClickedOn;
            EventChangeKey += GetKey;
            base.Initialize();
        }
        /// <summary>
        /// Updatable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            controlItems.Moving(Keys.W,Keys.S);
            if (game.SingleClick(Keys.Enter) || (game.SingleClickMouse() && controlItems.CursorColision()))
            {
                //Dìlej nìco pøi zmáèknutí enter na urèitém místì

                switch (controlItems.Selected.Text)
                {
                    case "throttle":

                        break;
                    case "brake":
                        OnEventChangeKey("brake", 1);
                        break;
                    case "turn left":

                        break;
                    case "turn right":

                        break;
                    case "handbrake":

                        break;
                    case "enter/leave car":

                        break;
                    case "reload gun":

                        break;
                    case "change gun":

                        break;
                    case "Back":
                        game.ComponentEnable(this, false);
                        game.ComponentEnable(game.componentGameMenu);
                        break;
                }
            }
            controlItems.CursorPosition();
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
            controlItems.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Loap of new Thread to get key
        /// </summary>
        /// <param name="text"></param>
        /// <param name="index"></param>      
        private void ClickedOn(string text, int index)      // if clicked on it will write "Click any key"
        {
            controlItems.UpdateItem(text, index, "Click any key");
        }

        protected virtual void OnEventChangeKey(string text, int index)
        {
            if (EventChangeKey != null) EventChangeKey(text, index);
        }

        private void GetKey(string text, int index)
        {
            while (game.keyState.GetPressedKeys().Length == 0)
            {
            }
            Keys[] keys = game.keyState.GetPressedKeys();
            if (keys.GetValue(0) != (object)Keys.Enter)
            {
                savedControls = new SavedControls(text, (Keys)keys.GetValue(0));
                game.controlsList.Insert(index, savedControls);
                game.controlsList.RemoveAt(index + 1);
                controlItems.UpdateItem(text, index, savedControls.Key.ToString());
                Enabled = true;
            }

        }
    }
}

