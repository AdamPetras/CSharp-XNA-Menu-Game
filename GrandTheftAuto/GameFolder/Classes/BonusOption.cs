using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class BonusOption
    {
        public double VitalityBonus { get; set; }
        public double StaminaBonus { get; set; }
        public double SpiritBonus { get; set; }
        public double AgilityBonus { get; set; }
        public double IntelectBonus { get; set; }

        public BonusOption()
        {
            VitalityBonus = 0;
            SpiritBonus = 0;
            SpiritBonus = 0;
            AgilityBonus = 0;
            IntelectBonus = 0;
        }
    }
}
