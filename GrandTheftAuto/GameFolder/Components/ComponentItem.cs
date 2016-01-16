using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentItem : DrawableGameComponent
    {
        private GameClass game;
        private Camera camera;
        private double dropTimer;
        public ComponentItem(GameClass game, Camera camera)
            : base(game)
        {
            this.game = game;
            this.camera = camera;
        }

        public override void Initialize()
        {
            game.itemList.Add(new Item("Lol", "Epic", new Vector2(-100, 0), game.spritDroppedArmour[(int)EWearing.Helm], game.spritStarterArmour[(int)EWearing.Helm], EWearing.Helm, false, new Item.ItemStats(10, EWhichBonus.Vitality), new Item.ItemStats(5, EWhichBonus.Intelect)));
            game.itemList.Add(new Item("Lol", "Epic", new Vector2(-100, 0), game.spritDroppedArmour[(int)EWearing.Helm], game.spritStarterArmour[(int)EWearing.Helm], EWearing.Helm, false, new Item.ItemStats(10, EWhichBonus.Vitality), new Item.ItemStats(5, EWhichBonus.Intelect)));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            IsDropped(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                camera.Transform);
            foreach (Item item in game.itemList)
            {
                game.spriteBatch.Draw(item.TextureBackground, item.Position, Color.White);
            }
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void IsDropped(GameTime gameTime)
        {
            foreach (Item item in game.itemList.Where(s => s.Dropped))
            {
                dropTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (dropTimer >= 5000)
                {
                    item.Dropped = false;
                    dropTimer = 0;
                }
            }
        }
    }
}
