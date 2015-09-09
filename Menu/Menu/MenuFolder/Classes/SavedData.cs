using System;
using Microsoft.Xna.Framework;

namespace Menu.MenuFolder.Classes
{
    public class SavedData
    {
        public Vector2 Position { get; set; }
        public float Angle { get; set; }
        public Vector2 CharacterPosition { get; set; }
        public float CharacterAngle { get; set; }
        public string Time { get; set; }
        public bool InTheCar { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="angle"></param>
        /// <param name="time"></param>
        public SavedData(Vector2 position, float angle,Vector2 characterPosition=default(Vector2),float characterAngle=0f, bool inTheCar = true, string time = "Empty")
        {
            Position = position;
            Angle = angle;
            CharacterPosition = characterPosition;
            CharacterAngle = characterAngle;
            InTheCar = inTheCar;
            Time = time;
        }
    }
}
