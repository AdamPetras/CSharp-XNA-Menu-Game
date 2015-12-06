using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class QuestMaster : Statistics
    {
        public string Name { get; private set; }
        public Rectangle TalkRectangle { get; private set; }
        public List<Quest> QuestList { get; set; }

        public QuestMaster(string name,int hp, Vector2 position, Texture2D texture, float angle, float speed,Camera camera)
        {
            Name = name;
            Hp = hp;
            Position = new Vector2(position.X-camera.Centering.X,position.Y-camera.Centering.Y);
            Texture = texture;
            Angle = angle;
            Speed = speed;
            Alive = true;
            Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            TalkRectangle = new Rectangle((int)position.X - 15, (int)position.Y - 15, texture.Width + 15, texture.Height + 15);
            QuestList = new List<Quest>();
        }
    }
}
