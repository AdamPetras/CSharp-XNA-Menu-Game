using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class Holster
    {
        private GunsList gunsList;
        private Character character;
        private List<Gun> holsterList;
        public Holster(GunsList gunsList,Character character)
        {
            this.gunsList = gunsList;
            this.character = character;
            holsterList = new List<Gun>();
        }

        public void PickUpGun()
        {
            for (int i=0; i<gunsList.GetGunsList().Count;i++)
            {
                if (character.CharacterRectangle().Intersects(gunsList.GetGunsList()[i].Rectangle))
                {
                    holsterList.Add(gunsList.GetGunsList()[i]);
                    gunsList.GetGunsList().Remove(gunsList.GetGunsList()[i]);
                    /* else
                    {                       NEDOKONČENÉ!!!
                        gunsList.AddAmmo(gun);
                    }*/
                }
            }

        }

        public List<Gun> GetHolster()
        {
            return holsterList;
        }
    }
}
