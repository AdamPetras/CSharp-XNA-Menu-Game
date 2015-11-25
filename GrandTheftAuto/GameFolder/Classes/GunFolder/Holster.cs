using System.Collections.Generic;
using System.Linq;
using GrandTheftAuto.MenuFolder.Classes;

namespace GrandTheftAuto.GameFolder.Classes.GunFolder
{
    public class Holster
    {
        public List<Gun> HolsterList { get; set; }
        private GunsOptions gunsOptions;
        private Character character;


        /// <summary>
        /// Constructor of Holster
        /// </summary>
        /// <param name="gunsOptions"></param>
        /// <param name="character"></param>
        /// <param name="savedData"></param>
        public Holster(GunsOptions gunsOptions,Character character,SavedData savedData)
        {
            this.gunsOptions = gunsOptions;
            this.character = character;
            HolsterList = new List<Gun>();
            if (savedData.Holster != null)
                HolsterList = savedData.Holster;
        }

        /// <summary>
        /// Method if character colide with gun
        /// </summary>
        public void PickUpGun(GunService characterUsing)
        {
            for (int i=0; i<gunsOptions.GunList.Count;i++)
            {
                if (character.Rectangle.Intersects(gunsOptions.GunList[i].Rectangle))
                {
                    if (HolsterList.Any(s=> s.EGun.Equals(gunsOptions.GunList[i].EGun))) //pokud holster obsahuje zbraň
                    {
                        int index = HolsterList.FindIndex(s => s.EGun.Equals(gunsOptions.GunList[i].EGun));  // najde index v holstru podle sebrané zbraně
                        AddAmmo(gunsOptions.GunList[i], HolsterList[index]);
                        HolsterList.Insert(index, gunsOptions.GunList[i]);
                        HolsterList.RemoveAt(index + 1);
                        gunsOptions.GunList.Remove(gunsOptions.GunList[i]);
                        //Aktualizace držící zbraně
                        if (characterUsing.SelectedGun !=null)
                            if (characterUsing.SelectedGun.EGun.Equals(HolsterList[index].EGun))
                        {
                            characterUsing.SelectedGun = HolsterList[index];
                        }
                    }
                    else if(!HolsterList.Contains(gunsOptions.GunList[i]))   //pokud holster ještě neobsahuje zbraň
                    {
                        HolsterList.Add(gunsOptions.GunList[i]);
                        gunsOptions.GunList.Remove(gunsOptions.GunList[i]);
                    }
                }
            }
        }
        /// <summary>
        /// Method to reload gun
        /// </summary>
        /// <param name="selectedGun"></param>
        public void Reload(Gun selectedGun)
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
        private void AddAmmo(Gun groundGun, Gun holsterGun)
        {
            groundGun.Magazine = holsterGun.Magazine;
            groundGun.Ammo += holsterGun.Ammo;
        }
    }
}
