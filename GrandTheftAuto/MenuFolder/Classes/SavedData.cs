using System.Collections.Generic;
using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.GameFolder.Classes.CarFolder;
using GrandTheftAuto.GameFolder.Classes.GunFolder;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.MenuFolder.Classes
{
    public class SavedData
    {
        public List<Car> CarList { get; set; }
        public Vector2 CharacterPosition { get; set; }
        public float CharacterAngle { get; set; }
        public string Time { get; set; }
        public bool InTheCar { get; set; }
        public List<Gun> Holster { get; set; }
        public List<Gun> GunsList { get; set; }
        public List<Enemy> EnemyList { get; set; }
        public int Score { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="characterPosition"></param>
        /// <param name="characterAngle"></param>
        /// <param name="carList"></param>
        /// <param name="inTheCar"></param>
        /// <param name="holster"></param>
        /// <param name="gunsList"></param>
        /// <param name="enemyList"></param>
        /// <param name="score"></param>
        /// <param name="time"></param>
        public SavedData(Vector2 characterPosition, float characterAngle, List<Car>carList = null , bool inTheCar = true, List<Gun> holster = null, List<Gun> gunsList = null, List<Enemy> enemyList = null,int score = 0, string time = "Empty")
        {
            CarList = carList;
            CharacterPosition = characterPosition;
            CharacterAngle = characterAngle;
            InTheCar = inTheCar;
            Time = time;
            Holster = holster;
            GunsList = gunsList;
            EnemyList = enemyList;
            Score = score;
        }
    }
}
