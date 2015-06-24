using Menu.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Menu
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentGameMenu : DrawableGameComponent
    {
        private MenuItems menuItems;
        private Game game;
        private ComponentAbout about;
        private ComponentControls controls;
        private TheGame theGame;
        public ComponentGameMenu(Game game,ComponentAbout about,ComponentControls controls,TheGame theGame)
            : base(game)
        {
            this.game = game;
            menuItems = new MenuItems(game);
            //pøidávání položek do menu
            menuItems.AddItem("Start");
            menuItems.AddItem("Controls");
            menuItems.AddItem("About");
            menuItems.AddItem("End");
            menuItems.Next();
            this.about = about;
            this.controls = controls;
            this.theGame = theGame;
        }
        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.SingleClick(Keys.Up))
            {
                menuItems.Before();
            }
            if (game.SingleClick(Keys.Down))
            {
                menuItems.Next();
            }
            if (game.SingleClick(Keys.Enter))
            {
                //Dìlej nìco pøi zmáèknutí enter na urèitém místì
                switch (menuItems.menu.text)
                {
                    case "Start":
                        SetComponents(theGame,true);
                        SetComponents(this, false);
                        break;
                    case "Controls":
                        SetComponents(controls,true);
                        break;
                    case "About":
                        SetComponents(about, true);
                        break;
                    case "End":
                        game.Exit();
                        break;
                }
            }
            if (game.SingleClick(Keys.Down) || game.SingleClick(Keys.Up))
            {
                SetComponents(controls,false);
                SetComponents(about, false);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.spritMenuBackground, Vector2.Zero, Color.White); //vykreslení backgroundu pro menu
            menuItems.DrawMenu();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SetComponents(GameComponent component, bool active)
        {
            component.Enabled = active;
            if (component is DrawableGameComponent)
                ((DrawableGameComponent)component).Visible = active;
        }
    }
}
