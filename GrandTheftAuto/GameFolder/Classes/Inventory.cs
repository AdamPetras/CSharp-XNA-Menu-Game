using System;
using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.MenuFolder;
using GrandTheftAuto.MenuFolder.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class Inventory
    {
        private GameClass game;
        private int sizerow,sizecolumn;
        private Vector2 grabVector;
        public List<InventoryItem> InventoryItems { get; private set; }

        public Inventory(GameClass game, int sizerow,int sizecolumn)
        {
            this.game = game;
            this.sizerow = sizerow;
            this.sizecolumn = sizecolumn;
            InventoryItems = new List<InventoryItem>();
        }

        public void AddSlot(int column, int row, Texture2D texture)
        {
            InventoryItems.Add(new InventoryItem(column, row, SetPosition(column, row), texture));
        }

        public void PickUpItem(Rectangle rectangle,List<Item> itemList)
        {
            if (itemList.Any(s => s.Rectangle.Intersects(rectangle)))
            {
                
                InventoryItem invItem = InventoryItems.Last(s => s.Empty);
                Item item = itemList.Find(s => s.Rectangle.Intersects(rectangle));
                invItem.Item = item;
                invItem.Empty = false;
                invItem.Texture = item.TextureInventory;
                invItem.Item.InTheInventory = true;
                itemList.Remove(item);
            }
        }

        private Vector2 SetPosition(int column, int row)
        {
            return new Vector2(OriginVector(column, row).X + row * 60, OriginVector(column, row).Y + column * 60);
        }

        public void DrawInventory()
        {
            foreach (InventoryItem inventoryItem in InventoryItems)
            {
                if (inventoryItem.Empty)
                    game.spriteBatch.Draw(inventoryItem.Texture, new Rectangle(inventoryItem.Rectangle.X, inventoryItem.Rectangle.Y, inventoryItem.Rectangle.Width, inventoryItem.Rectangle.Height), Color.White * 0.3f);
                else if (!inventoryItem.Empty)
                {
                    game.spriteBatch.Draw(inventoryItem.Texture, new Rectangle(inventoryItem.Rectangle.X, inventoryItem.Rectangle.Y, inventoryItem.Rectangle.Width, inventoryItem.Rectangle.Height), Color.White);
                }
            }
        }

        public void MovingWithItem()
        {
            foreach (InventoryItem inventoryItem in InventoryItems.Where(s => !s.Empty))
            {
                //pokud uchopí
                if ((inventoryItem.Rectangle.Contains(game.mouseState.Position) &&
                    game.mouseState.LeftButton == ButtonState.Pressed &&
                    game.mouseState.LeftButton != ButtonState.Released) || inventoryItem.Grab && game.mouseState.LeftButton != ButtonState.Released)
                {
                    Vector2 mousePosition = game.mouseState.Position.ToVector2();
                    if (inventoryItem.Grab == false)
                        grabVector = mousePosition - inventoryItem.Position; //vector na vyrovnání kde chytnu item tam mi zůstane uchycen
                    inventoryItem.Position = mousePosition - grabVector;    //uchycení - ta pozice toho okna
                    inventoryItem.Rectangle = new Rectangle((int)inventoryItem.Position.X, (int)inventoryItem.Position.Y, 50, 50); //update rectanglu
                    inventoryItem.Grab = true;

                }
                    //pokud vloží někde do inventáře
                else if (game.mouseState.LeftButton == ButtonState.Released && InventoryItems.Any(s => s.Rectangle.Intersects(inventoryItem.Rectangle) && s.Empty))
                {
                    inventoryItem.Grab = false;
                    InventoryItem item = InventoryItems.First(s => s.Rectangle.Intersects(inventoryItem.Rectangle) && s.Empty);
                    int row = item.Row, column = item.Column;                   
                    item.Column = inventoryItem.Column;
                    item.Row = inventoryItem.Row;
                    item.Position = SetPosition(item.Column, item.Row);
                    item.Rectangle = new Rectangle((int) item.Position.X, (int) item.Position.Y, 50, 50);
                    inventoryItem.Row = row;
                    inventoryItem.Column = column;
                    inventoryItem.Position = SetPosition(column, row);
                    inventoryItem.Rectangle = new Rectangle((int) inventoryItem.Position.X,(int) inventoryItem.Position.Y,50,50);
                }
                    //pokud dá jinde než do inventáře
                if (inventoryItem.Grab && InventoryItems.Any(s => !s.Rectangle.Intersects(inventoryItem.Rectangle)))
                {
                    if (game.mouseState.LeftButton == ButtonState.Released)
                    {
                        inventoryItem.Grab = false;
                        inventoryItem.Position = SetPosition(inventoryItem.Column, inventoryItem.Row);
                        inventoryItem.Rectangle = new Rectangle((int) inventoryItem.Position.X,
                            (int) inventoryItem.Position.Y, 40, 40);
                        Drop(inventoryItem);
                    }
                }
            }
        }

        public void DrawItemInfo()
        {
            foreach (InventoryItem inventoryItem in InventoryItems.Where(s => !s.Empty && !s.Grab))
            {
                if (inventoryItem.Rectangle.Contains(game.mouseState.Position))
                {
                    if(inventoryItem.Item.EItemType == EItemType.Stats)
                        game.spriteBatch.DrawString(game.smallestFont, inventoryItem.Item.Name + "\n" + inventoryItem.Item.Description + "\n" + inventoryItem.Item.StatString + ":" + inventoryItem.Item.Stats, new Vector2(inventoryItem.Rectangle.Right, inventoryItem.Rectangle.Top), Color.White);
                    else if (inventoryItem.Item.EItemType == EItemType.WithoutStats)
                    {
                        game.spriteBatch.DrawString(game.smallestFont, inventoryItem.Item.Name + "\n" + inventoryItem.Item.Description, new Vector2(inventoryItem.Rectangle.Right, inventoryItem.Rectangle.Top), Color.White);
                    }
                }
            }
        }

        private void Drop(InventoryItem inventoryItem)
        {
            game.itemList.Add(new Item(inventoryItem.Item,new Vector2(100,100),true));
            inventoryItem.Item = null;
            inventoryItem.Texture = game.spritPauseBackground;
            inventoryItem.Empty = true;
        }

        public Vector2 OriginVector(int column, int row)
        {
            return new Vector2(game.graphics.PreferredBackBufferWidth - (row + sizecolumn * 60), game.graphics.PreferredBackBufferHeight - (column + sizerow * 60));
        }
    }
}
