using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public enum ENpc
    {
        Quester,
        Guard
    }

    //NPC = None-playable character
    public class NPC : Stats
    {
        public ENpc ENpc { get; private set; }
        public Rectangle TalkRectangle { get; private set; }
        public NPC(ENpc eNpc, int hp, Vector2 position, Texture2D texture, float angle, float speed)
        {
            ENpc = eNpc;
            Hp = hp;
            Position = position;
            Texture = texture;
            Angle = angle;
            Speed = speed;
            Alive = true;
            Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            TalkRectangle = new Rectangle((int)position.X - 15, (int)position.Y - 15, texture.Width + 15, texture.Height + 15);
        }
    }
}
