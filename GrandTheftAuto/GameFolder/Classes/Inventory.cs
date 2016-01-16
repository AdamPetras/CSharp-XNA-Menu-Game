using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Inventory
    {
        private GameClass game;
        private int sizerow, sizecolumn;
        private Vector2 grabVector;
        private BonusOption bonusOption;
        private Character character;
        private List<InventoryItem> InventoryItems;

        public delegate void AddRemoveItemCharacter(Item item);

        public event AddRemoveItemCharacter EventAddRemoveItem;

        public Inventory(GameClass game, int sizerow, int sizecolumn, BonusOption bonusOption, Character character)
        {
            this.game = game;
            this.sizerow = sizerow;
            this.sizecolumn = sizecolumn;
            InventoryItems = new List<InventoryItem>();
            this.bonusOption = bonusOption;
            this.character = character;
            EventAddRemoveItem += AddStats;
        }

        public void AddSlot(int column, int row, Texture2D texture)
        {
            InventoryItems.Add(new InventoryItem(column, row, SetPosition(column, row), texture));
        }
        public void AddSlot(Vector2 position, Texture2D texture, EWearing eWearing)
        {
            InventoryItems.Add(new InventoryItem(position, texture, eWearing));
        }

        public void PickUpItem(Rectangle rectangle, List<Item> itemList)
        {
            if (itemList.Any(s => s.Rectangle.Intersects(rectangle) && !s.Dropped))
            {
                game.pickUpItemSound.Play();
                InventoryItem invItem = InventoryItems.First(s => s.Item == null && s.Empty);
                Item item = itemList.Find(s => s.Rectangle.Intersects(rectangle));
                invItem.Item = item;
                item.InventoryPosition = SetPosition(invItem.Column, invItem.Row);
                item.InventoryRectangle = new Rectangle((int)invItem.Item.InventoryPosition.X, (int)invItem.Item.InventoryPosition.Y, 64, 64);
                item.InventoryItem = invItem;
                invItem.Empty = false;
                invItem.Item.EItemState = EItemState.Inventory;
                itemList.Remove(item);
            }
        }

        private Vector2 SetPosition(int column, int row)
        {
            return new Vector2(OriginVector(column, row).X + row * 74, OriginVector(column, row).Y + column * 74);
        }

        public void DrawEmptyInventory()
        {
            foreach (InventoryItem inventoryItem in InventoryItems)
            {
                game.spriteBatch.Draw(inventoryItem.Texture, new Rectangle(inventoryItem.Rectangle.X, inventoryItem.Rectangle.Y, inventoryItem.Rectangle.Width, inventoryItem.Rectangle.Height), Color.White * 0.8f);

            }
        }

        public void DrawFullInventory()
        {
            foreach (InventoryItem inventoryItem in InventoryItems.Where(s => s.Item != null))
            {
                game.spriteBatch.Draw(inventoryItem.Item.TextureInventory, new Rectangle(inventoryItem.Item.InventoryRectangle.X, inventoryItem.Item.InventoryRectangle.Y, inventoryItem.Item.InventoryRectangle.Width, inventoryItem.Item.InventoryRectangle.Height), Color.White);
            }
        }

        public void MovingWithItem()
        {
            foreach (Item item in InventoryItems.Where(s => s.Item != null).Select(s => s.Item))
            {
                //pokud uchopí
                if (((item.InventoryRectangle.Contains(game.mouseState.Position) &&
                     game.mouseState.LeftButton == ButtonState.Pressed &&
                     game.mouseState.LeftButton != ButtonState.Released) ||
                    item.Grab && game.mouseState.LeftButton != ButtonState.Released))
                {
                    Vector2 mousePosition = game.mouseState.Position.ToVector2();
                    Item item1 = item;
                    if (InventoryItems.Where(s => s.Item != null && s.Item.Grab).ToList().Count == 1)
                    //přetahování přez itemy
                    {
                        item1 = InventoryItems.First(s => s.Item != null && s.Item.Grab).Item;
                    }
                    if (item.Grab == false) //podmínka pro provedení jen jednou
                        grabVector = mousePosition - item.InventoryPosition;    //vector na vyrovnání kde chytnu item tam mi zůstane uchycen     
                    item1.InventoryPosition = mousePosition - grabVector; //uchycení - ta pozice toho okna
                    item1.InventoryRectangle = new Rectangle((int)item.InventoryPosition.X, (int)item.InventoryPosition.Y, 64, 64); //update rectanglu 
                    item1.Grab = true;
                    item1.InventoryItem.Empty = true;
                    item1.EItemState = EItemState.Removed;                
                    OnEventAddRemoveItem(item); //pokud chytnu item ==> removnu
                }
                //pokud dá kde je item
                else if (game.mouseState.LeftButton == ButtonState.Released && item.Grab && InventoryItems.Any(s => s.Rectangle.Intersects(item.InventoryRectangle) && !s.Empty))
                {
                    item.Grab = false;
                    UpdateItemPosition(item);
                }
                //pokud vloží někde do inventáře nebo na charakter
                else if (game.mouseState.LeftButton == ButtonState.Released &&
                         InventoryItems.Any(s => s.Rectangle.Intersects(item.InventoryRectangle) && s.Empty))
                {
                    item.Grab = false;
                    InventoryItem inventoryItem = null;
                    //pokud dá tam kde patří v charakteru
                    if (InventoryItems.Any(s => s.Rectangle.Intersects(item.InventoryRectangle) && s.Empty && s.EWearing == item.EWearing && s.EWearing != EWearing.None))
                    {
                        game.moveItemSound.Play();
                        inventoryItem = InventoryItems.Find(s => s.Rectangle.Intersects(item.InventoryRectangle) && s.Empty && s.EWearing == item.EWearing && s.EWearing != EWearing.None);
                        item.EItemState = EItemState.OnCharacter;
                        OnEventAddRemoveItem(item);
                    }//pokud dá někde do inventáře
                    else if (InventoryItems.Any(s => s.Rectangle.Intersects(item.InventoryRectangle) && s.Empty && s.EWearing == EWearing.None))
                    {
                        game.moveItemSound.Play();
                        inventoryItem = InventoryItems.Find(s => s.Rectangle.Intersects(item.InventoryRectangle) && s.Empty && s.EWearing == EWearing.None);
                    }
                    UpdateItemPosition(item, inventoryItem);
                }
                //pokud vyhodí z charakteru nebo inventáře
                else if (game.mouseState.LeftButton == ButtonState.Released && item.Grab &&
         InventoryItems.Any(s => !s.Rectangle.Intersects(item.InventoryRectangle)))
                {
                    item.Grab = false;
                    DropItem(item);
                }
            }
        }
        //updatuje pozici itemu v inventáři
        public void UpdateItemPosition(Item item, InventoryItem inventoryItem = null)
        {
            if (inventoryItem != null)
            {
                item.InventoryItem = inventoryItem;
            }
            item.InventoryItem.Empty = false;
            item.InventoryPosition = item.InventoryItem.Position;
            item.InventoryRectangle = new Rectangle((int)item.InventoryItem.Position.X,
                (int)item.InventoryItem.Position.Y, 64, 64);

        }
        //vykreslení infa i itemech, pokud na ně najedu
        public void DrawItemInfo()
        {
            foreach (Item item in InventoryItems.Where(s => s.Item != null).Select(s => s.Item))
            {
                if (item.InventoryRectangle.Contains(game.mouseState.Position))
                {
                    if (item.EItemType == EItemType.Stats)
                    {
                        string stats = item.ItemStatistics.Aggregate("", (current, itemStats) => current + itemStats.WriteStats()); //projetí všech itemstatů a přičtení k stringu
                        game.spriteBatch.DrawString(game.smallestFont,
                                item.EWearing + "\n" + item.Name + "\n" + item.Description + "\n" + stats,
                                new Vector2(item.InventoryRectangle.Right, item.InventoryRectangle.Top),
                                Color.White);
                    }
                    else if (item.EItemType == EItemType.WithoutStats)
                    {
                        game.spriteBatch.DrawString(game.smallestFont, item.Name + "\n" + item.Description,
                            new Vector2(item.InventoryRectangle.Right, item.InventoryRectangle.Top), Color.White);
                    }
                }
            }
        }

        private void DropItem(Item item)
        {
            InventoryItem inventoryItem = InventoryItems.Find(s => s.Item == item);
            if (item.EItemType == EItemType.Stats)
                ItemDropped(item);
            else if (item.EItemType == EItemType.WithoutStats)
                game.itemList.Add(new Item(item.Name, item.Description, character.Position, item.TextureBackground, item.TextureInventory, true));
            inventoryItem.Item = null;
        }

        private void ItemDropped(Item item)
        {
            if (item.ItemStatistics.Count == 1)
                game.itemList.Add(new Item(item.Name, item.Description, character.Position, item.TextureBackground, item.TextureInventory, item.EWearing, true, item.ItemStatistics[0]));
            if (item.ItemStatistics.Count == 2)
                game.itemList.Add(new Item(item.Name, item.Description, character.Position, item.TextureBackground, item.TextureInventory, item.EWearing, true, item.ItemStatistics[0], item.ItemStatistics[1]));
            if (item.ItemStatistics.Count == 3)
                game.itemList.Add(new Item(item.Name, item.Description, character.Position, item.TextureBackground, item.TextureInventory, item.EWearing, true, item.ItemStatistics[0], item.ItemStatistics[1], item.ItemStatistics[2]));
            if (item.ItemStatistics.Count == 4)
                game.itemList.Add(new Item(item.Name, item.Description, character.Position, item.TextureBackground, item.TextureInventory, item.EWearing, true, item.ItemStatistics[0], item.ItemStatistics[1], item.ItemStatistics[2], item.ItemStatistics[3]));
            if (item.ItemStatistics.Count == 5)
                game.itemList.Add(new Item(item.Name, item.Description, character.Position, item.TextureBackground, item.TextureInventory, item.EWearing, true, item.ItemStatistics[0], item.ItemStatistics[1], item.ItemStatistics[2], item.ItemStatistics[3], item.ItemStatistics[4]));
        }

        //vyresleni infa o místech v charakteru
        public void DrawOnCharacterInfo()
        {
            foreach (InventoryItem inventoryItem in InventoryItems.Where(s => s.EWearing != EWearing.None))
            {
                game.spriteBatch.DrawString(game.smallestFont, inventoryItem.EWearing.ToString(), new Vector2(inventoryItem.Rectangle.Center.X, inventoryItem.Rectangle.Top - game.smallestFont.MeasureString(inventoryItem.EWearing.ToString()).Y / 2), Color.White, 0, game.smallestFont.MeasureString(inventoryItem.EWearing.ToString()) / 2, 1.5f, SpriteEffects.None, 0f);
            }
        }
        //pokud dám věc do charakteru tak se přičtou staty a pokud oddělám tak se odečtou
        private void AddStats(Item item)
        {
            if (item.EItemState == EItemState.OnCharacter && item.EItemBonus != EItemBonus.Added)
            {
                foreach (Item.ItemStats itemStats in item.ItemStatistics)
                {
                    bonusOption.TypeOfBonus(itemStats.EWhichBonus, itemStats.Stats, character);
                }
                item.EItemBonus = EItemBonus.Added;
            }
            else if (item.EItemState == EItemState.Removed && item.EItemBonus == EItemBonus.Added)
            {
                foreach (Item.ItemStats itemStats in item.ItemStatistics)
                {
                    bonusOption.TypeOfBonus(itemStats.EWhichBonus, -itemStats.Stats, character);
                }
                item.EItemBonus = EItemBonus.Removed;
            }
        }
        public Vector2 OriginVector(int column, int row)
        {
            return new Vector2(game.graphics.PreferredBackBufferWidth - (row + sizecolumn * 74), game.graphics.PreferredBackBufferHeight - (column + sizerow * 74));
        }

        protected virtual void OnEventAddRemoveItem(Item item)
        {
            if (EventAddRemoveItem != null) EventAddRemoveItem(item);
        }
    }
}
