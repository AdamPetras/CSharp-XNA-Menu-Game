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
        WithoutStats
    }

    public class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string StatString { get; private set; }
        public int Stats { get; private set; }
        public Texture2D TextureInventory { get; private set; }
        public Texture2D TextureBackground { get; set; }
        public bool InTheInventory { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle { get; set; }
        public EItemType EItemType { get; private set; }
        public bool Dropped { get; set; }

        public Item(Item item,Vector2 position,bool dropped)
        {
            Name = item.Name;
            Description = item.Description;
            StatString = item.StatString;
            Stats = item.Stats;
            TextureInventory = item.TextureInventory;
            TextureBackground = item.TextureBackground;
            InTheInventory = false;
            Dropped = dropped;
            Position = position;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, TextureBackground.Width, TextureBackground.Height);
            EItemType = item.EItemType;
        }

        public Item(string name,string description,string statString,int stats,Vector2 position,Texture2D textureBackground,Texture2D textureInventory)
        {
            Name = name;
            Description = description;
            StatString = statString;
            Stats = stats;
            InTheInventory = false;
            Dropped = false;
            Position = position;
            TextureInventory = textureInventory;
            TextureBackground = textureBackground;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, TextureBackground.Width, TextureBackground.Height);
            EItemType = EItemType.Stats;
        }
        public Item(string name, string description, Vector2 position, Texture2D textureBackground, Texture2D textureInventory)
        {
            Name = name;
            Description = description;
            InTheInventory = false;
            Dropped = false;
            Position = position;
            TextureInventory = textureInventory;
            TextureBackground = textureBackground;
            Rectangle = new Rectangle((int) Position.X,(int) Position.Y,TextureBackground.Width,TextureBackground.Height);
            EItemType = EItemType.WithoutStats;
        }
    }
}
