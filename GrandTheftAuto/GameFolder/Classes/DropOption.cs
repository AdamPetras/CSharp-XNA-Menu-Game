using System;
using System.Collections.Generic;
using GrandTheftAuto.MenuFolder;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class DropOption
    {
        private GameClass game;
        private List<Item> itemList;
        private const int STARTER = 0;
        private const int FIGHTER = 10;
        private const int BLOODSTRIKER = 20;
        private const int HEADHUNTER = 30;
        private const int ALLSTARS = 40;
        public DropOption(GameClass game)
        {
            this.game = game;
            itemList = new List<Item>();
        }

        public void SelectListOfItems(Enemy enemy, Character character)
        {
            //Questpoint a equip
            //Starter equip         0Qp
            //Fighter equip         10Qp
            //BloodStriker equip    20Qp
            //HeadHunter equip      30Qp
            //AllStars equip        40Qp
            itemList.Clear();   //vyčištění itemlistu kvůli zbytku z předchozího dropchance
            if (enemy.Name == EEnemies.Damage.ToString())
            {
                if (character.QuestPoints >= STARTER && character.QuestPoints <FIGHTER)
                    itemList.Add(new Item("Starter Boots", "The boots which you would wear at the start", 20f, enemy.Position, game.spritDroppedArmour[(int)EWearing.Boots], game.spritStarterArmour[(int)EWearing.Boots], EWearing.Boots, new Item.ItemStats(2, EWhichBonus.Vitality), new Item.ItemStats(2, EWhichBonus.Intelect)));
                else if (character.QuestPoints >= FIGHTER && character.QuestPoints < BLOODSTRIKER)
                    itemList.Add(new Item("Fighter Boots", "Good boots to fights", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Boots], game.spritFighterArmour[(int)EWearing.Boots], EWearing.Boots, new Item.ItemStats(5, EWhichBonus.Vitality), new Item.ItemStats(5, EWhichBonus.Intelect),new Item.ItemStats(3,EWhichBonus.Agility)));
                else if (character.QuestPoints>= BLOODSTRIKER&& character.QuestPoints< HEADHUNTER)
                    itemList.Add(new Item("BloodStriker Boots", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Boots], game.spritFighterArmour[(int)EWearing.Boots], EWearing.Boots, new Item.ItemStats(12, EWhichBonus.Vitality), new Item.ItemStats(12, EWhichBonus.Intelect),new Item.ItemStats(3,EWhichBonus.Agility)));                 
                else if (character.QuestPoints>= HEADHUNTER&& character.QuestPoints< ALLSTARS)
                    itemList.Add(new Item("HeadHunter Boots", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Boots], game.spritFighterArmour[(int)EWearing.Boots], EWearing.Boots, new Item.ItemStats(20, EWhichBonus.Vitality), new Item.ItemStats(20, EWhichBonus.Intelect),new Item.ItemStats(4,EWhichBonus.Agility)));
                //else if (character.QuestPoints>= ALLSTARS)
            }
            else if (enemy.Name == EEnemies.Tank.ToString())
            {
                if (character.QuestPoints >= STARTER && character.QuestPoints < FIGHTER)
                    itemList.Add(new Item("Starter Helm", "The helm which you would wear at the start", 20f, enemy.Position, game.spritDroppedArmour[(int)EWearing.Helm], game.spritStarterArmour[(int)EWearing.Helm], EWearing.Helm, new Item.ItemStats(3, EWhichBonus.Vitality), new Item.ItemStats(2, EWhichBonus.Intelect)));
                else if (character.QuestPoints >= FIGHTER && character.QuestPoints < BLOODSTRIKER)
                    itemList.Add(new Item("Fighter Helm", "Good helm to fights", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Helm], game.spritFighterArmour[(int)EWearing.Helm], EWearing.Helm, new Item.ItemStats(5, EWhichBonus.Vitality), new Item.ItemStats(5, EWhichBonus.Intelect),new Item.ItemStats(3,EWhichBonus.Agility)));
                else if (character.QuestPoints>= BLOODSTRIKER&& character.QuestPoints< HEADHUNTER)
                    itemList.Add(new Item("BloodStriker Helm", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Helm], game.spritFighterArmour[(int)EWearing.Helm], EWearing.Helm, new Item.ItemStats(12, EWhichBonus.Vitality), new Item.ItemStats(12, EWhichBonus.Intelect), new Item.ItemStats(3, EWhichBonus.Agility)));
                else if (character.QuestPoints>= HEADHUNTER&& character.QuestPoints< ALLSTARS)
                    itemList.Add(new Item("HeadHunter Helm", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Helm], game.spritFighterArmour[(int)EWearing.Helm], EWearing.Helm, new Item.ItemStats(20, EWhichBonus.Vitality), new Item.ItemStats(20, EWhichBonus.Intelect), new Item.ItemStats(4, EWhichBonus.Agility)));             
                //else if (character.QuestPoints>= ALLSTARS)
            }
            else if (enemy.Name == EEnemies.Speed.ToString())
            {
                if (character.QuestPoints >= STARTER && character.QuestPoints < FIGHTER)
                    itemList.Add(new Item("Starter Gloves", "The gloves which you would wear at the start", 20f, enemy.Position, game.spritDroppedArmour[(int)EWearing.Glove], game.spritStarterArmour[(int)EWearing.Glove], EWearing.Glove, new Item.ItemStats(2, EWhichBonus.Vitality), new Item.ItemStats(2, EWhichBonus.Intelect)));
                else if (character.QuestPoints >= FIGHTER && character.QuestPoints < BLOODSTRIKER)
                    itemList.Add(new Item("Fighter Gloves", "Good gloves to fights", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Glove], game.spritFighterArmour[(int)EWearing.Glove], EWearing.Glove, new Item.ItemStats(5, EWhichBonus.Vitality), new Item.ItemStats(5, EWhichBonus.Intelect),new Item.ItemStats(3,EWhichBonus.Agility)));
                else if (character.QuestPoints>= BLOODSTRIKER&& character.QuestPoints< HEADHUNTER)
                    itemList.Add(new Item("BloodStriker Gloves", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Glove], game.spritFighterArmour[(int)EWearing.Glove], EWearing.Glove, new Item.ItemStats(12, EWhichBonus.Vitality), new Item.ItemStats(12, EWhichBonus.Intelect),new Item.ItemStats(3,EWhichBonus.Agility)));
                else if (character.QuestPoints>= HEADHUNTER&& character.QuestPoints< ALLSTARS)
                    itemList.Add(new Item("HeadHunter Gloves", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Glove], game.spritFighterArmour[(int)EWearing.Glove], EWearing.Glove, new Item.ItemStats(20, EWhichBonus.Vitality), new Item.ItemStats(20, EWhichBonus.Intelect), new Item.ItemStats(4, EWhichBonus.Agility)));
               //else if (character.QuestPoints>= ALLSTARS)
            }
            else if (enemy.Name == EEnemies.EliteDamage.ToString())
            {
                if (character.QuestPoints >= STARTER && character.QuestPoints < FIGHTER)
                    itemList.Add(new Item("Starter Shoulders", "The shoulders which you would wear at the start", 20f, enemy.Position, game.spritDroppedArmour[(int)EWearing.Shoulders], game.spritStarterArmour[(int)EWearing.Shoulders], EWearing.Shoulders, new Item.ItemStats(4, EWhichBonus.Vitality), new Item.ItemStats(2, EWhichBonus.Intelect)));
                else if (character.QuestPoints >= FIGHTER && character.QuestPoints < BLOODSTRIKER)
                    itemList.Add(new Item("Fighter Shoulders", "Good shoulders to fights", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Shoulders], game.spritFighterArmour[(int)EWearing.Shoulders], EWearing.Shoulders, new Item.ItemStats(9, EWhichBonus.Vitality), new Item.ItemStats(7, EWhichBonus.Intelect),new Item.ItemStats(4,EWhichBonus.Agility)));
                else if (character.QuestPoints>= BLOODSTRIKER&& character.QuestPoints< HEADHUNTER)
                    itemList.Add(new Item("BloodStriker Shoulders", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Shoulders], game.spritFighterArmour[(int)EWearing.Shoulders], EWearing.Shoulders, new Item.ItemStats(17, EWhichBonus.Vitality), new Item.ItemStats(13, EWhichBonus.Intelect), new Item.ItemStats(4, EWhichBonus.Agility)));
                else if (character.QuestPoints>= HEADHUNTER&& character.QuestPoints< ALLSTARS)
                    itemList.Add(new Item("HeadHunter Shoulders", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Shoulders], game.spritFighterArmour[(int)EWearing.Shoulders], EWearing.Shoulders, new Item.ItemStats(24, EWhichBonus.Vitality), new Item.ItemStats(21, EWhichBonus.Intelect), new Item.ItemStats(4, EWhichBonus.Agility)));
                    //else if (character.QuestPoints>= ALLSTARS)
            }
            else if (enemy.Name == EEnemies.EliteTank.ToString())
            {
                 if (character.QuestPoints >= STARTER && character.QuestPoints < FIGHTER)
                     itemList.Add(new Item("Starter Ring", "The ring which you would wear at the start",20f, enemy.Position, game.spritDroppedArmour[(int)EWearing.Ring], game.spritStarterArmour[(int)EWearing.Ring], EWearing.Ring, new Item.ItemStats(4, EWhichBonus.Vitality), new Item.ItemStats(2, EWhichBonus.Intelect)));
                else if (character.QuestPoints >= FIGHTER && character.QuestPoints < BLOODSTRIKER)
                    itemList.Add(new Item("Fighter Ring", "Good ring to fights", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Ring], game.spritFighterArmour[(int)EWearing.Ring], EWearing.Ring, new Item.ItemStats(9, EWhichBonus.Vitality), new Item.ItemStats(7, EWhichBonus.Intelect),new Item.ItemStats(4,EWhichBonus.Agility)));
                else if (character.QuestPoints>= BLOODSTRIKER&& character.QuestPoints< HEADHUNTER)
                     itemList.Add(new Item("BloodStriker Ring", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Ring], game.spritFighterArmour[(int)EWearing.Ring], EWearing.Ring, new Item.ItemStats(17, EWhichBonus.Vitality), new Item.ItemStats(13, EWhichBonus.Intelect), new Item.ItemStats(4, EWhichBonus.Agility)));
                else if (character.QuestPoints>= HEADHUNTER&& character.QuestPoints< ALLSTARS)
                     itemList.Add(new Item("HeadHunter Ring", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Ring], game.spritFighterArmour[(int)EWearing.Ring], EWearing.Ring, new Item.ItemStats(24, EWhichBonus.Vitality), new Item.ItemStats(21, EWhichBonus.Intelect), new Item.ItemStats(4, EWhichBonus.Agility)));
                    //else if (character.QuestPoints>= ALLSTARS)
            }
            else if (enemy.Name == EEnemies.EliteUniversal.ToString())
            {
                if (character.QuestPoints >= STARTER && character.QuestPoints < FIGHTER)
                     itemList.Add(new Item("Starter Neck", "The neck which you would wear at the start",20f, enemy.Position, game.spritDroppedArmour[(int)EWearing.Neck], game.spritStarterArmour[(int)EWearing.Neck], EWearing.Neck, new Item.ItemStats(4, EWhichBonus.Vitality), new Item.ItemStats(2, EWhichBonus.Intelect)));
                else if (character.QuestPoints >= FIGHTER && character.QuestPoints < BLOODSTRIKER)
                    itemList.Add(new Item("Fighter Neck", "Good neck to fights", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Neck], game.spritFighterArmour[(int)EWearing.Neck], EWearing.Neck, new Item.ItemStats(9, EWhichBonus.Vitality), new Item.ItemStats(7, EWhichBonus.Intelect),new Item.ItemStats(4,EWhichBonus.Agility)));
                else if (character.QuestPoints>= BLOODSTRIKER&& character.QuestPoints< HEADHUNTER)
                    itemList.Add(new Item("BloodStriker Neck", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Neck], game.spritFighterArmour[(int)EWearing.Neck], EWearing.Neck, new Item.ItemStats(17, EWhichBonus.Vitality), new Item.ItemStats(13, EWhichBonus.Intelect), new Item.ItemStats(4, EWhichBonus.Agility)));
                else if (character.QuestPoints>= HEADHUNTER&& character.QuestPoints< ALLSTARS)
                    itemList.Add(new Item("HeadHunter Neck", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Neck], game.spritFighterArmour[(int)EWearing.Neck], EWearing.Neck, new Item.ItemStats(24, EWhichBonus.Vitality), new Item.ItemStats(21, EWhichBonus.Intelect), new Item.ItemStats(4, EWhichBonus.Agility)));
                //else if (character.QuestPoints>= ALLSTARS)
            }
            else if (enemy.Name == EEnemies.BossDamage.ToString())
            {
                if (character.QuestPoints >= STARTER && character.QuestPoints < FIGHTER)
                    itemList.Add(new Item("Starter PlateLegs", "The platelegs which you would wear at the start", 20f, enemy.Position, game.spritDroppedArmour[(int)EWearing.Legs], game.spritStarterArmour[(int)EWearing.Legs], EWearing.Legs, new Item.ItemStats(4, EWhichBonus.Vitality), new Item.ItemStats(2, EWhichBonus.Intelect)));
                else if (character.QuestPoints >= FIGHTER && character.QuestPoints < BLOODSTRIKER)
                    itemList.Add(new Item("Fighter Platelegs", "Good platelegs to fights", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Legs], game.spritFighterArmour[(int)EWearing.Legs], EWearing.Legs, new Item.ItemStats(13, EWhichBonus.Vitality), new Item.ItemStats(9, EWhichBonus.Intelect),new Item.ItemStats(6,EWhichBonus.Agility)));
                else if (character.QuestPoints>= BLOODSTRIKER&& character.QuestPoints< HEADHUNTER)
                    itemList.Add(new Item("BloodStriker Platelegs", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Legs], game.spritFighterArmour[(int)EWearing.Legs], EWearing.Legs, new Item.ItemStats(20, EWhichBonus.Vitality), new Item.ItemStats(15, EWhichBonus.Intelect), new Item.ItemStats(6, EWhichBonus.Agility)));
                else if (character.QuestPoints>= HEADHUNTER&& character.QuestPoints< ALLSTARS)
                    itemList.Add(new Item("HeadHunter Platelegs", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Legs], game.spritFighterArmour[(int)EWearing.Legs], EWearing.Legs, new Item.ItemStats(26, EWhichBonus.Vitality), new Item.ItemStats(22, EWhichBonus.Intelect), new Item.ItemStats(6, EWhichBonus.Agility)));
                //else if (character.QuestPoints>= ALLSTARS)
            }
            else if (enemy.Name == EEnemies.BossTank.ToString())
            {
                if (character.QuestPoints >= STARTER && character.QuestPoints < FIGHTER)
                    itemList.Add(new Item("Starter Chest", "The chest which you would wear at the start", 20f, enemy.Position, game.spritDroppedArmour[(int)EWearing.Chest], game.spritStarterArmour[(int)EWearing.Chest], EWearing.Chest, new Item.ItemStats(5, EWhichBonus.Vitality), new Item.ItemStats(2, EWhichBonus.Intelect)));
                else if (character.QuestPoints >= FIGHTER && character.QuestPoints < BLOODSTRIKER)
                    itemList.Add(new Item("Fighter Chest", "Good chest to fights", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Chest], game.spritFighterArmour[(int)EWearing.Chest], EWearing.Chest, new Item.ItemStats(13, EWhichBonus.Vitality), new Item.ItemStats(9, EWhichBonus.Intelect), new Item.ItemStats(6, EWhichBonus.Agility)));
                else if (character.QuestPoints>= BLOODSTRIKER&& character.QuestPoints< HEADHUNTER)
                    itemList.Add(new Item("BloodStriker Chest", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Chest], game.spritFighterArmour[(int)EWearing.Chest], EWearing.Chest, new Item.ItemStats(20, EWhichBonus.Vitality), new Item.ItemStats(15, EWhichBonus.Intelect), new Item.ItemStats(6, EWhichBonus.Agility)));
                else if (character.QuestPoints>= HEADHUNTER&& character.QuestPoints< ALLSTARS)
                    itemList.Add(new Item("HeadHunter Chest", "", 20f, enemy.Position, game.spritFighterArmour[(int)EWearing.Chest], game.spritFighterArmour[(int)EWearing.Chest], EWearing.Chest, new Item.ItemStats(26, EWhichBonus.Vitality), new Item.ItemStats(22, EWhichBonus.Intelect), new Item.ItemStats(6, EWhichBonus.Agility)));
                //else if (character.QuestPoints>= ALLSTARS)
            }
            Item item = DropChance();
            if (item != null)
                game.itemList.Add(item);
        }

        public Item DropChance()
        {
            Random rnd = new Random();
            double roll = rnd.Next(0, 1000);
            double together = 0;
            foreach (Item t in itemList)
            {
                together += t.DropChance;   //postupně každý průchodem přičítá všechny itemy
                if (roll < together)    //pokud je náhodné číslo menší než together tak vrátí
                {
                    return t;
                }
            }
            return null;
        }
    }
}
