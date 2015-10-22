using System.Collections.Generic;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.Gun;
using GrandTheftAuto.GameFolder.Classes.GunFolder;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.MenuFolder.Classes
{
    public class SavedData
    {
        public Vector2 Position { get; set; }
        public float Angle { get; set; }
        public Vector2 CharacterPosition { get; set; }
        public float CharacterAngle { get; set; }
        public string Time { get; set; }
        public bool InTheCar { get; set; }
        public int CarHp { get; set; }
        public List<Gun> Holster { get; set; }
        public List<Gun> GunsList { get; set; }
        public List<Enemy> EnemyList { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="angle"></param>
        /// <param name="time"></param>
        public SavedData(Vector2 position, float angle, Vector2 characterPosition = default(Vector2), float characterAngle = 0f, bool inTheCar = true, List<Gun> holster = null, List<Gun> gunsList = null, int carHp = 0, List<Enemy> enemyList = null, string time = "Empty")
        {
            Position = position;
            Angle = angle;
            CharacterPosition = characterPosition;
            CharacterAngle = characterAngle;
            InTheCar = inTheCar;
            Time = time;
            Holster = holster;
            GunsList = gunsList;
            EnemyList = enemyList;
            CarHp = carHp;
        }
    }
}
