using System;
using System.Linq;
using System.Threading;
using Menu.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Menu.MenuFolder.Components
{
    public class ComponentControls : DrawableGameComponent
    {
        private IMenu controlItems;
        private Game game;
        private Thread t1;
        private SavedControls savedControls;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public ComponentControls(Game game)
            : base(game)
        {
            this.game = game;
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
        public override void Initialize()
        {
            controlItems = new MenuItems(game, new Vector2(100, game.graphics.PreferredBackBufferHeight / 3),game.normalFont);
            for (int i = 0; i < game.controlsList.Count; i++)
                controlItems.AddItem(game.controlsList[i].Text, game.controlsList[i].Key.ToString());
            controlItems.AddItem("Back");
            controlItems.Next();
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
                controlItems.Before();
            }
            if (game.SingleClick(Keys.Down) || game.SingleClick(Keys.S))
            {
                controlItems.Next();
            }
            if (game.SingleClick(Keys.Enter) || (game.SingleClickMouse() && controlItems.CursorColision()))
            {
                //Dìlej nìco pøi zmáèknutí enter na urèitém místì
                switch (controlItems.Selected.Text)
                {
                    case "throttle":
                        ClickedOn("throttle", 0);
                        t1 = new Thread(delegate() { Loap("throttle", 0); });
                        t1.Start();
                        break;
                    case "brake":
                        ClickedOn("brake", 1);
                        t1 = new Thread(delegate() { Loap("brake", 1); });
                        t1.Start();
                        break;
                    case "turn left":
                        ClickedOn("turn left", 2);
                        t1 = new Thread(delegate() { Loap("turn left", 2); });
                        t1.Start();
                        break;
                    case "turn right":
                        ClickedOn("turn right", 3);
                        t1 = new Thread(delegate() { Loap("turn right", 3); });
                        t1.Start();
                        break;
                    case "handbrake":
                        ClickedOn("handbrake", 4);
                        t1 = new Thread(delegate() { Loap("handbrake", 4); });
                        t1.Start();
                        break;
                    case "enter/leave car":
                        ClickedOn("enter/leave car", 5);
                        t1 = new Thread(delegate() { Loap("enter/leave car", 5); });
                        t1.Start();
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
        private void Loap(string text, int index)       // smyèka pro naètìní klávesy
        {
            while (t1.IsAlive)
            {
                if (game.keyState.GetPressedKeys().Length != 0)
                {
                    try
                    {
                        Keys[] keys = game.keyState.GetPressedKeys();
                        if (keys.GetValue(0).ToString() != "Enter")     // pokud neni zmáèknutý Enter
                        {
                            if (!game.controlsList.Any(s => s.Key.Equals(keys.GetValue(0))))        // pokud již neni zaznamenán daný bind (vyhledá z controlslistu jestli neobsahuje již daný bind)
                            {
                                savedControls = new SavedControls(text, (Keys)keys.GetValue(0));
                                game.controlsList.Insert(index, savedControls);
                                game.controlsList.RemoveAt(index + 1);
                                controlItems.UpdateItem(text, index, savedControls.Key.ToString());
                                Enabled = true;
                                t1.Abort();
                            }
                            else
                            {
                                controlItems.UpdateItem(text, index, game.controlsList[index].Key.ToString());
                                Enabled = true;
                                t1.Abort();
                            }
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        controlItems.UpdateItem(text, index, game.controlsList[index].Key.ToString());
                        Enabled = true;
                        t1.Abort();
                    }
                }
            }
        }
        private void ClickedOn(string text, int index)      // if clicked on it will write "Click any key"
        {
            controlItems.UpdateItem(text, index, "Click any key");
            Enabled = false;
        }

    }
}

