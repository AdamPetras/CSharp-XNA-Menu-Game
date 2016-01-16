using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentInventory : DrawableGameComponent
    {
        private GameClass game;
        private Inventory inventory;
        private Character character;
        private EGameState eGameStateBefore;
        private bool run;
        private const int SIZEROW = 5;
        private const int SIZECOLUMN = 5;
        private ComponentEnemy componentEnemy;
        private ComponentCharacter componentCharacter;
        private ComponentGuns componentGuns;
        private ComponentCar componentCar;
        public ComponentInventory(GameClass game, Character character, BonusOption bonusOption, ComponentEnemy componentEnemy, ComponentCharacter componentCharacter, ComponentGuns componentGuns, ComponentCar componentCar)
            : base(game)
        {
            this.game = game;
            this.character = character;
            this.componentCharacter = componentCharacter;
            this.componentEnemy = componentEnemy;
            this.componentGuns = componentGuns;
            this.componentCar = componentCar;
            inventory = new Inventory(game, SIZEROW, SIZECOLUMN, bonusOption, character);
            run = false;
        }

        public override void Initialize()
        {
            for (int i = 0; i < SIZEROW; i++)
                for (int j = 0; j < SIZECOLUMN; j++)
                    inventory.AddSlot(i, j, game.spritInventoryEmpty);
            Vector2 origin = new Vector2(game.graphics.PreferredBackBufferWidth / 2, game.graphics.PreferredBackBufferHeight / 2);
            int width = game.graphics.PreferredBackBufferWidth / 6;
            inventory.AddSlot(new Vector2(origin.X - width, origin.Y - 200), game.spritWearEmpty[(int)EWearing.Helm], EWearing.Helm);
            inventory.AddSlot(new Vector2(origin.X - width, origin.Y - 120), game.spritWearEmpty[(int)EWearing.Neck], EWearing.Neck);
            inventory.AddSlot(new Vector2(origin.X - width, origin.Y - 40), game.spritWearEmpty[(int)EWearing.Chest], EWearing.Chest);
            inventory.AddSlot(new Vector2(origin.X - width, origin.Y + 40), game.spritWearEmpty[(int)EWearing.Shoulders], EWearing.Shoulders);
            inventory.AddSlot(new Vector2(origin.X + width, origin.Y - 200), game.spritWearEmpty[(int)EWearing.Legs], EWearing.Legs);
            inventory.AddSlot(new Vector2(origin.X + width, origin.Y - 120), game.spritWearEmpty[(int)EWearing.Boots], EWearing.Boots);
            inventory.AddSlot(new Vector2(origin.X + width, origin.Y - 40), game.spritWearEmpty[(int)EWearing.Glove], EWearing.Glove);
            inventory.AddSlot(new Vector2(origin.X + width, origin.Y + 40), game.spritWearEmpty[(int)EWearing.Ring], EWearing.Ring);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            if (run) //pokud je spuštěn inventář
            {
                inventory.MovingWithItem();
                if (game.SingleClick(Keys.Escape))      //Pauza
                {
                    game.EGameState = EGameState.Pause;
                    Enabled = false;
                }
            }
            inventory.PickUpItem(character.Rectangle, game.itemList);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            DrawOrder = 9;
            if (game.SingleClick(Keys.L, false) && run)
            {
                game.FXCloseSound.Play();    //zvukový effekt
                EnableorDisableComponent(true); //disabnutí componenty
                run = false;       //nastavení pomocné proměnně na false
                game.EGameState = eGameStateBefore; //nastavení EGameState
            }
            else if ((game.SingleClick(Keys.L) || run) && game.EGameState != EGameState.GameOver)
            {

                if (game.EGameState != EGameState.Inventory)
                {
                    eGameStateBefore = game.EGameState;
                    game.FXOpenSound.Play();    //zvukový effekt
                }
                game.spriteBatch.Draw(game.spritPauseBackground, Vector2.Zero, Color.White * 0.6f);     //zatmavení pozadí
                EnableorDisableComponent(false);
                game.EGameState = EGameState.Inventory;
                inventory.DrawEmptyInventory(); //vykreslení prázdného invu
                inventory.DrawFullInventory();  //vykreslení plného invu
                inventory.DrawItemInfo();       //vykreslení pokud je myš na nějakém itemu
                game.spriteBatch.Draw(game.spritOrc, new Rectangle(game.graphics.PreferredBackBufferWidth / 2, game.graphics.PreferredBackBufferHeight / 2, game.spritOrc.Width / 2, game.spritOrc.Height / 2), null, Color.White, 0f, new Vector2(game.spritOrc.Width / 2, game.spritOrc.Height / 2), SpriteEffects.None, 0f);
                inventory.DrawOnCharacterInfo();    //vykreslení informací o charakteru
                run = true;     //nastavení pomocné proměnně na true
                game.spriteBatch.Draw(game.cursor, game.mouseState.Position.ToVector2(), Color.White);      //vykreslení myši
            }
            game.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void EnableorDisableComponent(bool state)
        {
            componentEnemy.Enabled = state;
            if (eGameStateBefore == EGameState.InGameCar)
                componentCar.Enabled = state;
            else if (eGameStateBefore == EGameState.InGameOut)
                componentCharacter.Enabled = state;
            componentGuns.Enabled = state;
        }
    }
}
