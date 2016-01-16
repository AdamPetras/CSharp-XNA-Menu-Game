using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentDialogs: DrawableGameComponent
    {
        private GameClass game;
        private Camera camera;
        public DialogService dialogService;
        public ComponentDialogs(GameClass game)
            : base(game)
        {
            this.game = game;
            dialogService = new DialogService(game);
        }
        public ComponentDialogs(GameClass game,Camera camera) : base(game)
        {
            this.game = game;
            this.camera = camera;
            dialogService = new DialogService(game);
            dialogService.AddBackgroundDialog(new Vector2(0, 0),game.spritDialogBubble, game.smallFont,Color.White, "Dialog", 10);
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
                if (camera != null)
                    game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                        camera.Transform);
                else
                    game.spriteBatch.Begin();
            DrawOrder = 10;
                dialogService.DrawDialog();
                game.spriteBatch.End();
                base.Draw(gameTime);
        }
    }
}
