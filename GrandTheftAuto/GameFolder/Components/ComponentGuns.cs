using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.GunFolder;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentGuns:DrawableGameComponent
    {
        public GunService GunService { get; private set; }
        private readonly GameClass game;
        private readonly GameGraphics gameGraphics;
        private readonly Camera camera;
        public ComponentGuns(GameClass game,GameGraphics gameGraphics,Character character,SavedData savedData,Camera camera):base(game)
        {
            this.game = game;
            this.gameGraphics = gameGraphics;
            this.camera = camera;
            GunService = new GunService(game, character, savedData);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.EGameState != EGameState.InGameCar)
            {
                GunService.PickUpGun();
                GunService.Reloading(gameTime);
                GunService.BulletColision(gameGraphics.graphicsList);
                GunService.SelectGun();
                GunService.Shooting(gameTime);
                GunService.BulletFly();
                GunService.GeneratingGuns(gameTime,gameGraphics.graphicsList.ColisionList());
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

                game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                    camera.Transform);
                DrawOrder = 2;
                if (game.EGameState != EGameState.InGameCar)
                GunService.Draw();
                game.gunsOptions.DrawGuns();    //Vykreslení zbraní na zemi
                game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
