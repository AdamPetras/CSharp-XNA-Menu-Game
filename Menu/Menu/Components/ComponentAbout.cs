using Microsoft.Xna.Framework;

namespace Menu
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ComponentAbout : DrawableGameComponent
    {
        private Game game;
        private AboutItems about;
        public ComponentAbout(Game game)
            : base(game)
        {
            this.game = game;
            about = new AboutItems(game);
            about.AddItem("***GAME DESIGNERS***");
            about.AddItem("");
            about.AddItem("Graphics: ");
            about.AddItem("");
            about.AddItem("Programmer: ");
            about.AddItem("Lol");
            about.AddItem("");
        }
        public override void Initialize()
        {           
            
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.spritAbout,new Vector2(800,200), Color.LightBlue*0.3f );
            about.DrawAbout();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
