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
        public CharacterUsingGuns CharacterUsingGuns { get; private set; }
        private readonly GameClass game;
        private readonly GameGraphics gameGraphics;
        private readonly Camera camera;
        public ComponentGuns(GameClass game,GameGraphics gameGraphics,Character character,SavedData savedData,Camera camera):base(game)
        {
            this.game = game;
            this.gameGraphics = gameGraphics;
            this.camera = camera;
            CharacterUsingGuns = new CharacterUsingGuns(game, character, camera, savedData);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.EGameState != EGameState.InGameCar)
            {
                CharacterUsingGuns.PickUpGun();
                CharacterUsingGuns.Reloading(gameTime);
                CharacterUsingGuns.BulletColision(gameGraphics.graphicsList);
                CharacterUsingGuns.SelectGun();
                CharacterUsingGuns.Shooting(gameTime);
                CharacterUsingGuns.BulletFly();
                CharacterUsingGuns.SelectGun();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

                game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                    camera.Transform);
                DrawOrder = 2;
                if (game.EGameState != EGameState.InGameCar)
                CharacterUsingGuns.Draw();
                game.gunsOptions.DrawGuns();    //Vykreslení zbraní na zemi
                game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
