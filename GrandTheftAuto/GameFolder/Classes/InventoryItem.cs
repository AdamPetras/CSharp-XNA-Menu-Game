﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class InventoryItem
    {
        public bool Empty { get; set; }
        public Rectangle Rectangle { get; set; }
        public Item Item { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public EWearing EWearing { get; private set; }

        public InventoryItem(int column, int row, Vector2 position, Texture2D texture)
        {
            Column = column;
            Row = row;
            Position = position;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 64, 64);
            Texture = texture;
            Empty = true;
            EWearing = EWearing.None;
            Item = null;
        }
        public InventoryItem(Vector2 position, Texture2D texture, EWearing eWearing)
        {
            Position = position;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 64, 64);
            Texture = texture;
            Empty = true;
            Item = null;
            EWearing = eWearing;
        }
    }
}