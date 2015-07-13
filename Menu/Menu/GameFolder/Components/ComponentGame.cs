using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Components
{
    public class ComponentGame : DrawableGameComponent
    {
        private ComponentCar componentCar;
        private Game game;
        public ComponentGame(Game game)
            : base(game)
        {
            this.game = game;            
        }

        public override void Initialize()
        {
            componentCar = new ComponentCar(game);
            Game.Components.Add(componentCar);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
         
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }
    }
}