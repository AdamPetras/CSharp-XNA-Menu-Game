using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public enum EItemType
    {
        Stats,
        WithoutStats,
        None
    }
    public enum EWearing
    {

        Helm = 0,       //helma
        Chest = 1,      //tělo
        Legs = 2,       //gatě
        Boots = 3,      //boty
        Glove = 4,      //rukavice
        Shoulders = 5,  //ramena
        Neck = 6,       //náhrdelník
        Ring = 7,        //prsten
        None
    }

    public enum EItemState
    {
        None,
        OnCharacter,
        Removed,
        Inventory
    }

    public enum EItemBonus
    {
        None,
        Added,
        Removed
    }
    public class Item
    {
        public class ItemStats
        {
            public EWhichBonus EWhichBonus { get; private set; }
            public int Stats { get; private set; }

            public ItemStats(int stats, EWhichBonus eWhichBonus)
            {
                Stats = stats;
                EWhichBonus = eWhichBonus;
            }

            public string WriteStats()
            {
                return EWhichBonus + ": " + Stats + "\n";
            }
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Texture2D TextureInventory { get; set; }
        public Texture2D TextureBackground { get; set; }
        public bool Grab { get; set; }
        public InventoryItem InventoryItem { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 InventoryPosition { get; set; }
        public Rectangle Rectangle { get; set; }
        public Rectangle InventoryRectangle { get; set; }
        public EItemType EItemType { get; private set; }
        public EWearing EWearing { get; private set; }
        public EItemState EItemState { get; set; }
        public EItemBonus EItemBonus { get; set; }
        public float DropChance { get; set; }
        public bool Dropped { get; set; }
        public List<ItemStats> ItemStatistics { get; private set; }

        #region Drop item without stats
        public Item(string name, string description, float dropChance, Vector2 position, Texture2D textureBackground, Texture2D textureInventory)
        {
            Name = name;
            Description = description;
            EItemState = EItemState.None;
            Position = position;
            DropChance = dropChance;
            TextureInventory = textureInventory;
            TextureBackground = textureBackground;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, TextureBackground.Width, TextureBackground.Height);
            EItemType = EItemType.Stats;
            EItemBonus = EItemBonus.None;
            EWearing = EWearing.None;
            Grab = false;
            InventoryItem = null;
        }
        #endregion
        #region Drop item with stats
        public Item(string name, string description, float dropChance, Vector2 position, Texture2D textureBackground, Texture2D textureInventory, EWearing eWearing, params ItemStats[] itemStats)
        {
            Name = name;
            Description = description;
            ItemStatistics = itemStats.ToList();
            EItemState = EItemState.None;
            Position = position;
            DropChance = dropChance;
            TextureInventory = textureInventory;
            TextureBackground = textureBackground;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, TextureBackground.Width, TextureBackground.Height);
            EItemType = EItemType.Stats;
            EItemBonus = EItemBonus.None;
            Grab = false;
            EWearing = eWearing;
            InventoryItem = null;
        }
        #endregion
        #region Item with stats
        public Item(string name, string description, Vector2 position, Texture2D textureBackground, Texture2D textureInventory, EWearing eWearing, bool dropped = false, params ItemStats[] itemStats)
        {
            Name = name;
            Description = description;
            ItemStatistics = itemStats.ToList();
            EItemState = EItemState.None;
            Position = position;
            TextureInventory = textureInventory;
            TextureBackground = textureBackground;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, TextureBackground.Width, TextureBackground.Height);
            EItemType = EItemType.Stats;
            EItemBonus = EItemBonus.None;
            Grab = false;
            EWearing = eWearing;
            InventoryItem = null;
            Dropped = dropped;
        }
        #endregion
        #region Item without stats
        public Item(string name, string description, Vector2 position, Texture2D textureBackground, Texture2D textureInventory, bool dropped = false)
        {
            Name = name;
            Description = description;
            EItemBonus = EItemBonus.None;
            EItemState = EItemState.None;
            EWearing = EWearing.None;
            Position = position;
            TextureInventory = textureInventory;
            TextureBackground = textureBackground;
            Grab = false;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, TextureBackground.Width, TextureBackground.Height);
            EItemType = EItemType.WithoutStats;
            InventoryItem = null;
            Dropped = dropped;
        }
        #endregion

        public void UpdateRectangle()
        {
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, TextureBackground.Width, TextureBackground.Height);
        }
    }
}
