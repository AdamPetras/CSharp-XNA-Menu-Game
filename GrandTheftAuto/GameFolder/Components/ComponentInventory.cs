using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentInventory : DrawableGameComponent
    {
        private GameClass game;
        private Inventory inventory;
        private Character character;
        private bool run;
        private const int SIZEROW = 5;
        private const int SIZECOLUMN = 5;
        public ComponentInventory(GameClass game,Character character)
            : base(game)
        {
            this.game = game;
            this.character = character;
            inventory = new Inventory(game,SIZEROW,SIZECOLUMN);
            run = false;
        }

        public override void Initialize()
        {
            for (int i = 0; i < SIZEROW; i++)
                for (int j = 0; j < SIZECOLUMN; j++)
                    inventory.AddSlot(i, j, game.spritPauseBackground);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(run) //pokud je spuštěn inventář
            inventory.MovingWithItem();
            inventory.PickUpItem(character.Rectangle,game.itemList);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            DrawOrder = 9;
            if (game.SingleClick(Keys.L,false) && run)
            {
                run = false;
            }
            else if (game.SingleClick(Keys.L) || run)
            {
                inventory.DrawInventory();
                inventory.DrawItemInfo();
                run = true;
            }
            game.spriteBatch.Draw(game.cursor, game.mouseState.Position.ToVector2(), Color.White);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
