using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.GameFolder.Classes.Gun;
using GrandTheftAuto.MenuFolder.Classes;

namespace GrandTheftAuto.GameFolder.Classes.GunFolder
{
    public class Holster
    {
        private GunsOptions gunsOptions;
        private Character character;
        private List<GunFolder.Gun> holsterList;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gunsList"></param>
        /// <param name="character"></param>
        /// <param name="savedData"></param>
        public Holster(GunsOptions gunsOptions,Character character,SavedData savedData)
        {
            this.gunsOptions = gunsOptions;
            this.character = character;
            holsterList = new List<GunFolder.Gun>();
            if (savedData.Holster != null)
                holsterList = savedData.Holster;
        }
        /// <summary>
        /// Method if character colide with gun
        /// </summary>
        /// <param name="selectedGun"></param>
        public void PickUpGun(ref GunFolder.Gun selectedGun)
        {
            for (int i=0; i<gunsOptions.GetGunsList().Count;i++)
            {
                if (character.CharacterRectangle().Intersects(gunsOptions.GetGunsList()[i].Rectangle))
                {
                    if (holsterList.Any(s=> s.EGun.Equals(gunsOptions.GetGunsList()[i].EGun))) //pokud holster obsahuje zbraň
                    {
                        int index = holsterList.FindIndex(s => s.EGun.Equals(gunsOptions.GetGunsList()[i].EGun));  // najde index v holstru podle sebrané zbraně
                        AddAmmo(gunsOptions.GetGunsList()[i], holsterList[index]);
                        holsterList.Insert(index, gunsOptions.GetGunsList()[i]);
                        holsterList.RemoveAt(index + 1);
                        gunsOptions.GetGunsList().Remove(gunsOptions.GetGunsList()[i]);
                        //Aktualizace držící zbraně
                        if (selectedGun !=null)
                        if (selectedGun.EGun.Equals(holsterList[index].EGun))
                        {
                            selectedGun = holsterList[index];
                        }
                    }
                    else if(!holsterList.Contains(gunsOptions.GetGunsList()[i]))   //pokud holster ještě neobsahuje zbraň
                    {
                        holsterList.Add(gunsOptions.GetGunsList()[i]);
                        gunsOptions.GetGunsList().Remove(gunsOptions.GetGunsList()[i]);
                    }
                }
            }
        }
        /// <summary>
        /// Method to reload gun
        /// </summary>
        /// <param name="selectedGun"></param>
        public void Reload(GunFolder.Gun selectedGun)
        {
            if (selectedGun != null)
            {
                if (selectedGun.Magazine != selectedGun.MaxMagazine)
                {
                    int diference = selectedGun.MaxMagazine - selectedGun.Magazine;
                    selectedGun.Ammo -= diference;
                    selectedGun.Magazine += diference;
                    if (selectedGun.Ammo < 0)   //ošetření přetečení
                    {
                        selectedGun.Magazine += selectedGun.Ammo;
                        selectedGun.Ammo = 0;
                    }
                }
            }       
        }
        /// <summary>
        /// Method to add ammo if gun is in the holster
        /// </summary>
        /// <param name="groundGun"></param>
        /// <param name="holsterGun"></param>
        private void AddAmmo(GunFolder.Gun groundGun, GunFolder.Gun holsterGun)
        {
            groundGun.Magazine = holsterGun.Magazine;
            groundGun.Ammo += holsterGun.Ammo;
        }

        /// <summary>
        /// Method to get holster
        /// </summary>
        /// <returns></returns>
        public List<GunFolder.Gun> GetHolster()
        {
            return holsterList;
        }
    }
}
